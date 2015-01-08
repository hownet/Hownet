namespace Hownet.Stock
{
    partial class frNeedInfoList
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
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coMaterielID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorOneID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorTwoID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coSizeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNotAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNowAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDepotMeasureID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMListID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coStockRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coStockInfoID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coIsSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNeedIsEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coStringTaskID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDataTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMaterielRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton2);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.checkEdit1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 55);
            this.panel1.TabIndex = 0;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(193, 18);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "查询";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(586, 18);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(146, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "采购数量大于0的物料";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(70, 22);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "包括已完成";
            this.checkEdit1.Size = new System.Drawing.Size(96, 19);
            this.checkEdit1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 55);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.gridControl1.Size = new System.Drawing.Size(984, 285);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coMaterielID,
            this._coColorID,
            this._coColorOneID,
            this._coColorTwoID,
            this._coSizeID,
            this._coAmount,
            this._coNotAmount,
            this._coNowAmount,
            this._coDepotMeasureID,
            this._coPrice,
            this._coMoney,
            this._coMListID,
            this._coStockRemark,
            this._coID,
            this._coStockInfoID,
            this._coA,
            this._coIsSelect,
            this._coNeedIsEnd,
            this._coStringTaskID,
            this._coDataTime,
            this._coMaterielRemark,
            this._coNum});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // _coMaterielID
            // 
            this._coMaterielID.Caption = "物料名";
            this._coMaterielID.FieldName = "MaterielID";
            this._coMaterielID.Name = "_coMaterielID";
            this._coMaterielID.OptionsColumn.AllowEdit = false;
            this._coMaterielID.Visible = true;
            this._coMaterielID.VisibleIndex = 2;
            this._coMaterielID.Width = 102;
            // 
            // _coColorID
            // 
            this._coColorID.Caption = "颜色";
            this._coColorID.FieldName = "ColorID";
            this._coColorID.Name = "_coColorID";
            this._coColorID.OptionsColumn.AllowEdit = false;
            this._coColorID.Visible = true;
            this._coColorID.VisibleIndex = 3;
            this._coColorID.Width = 56;
            // 
            // _coColorOneID
            // 
            this._coColorOneID.Caption = "配色一";
            this._coColorOneID.FieldName = "ColorOneID";
            this._coColorOneID.Name = "_coColorOneID";
            this._coColorOneID.OptionsColumn.AllowEdit = false;
            this._coColorOneID.Visible = true;
            this._coColorOneID.VisibleIndex = 4;
            this._coColorOneID.Width = 39;
            // 
            // _coColorTwoID
            // 
            this._coColorTwoID.Caption = "配色二";
            this._coColorTwoID.FieldName = "ColorTwoID";
            this._coColorTwoID.Name = "_coColorTwoID";
            this._coColorTwoID.OptionsColumn.AllowEdit = false;
            this._coColorTwoID.Visible = true;
            this._coColorTwoID.VisibleIndex = 5;
            this._coColorTwoID.Width = 43;
            // 
            // _coSizeID
            // 
            this._coSizeID.Caption = "尺码";
            this._coSizeID.FieldName = "SizeID";
            this._coSizeID.Name = "_coSizeID";
            this._coSizeID.OptionsColumn.AllowEdit = false;
            this._coSizeID.Visible = true;
            this._coSizeID.VisibleIndex = 6;
            this._coSizeID.Width = 36;
            // 
            // _coAmount
            // 
            this._coAmount.Caption = "申购数量";
            this._coAmount.FieldName = "Amount";
            this._coAmount.Name = "_coAmount";
            this._coAmount.OptionsColumn.AllowEdit = false;
            this._coAmount.Visible = true;
            this._coAmount.VisibleIndex = 7;
            this._coAmount.Width = 58;
            // 
            // _coNotAmount
            // 
            this._coNotAmount.Caption = "未采购数量";
            this._coNotAmount.FieldName = "NotAmount";
            this._coNotAmount.Name = "_coNotAmount";
            this._coNotAmount.OptionsColumn.AllowEdit = false;
            this._coNotAmount.Visible = true;
            this._coNotAmount.VisibleIndex = 8;
            this._coNotAmount.Width = 59;
            // 
            // _coNowAmount
            // 
            this._coNowAmount.Caption = "本次采购";
            this._coNowAmount.FieldName = "NowAmount";
            this._coNowAmount.Name = "_coNowAmount";
            this._coNowAmount.Visible = true;
            this._coNowAmount.VisibleIndex = 9;
            this._coNowAmount.Width = 57;
            // 
            // _coDepotMeasureID
            // 
            this._coDepotMeasureID.Caption = "单位";
            this._coDepotMeasureID.FieldName = "DepotMeasureID";
            this._coDepotMeasureID.Name = "_coDepotMeasureID";
            this._coDepotMeasureID.OptionsColumn.AllowEdit = false;
            this._coDepotMeasureID.Visible = true;
            this._coDepotMeasureID.VisibleIndex = 10;
            this._coDepotMeasureID.Width = 57;
            // 
            // _coPrice
            // 
            this._coPrice.Caption = "单价";
            this._coPrice.FieldName = "Price";
            this._coPrice.Name = "_coPrice";
            this._coPrice.OptionsColumn.AllowEdit = false;
            this._coPrice.Visible = true;
            this._coPrice.VisibleIndex = 11;
            this._coPrice.Width = 57;
            // 
            // _coMoney
            // 
            this._coMoney.Caption = "金额";
            this._coMoney.FieldName = "Money";
            this._coMoney.Name = "_coMoney";
            this._coMoney.OptionsColumn.AllowEdit = false;
            this._coMoney.Visible = true;
            this._coMoney.VisibleIndex = 12;
            this._coMoney.Width = 57;
            // 
            // _coMListID
            // 
            this._coMListID.Caption = "MListID";
            this._coMListID.FieldName = "MListID";
            this._coMListID.Name = "_coMListID";
            this._coMListID.OptionsColumn.AllowEdit = false;
            // 
            // _coStockRemark
            // 
            this._coStockRemark.Caption = "申购说明";
            this._coStockRemark.ColumnEdit = this.repositoryItemMemoEdit1;
            this._coStockRemark.FieldName = "StockRemark";
            this._coStockRemark.Name = "_coStockRemark";
            this._coStockRemark.OptionsColumn.AllowEdit = false;
            this._coStockRemark.Visible = true;
            this._coStockRemark.VisibleIndex = 13;
            this._coStockRemark.Width = 57;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // _coID
            // 
            this._coID.Caption = "ID";
            this._coID.FieldName = "ID";
            this._coID.Name = "_coID";
            this._coID.OptionsColumn.AllowEdit = false;
            // 
            // _coStockInfoID
            // 
            this._coStockInfoID.Caption = "StockInfoID";
            this._coStockInfoID.FieldName = "StockInfoID";
            this._coStockInfoID.Name = "_coStockInfoID";
            this._coStockInfoID.OptionsColumn.AllowEdit = false;
            // 
            // _coA
            // 
            this._coA.Caption = "A";
            this._coA.FieldName = "A";
            this._coA.Name = "_coA";
            this._coA.OptionsColumn.AllowEdit = false;
            // 
            // _coIsSelect
            // 
            this._coIsSelect.Caption = "选择";
            this._coIsSelect.FieldName = "IsSelect";
            this._coIsSelect.Name = "_coIsSelect";
            this._coIsSelect.Width = 46;
            // 
            // _coNeedIsEnd
            // 
            this._coNeedIsEnd.Caption = "申购已完成";
            this._coNeedIsEnd.FieldName = "NeedIsEnd";
            this._coNeedIsEnd.Name = "_coNeedIsEnd";
            this._coNeedIsEnd.Visible = true;
            this._coNeedIsEnd.VisibleIndex = 14;
            this._coNeedIsEnd.Width = 98;
            // 
            // _coStringTaskID
            // 
            this._coStringTaskID.Caption = "StringTaskID";
            this._coStringTaskID.FieldName = "StringTaskID";
            this._coStringTaskID.Name = "_coStringTaskID";
            this._coStringTaskID.OptionsColumn.AllowEdit = false;
            // 
            // _coDataTime
            // 
            this._coDataTime.Caption = "申购日期";
            this._coDataTime.FieldName = "DataTime";
            this._coDataTime.Name = "_coDataTime";
            this._coDataTime.Visible = true;
            this._coDataTime.VisibleIndex = 0;
            this._coDataTime.Width = 58;
            // 
            // _coMaterielRemark
            // 
            this._coMaterielRemark.Caption = "物料说明";
            this._coMaterielRemark.FieldName = "MaterielRemark";
            this._coMaterielRemark.Name = "_coMaterielRemark";
            this._coMaterielRemark.OptionsColumn.AllowEdit = false;
            this._coMaterielRemark.Visible = true;
            this._coMaterielRemark.VisibleIndex = 15;
            this._coMaterielRemark.Width = 83;
            // 
            // _coNum
            // 
            this._coNum.Caption = "编号";
            this._coNum.FieldName = "Num";
            this._coNum.Name = "_coNum";
            this._coNum.Visible = true;
            this._coNum.VisibleIndex = 1;
            this._coNum.Width = 49;
            // 
            // frNeedInfoList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 340);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frNeedInfoList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "申购单明细";
            this.Load += new System.EventHandler(this.frNeedInfoList_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn _coMaterielID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorOneID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorTwoID;
        private DevExpress.XtraGrid.Columns.GridColumn _coSizeID;
        private DevExpress.XtraGrid.Columns.GridColumn _coAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coNotAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coNowAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coDepotMeasureID;
        private DevExpress.XtraGrid.Columns.GridColumn _coPrice;
        private DevExpress.XtraGrid.Columns.GridColumn _coMoney;
        private DevExpress.XtraGrid.Columns.GridColumn _coMListID;
        private DevExpress.XtraGrid.Columns.GridColumn _coStockRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraGrid.Columns.GridColumn _coStockInfoID;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
        private DevExpress.XtraGrid.Columns.GridColumn _coIsSelect;
        private DevExpress.XtraGrid.Columns.GridColumn _coNeedIsEnd;
        private DevExpress.XtraGrid.Columns.GridColumn _coStringTaskID;
        private DevExpress.XtraGrid.Columns.GridColumn _coDataTime;
        private DevExpress.XtraGrid.Columns.GridColumn _coMaterielRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coNum;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
    }
}