using Hownet.BaseContranl;
namespace Hownet.Task
{
    partial class SelectWorkingForm
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
            this._gcGYD = new DevExpress.XtraGrid.GridControl();
            this._gvGYD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coOrders = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coCustOder = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coWorkingID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coGroupBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coIsTicket = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this._reRemark = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this._lePW = new BaseContranl.LabAndLookupEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this._gcGYD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gvGYD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._reRemark)).BeginInit();
            this.SuspendLayout();
            // 
            // _gcGYD
            // 
            this._gcGYD.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gcGYD.Location = new System.Drawing.Point(0, 28);
            this._gcGYD.MainView = this._gvGYD;
            this._gcGYD.Name = "_gcGYD";
            this._gcGYD.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this._reRemark});
            this._gcGYD.Size = new System.Drawing.Size(555, 479);
            this._gcGYD.TabIndex = 5;
            this._gcGYD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gvGYD});
            // 
            // _gvGYD
            // 
            this._gvGYD.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coID,
            this._coOrders,
            this._coCustOder,
            this._coWorkingID,
            this._coGroupBy,
            this._coIsTicket,
            this._coPrice});
            this._gvGYD.GridControl = this._gcGYD;
            this._gvGYD.Name = "_gvGYD";
            this._gvGYD.OptionsBehavior.Editable = false;
            this._gvGYD.OptionsView.ShowGroupPanel = false;
            this._gvGYD.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this._coOrders, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // _coID
            // 
            this._coID.Caption = "ID";
            this._coID.FieldName = "ProductWorkingID";
            this._coID.Name = "_coID";
            // 
            // _coOrders
            // 
            this._coOrders.Caption = "序号";
            this._coOrders.FieldName = "Orders";
            this._coOrders.Name = "_coOrders";
            this._coOrders.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this._coOrders.Visible = true;
            this._coOrders.VisibleIndex = 0;
            this._coOrders.Width = 60;
            // 
            // _coCustOder
            // 
            this._coCustOder.Caption = "自定义序号";
            this._coCustOder.FieldName = "CustOder";
            this._coCustOder.Name = "_coCustOder";
            this._coCustOder.Visible = true;
            this._coCustOder.VisibleIndex = 1;
            // 
            // _coWorkingID
            // 
            this._coWorkingID.Caption = "工序名";
            this._coWorkingID.FieldName = "WorkName";
            this._coWorkingID.Name = "_coWorkingID";
            this._coWorkingID.Visible = true;
            this._coWorkingID.VisibleIndex = 2;
            this._coWorkingID.Width = 90;
            // 
            // _coGroupBy
            // 
            this._coGroupBy.Caption = "分组";
            this._coGroupBy.FieldName = "GroupBy";
            this._coGroupBy.Name = "_coGroupBy";
            this._coGroupBy.Visible = true;
            this._coGroupBy.VisibleIndex = 3;
            this._coGroupBy.Width = 50;
            // 
            // _coIsTicket
            // 
            this._coIsTicket.Caption = "出工票";
            this._coIsTicket.FieldName = "IsTicket";
            this._coIsTicket.Name = "_coIsTicket";
            this._coIsTicket.Visible = true;
            this._coIsTicket.VisibleIndex = 4;
            // 
            // _coPrice
            // 
            this._coPrice.Caption = "工价";
            this._coPrice.FieldName = "Price";
            this._coPrice.Name = "_coPrice";
            this._coPrice.Visible = true;
            this._coPrice.VisibleIndex = 5;
            // 
            // _reRemark
            // 
            this._reRemark.AutoHeight = false;
            this._reRemark.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._reRemark.Name = "_reRemark";
            // 
            // _lePW
            // 
            this._lePW.Dock = System.Windows.Forms.DockStyle.Top;
            this._lePW.editVal = 0;
            this._lePW.IsNotCanEdit = false;
            this._lePW.IsMust = false;
            this._lePW.labText = "工艺单：";
            this._lePW.lenth = new int[] {
        60,
        240};
            this._lePW.Location = new System.Drawing.Point(0, 0);
            this._lePW.Name = "_lePW";
            this._lePW.Par = null;
            this._lePW.Size = new System.Drawing.Size(555, 28);
            this._lePW.IsNotCanEdit = false;
            this._lePW.TabIndex = 4;
            this._lePW.EditValueChanged += new BaseContranl.LabAndLookupEdit.EditValueChangedHandler(this._lePW_EditValueChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(323, 3);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // SelectWorkingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 507);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this._gcGYD);
            this.Controls.Add(this._lePW);
            this.Name = "SelectWorkingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择工艺单";
            this.Load += new System.EventHandler(this.PrlductWorkingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._gcGYD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gvGYD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._reRemark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _gcGYD;
        private DevExpress.XtraGrid.Views.Grid.GridView _gvGYD;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraGrid.Columns.GridColumn _coOrders;
        private DevExpress.XtraGrid.Columns.GridColumn _coCustOder;
        private DevExpress.XtraGrid.Columns.GridColumn _coWorkingID;
        private DevExpress.XtraGrid.Columns.GridColumn _coGroupBy;
        private DevExpress.XtraGrid.Columns.GridColumn _coIsTicket;
        private DevExpress.XtraGrid.Columns.GridColumn _coPrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit _reRemark;
        private LabAndLookupEdit _lePW;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;

    }
}