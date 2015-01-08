namespace Hownet.BaseForm
{
    partial class frMeasureOne
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this._teName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._teEnName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(26, 35);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(84, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "中文單位名稱：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(76, 12);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "编号：";
            // 
            // _teName
            // 
            this._teName.Location = new System.Drawing.Point(108, 32);
            this._teName.Name = "_teName";
            this._teName.Size = new System.Drawing.Size(183, 20);
            this._teName.TabIndex = 7;
            this._teName.EditValueChanged += new System.EventHandler(this._teName_EditValueChanged);
            // 
            // _teEnName
            // 
            this._teEnName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this._teEnName.Location = new System.Drawing.Point(108, 9);
            this._teEnName.Name = "_teEnName";
            this._teEnName.Size = new System.Drawing.Size(183, 20);
            this._teEnName.TabIndex = 8;
            // 
            // _sbSaveAndContinue
            // 
            this._sbSaveAndContinue.Location = new System.Drawing.Point(22, 194);
            this._sbSaveAndContinue.Name = "_sbSaveAndContinue";
            this._sbSaveAndContinue.Size = new System.Drawing.Size(75, 23);
            this._sbSaveAndContinue.TabIndex = 10;
            this._sbSaveAndContinue.Text = "保存并继续";
            this._sbSaveAndContinue.Click += new System.EventHandler(this._sbSaveAndContinue_Click);
            // 
            // _sbSaveAndExit
            // 
            this._sbSaveAndExit.Location = new System.Drawing.Point(118, 193);
            this._sbSaveAndExit.Name = "_sbSaveAndExit";
            this._sbSaveAndExit.Size = new System.Drawing.Size(75, 23);
            this._sbSaveAndExit.TabIndex = 11;
            this._sbSaveAndExit.Text = "保存后退出";
            this._sbSaveAndExit.Click += new System.EventHandler(this._sbSaveAndExit_Click);
            // 
            // _sbCancel
            // 
            this._sbCancel.Location = new System.Drawing.Point(214, 192);
            this._sbCancel.Name = "_sbCancel";
            this._sbCancel.Size = new System.Drawing.Size(75, 23);
            this._sbCancel.TabIndex = 12;
            this._sbCancel.Text = "取消";
            this._sbCancel.Click += new System.EventHandler(this._sbCancel_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(74, 59);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "类型：";
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(108, 56);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "类型")});
            this.lookUpEdit1.Properties.DisplayMember = "Name";
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.ValueMember = "ID";
            this.lookUpEdit1.Size = new System.Drawing.Size(183, 20);
            this.lookUpEdit1.TabIndex = 13;
            this.lookUpEdit1.EditValueChanged += new System.EventHandler(this.lookUpEdit1_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "与默认单位：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(92, 3);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(12, 14);
            this.labelControl5.TabIndex = 15;
            this.labelControl5.Text = "米";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(132, 3);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(72, 14);
            this.labelControl6.TabIndex = 16;
            this.labelControl6.Text = "的换算结果：";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(75, 27);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(100, 20);
            this.textEdit1.TabIndex = 17;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(30, 57);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(129, 14);
            this.labelControl7.TabIndex = 18;
            this.labelControl7.Text = "(1当前单位÷1默认单位)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.labelControl7);
            this.panel1.Controls.Add(this.labelControl5);
            this.panel1.Controls.Add(this.textEdit1);
            this.panel1.Controls.Add(this.labelControl6);
            this.panel1.Location = new System.Drawing.Point(46, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 88);
            this.panel1.TabIndex = 19;
            // 
            // frMeasureOne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(301, 231);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this._sbCancel);
            this.Controls.Add(this._sbSaveAndExit);
            this.Controls.Add(this._sbSaveAndContinue);
            this.Controls.Add(this._teEnName);
            this.Controls.Add(this._teName);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frMeasureOne";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frMeasureOne";
            this.Load += new System.EventHandler(this.frColorOne_Load);
            ((System.ComponentModel.ISupportInitialize)(this._teName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._teEnName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private System.Windows.Forms.Panel panel1;
    }
}