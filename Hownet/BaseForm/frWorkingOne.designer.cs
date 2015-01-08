namespace Hownet.BaseForm
{
    partial class frWorkingOne
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this._teSn = new DevExpress.XtraEditors.TextEdit();
            this._teName = new DevExpress.XtraEditors.TextEdit();
            this._sbSaveAndContinue = new DevExpress.XtraEditors.SimpleButton();
            this._sbSaveAndExit = new DevExpress.XtraEditors.SimpleButton();
            this._sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this._teMeRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this._teSn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._teName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._teMeRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(43, 13);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "编号：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(19, 39);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "工序名称：";
            // 
            // _teSn
            // 
            this._teSn.ImeMode = System.Windows.Forms.ImeMode.Off;
            this._teSn.Location = new System.Drawing.Point(77, 10);
            this._teSn.Name = "_teSn";
            this._teSn.Size = new System.Drawing.Size(201, 21);
            this._teSn.TabIndex = 6;
            // 
            // _teName
            // 
            this._teName.Location = new System.Drawing.Point(77, 36);
            this._teName.Name = "_teName";
            this._teName.Size = new System.Drawing.Size(201, 21);
            this._teName.TabIndex = 7;
            this._teName.EditValueChanged += new System.EventHandler(this._teName_EditValueChanged);
            // 
            // _sbSaveAndContinue
            // 
            this._sbSaveAndContinue.Location = new System.Drawing.Point(11, 200);
            this._sbSaveAndContinue.Name = "_sbSaveAndContinue";
            this._sbSaveAndContinue.Size = new System.Drawing.Size(75, 23);
            this._sbSaveAndContinue.TabIndex = 10;
            this._sbSaveAndContinue.Text = "保存并继续";
            this._sbSaveAndContinue.Click += new System.EventHandler(this._sbSaveAndContinue_Click);
            // 
            // _sbSaveAndExit
            // 
            this._sbSaveAndExit.Location = new System.Drawing.Point(107, 199);
            this._sbSaveAndExit.Name = "_sbSaveAndExit";
            this._sbSaveAndExit.Size = new System.Drawing.Size(75, 23);
            this._sbSaveAndExit.TabIndex = 11;
            this._sbSaveAndExit.Text = "保存后退出";
            this._sbSaveAndExit.Click += new System.EventHandler(this._sbSaveAndExit_Click);
            // 
            // _sbCancel
            // 
            this._sbCancel.Location = new System.Drawing.Point(203, 198);
            this._sbCancel.Name = "_sbCancel";
            this._sbCancel.Size = new System.Drawing.Size(75, 23);
            this._sbCancel.TabIndex = 12;
            this._sbCancel.Text = "取消";
            this._sbCancel.Click += new System.EventHandler(this._sbCancel_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(19, 63);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "所属工种：";
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(77, 60);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "色系")});
            this.lookUpEdit1.Properties.DisplayMember = "Name";
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.ValueMember = "ID";
            this.lookUpEdit1.Size = new System.Drawing.Size(201, 21);
            this.lookUpEdit1.TabIndex = 14;
            // 
            // _teMeRemark
            // 
            this._teMeRemark.Location = new System.Drawing.Point(77, 108);
            this._teMeRemark.Name = "_teMeRemark";
            this._teMeRemark.Size = new System.Drawing.Size(201, 84);
            this._teMeRemark.TabIndex = 16;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(43, 108);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.TabIndex = 15;
            this.labelControl5.Text = "说明：";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(77, 85);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "是特殊工序";
            this.checkEdit1.Size = new System.Drawing.Size(105, 19);
            this.checkEdit1.TabIndex = 17;
            // 
            // frWorkingOne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(294, 230);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this._teMeRemark);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this._sbCancel);
            this.Controls.Add(this._sbSaveAndExit);
            this.Controls.Add(this._sbSaveAndContinue);
            this.Controls.Add(this._teName);
            this.Controls.Add(this._teSn);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frWorkingOne";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frColorOne";
            this.Load += new System.EventHandler(this.frColorOne_Load);
            ((System.ComponentModel.ISupportInitialize)(this._teSn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._teName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._teMeRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit _teSn;
        private DevExpress.XtraEditors.TextEdit _teName;
        private DevExpress.XtraEditors.SimpleButton _sbSaveAndContinue;
        private DevExpress.XtraEditors.SimpleButton _sbSaveAndExit;
        private DevExpress.XtraEditors.SimpleButton _sbCancel;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.MemoEdit _teMeRemark;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
    }
}