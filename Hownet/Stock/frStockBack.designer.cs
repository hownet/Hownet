using Hownet.BaseContranl;
namespace Hownet.Stock
{
    partial class frStockBack
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coMaterielID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coSizeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorOneID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorTwoID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNotAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNowAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDepotMeasureID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMainID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coStockPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMListID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNeedIsEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coStockRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMaterielRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coBrandID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reBEAmount = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this._coSpecName = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coSpecID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reBEAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton3);
            this.panel1.Controls.Add(this.simpleButton2);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(915, 31);
            this.panel1.TabIndex = 6;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(393, 2);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(75, 23);
            this.simpleButton3.TabIndex = 2;
            this.simpleButton3.Text = "显示所有";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton2.Location = new System.Drawing.Point(765, 3);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "取 消";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton1.Location = new System.Drawing.Point(646, 3);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "确 定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 31);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._reBEAmount});
            this.gridControl1.Size = new System.Drawing.Size(915, 307);
            this.gridControl1.TabIndex = 8;
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
            this._coAmount,
            this._coNotAmount,
            this._coNowAmount,
            this._coPrice,
            this._coDepotMeasureID,
            this._coMoney,
            this._coRemark,
            this._coMainID,
            this._coID,
            this._coA,
            this._coStockPrice,
            this._coMListID,
            this._coNeedIsEnd,
            this._coNum,
            this._coStockRemark,
            this._coMaterielRemark,
            this._coBrandID,
            this._coSpecName,
            this._coSpecID});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 40;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // _coMaterielID
            // 
            this._coMaterielID.AppearanceHeader.Options.UseTextOptions = true;
            this._coMaterielID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coMaterielID.Caption = "商品名称";
            this._coMaterielID.FieldName = "MaterielID";
            this._coMaterielID.Name = "_coMaterielID";
            this._coMaterielID.OptionsColumn.AllowEdit = false;
            this._coMaterielID.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this._coMaterielID.Visible = true;
            this._coMaterielID.VisibleIndex = 1;
            this._coMaterielID.Width = 71;
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
            this._coColorID.VisibleIndex = 3;
            this._coColorID.Width = 72;
            // 
            // _coSizeID
            // 
            this._coSizeID.Caption = "尺码";
            this._coSizeID.FieldName = "SizeID";
            this._coSizeID.Name = "_coSizeID";
            this._coSizeID.OptionsColumn.AllowEdit = false;
            this._coSizeID.Visible = true;
            this._coSizeID.VisibleIndex = 4;
            this._coSizeID.Width = 72;
            // 
            // _coColorOneID
            // 
            this._coColorOneID.Caption = "插色一";
            this._coColorOneID.FieldName = "ColorOneID";
            this._coColorOneID.Name = "_coColorOneID";
            this._coColorOneID.Visible = true;
            this._coColorOneID.VisibleIndex = 5;
            // 
            // _coColorTwoID
            // 
            this._coColorTwoID.Caption = "插色二";
            this._coColorTwoID.FieldName = "ColorTwoID";
            this._coColorTwoID.Name = "_coColorTwoID";
            this._coColorTwoID.Visible = true;
            this._coColorTwoID.VisibleIndex = 6;
            // 
            // _coAmount
            // 
            this._coAmount.AppearanceHeader.Options.UseTextOptions = true;
            this._coAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coAmount.Caption = "订购数量";
            this._coAmount.FieldName = "Amount";
            this._coAmount.Name = "_coAmount";
            this._coAmount.OptionsColumn.AllowEdit = false;
            this._coAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this._coAmount.Visible = true;
            this._coAmount.VisibleIndex = 8;
            this._coAmount.Width = 72;
            // 
            // _coNotAmount
            // 
            this._coNotAmount.Caption = "未到货数量";
            this._coNotAmount.FieldName = "NotAmount";
            this._coNotAmount.Name = "_coNotAmount";
            this._coNotAmount.OptionsColumn.AllowEdit = false;
            this._coNotAmount.Visible = true;
            this._coNotAmount.VisibleIndex = 9;
            this._coNotAmount.Width = 79;
            // 
            // _coNowAmount
            // 
            this._coNowAmount.Caption = "本次数量";
            this._coNowAmount.FieldName = "NowAmount";
            this._coNowAmount.Name = "_coNowAmount";
            this._coNowAmount.Visible = true;
            this._coNowAmount.VisibleIndex = 10;
            // 
            // _coPrice
            // 
            this._coPrice.AppearanceHeader.Options.UseTextOptions = true;
            this._coPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coPrice.Caption = "单价";
            this._coPrice.FieldName = "Price";
            this._coPrice.Name = "_coPrice";
            this._coPrice.Visible = true;
            this._coPrice.VisibleIndex = 12;
            this._coPrice.Width = 70;
            // 
            // _coDepotMeasureID
            // 
            this._coDepotMeasureID.AppearanceHeader.Options.UseTextOptions = true;
            this._coDepotMeasureID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coDepotMeasureID.Caption = "单位";
            this._coDepotMeasureID.FieldName = "DepotMeasureID";
            this._coDepotMeasureID.Name = "_coDepotMeasureID";
            this._coDepotMeasureID.OptionsColumn.AllowEdit = false;
            this._coDepotMeasureID.Visible = true;
            this._coDepotMeasureID.VisibleIndex = 11;
            this._coDepotMeasureID.Width = 70;
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
            this._coMoney.VisibleIndex = 13;
            this._coMoney.Width = 97;
            // 
            // _coRemark
            // 
            this._coRemark.AppearanceHeader.Options.UseTextOptions = true;
            this._coRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._coRemark.Caption = "说明";
            this._coRemark.FieldName = "Remark";
            this._coRemark.Name = "_coRemark";
            this._coRemark.OptionsColumn.AllowEdit = false;
            this._coRemark.Visible = true;
            this._coRemark.VisibleIndex = 14;
            this._coRemark.Width = 142;
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
            // _coStockPrice
            // 
            this._coStockPrice.Caption = "采购价格";
            this._coStockPrice.FieldName = "StockPrice";
            this._coStockPrice.Name = "_coStockPrice";
            this._coStockPrice.OptionsColumn.AllowEdit = false;
            // 
            // _coMListID
            // 
            this._coMListID.Caption = "MListID";
            this._coMListID.FieldName = "MListID";
            this._coMListID.Name = "_coMListID";
            this._coMListID.OptionsColumn.AllowEdit = false;
            // 
            // _coNeedIsEnd
            // 
            this._coNeedIsEnd.Caption = "采购已完成";
            this._coNeedIsEnd.FieldName = "NeedIsEnd";
            this._coNeedIsEnd.Name = "_coNeedIsEnd";
            this._coNeedIsEnd.Visible = true;
            this._coNeedIsEnd.VisibleIndex = 17;
            // 
            // _coNum
            // 
            this._coNum.Caption = "采购单编号";
            this._coNum.FieldName = "Num";
            this._coNum.Name = "_coNum";
            this._coNum.Visible = true;
            this._coNum.VisibleIndex = 0;
            // 
            // _coStockRemark
            // 
            this._coStockRemark.Caption = "采购说明";
            this._coStockRemark.FieldName = "StockRemark";
            this._coStockRemark.Name = "_coStockRemark";
            this._coStockRemark.Visible = true;
            this._coStockRemark.VisibleIndex = 15;
            // 
            // _coMaterielRemark
            // 
            this._coMaterielRemark.Caption = "物料说明";
            this._coMaterielRemark.FieldName = "MaterielRemark";
            this._coMaterielRemark.Name = "_coMaterielRemark";
            this._coMaterielRemark.Visible = true;
            this._coMaterielRemark.VisibleIndex = 16;
            // 
            // _coBrandID
            // 
            this._coBrandID.Caption = "商标";
            this._coBrandID.FieldName = "BrandID";
            this._coBrandID.Name = "_coBrandID";
            this._coBrandID.Visible = true;
            this._coBrandID.VisibleIndex = 2;
            // 
            // _reBEAmount
            // 
            this._reBEAmount.AutoHeight = false;
            this._reBEAmount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this._reBEAmount.Name = "_reBEAmount";
            this._reBEAmount.ReadOnly = true;
            // 
            // _coSpecName
            // 
            this._coSpecName.Caption = "规格";
            this._coSpecName.FieldName = "SpecName";
            this._coSpecName.Name = "_coSpecName";
            this._coSpecName.OptionsColumn.AllowEdit = false;
            // 
            // _coSpecID
            // 
            this._coSpecID.Caption = "SpecID";
            this._coSpecID.FieldName = "SpecID";
            this._coSpecID.Name = "_coSpecID";
            this._coSpecID.OptionsColumn.AllowEdit = false;
            // 
            // frStockBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 338);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frStockBack";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "采购收货";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reBEAmount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _coMaterielID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorID;
        private DevExpress.XtraGrid.Columns.GridColumn _coSizeID;
        private DevExpress.XtraGrid.Columns.GridColumn _coAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coPrice;
        private DevExpress.XtraGrid.Columns.GridColumn _coDepotMeasureID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMoney;
        private DevExpress.XtraGrid.Columns.GridColumn _coRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coMainID;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit _reBEAmount;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Columns.GridColumn _coNotAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coNowAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coStockPrice;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorOneID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorTwoID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMListID;
        private DevExpress.XtraGrid.Columns.GridColumn _coNeedIsEnd;
        private DevExpress.XtraGrid.Columns.GridColumn _coNum;
        private DevExpress.XtraGrid.Columns.GridColumn _coStockRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coMaterielRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coBrandID;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraGrid.Columns.GridColumn _coSpecName;
        private DevExpress.XtraGrid.Columns.GridColumn _coSpecID;

    }
}