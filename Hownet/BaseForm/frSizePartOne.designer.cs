namespace Hownet.BaseForm
{
    partial class frSizePartOne
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this._teName = new DevExpress.XtraEditors.TextEdit();
            this._teEnName = new DevExpress.XtraEditors.TextEdit();
            this._sbSaveAndContinue = new DevExpress.XtraEditors.SimpleButton();
            this._sbSaveAndExit = new DevExpress.XtraEditors.SimpleButton();
            this._sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this._teMeRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this._teName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._teEnName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._teMeRemark.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(23, 11);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(84, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "尺寸部位名称：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(46, 37);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "允许误差：";
            // 
            // _teName
            // 
            this._teName.Location = new System.Drawing.Point(105, 8);
            this._teName.Name = "_teName";
            this._teName.Size = new System.Drawing.Size(183, 21);
            this._teName.TabIndex = 7;
            // 
            // _teEnName
            // 
            this._teEnName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this._teEnName.Location = new System.Drawing.Point(105, 34);
            this._teEnName.Name = "_teEnName";
            this._teEnName.Size = new System.Drawing.Size(183, 21);
            this._teEnName.TabIndex = 8;
            // 
            // _sbSaveAndContinue
            // 
            this._sbSaveAndContinue.Location = new System.Drawing.Point(22, 153);
            this._sbSaveAndContinue.Name = "_sbSaveAndContinue";
            this._sbSaveAndContinue.Size = new System.Drawing.Size(75, 23);
            this._sbSaveAndContinue.TabIndex = 10;
            this._sbSaveAndContinue.Text = "保存并继续";
            this._sbSaveAndContinue.Click += new System.EventHandler(this._sbSaveAndContinue_Click);
            // 
            // _sbSaveAndExit
            // 
            this._sbSaveAndExit.Location = new System.Drawing.Point(118, 152);
            this._sbSaveAndExit.Name = "_sbSaveAndExit";
            this._sbSaveAndExit.Size = new System.Drawing.Size(75, 23);
            this._sbSaveAndExit.TabIndex = 11;
            this._sbSaveAndExit.Text = "保存后退出";
            this._sbSaveAndExit.Click += new System.EventHandler(this._sbSaveAndExit_Click);
            // 
            // _sbCancel
            // 
            this._sbCancel.Location = new System.Drawing.Point(214, 151);
            this._sbCancel.Name = "_sbCancel";
            this._sbCancel.Size = new System.Drawing.Size(75, 23);
            this._sbCancel.TabIndex = 12;
            this._sbCancel.Text = "取消";
            this._sbCancel.Click += new System.EventHandler(this._sbCancel_Click);
            // 
            // _teMeRemark
            // 
            this._teMeRemark.Location = new System.Drawing.Point(105, 60);
            this._teMeRemark.Name = "_teMeRemark";
            this._teMeRemark.Size = new System.Drawing.Size(183, 84);
            this._teMeRemark.TabIndex = 14;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(71, 60);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.TabIndex = 13;
            this.labelControl5.Text = "說明：";
            // 
            // frSizePartOne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(301, 181);
            this.Controls.Add(this._teMeRemark);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this._sbCancel);
            this.Controls.Add(this._sbSaveAndExit);
            this.Controls.Add(this._sbSaveAndContinue);
            this.Controls.Add(this._teEnName);
            this.Controls.Add(this._teName);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frSizePartOne";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frMeasureOne";
            this.Load += new System.EventHandler(this.frColorOne_Load);
            ((System.ComponentModel.ISupportInitialize)(this._teName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._teEnName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._teMeRemark.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit _teName;
        private DevExpress.XtraEditors.TextEdit _teEnName;
        private DevExpress.XtraEditors.SimpleButton _sbSaveAndContinue;
        private DevExpress.XtraEditors.SimpleButton _sbSaveAndExit;
        private DevExpress.XtraEditors.SimpleButton _sbCancel;
        private DevExpress.XtraEditors.MemoEdit _teMeRemark;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}