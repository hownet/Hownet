namespace Hownet.Stock
{
    partial class frShowNeedInfo
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMaterielID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coSpecif = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorOneID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorTwoID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coSizeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMeasureID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMListID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coRepertoryAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNeedAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._costockAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coHasStockAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNotAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNowUseRep = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coTemUse = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coProduceTaskID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coOutAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this._barCancelStock = new DevExpress.XtraBars.BarButtonItem();
            this._barIsEnd = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coIID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coSpecID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDepotInfoID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reDepotInfo = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMainID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coIA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coSpecName = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDepotInfoName = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coStockListID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reDepotInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textEdit1);
            this.panel1.Controls.Add(this.simpleButton2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 42);
            this.panel1.TabIndex = 0;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(699, 10);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "确定";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(284, 42);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(724, 307);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coNum,
            this._coMaterielID,
            this._coSpecif,
            this._coColorID,
            this._coColorOneID,
            this._coColorTwoID,
            this._coSizeID,
            this._coMeasureID,
            this._coMListID,
            this._coID,
            this._coRepertoryAmount,
            this._coNeedAmount,
            this._costockAmount,
            this._coHasStockAmount,
            this._coNotAmount,
            this._coNowUseRep,
            this._coTemUse,
            this._coProduceTaskID,
            this._coOutAmount});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowRowSizing = true;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this._coNotAmount, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            this.gridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseUp);
            // 
            // _coNum
            // 
            this._coNum.Caption = "编号";
            this._coNum.FieldName = "Num";
            this._coNum.Name = "_coNum";
            this._coNum.OptionsColumn.AllowEdit = false;
            this._coNum.Visible = true;
            this._coNum.VisibleIndex = 0;
            // 
            // _coMaterielID
            // 
            this._coMaterielID.Caption = "物料名";
            this._coMaterielID.FieldName = "MaterielID";
            this._coMaterielID.Name = "_coMaterielID";
            this._coMaterielID.OptionsColumn.AllowEdit = false;
            this._coMaterielID.Visible = true;
            this._coMaterielID.VisibleIndex = 1;
            // 
            // _coSpecif
            // 
            this._coSpecif.Caption = "规格";
            this._coSpecif.FieldName = "Specif";
            this._coSpecif.Name = "_coSpecif";
            this._coSpecif.OptionsColumn.AllowEdit = false;
            this._coSpecif.Visible = true;
            this._coSpecif.VisibleIndex = 2;
            // 
            // _coColorID
            // 
            this._coColorID.Caption = "颜色";
            this._coColorID.FieldName = "ColorID";
            this._coColorID.Name = "_coColorID";
            this._coColorID.OptionsColumn.AllowEdit = false;
            // 
            // _coColorOneID
            // 
            this._coColorOneID.Caption = "插色一";
            this._coColorOneID.FieldName = "ColorOneID";
            this._coColorOneID.Name = "_coColorOneID";
            this._coColorOneID.OptionsColumn.AllowEdit = false;
            // 
            // _coColorTwoID
            // 
            this._coColorTwoID.Caption = "插色二";
            this._coColorTwoID.FieldName = "ColorTwoID";
            this._coColorTwoID.Name = "_coColorTwoID";
            this._coColorTwoID.OptionsColumn.AllowEdit = false;
            // 
            // _coSizeID
            // 
            this._coSizeID.Caption = "尺码";
            this._coSizeID.FieldName = "SizeID";
            this._coSizeID.Name = "_coSizeID";
            this._coSizeID.OptionsColumn.AllowEdit = false;
            // 
            // _coMeasureID
            // 
            this._coMeasureID.Caption = "单位";
            this._coMeasureID.FieldName = "MeasureID";
            this._coMeasureID.Name = "_coMeasureID";
            this._coMeasureID.OptionsColumn.AllowEdit = false;
            this._coMeasureID.Visible = true;
            this._coMeasureID.VisibleIndex = 3;
            // 
            // _coMListID
            // 
            this._coMListID.Caption = "MListID";
            this._coMListID.FieldName = "MListID";
            this._coMListID.Name = "_coMListID";
            this._coMListID.OptionsColumn.AllowEdit = false;
            // 
            // _coID
            // 
            this._coID.Caption = "ID";
            this._coID.FieldName = "ID";
            this._coID.Name = "_coID";
            this._coID.OptionsColumn.AllowEdit = false;
            // 
            // _coRepertoryAmount
            // 
            this._coRepertoryAmount.Caption = "已有库存";
            this._coRepertoryAmount.FieldName = "RepertoryAmount";
            this._coRepertoryAmount.Name = "_coRepertoryAmount";
            this._coRepertoryAmount.OptionsColumn.AllowEdit = false;
            this._coRepertoryAmount.Visible = true;
            this._coRepertoryAmount.VisibleIndex = 5;
            // 
            // _coNeedAmount
            // 
            this._coNeedAmount.Caption = "已申购数量";
            this._coNeedAmount.FieldName = "NeedAmount";
            this._coNeedAmount.Name = "_coNeedAmount";
            this._coNeedAmount.OptionsColumn.AllowEdit = false;
            this._coNeedAmount.Visible = true;
            this._coNeedAmount.VisibleIndex = 6;
            // 
            // _costockAmount
            // 
            this._costockAmount.Caption = "应采购量";
            this._costockAmount.FieldName = "stockAmount";
            this._costockAmount.Name = "_costockAmount";
            this._costockAmount.OptionsColumn.AllowEdit = false;
            this._costockAmount.Visible = true;
            this._costockAmount.VisibleIndex = 4;
            // 
            // _coHasStockAmount
            // 
            this._coHasStockAmount.Caption = "已采购量";
            this._coHasStockAmount.FieldName = "HasStockAmount";
            this._coHasStockAmount.Name = "_coHasStockAmount";
            this._coHasStockAmount.OptionsColumn.AllowEdit = false;
            this._coHasStockAmount.Visible = true;
            this._coHasStockAmount.VisibleIndex = 7;
            // 
            // _coNotAmount
            // 
            this._coNotAmount.Caption = "欠数";
            this._coNotAmount.FieldName = "NotAmount";
            this._coNotAmount.Name = "_coNotAmount";
            this._coNotAmount.OptionsColumn.AllowEdit = false;
            this._coNotAmount.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this._coNotAmount.Visible = true;
            this._coNotAmount.VisibleIndex = 9;
            // 
            // _coNowUseRep
            // 
            this._coNowUseRep.Caption = "本次使用";
            this._coNowUseRep.FieldName = "NowUseRep";
            this._coNowUseRep.Name = "_coNowUseRep";
            this._coNowUseRep.Visible = true;
            this._coNowUseRep.VisibleIndex = 10;
            // 
            // _coTemUse
            // 
            this._coTemUse.Caption = "TemUse";
            this._coTemUse.FieldName = "TemUse";
            this._coTemUse.Name = "_coTemUse";
            this._coTemUse.OptionsColumn.AllowEdit = false;
            // 
            // _coProduceTaskID
            // 
            this._coProduceTaskID.Caption = "ProduceTaskID";
            this._coProduceTaskID.FieldName = "ProduceTaskID";
            this._coProduceTaskID.Name = "_coProduceTaskID";
            this._coProduceTaskID.OptionsColumn.AllowEdit = false;
            // 
            // _coOutAmount
            // 
            this._coOutAmount.Caption = "车间已领料";
            this._coOutAmount.FieldName = "OutAmount";
            this._coOutAmount.Name = "_coOutAmount";
            this._coOutAmount.OptionsColumn.AllowEdit = false;
            this._coOutAmount.Visible = true;
            this._coOutAmount.VisibleIndex = 8;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this._barCancelStock,
            this._barIsEnd});
            this.barManager1.MaxItemId = 2;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1008, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 349);
            this.barDockControlBottom.Size = new System.Drawing.Size(1008, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 349);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1008, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 349);
            // 
            // _barCancelStock
            // 
            this._barCancelStock.Caption = "取消采购";
            this._barCancelStock.Id = 0;
            this._barCancelStock.Name = "_barCancelStock";
            this._barCancelStock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barCancelStock_ItemClick);
            // 
            // _barIsEnd
            // 
            this._barIsEnd.Caption = "标记为配货已完成";
            this._barIsEnd.Id = 1;
            this._barIsEnd.Name = "_barIsEnd";
            this._barIsEnd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barIsEnd_ItemClick);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this._barCancelStock),
            new DevExpress.XtraBars.LinkPersistInfo(this._barIsEnd)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(846, 12);
            this.textEdit1.MenuManager = this.barManager1;
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(100, 20);
            this.textEdit1.TabIndex = 3;
            this.textEdit1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textEdit1_KeyPress);
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridControl2.Location = new System.Drawing.Point(0, 42);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._reDepotInfo});
            this.gridControl2.Size = new System.Drawing.Size(284, 307);
            this.gridControl2.TabIndex = 6;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coIID,
            this._coSpecID,
            this._coDepotInfoID,
            this.gridColumn8,
            this._coMainID,
            this._coIA,
            this.gridColumn1,
            this._coSpecName,
            this._coDepotInfoName,
            this._coStockListID});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsCustomization.AllowFilter = false;
            this.gridView2.OptionsCustomization.AllowGroup = false;
            this.gridView2.OptionsCustomization.AllowSort = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // _coIID
            // 
            this._coIID.Caption = "ID";
            this._coIID.FieldName = "ID";
            this._coIID.Name = "_coIID";
            this._coIID.OptionsColumn.AllowEdit = false;
            // 
            // _coSpecID
            // 
            this._coSpecID.Caption = "规格";
            this._coSpecID.FieldName = "SpecID";
            this._coSpecID.Name = "_coSpecID";
            this._coSpecID.Visible = true;
            this._coSpecID.VisibleIndex = 0;
            this._coSpecID.Width = 36;
            // 
            // _coDepotInfoID
            // 
            this._coDepotInfoID.Caption = "存放位置";
            this._coDepotInfoID.ColumnEdit = this._reDepotInfo;
            this._coDepotInfoID.FieldName = "DepotInfoID";
            this._coDepotInfoID.Name = "_coDepotInfoID";
            this._coDepotInfoID.Visible = true;
            this._coDepotInfoID.VisibleIndex = 1;
            this._coDepotInfoID.Width = 62;
            // 
            // _reDepotInfo
            // 
            this._reDepotInfo.AutoHeight = false;
            this._reDepotInfo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._reDepotInfo.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "货位")});
            this._reDepotInfo.DisplayMember = "Name";
            this._reDepotInfo.Name = "_reDepotInfo";
            this._reDepotInfo.NullText = "";
            this._reDepotInfo.ValueMember = "ID";
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "说明";
            this.gridColumn8.FieldName = "Remark";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            this.gridColumn8.Width = 40;
            // 
            // _coMainID
            // 
            this._coMainID.Caption = "MainID";
            this._coMainID.FieldName = "MainID";
            this._coMainID.Name = "_coMainID";
            this._coMainID.OptionsColumn.AllowEdit = false;
            // 
            // _coIA
            // 
            this._coIA.Caption = "A";
            this._coIA.FieldName = "A";
            this._coIA.Name = "_coIA";
            this._coIA.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "数量";
            this.gridColumn1.FieldName = "NotAmount";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 51;
            // 
            // _coSpecName
            // 
            this._coSpecName.Caption = "规格";
            this._coSpecName.FieldName = "SpecName";
            this._coSpecName.Name = "_coSpecName";
            this._coSpecName.OptionsColumn.AllowEdit = false;
            // 
            // _coDepotInfoName
            // 
            this._coDepotInfoName.Caption = "货位";
            this._coDepotInfoName.FieldName = "DepotInfoName";
            this._coDepotInfoName.Name = "_coDepotInfoName";
            this._coDepotInfoName.OptionsColumn.AllowEdit = false;
            // 
            // _coStockListID
            // 
            this._coStockListID.Caption = "条码";
            this._coStockListID.FieldName = "StockListID";
            this._coStockListID.Name = "_coStockListID";
            this._coStockListID.OptionsColumn.AllowEdit = false;
            this._coStockListID.Visible = true;
            this._coStockListID.VisibleIndex = 5;
            this._coStockListID.Width = 57;
            // 
            // frShowNeedInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 349);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frShowNeedInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物料明细";
            this.Load += new System.EventHandler(this.frShowInfo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reDepotInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _coNum;
        private DevExpress.XtraGrid.Columns.GridColumn _coMaterielID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorOneID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorTwoID;
        private DevExpress.XtraGrid.Columns.GridColumn _coSizeID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMeasureID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMListID;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem _barCancelStock;
        private DevExpress.XtraBars.BarButtonItem _barIsEnd;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraGrid.Columns.GridColumn _coRepertoryAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coNeedAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _costockAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coHasStockAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coNotAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coNowUseRep;
        private DevExpress.XtraGrid.Columns.GridColumn _coSpecif;
        private DevExpress.XtraGrid.Columns.GridColumn _coTemUse;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraGrid.Columns.GridColumn _coProduceTaskID;
        private DevExpress.XtraGrid.Columns.GridColumn _coOutAmount;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn _coIID;
        private DevExpress.XtraGrid.Columns.GridColumn _coSpecID;
        private DevExpress.XtraGrid.Columns.GridColumn _coDepotInfoID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reDepotInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn _coMainID;
        private DevExpress.XtraGrid.Columns.GridColumn _coIA;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn _coSpecName;
        private DevExpress.XtraGrid.Columns.GridColumn _coDepotInfoName;
        private DevExpress.XtraGrid.Columns.GridColumn _coStockListID;
    }
}