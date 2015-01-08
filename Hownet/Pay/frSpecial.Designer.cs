namespace Hownet.Pay
{
    partial class frSpecial
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reColor = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this._coCompanyID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reCompany = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this._coWorkingID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coGroupBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coOrders = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coPWIID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMainID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coIsCaiC = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coIsCut = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coOneAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coIsTicket = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMaterielID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reSize = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reSize)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(21, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "labelControl1";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(274, 12);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton3);
            this.panel1.Controls.Add(this.simpleButton2);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 83);
            this.panel1.TabIndex = 6;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton3.Location = new System.Drawing.Point(456, 11);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(75, 23);
            this.simpleButton3.TabIndex = 5;
            this.simpleButton3.Text = "删除";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.Location = new System.Drawing.Point(364, 12);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "返回";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Location = new System.Drawing.Point(12, 42);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(324, 28);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "有选择客户时，该客户的生产单将只使用设置了客户的工序。\r\n没有选择客户时，根据颜色来区分所需要使用的工序。";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 83);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._reColor,
            this._reCompany,
            this._reSize});
            this.gridControl1.Size = new System.Drawing.Size(583, 189);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coID,
            this._coColorID,
            this._coMaterielID,
            this._coCompanyID,
            this._coWorkingID,
            this._coGroupBy,
            this._coOrders,
            this._coPrice,
            this._coPWIID,
            this._coMainID,
            this._coA,
            this._coIsCaiC,
            this._coIsCut,
            this._coOneAmount,
            this._coIsTicket});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // _coID
            // 
            this._coID.Caption = "ID";
            this._coID.FieldName = "ID";
            this._coID.Name = "_coID";
            this._coID.OptionsColumn.AllowEdit = false;
            // 
            // _coColorID
            // 
            this._coColorID.Caption = "颜色";
            this._coColorID.ColumnEdit = this._reColor;
            this._coColorID.FieldName = "ColorID";
            this._coColorID.Name = "_coColorID";
            this._coColorID.Visible = true;
            this._coColorID.VisibleIndex = 0;
            // 
            // _reColor
            // 
            this._reColor.AutoHeight = false;
            this._reColor.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._reColor.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "颜色")});
            this._reColor.DisplayMember = "Name";
            this._reColor.Name = "_reColor";
            this._reColor.NullText = "";
            this._reColor.ValueMember = "ID";
            // 
            // _coCompanyID
            // 
            this._coCompanyID.Caption = "客户";
            this._coCompanyID.ColumnEdit = this._reCompany;
            this._coCompanyID.FieldName = "CompanyID";
            this._coCompanyID.Name = "_coCompanyID";
            this._coCompanyID.Visible = true;
            this._coCompanyID.VisibleIndex = 2;
            // 
            // _reCompany
            // 
            this._reCompany.AutoHeight = false;
            this._reCompany.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._reCompany.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "客户")});
            this._reCompany.DisplayMember = "Name";
            this._reCompany.Name = "_reCompany";
            this._reCompany.NullText = "";
            this._reCompany.ValueMember = "ID";
            // 
            // _coWorkingID
            // 
            this._coWorkingID.Caption = "普通工序";
            this._coWorkingID.FieldName = "WorkingID";
            this._coWorkingID.Name = "_coWorkingID";
            this._coWorkingID.Visible = true;
            this._coWorkingID.VisibleIndex = 3;
            // 
            // _coGroupBy
            // 
            this._coGroupBy.Caption = "分组";
            this._coGroupBy.FieldName = "GroupBy";
            this._coGroupBy.Name = "_coGroupBy";
            this._coGroupBy.Visible = true;
            this._coGroupBy.VisibleIndex = 4;
            // 
            // _coOrders
            // 
            this._coOrders.Caption = "序号";
            this._coOrders.FieldName = "Orders";
            this._coOrders.Name = "_coOrders";
            this._coOrders.Visible = true;
            this._coOrders.VisibleIndex = 5;
            // 
            // _coPrice
            // 
            this._coPrice.Caption = "工价";
            this._coPrice.FieldName = "Price";
            this._coPrice.Name = "_coPrice";
            this._coPrice.Visible = true;
            this._coPrice.VisibleIndex = 7;
            // 
            // _coPWIID
            // 
            this._coPWIID.Caption = "PWIID";
            this._coPWIID.FieldName = "PWIID";
            this._coPWIID.Name = "_coPWIID";
            this._coPWIID.OptionsColumn.AllowEdit = false;
            // 
            // _coMainID
            // 
            this._coMainID.Caption = "MainID";
            this._coMainID.FieldName = "MainID";
            this._coMainID.Name = "_coMainID";
            this._coMainID.OptionsColumn.AllowEdit = false;
            // 
            // _coA
            // 
            this._coA.Caption = "A";
            this._coA.FieldName = "A";
            this._coA.Name = "_coA";
            this._coA.OptionsColumn.AllowEdit = false;
            // 
            // _coIsCaiC
            // 
            this._coIsCaiC.Caption = "参与统计";
            this._coIsCaiC.FieldName = "IsCaiC";
            this._coIsCaiC.Name = "_coIsCaiC";
            this._coIsCaiC.Visible = true;
            this._coIsCaiC.VisibleIndex = 8;
            // 
            // _coIsCut
            // 
            this._coIsCut.Caption = "参与折扣";
            this._coIsCut.FieldName = "IsCut";
            this._coIsCut.Name = "_coIsCut";
            this._coIsCut.Visible = true;
            this._coIsCut.VisibleIndex = 9;
            // 
            // _coOneAmount
            // 
            this._coOneAmount.Caption = "每箱数量";
            this._coOneAmount.FieldName = "OneAmount";
            this._coOneAmount.Name = "_coOneAmount";
            this._coOneAmount.OptionsColumn.AllowEdit = false;
            this._coOneAmount.Visible = true;
            this._coOneAmount.VisibleIndex = 10;
            // 
            // _coIsTicket
            // 
            this._coIsTicket.Caption = "出工票";
            this._coIsTicket.FieldName = "IsTicket";
            this._coIsTicket.Name = "_coIsTicket";
            this._coIsTicket.Visible = true;
            this._coIsTicket.VisibleIndex = 6;
            // 
            // _coMaterielID
            // 
            this._coMaterielID.Caption = "尺码";
            this._coMaterielID.ColumnEdit = this._reSize;
            this._coMaterielID.FieldName = "MaterielID";
            this._coMaterielID.Name = "_coMaterielID";
            this._coMaterielID.Visible = true;
            this._coMaterielID.VisibleIndex = 1;
            // 
            // _reSize
            // 
            this._reSize.AutoHeight = false;
            this._reSize.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._reSize.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "尺码")});
            this._reSize.DisplayMember = "Name";
            this._reSize.Name = "_reSize";
            this._reSize.NullText = "";
            this._reSize.ValueMember = "ID";
            // 
            // frSpecial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 272);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frSpecial";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "特殊工序设置";
            this.Load += new System.EventHandler(this.frSpecial_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorID;
        private DevExpress.XtraGrid.Columns.GridColumn _coCompanyID;
        private DevExpress.XtraGrid.Columns.GridColumn _coWorkingID;
        private DevExpress.XtraGrid.Columns.GridColumn _coGroupBy;
        private DevExpress.XtraGrid.Columns.GridColumn _coOrders;
        private DevExpress.XtraGrid.Columns.GridColumn _coPrice;
        private DevExpress.XtraGrid.Columns.GridColumn _coPWIID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMainID;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reColor;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reCompany;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraGrid.Columns.GridColumn _coIsCaiC;
        private DevExpress.XtraGrid.Columns.GridColumn _coIsCut;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.Columns.GridColumn _coOneAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coIsTicket;
        private DevExpress.XtraGrid.Columns.GridColumn _coMaterielID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reSize;
    }
}