namespace Hownet.Pay
{
    partial class frSetNoPriceWorking
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
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMaterielID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coVer = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coWorkingID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coPrice = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.panel1.Size = new System.Drawing.Size(808, 78);
            this.panel1.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(611, 25);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "保存";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 78);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(808, 707);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coID,
            this._coA,
            this._coMaterielID,
            this._coVer,
            this._coWorkingID,
            this._coPrice});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
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
            // _coMaterielID
            // 
            this._coMaterielID.Caption = "款号";
            this._coMaterielID.FieldName = "MaterielID";
            this._coMaterielID.Name = "_coMaterielID";
            this._coMaterielID.OptionsColumn.AllowEdit = false;
            this._coMaterielID.Visible = true;
            this._coMaterielID.VisibleIndex = 0;
            this._coMaterielID.Width = 150;
            // 
            // _coVer
            // 
            this._coVer.Caption = "工艺单说明";
            this._coVer.FieldName = "Remark";
            this._coVer.Name = "_coVer";
            this._coVer.OptionsColumn.AllowEdit = false;
            this._coVer.Visible = true;
            this._coVer.VisibleIndex = 1;
            this._coVer.Width = 297;
            // 
            // _coWorkingID
            // 
            this._coWorkingID.Caption = "工序名";
            this._coWorkingID.FieldName = "WorkingID";
            this._coWorkingID.Name = "_coWorkingID";
            this._coWorkingID.OptionsColumn.AllowEdit = false;
            this._coWorkingID.Visible = true;
            this._coWorkingID.VisibleIndex = 2;
            this._coWorkingID.Width = 169;
            // 
            // _coPrice
            // 
            this._coPrice.Caption = "工价";
            this._coPrice.FieldName = "Price";
            this._coPrice.Name = "_coPrice";
            this._coPrice.Visible = true;
            this._coPrice.VisibleIndex = 3;
            this._coPrice.Width = 174;
            // 
            // frSetNoPriceWorking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 785);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frSetNoPriceWorking";
            this.Text = "设置未定工价";
            this.Load += new System.EventHandler(this.frSetNoPriceWorking_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
        private DevExpress.XtraGrid.Columns.GridColumn _coMaterielID;
        private DevExpress.XtraGrid.Columns.GridColumn _coVer;
        private DevExpress.XtraGrid.Columns.GridColumn _coWorkingID;
        private DevExpress.XtraGrid.Columns.GridColumn _coPrice;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}