namespace Hownet.BaseForm
{
    partial class frSupplier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frSupplier));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this._barAdd = new DevExpress.XtraBars.BarButtonItem();
            this._barEdit = new DevExpress.XtraBars.BarButtonItem();
            this._barDel = new DevExpress.XtraBars.BarButtonItem();
            this._barPrint = new DevExpress.XtraBars.BarButtonItem();
            this._barExcel = new DevExpress.XtraBars.BarButtonItem();
            this._barExit = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coSn = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coName = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coLinkMan = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMabile = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coProvince = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coCity = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coIsEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coLoanMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coLoanDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coLastMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this._rePro = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this._reCity = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this._coCounty = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._rePro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reCity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
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
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this._barAdd,
            this._barEdit,
            this._barDel,
            this._barPrint,
            this._barExcel,
            this._barExit,
            this.barButtonItem1});
            this.barManager1.MaxItemId = 9;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barDel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barExcel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barExit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // _barAdd
            // 
            this._barAdd.Caption = "新增";
            this._barAdd.Id = 0;
            this._barAdd.ImageIndex = 6;
            this._barAdd.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this._barAdd.Name = "_barAdd";
            this._barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barAdd_ItemClick);
            // 
            // _barEdit
            // 
            this._barEdit.Caption = "编辑";
            this._barEdit.Id = 1;
            this._barEdit.ImageIndex = 0;
            this._barEdit.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2);
            this._barEdit.Name = "_barEdit";
            this._barEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barEdit_ItemClick);
            // 
            // _barDel
            // 
            this._barDel.Caption = "删除";
            this._barDel.Id = 2;
            this._barDel.ImageIndex = 3;
            this._barDel.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D));
            this._barDel.Name = "_barDel";
            this._barDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barDel_ItemClick);
            // 
            // _barPrint
            // 
            this._barPrint.Caption = "打印";
            this._barPrint.Id = 4;
            this._barPrint.ImageIndex = 4;
            this._barPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P));
            this._barPrint.Name = "_barPrint";
            this._barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barPrint_ItemClick);
            // 
            // _barExcel
            // 
            this._barExcel.Caption = "导出Excel";
            this._barExcel.Id = 5;
            this._barExcel.ImageIndex = 1;
            this._barExcel.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E));
            this._barExcel.Name = "_barExcel";
            this._barExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barExcel_ItemClick);
            // 
            // _barExit
            // 
            this._barExit.Caption = "关闭";
            this._barExit.Id = 6;
            this._barExit.ImageIndex = 2;
            this._barExit.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q));
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
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(861, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 443);
            this.barDockControlBottom.Size = new System.Drawing.Size(861, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 412);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(861, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 412);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "EditTableHS.png");
            this.imageList1.Images.SetKeyName(1, "ExportExcel.png");
            this.imageList1.Images.SetKeyName(2, "GroupInkClose.png");
            this.imageList1.Images.SetKeyName(3, "GroupMacroRows.png");
            this.imageList1.Images.SetKeyName(4, "PrintDialogAccess.png");
            this.imageList1.Images.SetKeyName(5, "SaveAndClose.png");
            this.imageList1.Images.SetKeyName(6, "TableInsertCellsDialog.png");
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "恢復使用";
            this.barButtonItem1.Id = 8;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 59);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._rePro,
            this._reCity});
            this.gridControl1.Size = new System.Drawing.Size(861, 384);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView1.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridView1.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView1.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridView1.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gridView1.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gridView1.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gridView1.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(190)))), ((int)(((byte)(243)))));
            this.gridView1.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridView1.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gridView1.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gridView1.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gridView1.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.Empty.Options.UseBackColor = true;
            this.gridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.gridView1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridView1.Appearance.EvenRow.Options.UseForeColor = true;
            this.gridView1.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView1.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridView1.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView1.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridView1.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gridView1.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gridView1.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gridView1.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gridView1.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
            this.gridView1.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gridView1.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.gridView1.Appearance.FixedLine.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.gridView1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridView1.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView1.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridView1.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridView1.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gridView1.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gridView1.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.GroupButton.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gridView1.Appearance.GroupButton.Options.UseForeColor = true;
            this.gridView1.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gridView1.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gridView1.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gridView1.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView1.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridView1.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupRow.Options.UseBorderColor = true;
            this.gridView1.Appearance.GroupRow.Options.UseFont = true;
            this.gridView1.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridView1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView1.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridView1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridView1.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(228)))));
            this.gridView1.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(224)))), ((int)(((byte)(251)))));
            this.gridView1.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridView1.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridView1.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gridView1.Appearance.HorzLine.Options.UseBackColor = true;
            this.gridView1.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.OddRow.Options.UseBackColor = true;
            this.gridView1.Appearance.OddRow.Options.UseForeColor = true;
            this.gridView1.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(129)))), ((int)(((byte)(185)))));
            this.gridView1.Appearance.Preview.Options.UseBackColor = true;
            this.gridView1.Appearance.Preview.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.Row.Options.UseBackColor = true;
            this.gridView1.Appearance.Row.Options.UseForeColor = true;
            this.gridView1.Appearance.RowSeparator.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gridView1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(126)))), ((int)(((byte)(217)))));
            this.gridView1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gridView1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridView1.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gridView1.Appearance.VertLine.Options.UseBackColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coSn,
            this._coName,
            this._coLinkMan,
            this._coPhone,
            this._coFax,
            this._coMabile,
            this._coCountry,
            this._coProvince,
            this._coCity,
            this._coAddress,
            this._coEmail,
            this._coRemark,
            this._coA,
            this._coID,
            this._coIsEnd,
            this._coTypeID,
            this._coLoanMoney,
            this._coLoanDate,
            this._coLastMoney,
            this._coCounty});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseUp);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // _coSn
            // 
            this._coSn.Caption = "编号";
            this._coSn.FieldName = "Sn";
            this._coSn.Name = "_coSn";
            this._coSn.OptionsColumn.AllowEdit = false;
            this._coSn.Visible = true;
            this._coSn.VisibleIndex = 0;
            this._coSn.Width = 83;
            // 
            // _coName
            // 
            this._coName.Caption = "供应商名称";
            this._coName.FieldName = "Name";
            this._coName.Name = "_coName";
            this._coName.Visible = true;
            this._coName.VisibleIndex = 1;
            this._coName.Width = 101;
            // 
            // _coLinkMan
            // 
            this._coLinkMan.Caption = "联系人";
            this._coLinkMan.FieldName = "LinkMan";
            this._coLinkMan.Name = "_coLinkMan";
            this._coLinkMan.Visible = true;
            this._coLinkMan.VisibleIndex = 2;
            this._coLinkMan.Width = 57;
            // 
            // _coPhone
            // 
            this._coPhone.Caption = "电话";
            this._coPhone.FieldName = "Phone";
            this._coPhone.Name = "_coPhone";
            this._coPhone.Visible = true;
            this._coPhone.VisibleIndex = 3;
            this._coPhone.Width = 57;
            // 
            // _coFax
            // 
            this._coFax.Caption = "传真";
            this._coFax.FieldName = "Fax";
            this._coFax.Name = "_coFax";
            this._coFax.Visible = true;
            this._coFax.VisibleIndex = 4;
            this._coFax.Width = 57;
            // 
            // _coMabile
            // 
            this._coMabile.Caption = "手机";
            this._coMabile.FieldName = "Mabile";
            this._coMabile.Name = "_coMabile";
            this._coMabile.Visible = true;
            this._coMabile.VisibleIndex = 5;
            this._coMabile.Width = 57;
            // 
            // _coCountry
            // 
            this._coCountry.Caption = "国家";
            this._coCountry.FieldName = "Country";
            this._coCountry.Name = "_coCountry";
            this._coCountry.Visible = true;
            this._coCountry.VisibleIndex = 6;
            this._coCountry.Width = 70;
            // 
            // _coProvince
            // 
            this._coProvince.Caption = "省";
            this._coProvince.FieldName = "Province";
            this._coProvince.Name = "_coProvince";
            this._coProvince.Visible = true;
            this._coProvince.VisibleIndex = 7;
            // 
            // _coCity
            // 
            this._coCity.Caption = "城市";
            this._coCity.FieldName = "City";
            this._coCity.Name = "_coCity";
            this._coCity.Visible = true;
            this._coCity.VisibleIndex = 8;
            this._coCity.Width = 67;
            // 
            // _coAddress
            // 
            this._coAddress.Caption = "地址";
            this._coAddress.FieldName = "Address";
            this._coAddress.Name = "_coAddress";
            this._coAddress.Visible = true;
            this._coAddress.VisibleIndex = 10;
            this._coAddress.Width = 94;
            // 
            // _coEmail
            // 
            this._coEmail.Caption = "电子邮件";
            this._coEmail.FieldName = "Email";
            this._coEmail.Name = "_coEmail";
            this._coEmail.Visible = true;
            this._coEmail.VisibleIndex = 11;
            this._coEmail.Width = 62;
            // 
            // _coRemark
            // 
            this._coRemark.Caption = "说明";
            this._coRemark.FieldName = "Remark";
            this._coRemark.Name = "_coRemark";
            this._coRemark.Visible = true;
            this._coRemark.VisibleIndex = 12;
            this._coRemark.Width = 138;
            // 
            // _coA
            // 
            this._coA.Caption = "A";
            this._coA.FieldName = "A";
            this._coA.Name = "_coA";
            this._coA.OptionsColumn.AllowEdit = false;
            // 
            // _coID
            // 
            this._coID.Caption = "ID";
            this._coID.FieldName = "ID";
            this._coID.Name = "_coID";
            this._coID.OptionsColumn.AllowEdit = false;
            // 
            // _coIsEnd
            // 
            this._coIsEnd.Caption = "IsEnd";
            this._coIsEnd.FieldName = "IsEnd";
            this._coIsEnd.Name = "_coIsEnd";
            this._coIsEnd.OptionsColumn.AllowEdit = false;
            // 
            // _coTypeID
            // 
            this._coTypeID.Caption = "TypeID";
            this._coTypeID.FieldName = "TypeID";
            this._coTypeID.Name = "_coTypeID";
            this._coTypeID.OptionsColumn.AllowEdit = false;
            // 
            // _coLoanMoney
            // 
            this._coLoanMoney.Caption = "期初余额";
            this._coLoanMoney.FieldName = "LoanMoney";
            this._coLoanMoney.Name = "_coLoanMoney";
            this._coLoanMoney.Visible = true;
            this._coLoanMoney.VisibleIndex = 13;
            // 
            // _coLoanDate
            // 
            this._coLoanDate.Caption = "余额时间";
            this._coLoanDate.FieldName = "LoanDate";
            this._coLoanDate.Name = "_coLoanDate";
            this._coLoanDate.Visible = true;
            this._coLoanDate.VisibleIndex = 14;
            // 
            // _coLastMoney
            // 
            this._coLastMoney.Caption = "现在余额";
            this._coLastMoney.FieldName = "LastMoney";
            this._coLastMoney.Name = "_coLastMoney";
            this._coLastMoney.Visible = true;
            this._coLastMoney.VisibleIndex = 15;
            // 
            // _rePro
            // 
            this._rePro.AutoHeight = false;
            this._rePro.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._rePro.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "省")});
            this._rePro.DisplayMember = "Name";
            this._rePro.Name = "_rePro";
            this._rePro.ValueMember = "ID";
            // 
            // _reCity
            // 
            this._reCity.AutoHeight = false;
            this._reCity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._reCity.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "市")});
            this._reCity.DisplayMember = "Name";
            this._reCity.Name = "_reCity";
            this._reCity.ValueMember = "ID";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioGroup1.Location = new System.Drawing.Point(0, 31);
            this.radioGroup1.MenuManager = this.barManager1;
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "使用中"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "已刪除"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "全部")});
            this.radioGroup1.Size = new System.Drawing.Size(861, 28);
            this.radioGroup1.TabIndex = 6;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // _coCounty
            // 
            this._coCounty.Caption = "县";
            this._coCounty.FieldName = "County";
            this._coCounty.Name = "_coCounty";
            this._coCounty.Visible = true;
            this._coCounty.VisibleIndex = 9;
            // 
            // frSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 466);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frSupplier";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "供应商";
            this.Load += new System.EventHandler(this.frColor_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frColor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._rePro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reCity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem _barAdd;
        private DevExpress.XtraBars.BarButtonItem _barEdit;
        private DevExpress.XtraBars.BarButtonItem _barDel;
        private DevExpress.XtraBars.BarButtonItem _barPrint;
        private DevExpress.XtraBars.BarButtonItem _barExcel;
        private DevExpress.XtraBars.BarButtonItem _barExit;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _coName;
        private DevExpress.XtraGrid.Columns.GridColumn _coSn;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraGrid.Columns.GridColumn _coIsEnd;
        private DevExpress.XtraGrid.Columns.GridColumn _coRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coLinkMan;
        private DevExpress.XtraGrid.Columns.GridColumn _coMabile;
        private DevExpress.XtraGrid.Columns.GridColumn _coPhone;
        private DevExpress.XtraGrid.Columns.GridColumn _coFax;
        private DevExpress.XtraGrid.Columns.GridColumn _coCountry;
        private DevExpress.XtraGrid.Columns.GridColumn _coProvince;
        private DevExpress.XtraGrid.Columns.GridColumn _coCity;
        private DevExpress.XtraGrid.Columns.GridColumn _coAddress;
        private DevExpress.XtraGrid.Columns.GridColumn _coTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn _coEmail;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _rePro;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reCity;
        private DevExpress.XtraGrid.Columns.GridColumn _coLoanMoney;
        private DevExpress.XtraGrid.Columns.GridColumn _coLoanDate;
        private DevExpress.XtraGrid.Columns.GridColumn _coLastMoney;
        private DevExpress.XtraGrid.Columns.GridColumn _coCounty;
    }
}