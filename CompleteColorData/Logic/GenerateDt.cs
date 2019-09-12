using System;
using System.Data;
using CompleteColorData.DB;

namespace CompleteColorData.Logic
{
    public class GenerateDt
    {
        DtList dtList=new DtList();

        /// <summary>
        /// 运算
        /// </summary>
        /// <param name="sourceolddt">旧数据库导入DT</param>
        /// <param name="sourcenewdt">新数据库导入DT</param>
        /// <returns></returns>
        public DataTable GenerateTemp(DataTable sourceolddt,DataTable sourcenewdt)
        {
            var resultdt=new DataTable();

            try
            {
                //获取新数据完整模板(导出时需要)
                resultdt = dtList.ExportNewtempdt();
                //获取新数据模板(数据比较时使用)
                var newdt = dtList.Get_ImportNewtempdt();
                //获取旧数据模板(数据比较时使用)
                var olddt = dtList.Get_ImportOldtempdt();
                //获取旧数据模板 作用:保存需要进行删除的旧记录DT
                var delolddt = dtList.Get_ImportOldtempdt();

                //先循环整理新数据库导入的DT(注:若‘制造商’列为空的话,就不用将数据插入至newdt内)
                foreach (DataRow rows in sourcenewdt.Rows)
                {
                    if (Convert.ToString(rows[0]) == "") continue;
                    var newrow = newdt.NewRow();
                    newrow[0] = rows[0];            //制造商
                    newrow[1] = rows[1];            //标准色号
                    newrow[2] = rows[2];            //色母编码
                    newrow[3] = rows[3];            //色母量
                    newdt.Rows.Add(newrow);
                }

                //循环整理新数据库导入的DT(注:若‘车厂’列为空的话,就不用将数据插入至olddt内)
                for (var i = 0; i < sourceolddt.Rows.Count; i++)
                {
                    if (Convert.ToString(sourceolddt.Rows[i][1]) == "") continue;
                    var newrow = olddt.NewRow();
                    for (var j = 0; j < sourceolddt.Columns.Count; j++)
                    {
                        newrow[j] = sourceolddt.Rows[i][j];
                    }
                    olddt.Rows.Add(newrow);
                }

                //将olddt与newdt进行相关列比较,若相同,即记录,并最后将sourceolddt相关行删除(重)
                //对比条件:制造商(新):车厂(旧)  标准色号(新):颜色代码(旧) 色母编码(新):色母(旧) 色母量(KG)(新):量(克)(旧) 之间的对比
                //注:在使用Select函数时,在其内的条件名称不要带(),如:"量(克)"
                foreach (DataRow rows in newdt.Rows)
                {
                    var row = olddt.Select(@"车厂='" + Convert.ToString(rows[0]) + "' and 颜色代码 ='"+Convert.ToString(rows[1])+
                                           "' and 色母 ='"+Convert.ToString(rows[2]).Substring(3)+"' and 量 ='"+ Convert.ToDecimal(rows[3])+"'");

                    //若存在相同,即将其保存至delolddt内
                    for (var i = 0; i < row.Length; i++)
                    {
                        var newrow = delolddt.NewRow();
                        for (var j = 0; j < delolddt.Columns.Count; j++)
                        {
                            newrow[j] = row[i][j];
                        }
                        delolddt.Rows.Add(newrow);
                    }
                }

                //循环将delolddt中的‘ID’作为条件,并放到sourceolddt进行删除
                //(注:要删除DataTable内的数据,应该采用倒序循环DataTable.Rows.因为正序删除时索引会发生变化.程式发生异常,很难预料后果.)
                for (var i = sourceolddt.Rows.Count-1; i >=0; i--)
                {
                    var row = delolddt.Select("ID='" + sourceolddt.Rows[i][0] +"'");
                    if(row.Length>0)
                        sourceolddt.Rows[i].Delete();
                }
                //当使用Delete()进行行删除时,需继续使用AcceptChanges()方法来提交修改
                sourceolddt.AcceptChanges();

                //最后将整结过来的sourceolddt添加至resultdt内(注:以新模板方式导出)
                foreach (DataRow rows in sourceolddt.Rows)
                {
                    var newrow = resultdt.NewRow();
                    newrow[0] = rows[1];                   //制造商
                    newrow[1] = rows[4];                   //车型
                    newrow[2] = rows[6];                   //涂层
                    newrow[3] = rows[3];                   //颜色描述
                    newrow[4] = "";                        //二维码
                    newrow[5] = "";                        //内部色号
                    newrow[6] = rows[7];                   //主配方色号(差异色)
                    newrow[7] = "";                        //颜色组别
                    newrow[8] = rows[2];                   //标准色号
                    newrow[9] = "";                        //RGBValue
                    newrow[10] = rows[9];                  //版本日期
                    newrow[11] ="";                        //层
                    newrow[12] = "EW-"+rows[11];           //色母编码
                    newrow[13] =rows[12];                  //色母名称
                    newrow[14] =rows[13];                  //色母量(KG)
                    newrow[15] =rows[14];                  //累积量(KG)
                    newrow[16] =rows[10];                  //制作人
                    resultdt.Rows.Add(newrow);
                }
            }
            catch (Exception)
            {
                resultdt.Columns.Clear();
                resultdt.Rows.Clear();
            }
            return resultdt;
        }
    }
}
