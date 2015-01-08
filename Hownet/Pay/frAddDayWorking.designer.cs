using Hownet.BaseContranl;
namespace Hownet.Pay
{
    partial class frAddDayWorking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frAddDayWorking));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this._ltRemark = new BaseContranl.LabAndText();
            this._leMiniEmp = new BaseContranl.LabAndLookupEdit();
            this._ltNum = new BaseContranl.LabAndText();
            this._ldDate = new BaseContranl.LabAndData();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coInfoID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMainID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coWorkID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reWorkID = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this._coAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reMoney = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this._coMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coWorkingID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coPayInfoID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this._brFrist = new DevExpress.XtraBars.BarButtonItem();
            this._brPrv = new DevExpress.XtraBars.BarButtonItem();
            this._brNext = new DevExpress.XtraBars.BarButtonItem();
            this._brLast = new DevExpress.XtraBars.BarButtonItem();
            this._brAddNew = new DevExpress.XtraBars.BarButtonItem();
            this._brEdit = new DevExpress.XtraBars.BarButtonItem();
            this._brDel = new DevExpress.XtraBars.BarSubItem();
            this._barDelInfo = new DevExpress.XtraBars.BarButtonItem();
            this._barDelTable = new DevExpress.XtraBars.BarButtonItem();
            this._brSave = new DevExpress.XtraBars.BarButtonItem();
            this._brVerify = new DevExpress.XtraBars.BarButtonItem();
            this._brUnVerify = new DevExpress.XtraBars.BarButtonItem();
            this._brPrint = new DevExpress.XtraBars.BarButtonItem();
            this._brExit = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this._coRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reWorkID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reMoney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "AdpDiagramAddTable.png");
            this.imageList1.Images.SetKeyName(1, "AssignTask.png");
            this.imageList1.Images.SetKeyName(2, "DataContainer_MoveFirstHS.png");
            this.imageList1.Images.SetKeyName(3, "DataContainer_MoveLastHS.png");
            this.imageList1.Images.SetKeyName(4, "DataContainer_MoveNextHS.png");
            this.imageList1.Images.SetKeyName(5, "DataContainer_MovePreviousHS.png");
            this.imageList1.Images.SetKeyName(6, "DataContainer_NewRecordHS.png");
            this.imageList1.Images.SetKeyName(7, "Delete.png");
            this.imageList1.Images.SetKeyName(8, "EditComposePage.png");
            this.imageList1.Images.SetKeyName(9, "EditQuery.png");
            this.imageList1.Images.SetKeyName(10, "EditTableHS.png");
            this.imageList1.Images.SetKeyName(11, "EditWorkflowTask.png");
            this.imageList1.Images.SetKeyName(12, "ExportExcel.png");
            this.imageList1.Images.SetKeyName(13, "FieldsUpdate.png");
            this.imageList1.Images.SetKeyName(14, "Filter.png");
            this.imageList1.Images.SetKeyName(15, "FunctionHS.png");
            this.imageList1.Images.SetKeyName(16, "GroupInkClose.png");
            this.imageList1.Images.SetKeyName(17, "GroupMacroRows.png");
            this.imageList1.Images.SetKeyName(18, "PrintDialogAccess.png");
            this.imageList1.Images.SetKeyName(19, "SaveAndClose.png");
            this.imageList1.Images.SetKeyName(20, "TableOfContentsRemove.png");
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 31);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this._ltRemark);
            this.splitContainerControl1.Panel1.Controls.Add(this._leMiniEmp);
            this.splitContainerControl1.Panel1.Controls.Add(this._ltNum);
            this.splitContainerControl1.Panel1.Controls.Add(this._ldDate);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(873, 475);
            this.splitContainerControl1.SplitterPosition = 67;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // _ltRemark
            // 
            this._ltRemark.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._ltRemark.EditVal = "";
            this._ltRemark.IsCanEdit = true;
            this._ltRemark.IsHand = false;
            this._ltRemark.IsMust = false;
            this._ltRemark.labText = "说明：";
            this._ltRemark.lenth = new int[] {
        45,
        747};
            this._ltRemark.Location = new System.Drawing.Point(220, 39);
            this._ltRemark.Margin = new System.Windows.Forms.Padding(0);
            this._ltRemark.Mask = "";
            this._ltRemark.Name = "_ltRemark";
            this._ltRemark.Size = new System.Drawing.Size(644, 22);
            this._ltRemark.TabIndex = 32;
            this._ltRemark.val = "";
            // 
            // _leMiniEmp
            // 
            this._leMiniEmp.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._leMiniEmp.editVal = 0;
            this._leMiniEmp.IsNotCanEdit = false;
            this._leMiniEmp.IsMust = false;
            this._leMiniEmp.labText = "员工：";
            this._leMiniEmp.lenth = new int[] {
        70,
        110};
            this._leMiniEmp.Location = new System.Drawing.Point(239, 14);
            this._leMiniEmp.Name = "_leMiniEmp";
            this._leMiniEmp.Par = null;
            this._leMiniEmp.Size = new System.Drawing.Size(190, 22);
            this._leMiniEmp.IsNotCanEdit = false;
            this._leMiniEmp.TabIndex = 31;
            this._leMiniEmp.Visible = false;
            // 
            // _ltNum
            // 
            this._ltNum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._ltNum.EditVal = "";
            this._ltNum.IsCanEdit = true;
            this._ltNum.IsHand = false;
            this._ltNum.IsMust = false;
            this._ltNum.labText = "编号：";
            this._ltNum.lenth = new int[] {
        50,
        120};
            this._ltNum.Location = new System.Drawing.Point(33, 39);
            this._ltNum.Margin = new System.Windows.Forms.Padding(0);
            this._ltNum.Mask = "";
            this._ltNum.Name = "_ltNum";
            this._ltNum.Size = new System.Drawing.Size(170, 22);
            this._ltNum.TabIndex = 30;
            this._ltNum.val = "";
            // 
            // _ldDate
            // 
            this._ldDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._ldDate.IsMust = false;
            this._ldDate.IsShowClear = false;
            this._ldDate.labText = "日期：";
            this._ldDate.lenth = new int[] {
        45,
        110};
            this._ldDate.Location = new System.Drawing.Point(610, 14);
            this._ldDate.Margin = new System.Windows.Forms.Padding(0);
            this._ldDate.MaxDate = new System.DateTime(((long)(0)));
            this._ldDate.MinDate = new System.DateTime(((long)(0)));
            this._ldDate.Name = "_ldDate";
            this._ldDate.Size = new System.Drawing.Size(165, 22);
            this._ldDate.strLab = "";
//            this._ldDate.t = false;
            this._ldDate.TabIndex = 29;
            this._ldDate.val = null;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._reMoney,
            this._reWorkID});
            this.gridControl1.Size = new System.Drawing.Size(873, 403);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coInfoID,
            this._coMainID,
            this._coWorkID,
            this._coAmount,
            this._coPrice,
            this._coMoney,
            this._coA,
            this._coWorkingID,
            this._coPayInfoID,
            this._coEmployeeID,
            this._coRemark});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // _coInfoID
            // 
            this._coInfoID.Caption = "InfoID";
            this._coInfoID.FieldName = "InfoID";
            this._coInfoID.Name = "_coInfoID";
            // 
            // _coMainID
            // 
            this._coMainID.Caption = "MainID";
            this._coMainID.FieldName = "MainID";
            this._coMainID.Name = "_coMainID";
            // 
            // _coWorkID
            // 
            this._coWorkID.Caption = "工序";
            this._coWorkID.ColumnEdit = this._reWorkID;
            this._coWorkID.FieldName = "WorkingID";
            this._coWorkID.Name = "_coWorkID";
            this._coWorkID.Visible = true;
            this._coWorkID.VisibleIndex = 1;
            this._coWorkID.Width = 146;
            // 
            // _reWorkID
            // 
            this._reWorkID.AutoHeight = false;
            this._reWorkID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._reWorkID.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "工序名"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Price", "工价"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID")});
            this._reWorkID.DisplayMember = "Name";
            this._reWorkID.Name = "_reWorkID";
            this._reWorkID.NullText = "";
            this._reWorkID.ValueMember = "ID";
            // 
            // _coAmount
            // 
            this._coAmount.Caption = "数量";
            this._coAmount.FieldName = "Amount";
            this._coAmount.Name = "_coAmount";
            this._coAmount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this._coAmount.Visible = true;
            this._coAmount.VisibleIndex = 2;
            this._coAmount.Width = 129;
            // 
            // _coPrice
            // 
            this._coPrice.Caption = "工价";
            this._coPrice.ColumnEdit = this._reMoney;
            this._coPrice.FieldName = "Price";
            this._coPrice.Name = "_coPrice";
            this._coPrice.Visible = true;
            this._coPrice.VisibleIndex = 3;
            this._coPrice.Width = 73;
            // 
            // _reMoney
            // 
            this._reMoney.AutoHeight = false;
            this._reMoney.DisplayFormat.FormatString = "C4";
            this._reMoney.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this._reMoney.Name = "_reMoney";
            // 
            // _coMoney
            // 
            this._coMoney.Caption = "金额";
            this._coMoney.ColumnEdit = this._reMoney;
            this._coMoney.FieldName = "Money";
            this._coMoney.Name = "_coMoney";
            this._coMoney.OptionsColumn.AllowEdit = false;
            this._coMoney.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this._coMoney.Visible = true;
            this._coMoney.VisibleIndex = 4;
            this._coMoney.Width = 118;
            // 
            // _coA
            // 
            this._coA.Caption = "A";
            this._coA.FieldName = "A";
            this._coA.Name = "_coA";
            // 
            // _coWorkingID
            // 
            this._coWorkingID.Caption = "工序";
            this._coWorkingID.FieldName = "WorkingID";
            this._coWorkingID.Name = "_coWorkingID";
            // 
            // _coPayInfoID
            // 
            this._coPayInfoID.Caption = "PayInfoID";
            this._coPayInfoID.FieldName = "PayInfoID";
            this._coPayInfoID.Name = "_coPayInfoID";
            // 
            // _coEmployeeID
            // 
            this._coEmployeeID.Caption = "员工";
            this._coEmployeeID.FieldName = "EmployeeID";
            this._coEmployeeID.Name = "_coEmployeeID";
            this._coEmployeeID.Visible = true;
            this._coEmployeeID.VisibleIndex = 0;
            this._coEmployeeID.Width = 165;
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
            this._brFrist,
            this._brPrv,
            this._brNext,
            this._brLast,
            this._brAddNew,
            this._brEdit,
            this._brSave,
            this._brVerify,
            this._brUnVerify,
            this._brPrint,
            this._brExit,
            this._brDel,
            this._barDelInfo,
            this._barDelTable});
            this.barManager1.MaxItemId = 15;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brFrist, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brPrv, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brNext, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brLast, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brAddNew, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brDel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brVerify, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brUnVerify, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._brExit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // _brFrist
            // 
            this._brFrist.Caption = "首单";
            this._brFrist.Id = 0;
            this._brFrist.ImageIndex = 2;
            this._brFrist.Name = "_brFrist";
            this._brFrist.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // _brPrv
            // 
            this._brPrv.Caption = "前单";
            this._brPrv.Id = 1;
            this._brPrv.ImageIndex = 5;
            this._brPrv.Name = "_brPrv";
            this._brPrv.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItem5_ItemClick);
            // 
            // _brNext
            // 
            this._brNext.Caption = "下单";
            this._brNext.Id = 2;
            this._brNext.ImageIndex = 4;
            this._brNext.Name = "_brNext";
            this._brNext.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brNext_ItemClick);
            // 
            // _brLast
            // 
            this._brLast.Caption = "尾单";
            this._brLast.Id = 3;
            this._brLast.ImageIndex = 3;
            this._brLast.Name = "_brLast";
            this._brLast.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brLast_ItemClick);
            // 
            // _brAddNew
            // 
            this._brAddNew.Caption = "新建";
            this._brAddNew.Id = 4;
            this._brAddNew.ImageIndex = 0;
            this._brAddNew.Name = "_brAddNew";
            this._brAddNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brAddNew_ItemClick);
            // 
            // _brEdit
            // 
            this._brEdit.Caption = "编辑";
            this._brEdit.Id = 5;
            this._brEdit.ImageIndex = 10;
            this._brEdit.Name = "_brEdit";
            this._brEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem6_ItemClick);
            // 
            // _brDel
            // 
            this._brDel.Caption = "删除";
            this._brDel.Id = 12;
            this._brDel.ImageIndex = 7;
            this._brDel.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this._barDelInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this._barDelTable)});
            this._brDel.Name = "_brDel";
            // 
            // _barDelInfo
            // 
            this._barDelInfo.Caption = "明细";
            this._barDelInfo.Id = 13;
            this._barDelInfo.Name = "_barDelInfo";
            this._barDelInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barDelInfo_ItemClick);
            // 
            // _barDelTable
            // 
            this._barDelTable.Caption = "单据";
            this._barDelTable.Id = 14;
            this._barDelTable.Name = "_barDelTable";
            this._barDelTable.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelTable_ItemClick);
            // 
            // _brSave
            // 
            this._brSave.Caption = "保存";
            this._brSave.Id = 7;
            this._brSave.ImageIndex = 19;
            this._brSave.Name = "_brSave";
            this._brSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brSave_ItemClick);
            // 
            // _brVerify
            // 
            this._brVerify.Caption = "审核";
            this._brVerify.Id = 8;
            this._brVerify.ImageIndex = 11;
            this._brVerify.Name = "_brVerify";
            this._brVerify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brVerify_ItemClick);
            // 
            // _brUnVerify
            // 
            this._brUnVerify.Caption = "弃审";
            this._brUnVerify.Id = 9;
            this._brUnVerify.ImageIndex = 13;
            this._brUnVerify.Name = "_brUnVerify";
            this._brUnVerify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brUnVerify_ItemClick);
            // 
            // _brPrint
            // 
            this._brPrint.Caption = "打印";
            this._brPrint.Id = 10;
            this._brPrint.ImageIndex = 18;
            this._brPrint.Name = "_brPrint";
            this._brPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brPrint_ItemClick);
            // 
            // _brExit
            // 
            this._brExit.Caption = "退出";
            this._brExit.Id = 11;
            this._brExit.ImageIndex = 16;
            this._brExit.Name = "_brExit";
            this._brExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._brExit_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(873, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 506);
            this.barDockControlBottom.Size = new System.Drawing.Size(873, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 475);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(873, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 475);
            // 
            // _coRemark
            // 
            this._coRemark.Caption = "备注";
            this._coRemark.FieldName = "Remark";
            this._coRemark.Name = "_coRemark";
            this._coRemark.Visible = true;
            this._coRemark.VisibleIndex = 5;
            this._coRemark.Width = 226;
            // 
            // frAddDayWorking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 529);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frAddDayWorking";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "计时工资录入";
            this.Load += new System.EventHandler(this.Demo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reWorkID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reMoney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _coInfoID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMainID;
        private DevExpress.XtraGrid.Columns.GridColumn _coWorkingID;
        private DevExpress.XtraGrid.Columns.GridColumn _coAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coPrice;
        private DevExpress.XtraGrid.Columns.GridColumn _coMoney;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit _reMoney;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reWorkID;
        private DevExpress.XtraGrid.Columns.GridColumn _coWorkID;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem _brFrist;
        private DevExpress.XtraBars.BarButtonItem _brPrv;
        private DevExpress.XtraBars.BarButtonItem _brNext;
        private DevExpress.XtraBars.BarButtonItem _brLast;
        private DevExpress.XtraBars.BarButtonItem _brAddNew;
        private DevExpress.XtraBars.BarButtonItem _brSave;
        private DevExpress.XtraBars.BarButtonItem _brVerify;
        private DevExpress.XtraBars.BarButtonItem _brUnVerify;
        private DevExpress.XtraBars.BarButtonItem _brPrint;
        private DevExpress.XtraBars.BarButtonItem _brExit;
        public DevExpress.XtraBars.BarButtonItem _brEdit;
        private DevExpress.XtraBars.BarSubItem _brDel;
        private DevExpress.XtraBars.BarButtonItem _barDelInfo;
        private DevExpress.XtraBars.BarButtonItem _barDelTable;
        private DevExpress.XtraGrid.Columns.GridColumn _coPayInfoID;
        private LabAndText _ltNum;
        private LabAndData _ldDate;
        private LabAndLookupEdit _leMiniEmp;
        private LabAndText _ltRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn _coRemark;

    }
}