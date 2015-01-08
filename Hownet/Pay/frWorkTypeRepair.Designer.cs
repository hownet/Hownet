namespace Hownet.Pay
{
    partial class frWorkTypeRepair
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coEmployeeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coBiLi = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coBuTie = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coDepartmentID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 82);
            this.panel1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 82);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(560, 420);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView2});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coEmployeeID,
            this._coMoney,
            this._coBiLi,
            this._coBuTie,
            this._coDepartmentID});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // _coEmployeeID
            // 
            this._coEmployeeID.Caption = "员工";
            this._coEmployeeID.FieldName = "ID";
            this._coEmployeeID.Name = "_coEmployeeID";
            this._coEmployeeID.Visible = true;
            this._coEmployeeID.VisibleIndex = 1;
            // 
            // _coMoney
            // 
            this._coMoney.Caption = "本月工资";
            this._coMoney.FieldName = "Money";
            this._coMoney.Name = "_coMoney";
            this._coMoney.Visible = true;
            this._coMoney.VisibleIndex = 2;
            // 
            // _coBiLi
            // 
            this._coBiLi.Caption = "提成比例";
            this._coBiLi.FieldName = "BiLi";
            this._coBiLi.Name = "_coBiLi";
            this._coBiLi.Visible = true;
            this._coBiLi.VisibleIndex = 3;
            // 
            // _coBuTie
            // 
            this._coBuTie.Caption = "提成金额";
            this._coBuTie.FieldName = "BuTie";
            this._coBuTie.Name = "_coBuTie";
            this._coBuTie.Visible = true;
            this._coBuTie.VisibleIndex = 4;
            // 
            // _coDepartmentID
            // 
            this._coDepartmentID.Caption = "部门";
            this._coDepartmentID.FieldName = "DepartmentID";
            this._coDepartmentID.Name = "_coDepartmentID";
            this._coDepartmentID.Visible = true;
            this._coDepartmentID.VisibleIndex = 0;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.Name = "gridView2";
            // 
            // frWorkTypeRepair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 502);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frWorkTypeRepair";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工种补贴";
            this.Load += new System.EventHandler(this.frWorkTypeRepair_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn _coEmployeeID;
        private DevExpress.XtraGrid.Columns.GridColumn _coMoney;
        private DevExpress.XtraGrid.Columns.GridColumn _coBiLi;
        private DevExpress.XtraGrid.Columns.GridColumn _coBuTie;
        private DevExpress.XtraGrid.Columns.GridColumn _coDepartmentID;
    }
}