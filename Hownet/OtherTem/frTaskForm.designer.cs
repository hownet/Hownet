using Hownet.BaseContranl;
namespace Hownet.OtherTem
{
    partial class frTaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frTaskForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this._brFrist = new DevExpress.XtraBars.BarButtonItem();
            this._brPrv = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brNext = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brLast = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brAddNew = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brDel = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brSave = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this._brFilter = new DevExpress.XtraBars.BarButtonItem();
            this._barVerify = new DevExpress.XtraBars.BarButtonItem();
            this._barUnVerify = new DevExpress.XtraBars.BarButtonItem();
            this._brExit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this._leBrand = new Hownet.BaseContranl.LabAndLookupEdit();
            this._leMateriel = new Hownet.BaseContranl.LabAndLookupEdit();
            this._leCompany = new Hownet.BaseContranl.LabAndLookupEdit();
            this.amountList1 = new Hownet.BaseContranl.AmountList();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.userNum1 = new Hownet.BaseContranl.UserNum();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
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
            this._brDel,
            this._brFrist,
            this._brPrv,
            this._brNext,
            this._brLast,
            this._brFilter,
            this._brExit,
            this.barSubItem1,
            this._barUnVerify,
            this._barVerify,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3});
            this.barManager1.MaxItemId = 49;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brAddNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brDel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brFilter, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barVerify, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this._barUnVerify, "弃审", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            // _brDel
            // 
            this._brDel.Caption = "删除";
            this._brDel.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this._brDel.Glyph = ((System.Drawing.Image)(resources.GetObject("_brDel.Glyph")));
            this._brDel.Hint = "Ctrl+Delete";
            this._brDel.Id = 2;
            this._brDel.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete));
            this._brDel.Name = "_brDel";
            this._brDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brDel_ItemClick);
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
            // barSubItem1
            // 
            this.barSubItem1.Caption = "打印";
            this.barSubItem1.Glyph = global::Hownet.Properties.Resources.PrintDialogAccess;
            this.barSubItem1.Id = 24;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, false, this.barButtonItem3, false)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "标签";
            this.barButtonItem1.Id = 45;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick_1);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "标签（设计）";
            this.barButtonItem2.Id = 47;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "生产单（流式）";
            this.barButtonItem3.Id = 48;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
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
            // _barUnVerify
            // 
            this._barUnVerify.Caption = "任务";
            this._barUnVerify.Glyph = global::Hownet.Properties.Resources.FieldsUpdate;
            this._barUnVerify.Id = 26;
            this._barUnVerify.Name = "_barUnVerify";
            this._barUnVerify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barUnVerify_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(959, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 604);
            this.barDockControlBottom.Size = new System.Drawing.Size(959, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 573);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(959, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 573);
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
            // _leBrand
            // 
            this._leBrand.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._leBrand.editVal = 0;
            this._leBrand.FormName = 0;
            this._leBrand.IsMust = true;
            this._leBrand.IsNotCanEdit = false;
            this._leBrand.labText = "商标：";
            this._leBrand.lenth = new int[] {
        50,
        120};
            this._leBrand.Location = new System.Drawing.Point(469, 62);
            this._leBrand.Name = "_leBrand";
            this._leBrand.Par = null;
            this._leBrand.Size = new System.Drawing.Size(180, 22);
            this._leBrand.TabIndex = 22;
            // 
            // _leMateriel
            // 
            this._leMateriel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._leMateriel.editVal = 0;
            this._leMateriel.FormName = 0;
            this._leMateriel.IsMust = true;
            this._leMateriel.IsNotCanEdit = false;
            this._leMateriel.labText = "款号：";
            this._leMateriel.lenth = new int[] {
        50,
        120};
            this._leMateriel.Location = new System.Drawing.Point(254, 62);
            this._leMateriel.Name = "_leMateriel";
            this._leMateriel.Par = null;
            this._leMateriel.Size = new System.Drawing.Size(180, 22);
            this._leMateriel.TabIndex = 21;
            this._leMateriel.EditValueChanged += new Hownet.BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._leMateriel_EditValueChanged_1);
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
            this._leCompany.Location = new System.Drawing.Point(36, 62);
            this._leCompany.Name = "_leCompany";
            this._leCompany.Par = null;
            this._leCompany.Size = new System.Drawing.Size(180, 22);
            this._leCompany.TabIndex = 20;
            this._leCompany.Visible = false;
            // 
            // amountList1
            // 
            this.amountList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.amountList1.IsCanEdit = false;
            this.amountList1.IsCanEditCS = true;
            this.amountList1.IsEdit = false;
            this.amountList1.Location = new System.Drawing.Point(0, 119);
            this.amountList1.MaterielID = 0;
            this.amountList1.Name = "amountList1";
            this.amountList1.Size = new System.Drawing.Size(959, 485);
            this.amountList1.TabIndex = 0;
            this.amountList1.EditValueChanged += new Hownet.BaseContranl.AmountList.EditValueChangedHandler(this.amountList1_EditValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lookUpEdit1);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.userNum1);
            this.panel1.Controls.Add(this._leBrand);
            this.panel1.Controls.Add(this._leMateriel);
            this.panel1.Controls.Add(this._leCompany);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(959, 88);
            this.panel1.TabIndex = 6;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lookUpEdit1.Location = new System.Drawing.Point(735, 61);
            this.lookUpEdit1.MenuManager = this.barManager1;
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", "Com", null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", "New", null, true)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "仓库")});
            this.lookUpEdit1.Properties.DisplayMember = "Name";
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.ValueMember = "ID";
            this.lookUpEdit1.Size = new System.Drawing.Size(143, 20);
            this.lookUpEdit1.TabIndex = 28;
            this.lookUpEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lookUpEdit1_ButtonClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl1.Location = new System.Drawing.Point(677, 62);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 27;
            this.labelControl1.Text = "存放仓库：";
            // 
            // userNum1
            // 
            this.userNum1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userNum1.IsCanEdit = false;
            this.userNum1.JinDu = "";
            this.userNum1.LastEdit = "";
            this.userNum1.Location = new System.Drawing.Point(0, 1);
            this.userNum1.Name = "userNum1";
            this.userNum1.Num = 1;
            this.userNum1.NumDate = new System.DateTime(2010, 10, 17, 0, 0, 0, 0);
            this.userNum1.NumStr = "20101017-120101017-120101017-120101017-120101017-120101017-120101017-120101017-12" +
    "0101017-120101017-120101017-120101017-1";
            this.userNum1.Size = new System.Drawing.Size(356, 49);
            this.userNum1.TabIndex = 26;
            this.userNum1.VerifyUser = "";
            // 
            // frTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 604);
            this.Controls.Add(this.amountList1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frTaskForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "收货";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarLargeButtonItem _brAddNew;
        private DevExpress.XtraBars.BarLargeButtonItem _brSave;
        private DevExpress.XtraBars.BarLargeButtonItem _brDel;
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
        private DevExpress.XtraBars.BarButtonItem _barUnVerify;
        private DevExpress.XtraBars.BarButtonItem _barVerify;
        private LabAndLookupEdit _leBrand;
        private LabAndLookupEdit _leMateriel;
        private LabAndLookupEdit _leCompany;
        private AmountList amountList1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.Panel panel1;
        private UserNum userNum1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}