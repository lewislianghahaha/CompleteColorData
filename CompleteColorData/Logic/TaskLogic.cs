using System.Data;

namespace CompleteColorData.Logic
{
    public class TaskLogic
    {
        ImportDt importDt=new ImportDt();
        GenerateDt generateDt=new GenerateDt();
        ExportDt exportDt = new ExportDt();

        #region 变量

            private int _taskid;
            private DataTable _olddt;          //导入旧EXCEL记录
            private DataTable _newdt;          //导入新EXCEL记录
            private string _fileAddress;       //文件地址



            private DataTable _resultTable;     //返回DT(导出新模板时使用)
            private DataTable _resultolddt;    //返回旧EXCEL导入的DT
            private DataTable _resultnewdt;   //返回新EXCEL导入的DT
            private bool _resultMark;        //返回是否成功标记

        #endregion

        #region Set

            /// <summary>
            /// 中转ID
            /// </summary>
            public int TaskId { set { _taskid = value; } }
            /// <summary>
            /// 导入旧EXCEL记录
            /// </summary>
            public DataTable Olddt { set { _olddt = value; } }
            /// <summary>
            /// 导入新EXCEL记录
            /// </summary>
            public DataTable Newdt { set { _newdt = value; } }

            /// <summary>
            /// //接收文件地址信息
            /// </summary>
            public string FileAddress { set { _fileAddress = value; } }

        #endregion

        #region Get

        /// <summary>
        ///返回DataTable至主窗体
        /// </summary>
        public DataTable RestulTable => _resultTable;

            /// <summary>
            ///  返回是否成功标记
            /// </summary>
            public bool ResultMark => _resultMark;

            /// <summary>
            /// 返回旧EXCEL导入的DT
            /// </summary>
            public DataTable ResultOlddt => _resultolddt;

            /// <summary>
            /// 返回新EXCEL导入的DT
            /// </summary>
            public DataTable ResultNewdt => _resultnewdt;

        #endregion

        public void StartTask()
        {
            switch (_taskid)
            {
                //导入旧EXCEL
                case 0:
                    OpenOldExcelImporttoDt(_fileAddress);
                    break;
                //导入新EXCEL
                case 1:
                    OpenNewExcelImporttoDt(_fileAddress);
                    break;
                //运算
                case 2:
                    GenerateTemp(_olddt, _newdt);
                    break;
                //导出
                case 3:
                    ExportDtToExcel(_fileAddress,_resultTable);
                    break;
            }
        }

        /// <summary>
        /// 导入旧数据库模板
        /// </summary>
        /// <param name="fileAddress"></param>
        private void OpenOldExcelImporttoDt(string fileAddress)
        {
            _resultolddt = importDt.OpenExcelImporttoDt(0,fileAddress);
        }

        /// <summary>
        /// 导入新数据库模板
        /// </summary>
        /// <param name="fileAddress"></param>
        private void OpenNewExcelImporttoDt(string fileAddress)
        {
            _resultnewdt = importDt.OpenExcelImporttoDt(1,fileAddress);
        }

        /// <summary>
        /// 运算
        /// </summary>
        /// <param name="sourceolddt"></param>
        /// <param name="sourcenewdt"></param>
        private void GenerateTemp(DataTable sourceolddt, DataTable sourcenewdt)
        {
            _resultTable = generateDt.GenerateTemp(sourceolddt, sourcenewdt);
            _resultMark = _resultTable.Rows.Count > 0;
        }

        /// <summary>
        /// 导出DT（按照新数据模板方式）
        /// </summary>
        /// <param name="fileAddress"></param>
        /// <param name="exportdt"></param>
        private void ExportDtToExcel(string fileAddress,DataTable exportdt)
        {
            _resultMark = exportDt.ExportDtToExcel(fileAddress, exportdt);
        }

    }
}
