namespace Hownet.BaseForm
{
    partial class BarSpecialWork
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraBars.BarButtonItem _barAddTable;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarSpecialWork));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this._barEdit = new DevExpress.XtraBars.BarButtonItem();
            this._barDelInfo = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this._barPrint = new DevExpress.XtraBars.BarButtonItem();
            this._barVerify = new DevExpress.XtraBars.BarButtonItem();
            this._barUnVerify = new DevExpress.XtraBars.BarButtonItem();
            this._barExit = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this._barFill = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this._barLaVerify = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coMID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coName = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMaterielRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMaterielID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coSpecialWork = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reWork = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this._coCompanyID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reCompany = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this._coColorID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reColor = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this._coWorkingID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coOrders = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coGroupBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._barAddInfo = new DevExpress.XtraBars.BarButtonItem();
            this._barDelTable = new DevExpress.XtraBars.BarButtonItem();
            _barAddTable = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reWork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // _barAddTable
            // 
            _barAddTable.Caption = "单据";
            _barAddTable.Id = 6;
            _barAddTable.ImageIndex = 1;
            _barAddTable.Name = "_barAddTable";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "AddTableHS.png");
            this.imageList1.Images.SetKeyName(1, "AdpDiagramAddTable.png");
            this.imageList1.Images.SetKeyName(2, "AssignTask.png");
            this.imageList1.Images.SetKeyName(3, "DataContainer_MoveFirstHS.png");
            this.imageList1.Images.SetKeyName(4, "DataContainer_MoveLastHS.png");
            this.imageList1.Images.SetKeyName(5, "DataContainer_MoveNextHS.png");
            this.imageList1.Images.SetKeyName(6, "DataContainer_MovePreviousHS.png");
            this.imageList1.Images.SetKeyName(7, "DataContainer_NewRecordHS.png");
            this.imageList1.Images.SetKeyName(8, "Delete.png");
            this.imageList1.Images.SetKeyName(9, "EditComposePage.png");
            this.imageList1.Images.SetKeyName(10, "EditQuery.png");
            this.imageList1.Images.SetKeyName(11, "EditTableHS.png");
            this.imageList1.Images.SetKeyName(12, "EditWorkflowTask.png");
            this.imageList1.Images.SetKeyName(13, "ExportExcel.png");
            this.imageList1.Images.SetKeyName(14, "FieldsUpdate.png");
            this.imageList1.Images.SetKeyName(15, "Filter.png");
            this.imageList1.Images.SetKeyName(16, "FunctionHS.png");
            this.imageList1.Images.SetKeyName(17, "GroupInkClose.png");
            this.imageList1.Images.SetKeyName(18, "GroupMacroRows.png");
            this.imageList1.Images.SetKeyName(19, "PrintDialogAccess.png");
            this.imageList1.Images.SetKeyName(20, "SaveAndClose.png");
            this.imageList1.Images.SetKeyName(21, "TableOfContentsRemove.png");
            this.imageList1.Images.SetKeyName(22, "TableInsertCellsDialog.png");
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this.splitContainerControl1.Panel2;
            this.barManager1.Images = this.imageList1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this._barAddInfo,
            _barAddTable,
            this._barEdit,
            this._barDelInfo,
            this._barDelTable,
            this.barButtonItem1,
            this._barPrint,
            this._barVerify,
            this._barUnVerify,
            this._barExit,
            this._barFill,
            this.barStaticItem2,
            this._barLaVerify});
            this.barManager1.MaxItemId = 20;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barDelInfo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barVerify, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barUnVerify, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barExit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // _barEdit
            // 
            this._barEdit.Caption = "修改";
            this._barEdit.Id = 7;
            this._barEdit.ImageIndex = 11;
            this._barEdit.Name = "_barEdit";
            this._barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // _barDelInfo
            // 
            this._barDelInfo.Caption = "明细";
            this._barDelInfo.Id = 9;
            this._barDelInfo.ImageIndex = 18;
            this._barDelInfo.Name = "_barDelInfo";
            this._barDelInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "保存";
            this.barButtonItem1.Id = 11;
            this.barButtonItem1.ImageIndex = 20;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // _barPrint
            // 
            this._barPrint.Caption = "打印";
            this._barPrint.Id = 12;
            this._barPrint.ImageIndex = 19;
            this._barPrint.Name = "_barPrint";
            // 
            // _barVerify
            // 
            this._barVerify.Caption = "审核";
            this._barVerify.Id = 13;
            this._barVerify.ImageIndex = 12;
            this._barVerify.Name = "_barVerify";
            this._barVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // _barUnVerify
            // 
            this._barUnVerify.Caption = "弃审";
            this._barUnVerify.Id = 14;
            this._barUnVerify.ImageIndex = 14;
            this._barUnVerify.Name = "_barUnVerify";
            this._barUnVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // _barExit
            // 
            this._barExit.Caption = "退出";
            this._barExit.Id = 15;
            this._barExit.ImageIndex = 17;
            this._barExit.Name = "_barExit";
            this._barExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barExit_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this._barFill),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this._barLaVerify)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // _barFill
            // 
            this._barFill.Id = 16;
            this._barFill.Name = "_barFill";
            this._barFill.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Caption = "              ";
            this.barStaticItem2.Id = 17;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // _barLaVerify
            // 
            this._barLaVerify.Id = 18;
            this._barLaVerify.Name = "_barLaVerify";
            this._barLaVerify.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 26);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(817, 527);
            this.splitContainerControl1.SplitterPosition = 226;
            this.splitContainerControl1.TabIndex = 4;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView3;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(226, 527);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coMID,
            this._coName,
            this._coMaterielRemark});
            this.gridView3.GridControl = this.gridControl2;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.Editable = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView3_FocusedRowChanged);
            // 
            // _coMID
            // 
            this._coMID.Caption = "MaterielID";
            this._coMID.FieldName = "ID";
            this._coMID.Name = "_coMID";
            // 
            // _coName
            // 
            this._coName.Caption = "款号";
            this._coName.FieldName = "Name";
            this._coName.Name = "_coName";
            this._coName.Visible = true;
            this._coName.VisibleIndex = 0;
            this._coName.Width = 70;
            // 
            // _coMaterielRemark
            // 
            this._coMaterielRemark.Caption = "说明";
            this._coMaterielRemark.FieldName = "Remark";
            this._coMaterielRemark.Name = "_coMaterielRemark";
            this._coMaterielRemark.Visible = true;
            this._coMaterielRemark.VisibleIndex = 1;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.labelControl1);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.splitContainerControl2.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl2.Panel2.ShowCaption = true;
            this.splitContainerControl2.Size = new System.Drawing.Size(585, 527);
            this.splitContainerControl2.SplitterPosition = 66;
            this.splitContainerControl2.TabIndex = 1;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(585, 66);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "说明：\r\n在生成工票时，该款号中的特殊工序，将根据当前颜色，在以下列表中查找对应的普通工序，未指明颜色的，全部用列表中颜色为空的普通工序。\r\n特殊工序和对应工序都" +
                "为空的，在保存时将被删除。";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._reWork,
            this._reColor,
            this._reCompany});
            this.gridControl1.Size = new System.Drawing.Size(585, 455);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView2});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coID,
            this._coMaterielID,
            this._coSpecialWork,
            this._coCompanyID,
            this._coColorID,
            this._coWorkingID,
            this._coOrders,
            this._coGroupBy,
            this._coPrice,
            this._coA});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            // 
            // _coID
            // 
            this._coID.Caption = "ID";
            this._coID.FieldName = "ProductWorkingID";
            this._coID.Name = "_coID";
            // 
            // _coMaterielID
            // 
            this._coMaterielID.Caption = "MaterielID";
            this._coMaterielID.FieldName = "MaterielID";
            this._coMaterielID.Name = "_coMaterielID";
            // 
            // _coSpecialWork
            // 
            this._coSpecialWork.Caption = "特殊工序";
            this._coSpecialWork.ColumnEdit = this._reWork;
            this._coSpecialWork.FieldName = "SpecialWork";
            this._coSpecialWork.Name = "_coSpecialWork";
            this._coSpecialWork.Visible = true;
            this._coSpecialWork.VisibleIndex = 0;
            // 
            // _reWork
            // 
            this._reWork.AutoHeight = false;
            this._reWork.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._reWork.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("WorkingName", "特殊工序名"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GroupBy", "Name3", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Orders", "Name6", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this._reWork.DisplayMember = "WorkingName";
            this._reWork.Name = "_reWork";
            this._reWork.NullText = "";
            this._reWork.ValueMember = "WorkingID";
            // 
            // _coCompanyID
            // 
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
            // _coColorID
            // 
            this._coColorID.Caption = "颜色";
            this._coColorID.ColumnEdit = this._reColor;
            this._coColorID.FieldName = "ColorID";
            this._coColorID.Name = "_coColorID";
            this._coColorID.Visible = true;
            this._coColorID.VisibleIndex = 2;
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
            // _coWorkingID
            // 
            this._coWorkingID.Caption = "对应工序";
            this._coWorkingID.FieldName = "WorkingID";
            this._coWorkingID.Name = "_coWorkingID";
            this._coWorkingID.Visible = true;
            this._coWorkingID.VisibleIndex = 3;
            // 
            // _coOrders
            // 
            this._coOrders.Caption = "序号";
            this._coOrders.FieldName = "Orders";
            this._coOrders.Name = "_coOrders";
            this._coOrders.Visible = true;
            this._coOrders.VisibleIndex = 4;
            // 
            // _coGroupBy
            // 
            this._coGroupBy.Caption = "分组";
            this._coGroupBy.FieldName = "GroupBy";
            this._coGroupBy.Name = "_coGroupBy";
            this._coGroupBy.Visible = true;
            this._coGroupBy.VisibleIndex = 5;
            // 
            // _coPrice
            // 
            this._coPrice.Caption = "工价";
            this._coPrice.FieldName = "Price";
            this._coPrice.Name = "_coPrice";
            this._coPrice.Visible = true;
            this._coPrice.VisibleIndex = 6;
            // 
            // _coA
            // 
            this._coA.Caption = "A";
            this._coA.FieldName = "A";
            this._coA.Name = "_coA";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.Name = "gridView2";
            // 
            // _barAddInfo
            // 
            this._barAddInfo.Caption = "明细";
            this._barAddInfo.Id = 5;
            this._barAddInfo.ImageIndex = 22;
            this._barAddInfo.Name = "_barAddInfo";
            // 
            // _barDelTable
            // 
            this._barDelTable.Caption = "单据";
            this._barDelTable.Id = 10;
            this._barDelTable.ImageIndex = 21;
            this._barDelTable.Name = "_barDelTable";
            // 
            // BarSpecialWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 583);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BarSpecialWork";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "特殊工序设置";
            this.Load += new System.EventHandler(this.StockBackForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reWork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem _barAddInfo;
        private DevExpress.XtraBars.BarButtonItem _barEdit;
        private DevExpress.XtraBars.BarButtonItem _barDelInfo;
        private DevExpress.XtraBars.BarButtonItem _barDelTable;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem _barPrint;
        private DevExpress.XtraBars.BarButtonItem _barVerify;
        private DevExpress.XtraBars.BarButtonItem _barUnVerify;
        private DevExpress.XtraBars.BarButtonItem _barExit;
        private DevExpress.XtraBars.BarStaticItem _barFill;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.BarStaticItem _barLaVerify;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMaterielID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorID;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn _coSpecialWork;
        private DevExpress.XtraGrid.Columns.GridColumn _coWorkingID;
        private DevExpress.XtraGrid.Columns.GridColumn _coPrice;
        private DevExpress.XtraGrid.Columns.GridColumn _coGroupBy;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reWork;
        private DevExpress.XtraGrid.Columns.GridColumn _coOrders;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn _coMID;
        private DevExpress.XtraGrid.Columns.GridColumn _coName;
        private DevExpress.XtraGrid.Columns.GridColumn _coMaterielRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coCompanyID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reCompany;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reColor;
    }
}