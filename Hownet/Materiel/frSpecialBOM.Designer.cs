namespace Hownet.Materiel
{
    partial class frSpecialBOM
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
            this._coChildMaterielID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coWastage = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMeasureID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDepartmentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coUsingTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coIsTogethers = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coIsCaic = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMSIID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMaterielID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMainID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coToColorID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reCompany)).BeginInit();
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
            this.simpleButton1.Location = new System.Drawing.Point(448, 12);
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
            this.panel1.Size = new System.Drawing.Size(757, 83);
            this.panel1.TabIndex = 6;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton3.Location = new System.Drawing.Point(630, 11);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(75, 23);
            this.simpleButton3.TabIndex = 5;
            this.simpleButton3.Text = "删除";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.Location = new System.Drawing.Point(538, 12);
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
            this.labelControl2.Size = new System.Drawing.Size(336, 28);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "有选择客户时，该客户的生产计划将只使用设置了客户的物料。\r\n没有选择客户时，根据颜色来区分所需要使用的物料。";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 83);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._reColor,
            this._reCompany});
            this.gridControl1.Size = new System.Drawing.Size(757, 189);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coID,
            this._coColorID,
            this._coCompanyID,
            this._coChildMaterielID,
            this._coAmount,
            this._coWastage,
            this._coMeasureID,
            this._coA,
            this._coDepartmentID,
            this._coUsingTypeID,
            this._coIsTogethers,
            this._coPrice,
            this._coMoney,
            this._coIsCaic,
            this._coMSIID,
            this._coMaterielID,
            this._coMainID,
            this._coToColorID});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // _coID
            // 
            this._coID.Caption = "ID";
            this._coID.FieldName = "InfoID";
            this._coID.Name = "_coID";
            this._coID.OptionsColumn.AllowEdit = false;
            // 
            // _coColorID
            // 
            this._coColorID.AppearanceHeader.Options.UseTextOptions = true;
            this._coColorID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coColorID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this._coColorID.Caption = "成品颜色";
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
            this._coCompanyID.AppearanceHeader.Options.UseTextOptions = true;
            this._coCompanyID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coCompanyID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this._coCompanyID.Caption = "客户";
            this._coCompanyID.ColumnEdit = this._reCompany;
            this._coCompanyID.FieldName = "CompanyID";
            this._coCompanyID.Name = "_coCompanyID";
            this._coCompanyID.Visible = true;
            this._coCompanyID.VisibleIndex = 1;
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
            // _coChildMaterielID
            // 
            this._coChildMaterielID.AppearanceHeader.Options.UseTextOptions = true;
            this._coChildMaterielID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coChildMaterielID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this._coChildMaterielID.Caption = "普通材料";
            this._coChildMaterielID.FieldName = "ChildMaterielID";
            this._coChildMaterielID.Name = "_coChildMaterielID";
            this._coChildMaterielID.OptionsColumn.AllowEdit = false;
            // 
            // _coAmount
            // 
            this._coAmount.AppearanceHeader.Options.UseTextOptions = true;
            this._coAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coAmount.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this._coAmount.Caption = "用量";
            this._coAmount.FieldName = "Amount";
            this._coAmount.Name = "_coAmount";
            this._coAmount.OptionsColumn.AllowEdit = false;
            // 
            // _coWastage
            // 
            this._coWastage.AppearanceHeader.Options.UseTextOptions = true;
            this._coWastage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coWastage.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this._coWastage.Caption = "损耗";
            this._coWastage.FieldName = "Wastage";
            this._coWastage.Name = "_coWastage";
            this._coWastage.OptionsColumn.AllowEdit = false;
            // 
            // _coMeasureID
            // 
            this._coMeasureID.AppearanceHeader.Options.UseTextOptions = true;
            this._coMeasureID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coMeasureID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this._coMeasureID.Caption = "单位";
            this._coMeasureID.FieldName = "MeasureID";
            this._coMeasureID.Name = "_coMeasureID";
            this._coMeasureID.OptionsColumn.AllowEdit = false;
            // 
            // _coA
            // 
            this._coA.Caption = "A";
            this._coA.FieldName = "A";
            this._coA.Name = "_coA";
            this._coA.OptionsColumn.AllowEdit = false;
            // 
            // _coDepartmentID
            // 
            this._coDepartmentID.AppearanceHeader.Options.UseTextOptions = true;
            this._coDepartmentID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coDepartmentID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this._coDepartmentID.Caption = "领用部门";
            this._coDepartmentID.FieldName = "DepartmentID";
            this._coDepartmentID.Name = "_coDepartmentID";
            this._coDepartmentID.OptionsColumn.AllowEdit = false;
            // 
            // _coUsingTypeID
            // 
            this._coUsingTypeID.AppearanceHeader.Options.UseTextOptions = true;
            this._coUsingTypeID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coUsingTypeID.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this._coUsingTypeID.Caption = "使用方法";
            this._coUsingTypeID.FieldName = "UsingTypeID";
            this._coUsingTypeID.Name = "_coUsingTypeID";
            this._coUsingTypeID.OptionsColumn.AllowEdit = false;
            // 
            // _coIsTogethers
            // 
            this._coIsTogethers.AppearanceHeader.Options.UseTextOptions = true;
            this._coIsTogethers.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coIsTogethers.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this._coIsTogethers.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this._coIsTogethers.Caption = "是否为<br>主料";
            this._coIsTogethers.FieldName = "IsTogethers";
            this._coIsTogethers.Name = "_coIsTogethers";
            this._coIsTogethers.OptionsColumn.AllowEdit = false;
            // 
            // _coPrice
            // 
            this._coPrice.AppearanceHeader.Options.UseTextOptions = true;
            this._coPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this._coPrice.Caption = "单价";
            this._coPrice.FieldName = "Price";
            this._coPrice.Name = "_coPrice";
            this._coPrice.Visible = true;
            this._coPrice.VisibleIndex = 3;
            // 
            // _coMoney
            // 
            this._coMoney.AppearanceHeader.Options.UseTextOptions = true;
            this._coMoney.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coMoney.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this._coMoney.Caption = "单件金额";
            this._coMoney.FieldName = "Money";
            this._coMoney.Name = "_coMoney";
            this._coMoney.Visible = true;
            this._coMoney.VisibleIndex = 4;
            // 
            // _coIsCaic
            // 
            this._coIsCaic.AppearanceHeader.Options.UseTextOptions = true;
            this._coIsCaic.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coIsCaic.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this._coIsCaic.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this._coIsCaic.Caption = "是否参与<br>物料计算";
            this._coIsCaic.FieldName = "IsCaic";
            this._coIsCaic.Name = "_coIsCaic";
            this._coIsCaic.Visible = true;
            this._coIsCaic.VisibleIndex = 5;
            // 
            // _coMSIID
            // 
            this._coMSIID.Caption = "MSIID";
            this._coMSIID.FieldName = "MSIID";
            this._coMSIID.Name = "_coMSIID";
            this._coMSIID.OptionsColumn.AllowEdit = false;
            // 
            // _coMaterielID
            // 
            this._coMaterielID.Caption = "MaterielID";
            this._coMaterielID.FieldName = "MaterielID";
            this._coMaterielID.Name = "_coMaterielID";
            this._coMaterielID.OptionsColumn.AllowEdit = false;
            // 
            // _coMainID
            // 
            this._coMainID.Caption = "MainID";
            this._coMainID.FieldName = "MainID";
            this._coMainID.Name = "_coMainID";
            this._coMainID.OptionsColumn.AllowEdit = false;
            // 
            // _coToColorID
            // 
            this._coToColorID.Caption = "物料颜色";
            this._coToColorID.ColumnEdit = this._reColor;
            this._coToColorID.FieldName = "ToColorID";
            this._coToColorID.Name = "_coToColorID";
            this._coToColorID.Visible = true;
            this._coToColorID.VisibleIndex = 2;
            // 
            // frSpecialBOM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 272);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frSpecialBOM";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "特殊物料设置";
            this.Load += new System.EventHandler(this.frSpecial_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reCompany)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn _coAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coWastage;
        private DevExpress.XtraGrid.Columns.GridColumn _coMeasureID;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reColor;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reCompany;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraGrid.Columns.GridColumn _coDepartmentID;
        private DevExpress.XtraGrid.Columns.GridColumn _coUsingTypeID;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.Columns.GridColumn _coIsTogethers;
        private DevExpress.XtraGrid.Columns.GridColumn _coPrice;
        private DevExpress.XtraGrid.Columns.GridColumn _coMoney;
        private DevExpress.XtraGrid.Columns.GridColumn _coIsCaic;
        private DevExpress.XtraGrid.Columns.GridColumn _coMSIID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMaterielID;
        public DevExpress.XtraGrid.Columns.GridColumn _coChildMaterielID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMainID;
        private DevExpress.XtraGrid.Columns.GridColumn _coToColorID;
    }
}