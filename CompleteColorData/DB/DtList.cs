using System;
using System.Data;

namespace CompleteColorData.DB
{
    public class DtList
    {
        /// <summary>
        /// 加载新数据表EXCEL使用
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ImportNewtempdt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 4; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0:
                        dc.ColumnName = "制造商";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                    case 1:
                        dc.ColumnName = "标准色号";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 2:
                        dc.ColumnName = "色母编码";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3:
                        dc.ColumnName = "色母量";
                        dc.DataType = Type.GetType("System.Decimal"); 
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 导入旧数据库模板
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ImportOldtempdt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 14; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0:
                        dc.ColumnName = "车厂";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 1:
                        dc.ColumnName = "颜色代码";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 2:
                        dc.ColumnName = "颜色名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3:
                        dc.ColumnName = "适用车型";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 4:
                        dc.ColumnName = "品牌";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 5:
                        dc.ColumnName = "涂层";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 6:
                        dc.ColumnName = "差异色";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 7:
                        dc.ColumnName = "年份";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 8:
                        dc.ColumnName = "制作日期";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                    case 9:
                        dc.ColumnName = "制作人";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 10:
                        dc.ColumnName = "色母";
                        dc.DataType=Type.GetType("System.String");
                        break;
                    case 11:
                        dc.ColumnName = "色母名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 12:
                        dc.ColumnName = "量(克)";
                        dc.DataType = Type.GetType("System.Decimal"); 
                        break;
                    case 13:
                        dc.ColumnName = "累计量(克)";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 导出新数据库模板
        /// </summary>
        /// <returns></returns>
        public DataTable ExportNewtempdt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 12; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0:
                        dc.ColumnName = "制造商";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 1:
                        dc.ColumnName = "车型";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 2:
                        dc.ColumnName = "颜色描述";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3:
                        dc.ColumnName = "二维码";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 4:
                        dc.ColumnName = "内部色号";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 5:
                        dc.ColumnName = "标准色号";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 6:
                        dc.ColumnName = "版本日期";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 7:
                        dc.ColumnName = "层";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 8:
                        dc.ColumnName = "色母编码";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 9:
                        dc.ColumnName = "色母量(KG)";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 10:
                        dc.ColumnName = "累积量(KG)";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 11:
                        dc.ColumnName = "制作人";
                        dc.DataType = Type.GetType("System.String");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }
    }
}
