namespace CompleteColorData
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tmclose = new System.Windows.Forms.ToolStripMenuItem();
            this.btnimportold = new System.Windows.Forms.Button();
            this.btnimportnew = new System.Windows.Forms.Button();
            this.btngenerate = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmclose});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(231, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tmclose
            // 
            this.tmclose.Name = "tmclose";
            this.tmclose.Size = new System.Drawing.Size(44, 21);
            this.tmclose.Text = "关闭";
            // 
            // btnimportold
            // 
            this.btnimportold.Location = new System.Drawing.Point(42, 37);
            this.btnimportold.Name = "btnimportold";
            this.btnimportold.Size = new System.Drawing.Size(137, 23);
            this.btnimportold.TabIndex = 1;
            this.btnimportold.Text = "导入旧EXCEL数据";
            this.btnimportold.UseVisualStyleBackColor = true;
            // 
            // btnimportnew
            // 
            this.btnimportnew.Location = new System.Drawing.Point(42, 67);
            this.btnimportnew.Name = "btnimportnew";
            this.btnimportnew.Size = new System.Drawing.Size(137, 23);
            this.btnimportnew.TabIndex = 2;
            this.btnimportnew.Text = "导入新EXCEL数据";
            this.btnimportnew.UseVisualStyleBackColor = true;
            // 
            // btngenerate
            // 
            this.btngenerate.Location = new System.Drawing.Point(42, 96);
            this.btngenerate.Name = "btngenerate";
            this.btngenerate.Size = new System.Drawing.Size(137, 23);
            this.btngenerate.TabIndex = 3;
            this.btngenerate.Text = "运算";
            this.btngenerate.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 132);
            this.ControlBox = false;
            this.Controls.Add(this.btngenerate);
            this.Controls.Add(this.btnimportnew);
            this.Controls.Add(this.btnimportold);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "对比新旧数据库Excel,并以新模板导出";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tmclose;
        private System.Windows.Forms.Button btnimportold;
        private System.Windows.Forms.Button btnimportnew;
        private System.Windows.Forms.Button btngenerate;
    }
}

