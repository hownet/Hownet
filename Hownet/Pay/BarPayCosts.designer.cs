using Hownet.BaseContranl;
namespace Hownet.Pay
{
    partial class BarPayCosts
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
            this._barAddTable = new DevExpress.XtraBars.BarButtonItem();
            this._ltRemark = new BaseContranl.LabAndText();
            this._leType = new BaseContranl.LabAndLookupEdit();
            this._ltNum = new BaseContranl.LabAndText();
            this._ldDate = new BaseContranl.LabAndData();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMainID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this._barFrist = new DevExpress.XtraBars.BarButtonItem();
            this._barPve = new DevExpress.XtraBars.BarButtonItem();
            this._barNext = new DevExpress.XtraBars.BarButtonItem();
            this._barLast = new DevExpress.XtraBars.BarButtonItem();
            this._barNew = new DevExpress.XtraBars.BarSubItem();
            this._barAddInfo = new DevExpress.XtraBars.BarButtonItem();
            this._barEdit = new DevExpress.XtraBars.BarButtonItem();
            this._barDel = new DevExpress.XtraBars.BarSubItem();
            this._barDelInfo = new DevExpress.XtraBars.BarButtonItem();
            this._barDelTable = new DevExpress.XtraBars.BarButtonItem();
            this._barSave = new DevExpress.XtraBars.BarButtonItem();
            this._barPrint = new DevExpress.XtraBars.BarButtonItem();
            this._barVerify = new DevExpress.XtraBars.BarButtonItem();
            this._barUnVerify = new DevExpress.XtraBars.BarButtonItem();
            this._barExit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _barAddTable
            // 
            this._barAddTable.Caption = "单据";
            this._barAddTable.Glyph = global::Hownet.Properties.Resources.AdpDiagramAddTable;
            this._barAddTable.Id = 6;
            this._barAddTable.Name = "_barAddTable";
            this._barAddTable.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barAddTable_ItemClick);
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
            this._ltRemark.Location = new System.Drawing.Point(22, 66);
            this._ltRemark.Margin = new System.Windows.Forms.Padding(0);
            this._ltRemark.Mask = "";
            this._ltRemark.Name = "_ltRemark";
            this._ltRemark.Size = new System.Drawing.Size(644, 22);
            this._ltRemark.TabIndex = 30;
            this._ltRemark.val = "";
            // 
            // _leType
            // 
            this._leType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._leType.editVal = 0;
            this._leType.IsNotCanEdit = false;
            this._leType.IsMust = false;
            this._leType.labText = "费用类型：";
            this._leType.lenth = new int[] {
        70,
        110};
            this._leType.Location = new System.Drawing.Point(190, 40);
            this._leType.Name = "_leType";
            this._leType.Par = null;
            this._leType.Size = new System.Drawing.Size(190, 22);
            this._leType.IsNotCanEdit = false;
            this._leType.TabIndex = 29;
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
            this._ltNum.Location = new System.Drawing.Point(17, 15);
            this._ltNum.Margin = new System.Windows.Forms.Padding(0);
            this._ltNum.Mask = "";
            this._ltNum.Name = "_ltNum";
            this._ltNum.Size = new System.Drawing.Size(170, 22);
            this._ltNum.TabIndex = 28;
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
            this._ldDate.Location = new System.Drawing.Point(11, 41);
            this._ldDate.Margin = new System.Windows.Forms.Padding(0);
            this._ldDate.MaxDate = new System.DateTime(((long)(0)));
            this._ldDate.MinDate = new System.DateTime(((long)(0)));
            this._ldDate.Name = "_ldDate";
            this._ldDate.Size = new System.Drawing.Size(165, 22);
            this._ldDate.strLab = "";
            this._ldDate.TabIndex = 27;
            this._ldDate.val = null;
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.textEdit1);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Location = new System.Drawing.Point(410, 38);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(200, 25);
            this.panelControl1.TabIndex = 26;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(155, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(40, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(48, 2);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(100, 21);
            this.textEdit1.TabIndex = 1;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(5, 5);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(36, 14);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "批量：";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 131);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(675, 452);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coEmployeeID,
            this._coMoney,
            this._coRemark,
            this._coID,
            this._coA,
            this._coMainID});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            this.gridView1.RowCountChanged += new System.EventHandler(this.gridView1_RowCountChanged);
            // 
            // _coEmployeeID
            // 
            this._coEmployeeID.Caption = "员工";
            this._coEmployeeID.FieldName = "EmployeeID";
            this._coEmployeeID.Name = "_coEmployeeID";
            this._coEmployeeID.Visible = true;
            this._coEmployeeID.VisibleIndex = 0;
            // 
            // _coMoney
            // 
            this._coMoney.Caption = "金额";
            this._coMoney.FieldName = "Money";
            this._coMoney.Name = "_coMoney";
            this._coMoney.Visible = true;
            this._coMoney.VisibleIndex = 1;
            // 
            // _coRemark
            // 
            this._coRemark.Caption = "说明";
            this._coRemark.FieldName = "Remark";
            this._coRemark.Name = "_coRemark";
            this._coRemark.Visible = true;
            this._coRemark.VisibleIndex = 2;
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
            // _coMainID
            // 
            this._coMainID.Caption = "MainID";
            this._coMainID.FieldName = "MainID";
            this._coMainID.Name = "_coMainID";
            this._coMainID.OptionsColumn.AllowEdit = false;
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
            this._barFrist,
            this._barPve,
            this._barNext,
            this._barLast,
            this._barNew,
            this._barAddInfo,
            this._barAddTable,
            this._barEdit,
            this._barDel,
            this._barDelInfo,
            this._barDelTable,
            this._barSave,
            this._barPrint,
            this._barVerify,
            this._barUnVerify,
            this._barExit});
            this.barManager1.MaxItemId = 19;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barFrist, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barPve, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barNext, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barLast, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barNew, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barDel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            // _barFrist
            // 
            this._barFrist.Caption = "首单";
            this._barFrist.Glyph = global::Hownet.Properties.Resources.DataContainer_MoveFirstHS;
            this._barFrist.Id = 0;
            this._barFrist.Name = "_barFrist";
            this._barFrist.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barFrist_ItemClick);
            // 
            // _barPve
            // 
            this._barPve.Caption = "上单";
            this._barPve.Glyph = global::Hownet.Properties.Resources.DataContainer_MovePreviousHS;
            this._barPve.Id = 1;
            this._barPve.Name = "_barPve";
            this._barPve.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barPve_ItemClick);
            // 
            // _barNext
            // 
            this._barNext.Caption = "下单";
            this._barNext.Glyph = global::Hownet.Properties.Resources.DataContainer_MoveNextHS;
            this._barNext.Id = 2;
            this._barNext.Name = "_barNext";
            this._barNext.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barNext_ItemClick);
            // 
            // _barLast
            // 
            this._barLast.Caption = "尾单";
            this._barLast.Glyph = global::Hownet.Properties.Resources.DataContainer_MoveLastHS;
            this._barLast.Id = 3;
            this._barLast.Name = "_barLast";
            this._barLast.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barLast_ItemClick);
            // 
            // _barNew
            // 
            this._barNew.Caption = "新添";
            this._barNew.Glyph = global::Hownet.Properties.Resources.DataContainer_NewRecordHS;
            this._barNew.Id = 4;
            this._barNew.ImageIndex = 7;
            this._barNew.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this._barAddInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this._barAddTable)});
            this._barNew.Name = "_barNew";
            // 
            // _barAddInfo
            // 
            this._barAddInfo.Caption = "明细";
            this._barAddInfo.Glyph = global::Hownet.Properties.Resources.TableInsertCellsDialog;
            this._barAddInfo.Id = 5;
            this._barAddInfo.Name = "_barAddInfo";
            // 
            // _barEdit
            // 
            this._barEdit.Caption = "修改";
            this._barEdit.Glyph = global::Hownet.Properties.Resources.EditComposePage;
            this._barEdit.Id = 7;
            this._barEdit.Name = "_barEdit";
            // 
            // _barDel
            // 
            this._barDel.Caption = "删除";
            this._barDel.Glyph = global::Hownet.Properties.Resources.Delete;
            this._barDel.Id = 8;
            this._barDel.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this._barDelInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this._barDelTable)});
            this._barDel.Name = "_barDel";
            // 
            // _barDelInfo
            // 
            this._barDelInfo.Caption = "明细";
            this._barDelInfo.Glyph = global::Hownet.Properties.Resources.GroupMacroRows;
            this._barDelInfo.Id = 9;
            this._barDelInfo.Name = "_barDelInfo";
            this._barDelInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barDelInfo_ItemClick);
            // 
            // _barDelTable
            // 
            this._barDelTable.Caption = "单据";
            this._barDelTable.Glyph = global::Hownet.Properties.Resources.TableOfContentsRemove;
            this._barDelTable.Id = 10;
            this._barDelTable.Name = "_barDelTable";
            this._barDelTable.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barDelTable_ItemClick);
            // 
            // _barSave
            // 
            this._barSave.Caption = "保存";
            this._barSave.Glyph = global::Hownet.Properties.Resources.SaveAndClose;
            this._barSave.Id = 11;
            this._barSave.Name = "_barSave";
            this._barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barSave_ItemClick);
            // 
            // _barPrint
            // 
            this._barPrint.Caption = "打印";
            this._barPrint.Glyph = global::Hownet.Properties.Resources.PrintDialogAccess;
            this._barPrint.Id = 12;
            this._barPrint.Name = "_barPrint";
            this._barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barPrint_ItemClick);
            // 
            // _barVerify
            // 
            this._barVerify.Caption = "审核";
            this._barVerify.Glyph = global::Hownet.Properties.Resources.EditWorkflowTask;
            this._barVerify.Id = 13;
            this._barVerify.Name = "_barVerify";
            this._barVerify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barVerify_ItemClick);
            // 
            // _barUnVerify
            // 
            this._barUnVerify.Caption = "弃审";
            this._barUnVerify.Glyph = global::Hownet.Properties.Resources.FieldsUpdate;
            this._barUnVerify.Id = 14;
            this._barUnVerify.Name = "_barUnVerify";
            this._barUnVerify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barUnVerify_ItemClick);
            // 
            // _barExit
            // 
            this._barExit.Caption = "退出";
            this._barExit.Glyph = global::Hownet.Properties.Resources.GroupInkClose;
            this._barExit.Id = 15;
            this._barExit.Name = "_barExit";
            this._barExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barExit_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(675, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 583);
            this.barDockControlBottom.Size = new System.Drawing.Size(675, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 552);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(675, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 552);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.label1);
            this.panelControl2.Controls.Add(this._ltRemark);
            this.panelControl2.Controls.Add(this._leType);
            this.panelControl2.Controls.Add(this._ltNum);
            this.panelControl2.Controls.Add(this.panelControl1);
            this.panelControl2.Controls.Add(this._ldDate);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 31);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(675, 100);
            this.panelControl2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(295, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 27);
            this.label1.TabIndex = 31;
            this.label1.Text = "员工费用记录";
            // 
            // BarPayCosts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 583);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BarPayCosts";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "员工费用记录";
            this.Load += new System.EventHandler(this.StockBackForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem _barFrist;
        private DevExpress.XtraBars.BarButtonItem _barPve;
        private DevExpress.XtraBars.BarButtonItem _barNext;
        private DevExpress.XtraBars.BarButtonItem _barLast;
        private DevExpress.XtraBars.BarSubItem _barNew;
        private DevExpress.XtraBars.BarButtonItem _barAddInfo;
        private DevExpress.XtraBars.BarButtonItem _barEdit;
        private DevExpress.XtraBars.BarSubItem _barDel;
        private DevExpress.XtraBars.BarButtonItem _barDelInfo;
        private DevExpress.XtraBars.BarButtonItem _barDelTable;
        private DevExpress.XtraBars.BarButtonItem _barSave;
        private DevExpress.XtraBars.BarButtonItem _barPrint;
        private DevExpress.XtraBars.BarButtonItem _barVerify;
        private DevExpress.XtraBars.BarButtonItem _barUnVerify;
        private DevExpress.XtraBars.BarButtonItem _barExit;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _coEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMoney;
        private DevExpress.XtraGrid.Columns.GridColumn _coRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
        private DevExpress.XtraBars.BarButtonItem _barAddTable;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private LabAndData _ldDate;
        private LabAndText _ltNum;
        private LabAndLookupEdit _leType;
        private LabAndText _ltRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coMainID;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Label label1;
    }
}