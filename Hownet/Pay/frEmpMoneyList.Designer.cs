namespace Hownet.Pay
{
    partial class frEmpMoneyList
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
            this.panel1 = new System.Windows.Forms.Panel();
            this._ldDtTwo = new BaseContranl.LabAndData();
            this._ldDtOne = new BaseContranl.LabAndData();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField2 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField3 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._ldDtTwo);
            this.panel1.Controls.Add(this._ldDtOne);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(666, 45);
            this.panel1.TabIndex = 0;
            // 
            // _ldDtTwo
            // 
            this._ldDtTwo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._ldDtTwo.IsMust = false;
            this._ldDtTwo.IsShowClear = false;
            this._ldDtTwo.labText = "结止日期：";
            this._ldDtTwo.lenth = new int[] {
        60,
        140};
            this._ldDtTwo.Location = new System.Drawing.Point(301, 13);
            this._ldDtTwo.Margin = new System.Windows.Forms.Padding(0);
            this._ldDtTwo.MaxDate = new System.DateTime(((long)(0)));
            this._ldDtTwo.MinDate = new System.DateTime(((long)(0)));
            this._ldDtTwo.Name = "_ldDtTwo";
            this._ldDtTwo.Size = new System.Drawing.Size(210, 22);
            this._ldDtTwo.strLab = "";
            this._ldDtTwo.t = false;
            this._ldDtTwo.TabIndex = 4;
            this._ldDtTwo.val = null;
            // 
            // _ldDtOne
            // 
            this._ldDtOne.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._ldDtOne.IsMust = false;
            this._ldDtOne.IsShowClear = false;
            this._ldDtOne.labText = "开始日期：";
            this._ldDtOne.lenth = new int[] {
        60,
        140};
            this._ldDtOne.Location = new System.Drawing.Point(72, 13);
            this._ldDtOne.Margin = new System.Windows.Forms.Padding(0);
            this._ldDtOne.MaxDate = new System.DateTime(((long)(0)));
            this._ldDtOne.MinDate = new System.DateTime(((long)(0)));
            this._ldDtOne.Name = "_ldDtOne";
            this._ldDtOne.Size = new System.Drawing.Size(210, 22);
            this._ldDtOne.strLab = "";
            this._ldDtOne.t = false;
            this._ldDtOne.TabIndex = 3;
            this._ldDtOne.val = null;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton1.Location = new System.Drawing.Point(537, 13);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "查询";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.Appearance.ColumnHeaderArea.Options.UseTextOptions = true;
            this.pivotGridControl1.Appearance.ColumnHeaderArea.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.pivotGridControl1.Appearance.FieldHeader.Options.UseTextOptions = true;
            this.pivotGridControl1.Appearance.FieldHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.pivotGridControl1.Appearance.HeaderArea.Options.UseTextOptions = true;
            this.pivotGridControl1.Appearance.HeaderArea.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.pivotGridControl1.Appearance.RowHeaderArea.Options.UseTextOptions = true;
            this.pivotGridControl1.Appearance.RowHeaderArea.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.pivotGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pivotGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pivotGridControl1.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pivotGridField1,
            this.pivotGridField2,
            this.pivotGridField3});
            this.pivotGridControl1.Location = new System.Drawing.Point(0, 45);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.Size = new System.Drawing.Size(666, 328);
            this.pivotGridControl1.TabIndex = 1;
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField1.AreaIndex = 0;
            this.pivotGridField1.Caption = "工资";
            this.pivotGridField1.FieldName = "Month";
            this.pivotGridField1.Name = "pivotGridField1";
            // 
            // pivotGridField2
            // 
            this.pivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField2.AreaIndex = 0;
            this.pivotGridField2.Caption = "工资汇总区间";
            this.pivotGridField2.FieldName = "Date";
            this.pivotGridField2.Name = "pivotGridField2";
            this.pivotGridField2.Width = 180;
            // 
            // pivotGridField3
            // 
            this.pivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField3.AreaIndex = 0;
            this.pivotGridField3.Caption = "员工";
            this.pivotGridField3.FieldName = "EmployeeName";
            this.pivotGridField3.Name = "pivotGridField3";
            // 
            // frEmpMoneyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 373);
            this.Controls.Add(this.pivotGridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frEmpMoneyList";
            this.Text = "员工工资分析";
            this.Load += new System.EventHandler(this.frEmpMoneyList_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private BaseContranl.LabAndData _ldDtTwo;
        private BaseContranl.LabAndData _ldDtOne;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField2;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField3;
    }
}