using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using CompleteColorData.Logic;
using Mergedt;

namespace CompleteColorData
{
    public partial class Main : Form
    {
        TaskLogic task=new TaskLogic();
        Load load=new Load();

        //保存旧数据库导入的DT
        private DataTable _importolddt;
        //保存新数据库导入的DT
        private DataTable _importnewdt;

        public Main()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            btnimportold.Click += Btnimportold_Click;
            btnimportnew.Click += Btnimportnew_Click;
            btngenerate.Click += Btngenerate_Click;
            tmclose.Click += Tmclose_Click;
        }

        /// <summary>
        /// 导入旧数据EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btnimportold_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog { Filter = "Xlsx文件|*.xlsx" };
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                var fileAdd = openFileDialog.FileName;

                //将所需的值赋到Task类内
                task.TaskId = 0;
                task.FileAddress = fileAdd;

                //使用子线程工作(作用:通过调用子线程进行控制Load窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                if (_importolddt.Rows.Count == 0) throw new Exception("不能成功导入EXCEL内容,请检查模板是否正确.");
                else
                {
                    var clickMessage = $"导入旧数据表成功,是否进行运算功能?";
                    var clickMes = $"运算成功,是否进行导出至Excel?";

                    if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        //if (!Generatedt(_importdt, rbFormualChange.Checked ? 0 : 1)) throw new Exception("运算不成功,请联系管理员");
                        //else if (MessageBox.Show(clickMes, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        //{
                        //    Exportdt();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 导入新数据EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btnimportnew_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog { Filter = "Xlsx文件|*.xlsx" };
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                var fileAdd = openFileDialog.FileName;


                //将所需的值赋到Task类内
                task.TaskId = 1;
                task.FileAddress = fileAdd;

                //使用子线程工作(作用:通过调用子线程进行控制Load窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                if (_importnewdt.Rows.Count == 0) throw new Exception("不能成功导入EXCEL内容,请检查模板是否正确.");
                else
                {
                    var clickMessage = $"导入成功,是否进行运算功能?";
                    var clickMes = $"运算成功,是否进行导出至Excel?";

                    if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        //if (!Generatedt(_importdt, rbFormualChange.Checked ? 0 : 1)) throw new Exception("运算不成功,请联系管理员");
                        //else if (MessageBox.Show(clickMes, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        //{
                        //    Exportdt();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 运算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btngenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //if (_importdt.Rows.Count == 0) throw new Exception("没有成功导入EXCEL文件,不能执行运算操作");
                //if (!Generatedt(_importdt, rbFormualChange.Checked ? 0 : 1)) throw new Exception("运算不成功,请联系管理员");
                MessageBox.Show($"运算成功,请点击导出按钮", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmclose_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///子线程使用(重:用于监视功能调用情况,当完成时进行关闭LoadForm)
        /// </summary>
        private void Start()
        {
            task.StartTask();

            //当完成后将Form2子窗体关闭
            this.Invoke((ThreadStart)(() => {
                load.Close();
            }));
        }

        /// <summary>
        /// 运算功能
        /// </summary>
        bool Generatedt(DataTable dt, int typeid)
        {
            var result = true;
            try
            {
                task.TaskId = 1;
               // task.Data = dt;

                //使用子线程工作(作用:通过调用子线程进行控制Load窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                result = task.ResultMark;
                //_tempdt = task.Tempdt;
                //_tempdtldt = task.Tempdtldt;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 导出功能
        /// </summary>
        void Exportdt()
        {
            //获取下拉列表信息
           // var dvCustidlist = (DataRowView)comselect.Items[comselect.SelectedIndex];
           // var selectid = Convert.ToInt32(dvCustidlist["Id"]);

            var saveFileDialog = new SaveFileDialog { Filter = "Xlsx文件|*.xlsx" };
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            var fileAdd = saveFileDialog.FileName;

            task.TaskId = 2;
            task.FileAddress = fileAdd;
           // task.Selectcomid = selectid;

            //使用子线程工作(作用:通过调用子线程进行控制Load窗体的关闭情况)
            new Thread(Start).Start();
            load.StartPosition = FormStartPosition.CenterScreen;
            load.ShowDialog();

            if (!task.ResultMark) throw new Exception("导出异常");
            else
            {
                MessageBox.Show($"导出成功!可从EXCEL中查阅导出效果", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
