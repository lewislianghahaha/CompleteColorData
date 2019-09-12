using System;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CompleteColorData.Logic
{
    public class ExportDt
    {
        /// <summary>
        /// 导出记录
        /// </summary>
        /// <param name="fileAddress"></param>
        /// <param name="exportdt"></param>
        /// <returns></returns>
        public bool ExportDtToExcel(string fileAddress, DataTable exportdt)
        {
            var result = true;
            var sheetcount = 0;  //记录所需的sheet页总数
            var rownum = 1;

            try
            {
                //声明一个WorkBook
                var xssfWorkbook = new XSSFWorkbook();
                //执行sheet页(注:1)先列表temp行数判断需拆分多少个sheet表进行填充; 以一个sheet表有10W行记录填充为基准)
                sheetcount = exportdt.Rows.Count % 100000 == 0 ? exportdt.Rows.Count / 100000 : exportdt.Rows.Count / 100000 + 1;
                //i为EXCEL的Sheet页数ID
                for (var i = 1; i <= sheetcount; i++)
                {
                    //创建sheet页
                    var sheet = xssfWorkbook.CreateSheet("Sheet" + i);
                    //创建"标题行"
                    var row = sheet.CreateRow(0);
                    //创建sheet页各列标题
                    for (var j = 0; j < exportdt.Columns.Count; j++)
                    {
                        //设置列宽度
                        sheet.SetColumnWidth(j, (int)((20 + 0.72) * 256));
                        //创建标题
                        switch (j)
                        {
                            #region SetCellValue
                            case 0:
                                row.CreateCell(j).SetCellValue("制造商");
                                break;
                            case 1:
                                row.CreateCell(j).SetCellValue("车型");
                                break;
                            case 2:
                                row.CreateCell(j).SetCellValue("涂层");
                                break;
                            case 3:
                                row.CreateCell(j).SetCellValue("颜色描述");
                                break;
                            case 4:
                                row.CreateCell(j).SetCellValue("二维码");
                                break;
                            case 5:
                                row.CreateCell(j).SetCellValue("内部色号");
                                break;
                            case 6:
                                row.CreateCell(j).SetCellValue("主配方色号(差异色)");
                                break;
                            case 7:
                                row.CreateCell(j).SetCellValue("颜色组别");
                                break;
                            case 8:
                                row.CreateCell(j).SetCellValue("标准色号");
                                break;
                            case 9:
                                row.CreateCell(j).SetCellValue("RGBValue");
                                break;
                            case 10:
                                row.CreateCell(j).SetCellValue("版本日期");
                                break;
                            case 11:
                                row.CreateCell(j).SetCellValue("层");
                                break;
                            case 12:
                                row.CreateCell(j).SetCellValue("色母编码");
                                break;
                            case 13:
                                row.CreateCell(j).SetCellValue("色母名称");
                                break;
                            case 14:
                                row.CreateCell(j).SetCellValue("色母量(KG)");
                                break;
                            case 15:
                                row.CreateCell(j).SetCellValue("累积量(KG)");
                                break;
                            case 16:
                                row.CreateCell(j).SetCellValue("制作人");
                                break;
                                #endregion
                        }
                    }

                    //计算进行循环的起始行
                    var startrow = (i - 1) * 100000;
                    //计算进行循环的结束行
                    var endrow = i == sheetcount ? exportdt.Rows.Count : i * 100000;

                    //每一个sheet表显示90000行  
                    for (var j = startrow; j < endrow; j++)
                    {
                        //创建行
                        row = sheet.CreateRow(rownum);
                        //循环获取DT内的列值记录
                        for (var k = 0; k < exportdt.Columns.Count; k++)
                        {
                            if (Convert.ToString(exportdt.Rows[j][k]) == "") continue;
                            else
                            {
                                if (k == 14 || k == 15)
                                {
                                    row.CreateCell(k, CellType.Numeric).SetCellValue(Convert.ToDouble(exportdt.Rows[j][k]));
                                }
                                //除‘色母量’以及‘累积量’外的值的转换赋值
                                row.CreateCell(k, CellType.String).SetCellValue(Convert.ToString(exportdt.Rows[j][k]));
                            }
                        }
                        rownum++;
                    }
                    //当一个SHEET页填充完毕后,需将变量初始化
                    rownum = 1;
                }

                //写入数据
                var file = new FileStream(fileAddress, FileMode.Create);
                xssfWorkbook.Write(file);
                file.Close();           //关闭文件流
                xssfWorkbook.Close();   //关闭工作簿
                file.Dispose();         //释放文件流
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}
