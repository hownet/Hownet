namespace Hownet.BaseForm
{
    partial class BarEmployeeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarEmployeeForm));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this._brFrist = new DevExpress.XtraBars.BarButtonItem();
            this._brPrv = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brNext = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brLast = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brAddNew = new DevExpress.XtraBars.BarLargeButtonItem();
            this._barView = new DevExpress.XtraBars.BarButtonItem();
            this._brEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this._brPrint = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brExit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this._brDel = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brVerify = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brUnVerify = new DevExpress.XtraBars.BarLargeButtonItem();
            this._brFilter = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.label1 = new System.Windows.Forms.Label();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coSn = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coEmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDepartmentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coSex = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coWorkTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coIdentityCard = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDefaultWT = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDeposit = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNeedDeposit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
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
            this._brDel,
            this._brPrint,
            this._brFrist,
            this._brPrv,
            this._brNext,
            this._brLast,
            this._brVerify,
            this._brUnVerify,
            this._brFilter,
            this._brExit,
            this._brEdit,
            this.barButtonItem1,
            this.barButtonItem2,
            this._barView});
            this.barManager1.MaxItemId = 19;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barView, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brExit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            // _barView
            // 
            this._barView.Caption = "查看";
            this._barView.Glyph = ((System.Drawing.Image)(resources.GetObject("_barView.Glyph")));
            this._barView.Id = 18;
            this._barView.Name = "_barView";
            this._barView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barView_ItemClick);
            // 
            // _brEdit
            // 
            this._brEdit.Caption = "编辑";
            this._brEdit.Glyph = ((System.Drawing.Image)(resources.GetObject("_brEdit.Glyph")));
            this._brEdit.Hint = "F2";
            this._brEdit.Id = 15;
            this._brEdit.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2);
            this._brEdit.Name = "_brEdit";
            this._brEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brEdit_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "导出Excel";
            this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
            this.barButtonItem1.Hint = "Ctrl+E";
            this.barButtonItem1.Id = 16;
            this.barButtonItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick_1);
            // 
            // _brPrint
            // 
            this._brPrint.Caption = "打印";
            this._brPrint.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this._brPrint.Glyph = ((System.Drawing.Image)(resources.GetObject("_brPrint.Glyph")));
            this._brPrint.Hint = "Ctrl+P";
            this._brPrint.Id = 3;
            this._brPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P));
            this._brPrint.Name = "_brPrint";
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
            this.barDockControlTop.Size = new System.Drawing.Size(837, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 702);
            this.barDockControlBottom.Size = new System.Drawing.Size(837, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 671);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(837, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 671);
            // 
            // _brDel
            // 
            this._brDel.Caption = "删除";
            this._brDel.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this._brDel.Glyph = ((System.Drawing.Image)(resources.GetObject("_brDel.Glyph")));
            this._brDel.Id = 2;
            this._brDel.Name = "_brDel";
            // 
            // _brVerify
            // 
            this._brVerify.Caption = "审核";
            this._brVerify.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this._brVerify.Glyph = ((System.Drawing.Image)(resources.GetObject("_brVerify.Glyph")));
            this._brVerify.Id = 11;
            this._brVerify.Name = "_brVerify";
            // 
            // _brUnVerify
            // 
            this._brUnVerify.Caption = "弃审";
            this._brUnVerify.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this._brUnVerify.Glyph = ((System.Drawing.Image)(resources.GetObject("_brUnVerify.Glyph")));
            this._brUnVerify.Id = 12;
            this._brUnVerify.Name = "_brUnVerify";
            // 
            // _brFilter
            // 
            this._brFilter.Caption = "筛选";
            this._brFilter.Glyph = ((System.Drawing.Image)(resources.GetObject("_brFilter.Glyph")));
            this._brFilter.Id = 13;
            this._brFilter.Name = "_brFilter";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "员工发卡";
            this.barButtonItem2.Id = 17;
            this.barButtonItem2.Name = "barButtonItem2";
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
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 31);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.radioGroup1);
            this.splitContainerControl1.Panel1.Controls.Add(this.label1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl3);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(837, 671);
            this.splitContainerControl1.SplitterPosition = 35;
            this.splitContainerControl1.TabIndex = 5;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Dock = System.Windows.Forms.DockStyle.Left;
            this.radioGroup1.Location = new System.Drawing.Point(0, 0);
            this.radioGroup1.MenuManager = this.barManager1;
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "在职"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "离职"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "所有")});
            this.radioGroup1.Size = new System.Drawing.Size(186, 35);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(369, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "员工列表";
            // 
            // gridControl3
            // 
            this.gridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl3.Location = new System.Drawing.Point(0, 0);
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.Size = new System.Drawing.Size(837, 631);
            this.gridControl3.TabIndex = 0;
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coID,
            this._coSn,
            this._coEmployeeName,
            this._coDepartmentID,
            this._coSex,
            this._coWorkTypeID,
            this._coPhone,
            this._coIdentityCard,
            this.gridColumn10,
            this.gridColumn1,
            this._coDefaultWT,
            this._coDeposit,
            this._coNeedDeposit});
            this.gridView3.GridControl = this.gridControl3;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.Editable = false;
            this.gridView3.OptionsView.ShowAutoFilterRow = true;
            this.gridView3.OptionsView.ShowFooter = true;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // _coID
            // 
            this._coID.Caption = "EmployeeID";
            this._coID.FieldName = "ID";
            this._coID.Name = "_coID";
            // 
            // _coSn
            // 
            this._coSn.Caption = "编号";
            this._coSn.FieldName = "Sn";
            this._coSn.Name = "_coSn";
            this._coSn.OptionsFilter.AllowFilter = false;
            this._coSn.Visible = true;
            this._coSn.VisibleIndex = 0;
            // 
            // _coEmployeeName
            // 
            this._coEmployeeName.Caption = "姓名";
            this._coEmployeeName.FieldName = "Name";
            this._coEmployeeName.Name = "_coEmployeeName";
            this._coEmployeeName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this._coEmployeeName.Visible = true;
            this._coEmployeeName.VisibleIndex = 1;
            // 
            // _coDepartmentID
            // 
            this._coDepartmentID.Caption = "所属部门";
            this._coDepartmentID.FieldName = "DepartmentName";
            this._coDepartmentID.Name = "_coDepartmentID";
            this._coDepartmentID.Visible = true;
            this._coDepartmentID.VisibleIndex = 2;
            // 
            // _coSex
            // 
            this._coSex.Caption = "性别";
            this._coSex.FieldName = "Sex";
            this._coSex.Name = "_coSex";
            this._coSex.OptionsFilter.AllowFilter = false;
            this._coSex.Visible = true;
            this._coSex.VisibleIndex = 3;
            // 
            // _coWorkTypeID
            // 
            this._coWorkTypeID.Caption = "工种";
            this._coWorkTypeID.FieldName = "WorkTypeName";
            this._coWorkTypeID.Name = "_coWorkTypeID";
            this._coWorkTypeID.OptionsFilter.AllowFilter = false;
            this._coWorkTypeID.Visible = true;
            this._coWorkTypeID.VisibleIndex = 4;
            // 
            // _coPhone
            // 
            this._coPhone.Caption = "电话";
            this._coPhone.FieldName = "Phone";
            this._coPhone.Name = "_coPhone";
            this._coPhone.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this._coPhone.OptionsFilter.AllowFilter = false;
            this._coPhone.Visible = true;
            this._coPhone.VisibleIndex = 5;
            // 
            // _coIdentityCard
            // 
            this._coIdentityCard.Caption = "身份证号码";
            this._coIdentityCard.FieldName = "IdentityCard";
            this._coIdentityCard.Name = "_coIdentityCard";
            this._coIdentityCard.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this._coIdentityCard.OptionsFilter.AllowFilter = false;
            this._coIdentityCard.Visible = true;
            this._coIdentityCard.VisibleIndex = 6;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "到职日期";
            this.gridColumn10.FieldName = "AccDate";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsFilter.AllowFilter = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 7;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "PY";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // _coDefaultWT
            // 
            this._coDefaultWT.Caption = "默认工种";
            this._coDefaultWT.FieldName = "DefaultWT";
            this._coDefaultWT.Name = "_coDefaultWT";
            this._coDefaultWT.Visible = true;
            this._coDefaultWT.VisibleIndex = 8;
            // 
            // _coDeposit
            // 
            this._coDeposit.Caption = "在厂押金";
            this._coDeposit.FieldName = "Deposit";
            this._coDeposit.Name = "_coDeposit";
            this._coDeposit.Visible = true;
            this._coDeposit.VisibleIndex = 9;
            // 
            // _coNeedDeposit
            // 
            this._coNeedDeposit.Caption = "需扣押金";
            this._coNeedDeposit.FieldName = "NeedDeposit";
            this._coNeedDeposit.Name = "_coNeedDeposit";
            this._coNeedDeposit.Visible = true;
            this._coNeedDeposit.VisibleIndex = 10;
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // BarEmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 702);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BarEmployeeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "员工列表";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarLargeButtonItem _brAddNew;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLargeButtonItem _brDel;
        private DevExpress.XtraBars.BarLargeButtonItem _brPrint;
        private DevExpress.XtraBars.BarButtonItem _brFrist;
        private DevExpress.XtraBars.BarLargeButtonItem _brPrv;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraBars.BarLargeButtonItem _brNext;
        private DevExpress.XtraBars.BarLargeButtonItem _brLast;
        private DevExpress.XtraBars.BarLargeButtonItem _brVerify;
        private DevExpress.XtraBars.BarLargeButtonItem _brUnVerify;
        private DevExpress.XtraBars.BarButtonItem _brFilter;
        private DevExpress.XtraBars.BarButtonItem _brExit;
        private DevExpress.XtraBars.BarButtonItem _brEdit;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraGrid.GridControl gridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraGrid.Columns.GridColumn _coSn;
        private DevExpress.XtraGrid.Columns.GridColumn _coEmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn _coDepartmentID;
        private DevExpress.XtraGrid.Columns.GridColumn _coSex;
        private DevExpress.XtraGrid.Columns.GridColumn _coWorkTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn _coPhone;
        private DevExpress.XtraGrid.Columns.GridColumn _coIdentityCard;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraBars.BarButtonItem _barView;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraGrid.Columns.GridColumn _coDefaultWT;
        private DevExpress.XtraGrid.Columns.GridColumn _coDeposit;
        private DevExpress.XtraGrid.Columns.GridColumn _coNeedDeposit;
    }
}