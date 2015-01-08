namespace Hownet.Pay
{
    partial class BarTaskCar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarTaskCar));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coBoxNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coSizeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorOneID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorTwoID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coWorkTicketID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this._barEdit = new DevExpress.XtraBars.BarButtonItem();
            this._barPrint = new DevExpress.XtraBars.BarButtonItem();
            this._barExit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this._barAddInfo = new DevExpress.XtraBars.BarButtonItem();
            this._barDelInfo = new DevExpress.XtraBars.BarButtonItem();
            this._barDelTable = new DevExpress.XtraBars.BarButtonItem();
            this._barFill = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this._barLaVerify = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.label1 = new System.Windows.Forms.Label();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coOneAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            _barAddTable = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
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
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(634, 503);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coBoxNum,
            this._coColorID,
            this._coSizeID,
            this._coColorOneID,
            this._coColorTwoID,
            this._coAmount,
            this._coWorkTicketID});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsNavigation.AutoMoveRowFocus = false;
            this.gridView1.OptionsSelection.InvertSelection = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gridView1_FocusedColumnChanged);
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            this.gridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseUp);
            // 
            // _coBoxNum
            // 
            this._coBoxNum.Caption = "箱号";
            this._coBoxNum.FieldName = "BoxNum";
            this._coBoxNum.Name = "_coBoxNum";
            this._coBoxNum.OptionsColumn.AllowEdit = false;
            this._coBoxNum.Visible = true;
            this._coBoxNum.VisibleIndex = 0;
            // 
            // _coColorID
            // 
            this._coColorID.Caption = "颜色";
            this._coColorID.FieldName = "ColorID";
            this._coColorID.Name = "_coColorID";
            this._coColorID.OptionsColumn.AllowEdit = false;
            this._coColorID.Visible = true;
            this._coColorID.VisibleIndex = 1;
            // 
            // _coSizeID
            // 
            this._coSizeID.Caption = "尺码";
            this._coSizeID.FieldName = "SizeID";
            this._coSizeID.Name = "_coSizeID";
            this._coSizeID.OptionsColumn.AllowEdit = false;
            this._coSizeID.Visible = true;
            this._coSizeID.VisibleIndex = 2;
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
            // _coAmount
            // 
            this._coAmount.Caption = "数量";
            this._coAmount.FieldName = "Amount";
            this._coAmount.Name = "_coAmount";
            this._coAmount.OptionsColumn.AllowEdit = false;
            this._coAmount.Visible = true;
            this._coAmount.VisibleIndex = 5;
            // 
            // _coWorkTicketID
            // 
            this._coWorkTicketID.Caption = "WorkTicketID";
            this._coWorkTicketID.FieldName = "ID";
            this._coWorkTicketID.Name = "_coWorkTicketID";
            this._coWorkTicketID.OptionsColumn.AllowEdit = false;
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
            this.barManager1.Images = this.imageList1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this._barAddInfo,
            _barAddTable,
            this._barEdit,
            this._barDelInfo,
            this._barDelTable,
            this._barPrint,
            this._barExit,
            this._barFill,
            this.barStaticItem2,
            this._barLaVerify,
            this.barButtonItem1,
            this.barButtonItem2});
            this.barManager1.MaxItemId = 24;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barEdit, "", false, true, false, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barPrint, "", false, true, false, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            // 
            // _barPrint
            // 
            this._barPrint.Caption = "打印";
            this._barPrint.Id = 12;
            this._barPrint.ImageIndex = 19;
            this._barPrint.Name = "_barPrint";
            // 
            // _barExit
            // 
            this._barExit.Caption = "退出";
            this._barExit.Id = 15;
            this._barExit.ImageIndex = 17;
            this._barExit.Name = "_barExit";
            this._barExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barExit_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(909, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 576);
            this.barDockControlBottom.Size = new System.Drawing.Size(909, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 545);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(909, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 545);
            // 
            // _barAddInfo
            // 
            this._barAddInfo.Caption = "明细";
            this._barAddInfo.Id = 5;
            this._barAddInfo.ImageIndex = 22;
            this._barAddInfo.Name = "_barAddInfo";
            // 
            // _barDelInfo
            // 
            this._barDelInfo.Caption = "明细";
            this._barDelInfo.Id = 9;
            this._barDelInfo.ImageIndex = 18;
            this._barDelInfo.Name = "_barDelInfo";
            // 
            // _barDelTable
            // 
            this._barDelTable.Caption = "单据";
            this._barDelTable.Id = 10;
            this._barDelTable.ImageIndex = 21;
            this._barDelTable.Name = "_barDelTable";
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
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "修改";
            this.barButtonItem1.Id = 19;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "打印";
            this.barButtonItem2.Id = 20;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(909, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "工票发卡记录";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 73);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(909, 503);
            this.splitContainerControl1.SplitterPosition = 270;
            this.splitContainerControl1.TabIndex = 5;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.MenuManager = this.barManager1;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(270, 503);
            this.gridControl2.TabIndex = 0;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coOneAmount});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView2_FocusedRowChanged);
            // 
            // _coOneAmount
            // 
            this._coOneAmount.Caption = "单箱数量";
            this._coOneAmount.FieldName = "OneAmount";
            this._coOneAmount.Name = "_coOneAmount";
            this._coOneAmount.Visible = true;
            this._coOneAmount.VisibleIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(723, 38);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 18);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "按分组发卡";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // BarTaskCar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 576);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BarTaskCar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "工票发卡记录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BarTaskCar_FormClosing);
            this.Load += new System.EventHandler(this.StockBackForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem _barAddInfo;
        private DevExpress.XtraBars.BarButtonItem _barEdit;
        private DevExpress.XtraBars.BarButtonItem _barDelInfo;
        private DevExpress.XtraBars.BarButtonItem _barDelTable;
        private DevExpress.XtraBars.BarButtonItem _barPrint;
        private DevExpress.XtraBars.BarButtonItem _barExit;
        private DevExpress.XtraBars.BarStaticItem _barFill;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.BarStaticItem _barLaVerify;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _coBoxNum;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorID;
        private DevExpress.XtraGrid.Columns.GridColumn _coSizeID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorOneID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorTwoID;
        private DevExpress.XtraGrid.Columns.GridColumn _coAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coWorkTicketID;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn _coOneAmount;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}