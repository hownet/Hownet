namespace Hownet.BaseContranl
{
    partial class UserNum
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this._leNum = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this._leEdit = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._leVerify = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._leJinDu = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(-1, -1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "编号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _leNum
            // 
            this._leNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._leNum.Location = new System.Drawing.Point(-1, 17);
            this._leNum.Margin = new System.Windows.Forms.Padding(0);
            this._leNum.Name = "_leNum";
            this._leNum.Size = new System.Drawing.Size(92, 31);
            this._leNum.TabIndex = 7;
            this._leNum.Text = "SCZD\r\n2010年03月11日";
            this._leNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._leNum.Click += new System.EventHandler(this._leNum_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(0, 14);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(95, 21);
            this.dateTimePicker1.TabIndex = 8;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            this.dateTimePicker1.Leave += new System.EventHandler(this.dateTimePicker1_Leave);
            // 
            // _leEdit
            // 
            this._leEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._leEdit.Location = new System.Drawing.Point(88, 17);
            this._leEdit.Margin = new System.Windows.Forms.Padding(0);
            this._leEdit.Name = "_leEdit";
            this._leEdit.Size = new System.Drawing.Size(92, 31);
            this._leEdit.TabIndex = 10;
            this._leEdit.Text = "2010年03月11日";
            this._leEdit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(88, -1);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "制单";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _leVerify
            // 
            this._leVerify.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._leVerify.Location = new System.Drawing.Point(176, 17);
            this._leVerify.Margin = new System.Windows.Forms.Padding(0);
            this._leVerify.Name = "_leVerify";
            this._leVerify.Size = new System.Drawing.Size(92, 31);
            this._leVerify.TabIndex = 12;
            this._leVerify.Text = "2010年03月11日";
            this._leVerify.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(176, -1);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 19);
            this.label6.TabIndex = 11;
            this.label6.Text = "审核";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(265, -1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "进度";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _leJinDu
            // 
            this._leJinDu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._leJinDu.ForeColor = System.Drawing.Color.Red;
            this._leJinDu.Location = new System.Drawing.Point(265, 17);
            this._leJinDu.Margin = new System.Windows.Forms.Padding(0);
            this._leJinDu.Name = "_leJinDu";
            this._leJinDu.Size = new System.Drawing.Size(92, 31);
            this._leJinDu.TabIndex = 12;
            this._leJinDu.Text = "2010年03月11日";
            this._leJinDu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(-1, 30);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(90, 18);
            this.textBox1.TabIndex = 13;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // UserNum
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label6);
            this.Controls.Add(this._leJinDu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._leVerify);
            this.Controls.Add(this._leEdit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._leNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "UserNum";
            this.Size = new System.Drawing.Size(354, 47);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _leNum;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label _leEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label _leVerify;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _leJinDu;
        private System.Windows.Forms.TextBox textBox1;
    }
}
