using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using CompleteColorData.DB;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CompleteColorData.Logic
{
    public class ImportDt
    {
        DtList dtList = new DtList();

        /// <summary>
        /// 打开及导入至DT
        /// </summary>
        /// <param name="importid">0:导入旧数据库模板 1:导入新数据库模板</param>
        /// <param name="fileAddress"></param>
        /// <returns></returns>
        public DataTable OpenExcelImporttoDt(int importid,string fileAddress)
        {
            var resultdt = new DataTable();
            try
            {
                //使用NPOI技术进行导入EXCEL至DATATABLE
                var importExcelDt = OpenExcelToDataTable(importid,fileAddress);
                //将从EXCEL过来的记录集为空的行清除
                var dt = RemoveEmptyRows(importExcelDt);
                //将ID值进行添加(注:旧数据模板才适用)
                resultdt = importid == 0 ? AddIdValue(dt) : dt;
            }
            catch (Exception)
            {
                resultdt.Rows.Clear();
                resultdt.Columns.Clear();
            }
            return resultdt;
        }

        /// <summary>
        /// 导入DT
        /// </summary>
        /// <param name="importid">0:导入旧数据库模板 1:导入新数据库模板</param>
        /// <param name="fileAddress"></param>
        /// <returns></returns>
        private DataTable OpenExcelToDataTable(int importid,string fileAddress)
        {
            //定义EXCEL列数
            var colnum = 0;
            IWorkbook wk;

            //创建表标题
            var dt = new DataTable();

            dt = importid == 0 ? dtList.Get_ImportOldtempdt() : dtList.Get_ImportNewtempdt();

            using (var fsRead = File.OpenRead(fileAddress))
            {
                wk = new XSSFWorkbook(fsRead);
                //获取第一个sheet
                var sheet = wk.GetSheetAt(0);
                //获取第一行
                //var hearRow = sheet.GetRow(0);

                //创建完标题后,开始从第二行起读取对应列的值
                for (var r = 1; r <= sheet.LastRowNum; r++)
                {
                    var result = false;
                    var dr = dt.NewRow();
                    //获取当前行(注:只能获取行中有值的项,为空的项不能获取;即row.Cells.Count得出的总列数就只会汇总"有值的列"之和)
                    var row = sheet.GetRow(r);
                    if (row == null) continue;

                    colnum = importid == 0 ? 15 : 4;

                    //读取每列
                    for (var j = 0; j < colnum/*row.Cells.Count*/; j++)
                    {
                        //循环获取行中的单元格
                        var cell = row.GetCell(j);
                        var cellValue = GetCellValue(cell);
                        if (cellValue == string.Empty)
                        {
                            continue;
                        }
                        else
                        {
                            dr[j] = cellValue;
                        }

                        //全为空就不取
                        if (dr[j].ToString() != "")
                        {
                            result = true;
                        }
                    }

                    if (result == true)
                    {
                        //把每行增加到DataTable
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 检查单元格的数据类型并获其中的值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.Blank: //空数据类型 这里类型注意一下，不同版本NPOI大小写可能不一样,有的版本是Blank（首字母大写)
                    return string.Empty;
                case CellType.Boolean: //bool类型
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric: //数字类型
                    if (DateUtil.IsCellDateFormatted(cell))//日期类型
                    {
                        return cell.DateCellValue.ToString();
                    }
                    else //其它数字
                    {
                        return cell.NumericCellValue.ToString();

                    }

                case CellType.Unknown: //无法识别类型
                default: //默认类型                    
                    return cell.ToString();
                case CellType.String: //string 类型
                    return cell.StringCellValue;
                case CellType.Formula: //带公式类型
                    try
                    {
                        var e = new XSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }

            }
        }

        /// <summary>
        ///  将从EXCEL导入的DATATABLE的空白行清空
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected DataTable RemoveEmptyRows(DataTable dt)
        {
            var removeList = new List<DataRow>();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var isNull = true;
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    //将不为空的行标记为False
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        isNull = false;
                    }
                }
                //将整行都为空白的记录进行记录
                if (isNull)
                {
                    removeList.Add(dt.Rows[i]);
                }
            }

            //将整理出来的所有空白行通过循环进行删除
            for (var i = 0; i < removeList.Count; i++)
            {
                dt.Rows.Remove(removeList[i]);
            }
            return dt;
        }

        /// <summary>
        /// 将DT中的ID值按情况进行添加(注:当要导入旧数据库模板记录时才使用)
        /// </summary>
        /// <param name="sourcedt"></param>
        /// <returns></returns>
        private DataTable AddIdValue(DataTable sourcedt)
        {
            //ID中间值
            var tempid=0;
            //定义ID值(每一行完成后,会进行清空)
            var id = 0;

            for (var i = 0; i < sourcedt.Rows.Count; i++)
            {
                //当第一行时,ID值为1
                if (i == 0)
                {
                    id = tempid = 1;
                }
                //当检测到‘车厂’列为空时,此行的ID列值为tempid
                else if (Convert.ToString(sourcedt.Rows[i][1])=="")
                {
                    id = tempid;
                }
                //当检测到‘车厂’列不为空时,此行的ID列值为tempid++
                else if (Convert.ToString(sourcedt.Rows[i][1])!="")
                {
                    id = tempid = tempid + 1;
                }

                sourcedt.BeginInit();
                sourcedt.Rows[i][0] = id;
                sourcedt.EndInit();
                //当每行循环完成后将ID清空
                id = 0;
            }
            return sourcedt;
        }
    }
}
