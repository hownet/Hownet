namespace Hownet.Sell
{
    partial class frSellReportey
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coRepertoryListID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coRepertoryID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDepotInfoID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMListID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMaterielID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coBrandID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coSizeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorOneID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coColorTwoID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coRepertoryAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMeasureID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNowAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(976, 63);
            this.panel1.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(828, 24);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 63);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(976, 441);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coRepertoryListID,
            this._coRepertoryID,
            this._coDepotInfoID,
            this._coMListID,
            this._coMaterielID,
            this._coBrandID,
            this._coColorID,
            this._coSizeID,
            this._coColorOneID,
            this._coColorTwoID,
            this._coAmount,
            this._coRepertoryAmount,
            this._coMeasureID,
            this._coNowAmount,
            this._coA});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // _coRepertoryListID
            // 
            this._coRepertoryListID.Caption = "RepertoryListID";
            this._coRepertoryListID.FieldName = "RepertoryListID";
            this._coRepertoryListID.Name = "_coRepertoryListID";
            this._coRepertoryListID.OptionsColumn.AllowEdit = false;
            // 
            // _coRepertoryID
            // 
            this._coRepertoryID.Caption = "RepertoryID";
            this._coRepertoryID.FieldName = "RepertoryID";
            this._coRepertoryID.Name = "_coRepertoryID";
            this._coRepertoryID.OptionsColumn.AllowEdit = false;
            // 
            // _coDepotInfoID
            // 
            this._coDepotInfoID.Caption = "货位";
            this._coDepotInfoID.FieldName = "DepotInfoID";
            this._coDepotInfoID.Name = "_coDepotInfoID";
            this._coDepotInfoID.OptionsColumn.AllowEdit = false;
            this._coDepotInfoID.Visible = true;
            this._coDepotInfoID.VisibleIndex = 0;
            // 
            // _coMListID
            // 
            this._coMListID.Caption = "MListID";
            this._coMListID.FieldName = "MListID";
            this._coMListID.Name = "_coMListID";
            this._coMListID.OptionsColumn.AllowEdit = false;
            // 
            // _coMaterielID
            // 
            this._coMaterielID.Caption = "款号";
            this._coMaterielID.FieldName = "MaterielID";
            this._coMaterielID.Name = "_coMaterielID";
            this._coMaterielID.OptionsColumn.AllowEdit = false;
            this._coMaterielID.Visible = true;
            this._coMaterielID.VisibleIndex = 1;
            // 
            // _coBrandID
            // 
            this._coBrandID.Caption = "商标";
            this._coBrandID.FieldName = "BrandID";
            this._coBrandID.Name = "_coBrandID";
            this._coBrandID.OptionsColumn.AllowEdit = false;
            this._coBrandID.Visible = true;
            this._coBrandID.VisibleIndex = 2;
            // 
            // _coColorID
            // 
            this._coColorID.Caption = "颜色";
            this._coColorID.FieldName = "ColorID";
            this._coColorID.Name = "_coColorID";
            this._coColorID.OptionsColumn.AllowEdit = false;
            this._coColorID.Visible = true;
            this._coColorID.VisibleIndex = 3;
            // 
            // _coSizeID
            // 
            this._coSizeID.Caption = "尺码";
            this._coSizeID.FieldName = "SizeID";
            this._coSizeID.Name = "_coSizeID";
            this._coSizeID.OptionsColumn.AllowEdit = false;
            this._coSizeID.Visible = true;
            this._coSizeID.VisibleIndex = 4;
            // 
            // _coColorOneID
            // 
            this._coColorOneID.Caption = "插色一";
            this._coColorOneID.FieldName = "ColorOneID";
            this._coColorOneID.Name = "_coColorOneID";
            this._coColorOneID.OptionsColumn.AllowEdit = false;
            this._coColorOneID.Visible = true;
            this._coColorOneID.VisibleIndex = 5;
            // 
            // _coColorTwoID
            // 
            this._coColorTwoID.Caption = "插色二";
            this._coColorTwoID.FieldName = "ColorTwoID";
            this._coColorTwoID.Name = "_coColorTwoID";
            this._coColorTwoID.OptionsColumn.AllowEdit = false;
            this._coColorTwoID.Visible = true;
            this._coColorTwoID.VisibleIndex = 6;
            // 
            // _coAmount
            // 
            this._coAmount.Caption = "货位数量";
            this._coAmount.FieldName = "RLAmount";
            this._coAmount.Name = "_coAmount";
            this._coAmount.OptionsColumn.AllowEdit = false;
            this._coAmount.Visible = true;
            this._coAmount.VisibleIndex = 7;
            // 
            // _coRepertoryAmount
            // 
            this._coRepertoryAmount.Caption = "库存数量";
            this._coRepertoryAmount.FieldName = "RepertoryAmount";
            this._coRepertoryAmount.Name = "_coRepertoryAmount";
            this._coRepertoryAmount.OptionsColumn.AllowEdit = false;
            this._coRepertoryAmount.Visible = true;
            this._coRepertoryAmount.VisibleIndex = 8;
            // 
            // _coMeasureID
            // 
            this._coMeasureID.Caption = "单位";
            this._coMeasureID.FieldName = "MeasureID";
            this._coMeasureID.Name = "_coMeasureID";
            this._coMeasureID.OptionsColumn.AllowEdit = false;
            this._coMeasureID.Visible = true;
            this._coMeasureID.VisibleIndex = 9;
            // 
            // _coNowAmount
            // 
            this._coNowAmount.Caption = "出库数量";
            this._coNowAmount.FieldName = "NowAmount";
            this._coNowAmount.Name = "_coNowAmount";
            this._coNowAmount.Visible = true;
            this._coNowAmount.VisibleIndex = 10;
            // 
            // _coA
            // 
            this._coA.Caption = "A";
            this._coA.FieldName = "A";
            this._coA.Name = "_coA";
            this._coA.OptionsColumn.AllowEdit = false;
            // 
            // frSellReportey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 504);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frSellReportey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "库存明细";
            this.Load += new System.EventHandler(this.frSellReportey_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Columns.GridColumn _coRepertoryListID;
        private DevExpress.XtraGrid.Columns.GridColumn _coRepertoryID;
        private DevExpress.XtraGrid.Columns.GridColumn _coDepotInfoID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMListID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMaterielID;
        private DevExpress.XtraGrid.Columns.GridColumn _coBrandID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorID;
        private DevExpress.XtraGrid.Columns.GridColumn _coSizeID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorOneID;
        private DevExpress.XtraGrid.Columns.GridColumn _coColorTwoID;
        private DevExpress.XtraGrid.Columns.GridColumn _coAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coRepertoryAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coMeasureID;
        private DevExpress.XtraGrid.Columns.GridColumn _coNowAmount;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
    }
}