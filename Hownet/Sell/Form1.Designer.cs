namespace Hownet.Sell
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.amountList1 = new BaseContranl.AmountList();
            this.SuspendLayout();
            // 
            // amountList1
            // 
            this.amountList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.amountList1.IsCanEdit = false;
            this.amountList1.IsEdit = false;
            this.amountList1.Location = new System.Drawing.Point(0, 0);
            this.amountList1.MaterielID = 0;
            this.amountList1.Name = "amountList1";
            this.amountList1.Size = new System.Drawing.Size(753, 330);
            this.amountList1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 330);
            this.Controls.Add(this.amountList1);
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "销售明细";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BaseContranl.AmountList amountList1;
    }
}

