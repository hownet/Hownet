namespace Hownet.BaseForm
{
    partial class frPackingOne
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this._teMeRemark = new DevExpress.XtraEditors.MemoEdit();
            this._sbSaveAndContinue = new DevExpress.XtraEditors.SimpleButton();
            this._sbSaveAndExit = new DevExpress.XtraEditors.SimpleButton();
            this._sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this._meName = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this._teMeRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._meName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.LineVisible = true;
            this.labelControl1.Location = new System.Drawing.Point(3, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "包装方法：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(3, 65);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 14);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "详细说明：";
            // 
            // _teMeRemark
            // 
            this._teMeRemark.ImeMode = System.Windows.Forms.ImeMode.On;
            this._teMeRemark.Location = new System.Drawing.Point(62, 65);
            this._teMeRemark.Name = "_teMeRemark";
            this._teMeRemark.Size = new System.Drawing.Size(226, 84);
            this._teMeRemark.TabIndex = 1;
            // 
            // _sbSaveAndContinue
            // 
            this._sbSaveAndContinue.Location = new System.Drawing.Point(21, 171);
            this._sbSaveAndContinue.Name = "_sbSaveAndContinue";
            this._sbSaveAndContinue.Size = new System.Drawing.Size(75, 23);
            this._sbSaveAndContinue.TabIndex = 2;
            this._sbSaveAndContinue.Text = "&s保存并继续";
            this._sbSaveAndContinue.Click += new System.EventHandler(this._sbSaveAndContinue_Click);
            // 
            // _sbSaveAndExit
            // 
            this._sbSaveAndExit.Location = new System.Drawing.Point(117, 170);
            this._sbSaveAndExit.Name = "_sbSaveAndExit";
            this._sbSaveAndExit.Size = new System.Drawing.Size(75, 23);
            this._sbSaveAndExit.TabIndex = 3;
            this._sbSaveAndExit.Text = "&e保存后退出";
            this._sbSaveAndExit.Click += new System.EventHandler(this._sbSaveAndExit_Click);
            // 
            // _sbCancel
            // 
            this._sbCancel.Location = new System.Drawing.Point(213, 169);
            this._sbCancel.Name = "_sbCancel";
            this._sbCancel.Size = new System.Drawing.Size(75, 23);
            this._sbCancel.TabIndex = 4;
            this._sbCancel.Text = "&c取消";
            this._sbCancel.Click += new System.EventHandler(this._sbCancel_Click);
            // 
            // _meName
            // 
            this._meName.ImeMode = System.Windows.Forms.ImeMode.On;
            this._meName.Location = new System.Drawing.Point(62, 7);
            this._meName.Name = "_meName";
            this._meName.Size = new System.Drawing.Size(226, 52);
            this._meName.TabIndex = 0;
            // 
            // frPackingOne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(310, 202);
            this.Controls.Add(this._sbCancel);
            this.Controls.Add(this._sbSaveAndExit);
            this.Controls.Add(this._sbSaveAndContinue);
            this.Controls.Add(this._meName);
            this.Controls.Add(this._teMeRemark);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frPackingOne";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frBrandOne";
            this.Load += new System.EventHandler(this.frColorOne_Load);
            ((System.ComponentModel.ISupportInitialize)(this._teMeRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._meName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.MemoEdit _teMeRemark;
        private DevExpress.XtraEditors.SimpleButton _sbSaveAndContinue;
        private DevExpress.XtraEditors.SimpleButton _sbSaveAndExit;
        private DevExpress.XtraEditors.SimpleButton _sbCancel;
        private DevExpress.XtraEditors.MemoEdit _meName;
    }
}