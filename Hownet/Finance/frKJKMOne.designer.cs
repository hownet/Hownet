namespace Hownet.Finance
{
    partial class frKJKMOne
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this._te上级科目 = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this._te科目编号 = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this._te科目名称 = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._te上级科目.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._te科目编号.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._te科目名称.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "上级科目：";
            // 
            // _te上级科目
            // 
            this._te上级科目.Enabled = false;
            this._te上级科目.Location = new System.Drawing.Point(92, 35);
            this._te上级科目.Name = "_te上级科目";
            this._te上级科目.Size = new System.Drawing.Size(171, 21);
            this._te上级科目.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "科目编号：";
            // 
            // _te科目编号
            // 
            this._te科目编号.Location = new System.Drawing.Point(92, 62);
            this._te科目编号.Name = "_te科目编号";
            this._te科目编号.Size = new System.Drawing.Size(171, 21);
            this._te科目编号.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "科目名称：";
            // 
            // _te科目名称
            // 
            this._te科目名称.Location = new System.Drawing.Point(92, 89);
            this._te科目名称.Name = "_te科目名称";
            this._te科目名称.Size = new System.Drawing.Size(171, 21);
            this._te科目名称.TabIndex = 2;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(49, 200);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "保存";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(155, 200);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "退出";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Enabled = false;
            this.textEdit1.Location = new System.Drawing.Point(92, 116);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Mask.EditMask = "c";
            this.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEdit1.Size = new System.Drawing.Size(171, 21);
            this.textEdit1.TabIndex = 3;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(50, 117);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "余额：";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(92, 144);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 18);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "余额在借方";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // frKJKMOne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 245);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this._te科目名称);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._te科目编号);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._te上级科目);
            this.Controls.Add(this.label1);
            this.Name = "frKJKMOne";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frKJKMOne";
            this.Load += new System.EventHandler(this.frKJKMOne_Load);
            ((System.ComponentModel.ISupportInitialize)(this._te上级科目.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._te科目编号.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._te科目名称.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit _te上级科目;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit _te科目编号;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit _te科目名称;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}