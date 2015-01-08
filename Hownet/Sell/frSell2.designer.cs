using Hownet.BaseContranl;
namespace Hownet.Sell
{
    partial class frSell2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frSell2));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this._brFrist = new DevExpress.XtraBars.BarButtonItem();
            this._brPrv = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brNext = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brLast = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brAddNew = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brSave = new DevExpress.XtraBars.BarLargeButtonItem();
            this._barDel = new DevExpress.XtraBars.BarSubItem();
            this._barDelTable = new DevExpress.XtraBars.BarButtonItem();
            this._barDelInfo = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this._barPrintTable = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this._barPrintInfo = new DevExpress.XtraBars.BarButtonItem();
            this._brFilter = new DevExpress.XtraBars.BarButtonItem();
            this._barVerify = new DevExpress.XtraBars.BarButtonItem();
            this._barUnVierfy = new DevExpress.XtraBars.BarButtonItem();
            this._barPosting = new DevExpress.XtraBars.BarButtonItem();
            this._barUnPosting = new DevExpress.XtraBars.BarButtonItem();
            this._brExit = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this._barLoan = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this._ldDate = new Hownet.BaseContranl.LabAndData();
            this._ltNum = new Hownet.BaseContranl.LabAndText();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucGridLookup1 = new Hownet.BaseContranl.ucGridLookup();
            this.label2 = new System.Windows.Forms.Label();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this._leDepotID = new Hownet.BaseContranl.LabAndLookupEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this._ltRemark = new Hownet.BaseContranl.LabAndText();
            this.label1 = new System.Windows.Forms.Label();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coMaterielID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reMateriel = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this._coBrandID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coConversion = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coBoxMeasureAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMainID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMeasureID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coBoxMeasureID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reBEAmount = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reMateriel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reBEAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this._brAddNew,
            this._brSave,
            this._brFrist,
            this._brPrv,
            this._brNext,
            this._brLast,
            this._brFilter,
            this._brExit,
            this.barSubItem1,
            this._barPrintTable,
            this._barPrintInfo,
            this._barVerify,
            this._barUnVierfy,
            this._barLoan,
            this._barDel,
            this._barDelTable,
            this._barDelInfo,
            this.barButtonItem1,
            this._barPosting,
            this._barUnPosting});
            this.barManager1.MaxItemId = 51;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            this.barManager1.StatusBar = this.bar2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 4";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brFrist, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brPrv, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brNext, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brLast, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brAddNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barDel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brFilter, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barVerify, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barUnVierfy, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barPosting, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barUnPosting, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brExit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 4";
            // 
            // _brFrist
            // 
            this._brFrist.Caption = "首单";
            this._brFrist.Glyph = ((System.Drawing.Image)(resources.GetObject("_brFrist.Glyph")));
            this._brFrist.Hint = "Ctrl+Up";
            this._brFrist.Id = 4;
            this._brFrist.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up));
            this._brFrist.Name = "_brFrist";
            this._brFrist.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // _brPrv
            // 
            this._brPrv.Caption = "前单";
            this._brPrv.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this._brPrv.Glyph = ((System.Drawing.Image)(resources.GetObject("_brPrv.Glyph")));
            this._brPrv.Hint = "Ctrl+Left";
            this._brPrv.Id = 5;
            this._brPrv.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left));
            this._brPrv.Name = "_brPrv";
            this._brPrv.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItem5_ItemClick);
            // 
            // _brNext
            // 
            this._brNext.Caption = "下单";
            this._brNext.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this._brNext.Glyph = ((System.Drawing.Image)(resources.GetObject("_brNext.Glyph")));
            this._brNext.Hint = "Ctrl+Right";
            this._brNext.Id = 9;
            this._brNext.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right));
            this._brNext.Name = "_brNext";
            this._brNext.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brNext_ItemClick);
            // 
            // _brLast
            // 
            this._brLast.Caption = "尾单";
            this._brLast.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this._brLast.Glyph = ((System.Drawing.Image)(resources.GetObject("_brLast.Glyph")));
            this._brLast.Hint = "Ctrl+Down";
            this._brLast.Id = 10;
            this._brLast.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down));
            this._brLast.Name = "_brLast";
            this._brLast.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brLast_ItemClick);
            // 
            // _brAddNew
            // 
            this._brAddNew.Caption = "新建";
            this._brAddNew.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this._brAddNew.Glyph = ((System.Drawing.Image)(resources.GetObject("_brAddNew.Glyph")));
            this._brAddNew.Hint = "F5";
            this._brAddNew.Id = 0;
            this._brAddNew.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this._brAddNew.Name = "_brAddNew";
            this._brAddNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brAddNew_ItemClick);
            // 
            // _brSave
            // 
            this._brSave.Caption = "保存";
            this._brSave.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this._brSave.Glyph = ((System.Drawing.Image)(resources.GetObject("_brSave.Glyph")));
            this._brSave.Hint = "Ctrl+S";
            this._brSave.Id = 1;
            this._brSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this._brSave.Name = "_brSave";
            this._brSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brSave_ItemClick);
            // 
            // _barDel
            // 
            this._barDel.Caption = "删除";
            this._barDel.Glyph = global::Hownet.Properties.Resources.Delete;
            this._barDel.Id = 45;
            this._barDel.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this._barDelTable),
            new DevExpress.XtraBars.LinkPersistInfo(this._barDelInfo)});
            this._barDel.Name = "_barDel";
            // 
            // _barDelTable
            // 
            this._barDelTable.Caption = "单据";
            this._barDelTable.Id = 46;
            this._barDelTable.Name = "_barDelTable";
            this._barDelTable.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barDelTable_ItemClick);
            // 
            // _barDelInfo
            // 
            this._barDelInfo.Caption = "明细";
            this._barDelInfo.Id = 47;
            this._barDelInfo.Name = "_barDelInfo";
            this._barDelInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barDelInfo_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "打印";
            this.barSubItem1.Glyph = global::Hownet.Properties.Resources.PrintDialogAccess;
            this.barSubItem1.Id = 24;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this._barPrintTable),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this._barPrintInfo)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // _barPrintTable
            // 
            this._barPrintTable.Caption = "销售单";
            this._barPrintTable.Id = 25;
            this._barPrintTable.Name = "_barPrintTable";
            this._barPrintTable.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barPrintTable_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "销售单-A5";
            this.barButtonItem1.Id = 48;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick_1);
            // 
            // _barPrintInfo
            // 
            this._barPrintInfo.Caption = "销售单明细";
            this._barPrintInfo.Id = 27;
            this._barPrintInfo.Name = "_barPrintInfo";
            this._barPrintInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barPrintInfo_ItemClick);
            // 
            // _brFilter
            // 
            this._brFilter.Caption = "筛选";
            this._brFilter.Glyph = ((System.Drawing.Image)(resources.GetObject("_brFilter.Glyph")));
            this._brFilter.Hint = "Ctrl+F";
            this._brFilter.Id = 13;
            this._brFilter.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F));
            this._brFilter.Name = "_brFilter";
            // 
            // _barVerify
            // 
            this._barVerify.Caption = "审核";
            this._barVerify.Glyph = global::Hownet.Properties.Resources.EditWorkflowTask;
            this._barVerify.Id = 38;
            this._barVerify.Name = "_barVerify";
            this._barVerify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barVerify_ItemClick);
            // 
            // _barUnVierfy
            // 
            this._barUnVierfy.Caption = "弃审";
            this._barUnVierfy.Glyph = global::Hownet.Properties.Resources.FieldsUpdate;
            this._barUnVierfy.Id = 43;
            this._barUnVierfy.Name = "_barUnVierfy";
            this._barUnVierfy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barUnVierfy_ItemClick);
            // 
            // _barPosting
            // 
            this._barPosting.Caption = "过帐";
            this._barPosting.Glyph = global::Hownet.Properties.Resources.FollowUpComposeMenu;
            this._barPosting.Id = 49;
            this._barPosting.Name = "_barPosting";
            this._barPosting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // _barUnPosting
            // 
            this._barUnPosting.Caption = "退帐";
            this._barUnPosting.Glyph = global::Hownet.Properties.Resources.FieldsUpdate;
            this._barUnPosting.Id = 50;
            this._barUnPosting.Name = "_barUnPosting";
            this._barUnPosting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barUnPosting_ItemClick);
            // 
            // _brExit
            // 
            this._brExit.Caption = "关闭";
            this._brExit.Glyph = ((System.Drawing.Image)(resources.GetObject("_brExit.Glyph")));
            this._brExit.Hint = "Ctrl+Q";
            this._brExit.Id = 14;
            this._brExit.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q));
            this._brExit.Name = "_brExit";
            this._brExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brExit_ItemClick);
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 3";
            this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this._barLoan)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Custom 3";
            // 
            // _barLoan
            // 
            this._barLoan.Id = 44;
            this._barLoan.Name = "_barLoan";
            this._barLoan.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(901, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 409);
            this.barDockControlBottom.Size = new System.Drawing.Size(901, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 378);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(901, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 378);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // _ldDate
            // 
            this._ldDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._ldDate.IsMust = false;
            this._ldDate.IsShowClear = false;
            this._ldDate.labText = "发货日期：";
            this._ldDate.lenth = new int[] {
        60,
        120};
            this._ldDate.Location = new System.Drawing.Point(590, 39);
            this._ldDate.Margin = new System.Windows.Forms.Padding(0);
            this._ldDate.MaxDate = new System.DateTime(((long)(0)));
            this._ldDate.MinDate = new System.DateTime(((long)(0)));
            this._ldDate.Name = "_ldDate";
            this._ldDate.Size = new System.Drawing.Size(190, 22);
            this._ldDate.strLab = "";
            this._ldDate.TabIndex = 23;
            this._ldDate.val = null;
            // 
            // _ltNum
            // 
            this._ltNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._ltNum.EditVal = "";
            this._ltNum.IsCanEdit = false;
            this._ltNum.IsHand = false;
            this._ltNum.IsMust = false;
            this._ltNum.labText = "编号：";
            this._ltNum.lenth = new int[] {
        50,
        120};
            this._ltNum.Location = new System.Drawing.Point(600, 17);
            this._ltNum.Margin = new System.Windows.Forms.Padding(0);
            this._ltNum.Mask = "";
            this._ltNum.Name = "_ltNum";
            this._ltNum.Size = new System.Drawing.Size(180, 22);
            this._ltNum.TabIndex = 19;
            this._ltNum.val = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucGridLookup1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.simpleButton2);
            this.panel1.Controls.Add(this._leDepotID);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this._ltRemark);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this._ldDate);
            this.panel1.Controls.Add(this._ltNum);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(901, 91);
            this.panel1.TabIndex = 6;
            // 
            // ucGridLookup1
            // 
            this.ucGridLookup1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ucGridLookup1.Location = new System.Drawing.Point(165, 38);
            this.ucGridLookup1.Name = "ucGridLookup1";
            this.ucGridLookup1.Size = new System.Drawing.Size(167, 22);
            this.ucGridLookup1.TabIndex = 30;
            this.ucGridLookup1.Values = 0;
            this.ucGridLookup1.EditValueChanged += new Hownet.BaseContranl.ucGridLookup.EditValueChangedHandler(this.ucGridLookup1_EditValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 31;
            this.label2.Text = "客户：";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton2.Location = new System.Drawing.Point(338, 39);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(43, 23);
            this.simpleButton2.TabIndex = 29;
            this.simpleButton2.Text = "还款";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // _leDepotID
            // 
            this._leDepotID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._leDepotID.editVal = 0;
            this._leDepotID.FormName = 0;
            this._leDepotID.IsMust = false;
            this._leDepotID.IsNotCanEdit = false;
            this._leDepotID.labText = "出货仓库：";
            this._leDepotID.lenth = new int[] {
        60,
        120};
            this._leDepotID.Location = new System.Drawing.Point(397, 39);
            this._leDepotID.Name = "_leDepotID";
            this._leDepotID.Par = null;
            this._leDepotID.Size = new System.Drawing.Size(190, 22);
            this._leDepotID.TabIndex = 28;
            this._leDepotID.EditValueChanged += new Hownet.BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leDepotID_EditValueChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(823, 3);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 27;
            this.simpleButton1.Text = "打印单据";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // _ltRemark
            // 
            this._ltRemark.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._ltRemark.EditVal = "";
            this._ltRemark.IsCanEdit = false;
            this._ltRemark.IsHand = false;
            this._ltRemark.IsMust = false;
            this._ltRemark.labText = "说明：";
            this._ltRemark.lenth = new int[] {
        50,
        600};
            this._ltRemark.Location = new System.Drawing.Point(118, 63);
            this._ltRemark.Margin = new System.Windows.Forms.Padding(0);
            this._ltRemark.Mask = "";
            this._ltRemark.Name = "_ltRemark";
            this._ltRemark.Size = new System.Drawing.Size(660, 22);
            this._ltRemark.TabIndex = 26;
            this._ltRemark.val = "";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(406, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 28);
            this.label1.TabIndex = 25;
            this.label1.Text = "销售发货";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 122);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._reBEAmount,
            this._reMateriel});
            this.gridControl1.Size = new System.Drawing.Size(901, 287);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coMaterielID,
            this._coBrandID,
            this._coAmount,
            this._coPrice,
            this._coConversion,
            this._coBoxMeasureAmount,
            this._coMoney,
            this._coRemark,
            this._coMainID,
            this._coID,
            this._coA,
            this._coMeasureID,
            this._coBoxMeasureID});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 40;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            this.gridView1.RowCountChanged += new System.EventHandler(this.gridView1_RowCountChanged);
            // 
            // _coMaterielID
            // 
            this._coMaterielID.AppearanceHeader.Options.UseTextOptions = true;
            this._coMaterielID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coMaterielID.Caption = "款号";
            this._coMaterielID.ColumnEdit = this._reMateriel;
            this._coMaterielID.FieldName = "MaterielID";
            this._coMaterielID.Name = "_coMaterielID";
            this._coMaterielID.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this._coMaterielID.Visible = true;
            this._coMaterielID.VisibleIndex = 0;
            this._coMaterielID.Width = 74;
            // 
            // _reMateriel
            // 
            this._reMateriel.AutoHeight = false;
            this._reMateriel.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._reMateriel.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "款号")});
            this._reMateriel.DisplayMember = "Name";
            this._reMateriel.Name = "_reMateriel";
            this._reMateriel.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this._reMateriel.ValueMember = "ID";
            this._reMateriel.QueryCloseUp += new System.ComponentModel.CancelEventHandler(this._reMateriel_QueryCloseUp);
            this._reMateriel.Leave += new System.EventHandler(this._reMateriel_Leave);
            // 
            // _coBrandID
            // 
            this._coBrandID.AppearanceHeader.Options.UseTextOptions = true;
            this._coBrandID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coBrandID.Caption = "商标";
            this._coBrandID.FieldName = "BrandID";
            this._coBrandID.Name = "_coBrandID";
            this._coBrandID.Visible = true;
            this._coBrandID.VisibleIndex = 1;
            // 
            // _coAmount
            // 
            this._coAmount.AppearanceHeader.Options.UseTextOptions = true;
            this._coAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coAmount.Caption = "数量";
            this._coAmount.FieldName = "Amount";
            this._coAmount.Name = "_coAmount";
            this._coAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this._coAmount.Visible = true;
            this._coAmount.VisibleIndex = 2;
            // 
            // _coPrice
            // 
            this._coPrice.AppearanceHeader.Options.UseTextOptions = true;
            this._coPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coPrice.Caption = "单价";
            this._coPrice.FieldName = "Price";
            this._coPrice.Name = "_coPrice";
            this._coPrice.Visible = true;
            this._coPrice.VisibleIndex = 3;
            // 
            // _coConversion
            // 
            this._coConversion.AppearanceHeader.Options.UseTextOptions = true;
            this._coConversion.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coConversion.Caption = "每包数量";
            this._coConversion.FieldName = "Conversion";
            this._coConversion.Name = "_coConversion";
            // 
            // _coBoxMeasureAmount
            // 
            this._coBoxMeasureAmount.AppearanceHeader.Options.UseTextOptions = true;
            this._coBoxMeasureAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coBoxMeasureAmount.Caption = "包数";
            this._coBoxMeasureAmount.FieldName = "BoxMeasureAmount";
            this._coBoxMeasureAmount.Name = "_coBoxMeasureAmount";
            this._coBoxMeasureAmount.OptionsColumn.AllowEdit = false;
            this._coBoxMeasureAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            // 
            // _coMoney
            // 
            this._coMoney.AppearanceHeader.Options.UseTextOptions = true;
            this._coMoney.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coMoney.Caption = "金额";
            this._coMoney.FieldName = "Money";
            this._coMoney.Name = "_coMoney";
            this._coMoney.OptionsColumn.AllowEdit = false;
            this._coMoney.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this._coMoney.Visible = true;
            this._coMoney.VisibleIndex = 4;
            this._coMoney.Width = 103;
            // 
            // _coRemark
            // 
            this._coRemark.AppearanceHeader.Options.UseTextOptions = true;
            this._coRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coRemark.Caption = "说明";
            this._coRemark.FieldName = "Remark";
            this._coRemark.Name = "_coRemark";
            this._coRemark.Visible = true;
            this._coRemark.VisibleIndex = 5;
            this._coRemark.Width = 145;
            // 
            // _coMainID
            // 
            this._coMainID.Caption = "MainID";
            this._coMainID.FieldName = "MainID";
            this._coMainID.Name = "_coMainID";
            this._coMainID.OptionsColumn.AllowEdit = false;
            // 
            // _coID
            // 
            this._coID.Caption = "ID";
            this._coID.FieldName = "ID";
            this._coID.Name = "_coID";
            this._coID.OptionsColumn.AllowEdit = false;
            // 
            // _coA
            // 
            this._coA.Caption = "A";
            this._coA.FieldName = "A";
            this._coA.Name = "_coA";
            this._coA.OptionsColumn.AllowEdit = false;
            // 
            // _coMeasureID
            // 
            this._coMeasureID.Caption = "MeasureID";
            this._coMeasureID.FieldName = "MeasureID";
            this._coMeasureID.Name = "_coMeasureID";
            this._coMeasureID.OptionsColumn.AllowEdit = false;
            // 
            // _coBoxMeasureID
            // 
            this._coBoxMeasureID.Caption = "BoxMeasureID";
            this._coBoxMeasureID.FieldName = "BoxMeasureID";
            this._coBoxMeasureID.Name = "_coBoxMeasureID";
            this._coBoxMeasureID.OptionsColumn.AllowEdit = false;
            // 
            // _reBEAmount
            // 
            this._reBEAmount.AutoHeight = false;
            this._reBEAmount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this._reBEAmount.Name = "_reBEAmount";
            this._reBEAmount.ReadOnly = true;
            this._reBEAmount.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this._reBEAmount_ButtonClick);
            // 
            // frSell2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 436);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frSell2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "只扣库存";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reMateriel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reBEAmount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarLargeButtonItem _brAddNew;
        private DevExpress.XtraBars.BarLargeButtonItem _brSave;
        private DevExpress.XtraBars.BarButtonItem _brFrist;
        private DevExpress.XtraBars.BarLargeButtonItem _brPrv;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraBars.BarLargeButtonItem _brNext;
        private DevExpress.XtraBars.BarLargeButtonItem _brLast;
        private DevExpress.XtraBars.BarButtonItem _brFilter;
        private DevExpress.XtraBars.BarButtonItem _brExit;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem _barPrintTable;
        private DevExpress.XtraBars.BarButtonItem _barPrintInfo;
        private DevExpress.XtraBars.BarButtonItem _barVerify;
        private LabAndData _ldDate;
        private LabAndText _ltNum;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraBars.BarButtonItem _barUnVierfy;
        private LabAndText _ltRemark;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarStaticItem _barLoan;
        private DevExpress.XtraBars.BarSubItem _barDel;
        private DevExpress.XtraBars.BarButtonItem _barDelTable;
        private DevExpress.XtraBars.BarButtonItem _barDelInfo;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit _reBEAmount;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reMateriel;
        private LabAndLookupEdit _leDepotID;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _coMaterielID;
        private DevExpress.XtraGrid.Columns.GridColumn _coBrandID;
        private DevExpress.XtraGrid.Columns.GridColumn _coAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coPrice;
        private DevExpress.XtraGrid.Columns.GridColumn _coConversion;
        private DevExpress.XtraGrid.Columns.GridColumn _coBoxMeasureAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coMoney;
        private DevExpress.XtraGrid.Columns.GridColumn _coRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coMainID;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
        private DevExpress.XtraGrid.Columns.GridColumn _coMeasureID;
        private DevExpress.XtraGrid.Columns.GridColumn _coBoxMeasureID;
        private ucGridLookup ucGridLookup1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraBars.BarButtonItem _barPosting;
        private DevExpress.XtraBars.BarButtonItem _barUnPosting;
    }
}