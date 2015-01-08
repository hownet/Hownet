using Hownet.BaseContranl;
namespace Hownet.Stock
{
    partial class frStockLinLiao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frStockLinLiao));
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
            this._brFilter = new DevExpress.XtraBars.BarButtonItem();
            this._barVerify = new DevExpress.XtraBars.BarButtonItem();
            this._barUnVierfy = new DevExpress.XtraBars.BarButtonItem();
            this._brExit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this._barLoan = new DevExpress.XtraBars.BarStaticItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this._ldDate = new Hownet.BaseContranl.LabAndData();
            this._leCompany = new Hownet.BaseContranl.LabAndLookupEdit();
            this._ltNum = new Hownet.BaseContranl.LabAndText();
            this.panel1 = new System.Windows.Forms.Panel();
            this._leDepot = new Hownet.BaseContranl.LabAndLookupEdit();
            this._ltRemark = new Hownet.BaseContranl.LabAndText();
            this.label1 = new System.Windows.Forms.Label();
            this._laStockNum = new Hownet.BaseContranl.LabAndText();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coMaterielID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coSizeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorOneID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorTwoID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNeedAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDepotAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMeasureID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMainID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMListID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coOutAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNotAllotAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coStockInfoID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDemandID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reBEAmount = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reBEAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
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
            this._barVerify,
            this._barUnVierfy,
            this._barLoan,
            this._barDel,
            this._barDelTable,
            this._barDelInfo});
            this.barManager1.MaxItemId = 48;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brAddNew, "", false, true, false, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brSave, "", false, true, false, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barDel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brFilter, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barVerify, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barUnVierfy, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            new DevExpress.XtraBars.LinkPersistInfo(this._barPrintTable)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // _barPrintTable
            // 
            this._barPrintTable.Caption = "领料单";
            this._barPrintTable.Id = 25;
            this._barPrintTable.Name = "_barPrintTable";
            this._barPrintTable.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barPrintTable_ItemClick);
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
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(824, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 436);
            this.barDockControlBottom.Size = new System.Drawing.Size(824, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 405);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(824, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 405);
            // 
            // _barLoan
            // 
            this._barLoan.Id = 44;
            this._barLoan.Name = "_barLoan";
            this._barLoan.TextAlignment = System.Drawing.StringAlignment.Near;
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
            this._ldDate.Location = new System.Drawing.Point(561, 39);
            this._ldDate.Margin = new System.Windows.Forms.Padding(0);
            this._ldDate.MaxDate = new System.DateTime(((long)(0)));
            this._ldDate.MinDate = new System.DateTime(((long)(0)));
            this._ldDate.Name = "_ldDate";
            this._ldDate.Size = new System.Drawing.Size(190, 22);
            this._ldDate.strLab = "";
            this._ldDate.TabIndex = 23;
            this._ldDate.val = null;
            // 
            // _leCompany
            // 
            this._leCompany.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._leCompany.editVal = 0;
            this._leCompany.FormName = 0;
            this._leCompany.IsMust = false;
            this._leCompany.IsNotCanEdit = false;
            this._leCompany.labText = "供应商：";
            this._leCompany.lenth = new int[] {
        50,
        100};
            this._leCompany.Location = new System.Drawing.Point(206, 39);
            this._leCompany.Name = "_leCompany";
            this._leCompany.Par = null;
            this._leCompany.Size = new System.Drawing.Size(160, 22);
            this._leCompany.TabIndex = 20;
            this._leCompany.EditValueChanged += new Hownet.BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leCompany_EditValueChanged);
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
        130};
            this._ltNum.Location = new System.Drawing.Point(562, 9);
            this._ltNum.Margin = new System.Windows.Forms.Padding(0);
            this._ltNum.Mask = "";
            this._ltNum.Name = "_ltNum";
            this._ltNum.Size = new System.Drawing.Size(180, 22);
            this._ltNum.TabIndex = 19;
            this._ltNum.val = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._leDepot);
            this.panel1.Controls.Add(this._ltRemark);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this._ldDate);
            this.panel1.Controls.Add(this._laStockNum);
            this.panel1.Controls.Add(this._ltNum);
            this.panel1.Controls.Add(this._leCompany);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 91);
            this.panel1.TabIndex = 6;
            // 
            // _leDepot
            // 
            this._leDepot.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._leDepot.editVal = 0;
            this._leDepot.FormName = 0;
            this._leDepot.IsMust = false;
            this._leDepot.IsNotCanEdit = false;
            this._leDepot.labText = "出货仓库：";
            this._leDepot.lenth = new int[] {
        60,
        125};
            this._leDepot.Location = new System.Drawing.Point(372, 39);
            this._leDepot.Name = "_leDepot";
            this._leDepot.Par = null;
            this._leDepot.Size = new System.Drawing.Size(195, 22);
            this._leDepot.TabIndex = 27;
            this._leDepot.EditValueChanged += new Hownet.BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leDepot_EditValueChanged);
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
        681};
            this._ltRemark.Location = new System.Drawing.Point(25, 64);
            this._ltRemark.Margin = new System.Windows.Forms.Padding(0);
            this._ltRemark.Mask = "";
            this._ltRemark.Name = "_ltRemark";
            this._ltRemark.Size = new System.Drawing.Size(731, 22);
            this._ltRemark.TabIndex = 26;
            this._ltRemark.val = "";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(292, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 28);
            this.label1.TabIndex = 25;
            this.label1.Text = "半成品外发加工领料";
            // 
            // _laStockNum
            // 
            this._laStockNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._laStockNum.EditVal = "";
            this._laStockNum.IsCanEdit = false;
            this._laStockNum.IsHand = false;
            this._laStockNum.IsMust = false;
            this._laStockNum.labText = "加工单：";
            this._laStockNum.lenth = new int[] {
        50,
        130};
            this._laStockNum.Location = new System.Drawing.Point(25, 41);
            this._laStockNum.Margin = new System.Windows.Forms.Padding(0);
            this._laStockNum.Mask = "";
            this._laStockNum.Name = "_laStockNum";
            this._laStockNum.Size = new System.Drawing.Size(180, 22);
            this._laStockNum.TabIndex = 19;
            this._laStockNum.val = "";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 122);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._reBEAmount});
            this.gridControl1.Size = new System.Drawing.Size(824, 314);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coMaterielID,
            this._coColorID,
            this._coSizeID,
            this._coColorOneID,
            this._coColorTwoID,
            this._coNeedAmount,
            this._coDepotAmount,
            this._coAmount,
            this._coMeasureID,
            this._coRemark,
            this._coMainID,
            this._coID,
            this._coA,
            this._coMListID,
            this._coOutAmount,
            this._coNotAllotAmount,
            this._coStockInfoID,
            this._coDemandID});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 40;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            this.gridView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanging);
            this.gridView1.RowCountChanged += new System.EventHandler(this.gridView1_RowCountChanged);
            // 
            // _coMaterielID
            // 
            this._coMaterielID.AppearanceHeader.Options.UseTextOptions = true;
            this._coMaterielID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coMaterielID.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this._coMaterielID.Caption = "商品名称";
            this._coMaterielID.FieldName = "MaterielID";
            this._coMaterielID.Name = "_coMaterielID";
            this._coMaterielID.OptionsColumn.AllowEdit = false;
            this._coMaterielID.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this._coMaterielID.Visible = true;
            this._coMaterielID.VisibleIndex = 0;
            this._coMaterielID.Width = 68;
            // 
            // _coColorID
            // 
            this._coColorID.AppearanceHeader.Options.UseTextOptions = true;
            this._coColorID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coColorID.Caption = "颜色";
            this._coColorID.FieldName = "ColorID";
            this._coColorID.Name = "_coColorID";
            this._coColorID.OptionsColumn.AllowEdit = false;
            this._coColorID.Visible = true;
            this._coColorID.VisibleIndex = 1;
            this._coColorID.Width = 69;
            // 
            // _coSizeID
            // 
            this._coSizeID.Caption = "尺码";
            this._coSizeID.FieldName = "SizeID";
            this._coSizeID.Name = "_coSizeID";
            this._coSizeID.OptionsColumn.AllowEdit = false;
            this._coSizeID.Visible = true;
            this._coSizeID.VisibleIndex = 2;
            this._coSizeID.Width = 69;
            // 
            // _coColorOneID
            // 
            this._coColorOneID.Caption = "插色一";
            this._coColorOneID.FieldName = "ColorOneID";
            this._coColorOneID.Name = "_coColorOneID";
            this._coColorOneID.OptionsColumn.AllowEdit = false;
            this._coColorOneID.Visible = true;
            this._coColorOneID.VisibleIndex = 3;
            // 
            // _coColorTwoID
            // 
            this._coColorTwoID.Caption = "插色二";
            this._coColorTwoID.FieldName = "ColorTwoID";
            this._coColorTwoID.Name = "_coColorTwoID";
            this._coColorTwoID.OptionsColumn.AllowEdit = false;
            this._coColorTwoID.Visible = true;
            this._coColorTwoID.VisibleIndex = 4;
            // 
            // _coNeedAmount
            // 
            this._coNeedAmount.Caption = "需领数量";
            this._coNeedAmount.FieldName = "NeedAmount";
            this._coNeedAmount.Name = "_coNeedAmount";
            this._coNeedAmount.OptionsColumn.AllowEdit = false;
            this._coNeedAmount.Visible = true;
            this._coNeedAmount.VisibleIndex = 5;
            // 
            // _coDepotAmount
            // 
            this._coDepotAmount.AppearanceHeader.Options.UseTextOptions = true;
            this._coDepotAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coDepotAmount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this._coDepotAmount.Caption = "库存数量";
            this._coDepotAmount.FieldName = "RepertoryAmount";
            this._coDepotAmount.Name = "_coDepotAmount";
            this._coDepotAmount.Visible = true;
            this._coDepotAmount.VisibleIndex = 8;
            this._coDepotAmount.Width = 69;
            // 
            // _coAmount
            // 
            this._coAmount.AppearanceHeader.Options.UseTextOptions = true;
            this._coAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coAmount.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this._coAmount.Caption = "领货数量";
            this._coAmount.FieldName = "Amount";
            this._coAmount.Name = "_coAmount";
            this._coAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this._coAmount.Visible = true;
            this._coAmount.VisibleIndex = 9;
            this._coAmount.Width = 66;
            // 
            // _coMeasureID
            // 
            this._coMeasureID.AppearanceHeader.Options.UseTextOptions = true;
            this._coMeasureID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coMeasureID.Caption = "单位";
            this._coMeasureID.FieldName = "CompanyMeasureID";
            this._coMeasureID.Name = "_coMeasureID";
            this._coMeasureID.OptionsColumn.AllowEdit = false;
            this._coMeasureID.Visible = true;
            this._coMeasureID.VisibleIndex = 10;
            this._coMeasureID.Width = 69;
            // 
            // _coRemark
            // 
            this._coRemark.AppearanceHeader.Options.UseTextOptions = true;
            this._coRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coRemark.Caption = "说明";
            this._coRemark.FieldName = "Remark";
            this._coRemark.Name = "_coRemark";
            this._coRemark.Visible = true;
            this._coRemark.VisibleIndex = 11;
            this._coRemark.Width = 133;
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
            // _coMListID
            // 
            this._coMListID.Caption = "MListID";
            this._coMListID.FieldName = "MListID";
            this._coMListID.Name = "_coMListID";
            this._coMListID.OptionsColumn.AllowEdit = false;
            // 
            // _coOutAmount
            // 
            this._coOutAmount.Caption = "已领数量";
            this._coOutAmount.FieldName = "NotPriceAmount";
            this._coOutAmount.Name = "_coOutAmount";
            this._coOutAmount.OptionsColumn.AllowEdit = false;
            this._coOutAmount.Visible = true;
            this._coOutAmount.VisibleIndex = 6;
            // 
            // _coNotAllotAmount
            // 
            this._coNotAllotAmount.Caption = "未领数量";
            this._coNotAllotAmount.FieldName = "NotAmount";
            this._coNotAllotAmount.Name = "_coNotAllotAmount";
            this._coNotAllotAmount.OptionsColumn.AllowEdit = false;
            this._coNotAllotAmount.Visible = true;
            this._coNotAllotAmount.VisibleIndex = 7;
            // 
            // _coStockInfoID
            // 
            this._coStockInfoID.Caption = "StockInfoID";
            this._coStockInfoID.FieldName = "StockInfoID";
            this._coStockInfoID.Name = "_coStockInfoID";
            this._coStockInfoID.OptionsColumn.AllowEdit = false;
            // 
            // _coDemandID
            // 
            this._coDemandID.Caption = "DemandID";
            this._coDemandID.FieldName = "DemandID";
            this._coDemandID.Name = "_coDemandID";
            this._coDemandID.OptionsColumn.AllowEdit = false;
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
            // frStockLinLiao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 436);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frStockLinLiao";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "半成品外发加工领料";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem _barVerify;
        private LabAndData _ldDate;
        private LabAndLookupEdit _leCompany;
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
        private DevExpress.XtraBars.BarStaticItem _barLoan;
        private DevExpress.XtraBars.BarSubItem _barDel;
        private DevExpress.XtraBars.BarButtonItem _barDelTable;
        private DevExpress.XtraBars.BarButtonItem _barDelInfo;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit _reBEAmount;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _coMaterielID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorID;
        private DevExpress.XtraGrid.Columns.GridColumn _coAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coMeasureID;
        private DevExpress.XtraGrid.Columns.GridColumn _coRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coMainID;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
        private LabAndLookupEdit _leDepot;
        private DevExpress.XtraGrid.Columns.GridColumn _coSizeID;
        private DevExpress.XtraGrid.Columns.GridColumn _coDepotAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorOneID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorTwoID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMListID;
        private DevExpress.XtraGrid.Columns.GridColumn _coNeedAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coOutAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coNotAllotAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coStockInfoID;
        private DevExpress.XtraGrid.Columns.GridColumn _coDemandID;
        private LabAndText _laStockNum;
    }
}