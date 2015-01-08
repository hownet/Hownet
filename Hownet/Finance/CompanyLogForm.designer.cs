namespace Hownet.Finance
{
    partial class CompanyLogForm
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coCompanyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDebit = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coLenders = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coIndexs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this._coWhys = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coCompanyID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this._leCompany = new BaseContranl.LabAndLookupEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsView.AutoCalcPreviewLineCount = true;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowFooter = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode2.LevelTemplate = this.gridView2;
            gridLevelNode2.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControl1.Size = new System.Drawing.Size(866, 530);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView2});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coID,
            this._coCompanyName,
            this._coDateTime,
            this._coRemark,
            this._coDebit,
            this._coLenders,
            this._coMoney,
            this._coIndexs,
            this._coWhys,
            this._coCompanyID,
            this._coName});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Debit", this._coDebit, ""),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Lenders", this._coLenders, "")});
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowColumnResizing = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this._coName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // _coID
            // 
            this._coID.Caption = "ID";
            this._coID.FieldName = "ID";
            this._coID.Name = "_coID";
            this._coID.OptionsColumn.AllowEdit = false;
            // 
            // _coCompanyName
            // 
            this._coCompanyName.Caption = "公司名";
            this._coCompanyName.FieldName = "CompanyName";
            this._coCompanyName.Name = "_coCompanyName";
            this._coCompanyName.OptionsColumn.AllowEdit = false;
            this._coCompanyName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this._coCompanyName.Visible = true;
            this._coCompanyName.VisibleIndex = 0;
            // 
            // _coDateTime
            // 
            this._coDateTime.Caption = "日期";
            this._coDateTime.FieldName = "DateTime";
            this._coDateTime.Name = "_coDateTime";
            this._coDateTime.OptionsColumn.AllowEdit = false;
            this._coDateTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this._coDateTime.Visible = true;
            this._coDateTime.VisibleIndex = 1;
            // 
            // _coRemark
            // 
            this._coRemark.Caption = "摘要";
            this._coRemark.FieldName = "Remark";
            this._coRemark.Name = "_coRemark";
            this._coRemark.OptionsColumn.AllowEdit = false;
            this._coRemark.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this._coRemark.Visible = true;
            this._coRemark.VisibleIndex = 2;
            // 
            // _coDebit
            // 
            this._coDebit.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this._coDebit.AppearanceCell.Options.UseBackColor = true;
            this._coDebit.Caption = "借方";
            this._coDebit.FieldName = "Debit";
            this._coDebit.Name = "_coDebit";
            this._coDebit.OptionsColumn.AllowEdit = false;
            this._coDebit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this._coDebit.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this._coDebit.Visible = true;
            this._coDebit.VisibleIndex = 3;
            // 
            // _coLenders
            // 
            this._coLenders.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this._coLenders.AppearanceCell.Options.UseBackColor = true;
            this._coLenders.Caption = "贷方";
            this._coLenders.FieldName = "Lenders";
            this._coLenders.Name = "_coLenders";
            this._coLenders.OptionsColumn.AllowEdit = false;
            this._coLenders.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this._coLenders.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this._coLenders.Visible = true;
            this._coLenders.VisibleIndex = 4;
            // 
            // _coMoney
            // 
            this._coMoney.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._coMoney.AppearanceCell.Options.UseBackColor = true;
            this._coMoney.Caption = "余额";
            this._coMoney.FieldName = "Money";
            this._coMoney.Name = "_coMoney";
            this._coMoney.OptionsColumn.AllowEdit = false;
            this._coMoney.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this._coMoney.Visible = true;
            this._coMoney.VisibleIndex = 5;
            // 
            // _coIndexs
            // 
            this._coIndexs.Caption = "单据号";
            this._coIndexs.ColumnEdit = this.repositoryItemButtonEdit1;
            this._coIndexs.FieldName = "Indexs";
            this._coIndexs.Name = "_coIndexs";
            this._coIndexs.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this._coIndexs.Visible = true;
            this._coIndexs.VisibleIndex = 6;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.ReadOnly = true;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // _coWhys
            // 
            this._coWhys.Caption = "Whys";
            this._coWhys.FieldName = "Whys";
            this._coWhys.Name = "_coWhys";
            this._coWhys.OptionsColumn.AllowEdit = false;
            // 
            // _coCompanyID
            // 
            this._coCompanyID.Caption = "公司名";
            this._coCompanyID.FieldName = "CompanyID";
            this._coCompanyID.Name = "_coCompanyID";
            this._coCompanyID.OptionsColumn.AllowEdit = false;
            // 
            // _coName
            // 
            this._coName.Caption = "公司名称";
            this._coName.FieldName = "CompanyName";
            this._coName.Name = "_coName";
            this._coName.OptionsColumn.AllowEdit = false;
            this._coName.Visible = true;
            this._coName.VisibleIndex = 7;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.simpleButton3);
            this.splitContainerControl1.Panel1.Controls.Add(this.dateEdit2);
            this.splitContainerControl1.Panel1.Controls.Add(this.dateEdit1);
            this.splitContainerControl1.Panel1.Controls.Add(this.simpleButton2);
            this.splitContainerControl1.Panel1.Controls.Add(this.simpleButton1);
            this.splitContainerControl1.Panel1.Controls.Add(this._leCompany);
            this.splitContainerControl1.Panel1.Controls.Add(this.radioGroup1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(866, 603);
            this.splitContainerControl1.SplitterPosition = 68;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton2.Location = new System.Drawing.Point(714, 5);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(108, 23);
            this.simpleButton2.TabIndex = 27;
            this.simpleButton2.Text = "打印货款明细";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton1.Location = new System.Drawing.Point(618, 5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(90, 23);
            this.simpleButton1.TabIndex = 26;
            this.simpleButton1.Text = "打印帐目明细";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // _leCompany
            // 
            this._leCompany.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._leCompany.editVal = 0;
            this._leCompany.FormName = 0;
            this._leCompany.IsMust = false;
            this._leCompany.IsNotCanEdit = false;
            this._leCompany.labText = "客户：";
            this._leCompany.lenth = new int[] {
        50,
        120};
            this._leCompany.Location = new System.Drawing.Point(372, 5);
            this._leCompany.Name = "_leCompany";
            this._leCompany.Par = null;
            this._leCompany.Size = new System.Drawing.Size(180, 22);
            this._leCompany.TabIndex = 25;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(1, 1);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "客户"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "供应商"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "加工商")});
            this.radioGroup1.Size = new System.Drawing.Size(347, 33);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(13, 41);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit1.Size = new System.Drawing.Size(119, 21);
            this.dateEdit1.TabIndex = 28;
            // 
            // dateEdit2
            // 
            this.dateEdit2.EditValue = null;
            this.dateEdit2.Location = new System.Drawing.Point(150, 41);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit2.Size = new System.Drawing.Size(119, 21);
            this.dateEdit2.TabIndex = 28;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(300, 39);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(75, 23);
            this.simpleButton3.TabIndex = 29;
            this.simpleButton3.Text = "查询";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // CompanyLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 603);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "CompanyLogForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "对帐单";
            this.Load += new System.EventHandler(this.CompanyLogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraGrid.Columns.GridColumn _coCompanyName;
        private DevExpress.XtraGrid.Columns.GridColumn _coDateTime;
        private DevExpress.XtraGrid.Columns.GridColumn _coRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coDebit;
        private DevExpress.XtraGrid.Columns.GridColumn _coLenders;
        private DevExpress.XtraGrid.Columns.GridColumn _coMoney;
        private DevExpress.XtraGrid.Columns.GridColumn _coIndexs;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn _coWhys;
        private DevExpress.XtraGrid.Columns.GridColumn _coCompanyID;
        private DevExpress.XtraGrid.Columns.GridColumn _coName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private BaseContranl.LabAndLookupEdit _leCompany;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.DateEdit dateEdit2;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
    }
}