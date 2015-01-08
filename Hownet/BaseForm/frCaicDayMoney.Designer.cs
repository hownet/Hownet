namespace BaseForm
{
    partial class frCaicDayMoney
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
            this._coDayMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coNightMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coLateAtNightMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coTXBT = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(593, 61);
            this.panel1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 61);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemMemoEdit1});
            this.gridControl1.Size = new System.Drawing.Size(593, 284);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coDayMoney,
            this._coNightMoney,
            this._coLateAtNightMoney,
            this._coTXBT,
            this._coRemark,
            this._coID,
            this._coA});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // _coDayMoney
            // 
            this._coDayMoney.Caption = "白班工资";
            this._coDayMoney.ColumnEdit = this.repositoryItemTextEdit1;
            this._coDayMoney.FieldName = "DayMoney";
            this._coDayMoney.Name = "_coDayMoney";
            this._coDayMoney.Visible = true;
            this._coDayMoney.VisibleIndex = 0;
            this._coDayMoney.Width = 77;
            // 
            // _coNightMoney
            // 
            this._coNightMoney.Caption = "晚班工资";
            this._coNightMoney.ColumnEdit = this.repositoryItemTextEdit1;
            this._coNightMoney.FieldName = "NightMoney";
            this._coNightMoney.Name = "_coNightMoney";
            this._coNightMoney.Visible = true;
            this._coNightMoney.VisibleIndex = 1;
            this._coNightMoney.Width = 81;
            // 
            // _coLateAtNightMoney
            // 
            this._coLateAtNightMoney.Caption = "通宵工资";
            this._coLateAtNightMoney.ColumnEdit = this.repositoryItemTextEdit1;
            this._coLateAtNightMoney.FieldName = "LateAtNightMoney";
            this._coLateAtNightMoney.Name = "_coLateAtNightMoney";
            this._coLateAtNightMoney.Visible = true;
            this._coLateAtNightMoney.VisibleIndex = 2;
            this._coLateAtNightMoney.Width = 80;
            // 
            // _coTXBT
            // 
            this._coTXBT.Caption = "通宵补贴";
            this._coTXBT.ColumnEdit = this.repositoryItemTextEdit1;
            this._coTXBT.FieldName = "TXBT";
            this._coTXBT.Name = "_coTXBT";
            this._coTXBT.Visible = true;
            this._coTXBT.VisibleIndex = 3;
            this._coTXBT.Width = 79;
            // 
            // _coRemark
            // 
            this._coRemark.Caption = "说明";
            this._coRemark.ColumnEdit = this.repositoryItemMemoEdit1;
            this._coRemark.FieldName = "Remark";
            this._coRemark.Name = "_coRemark";
            this._coRemark.Visible = true;
            this._coRemark.VisibleIndex = 4;
            this._coRemark.Width = 281;
            // 
            // _coID
            // 
            this._coID.Caption = "ID";
            this._coID.FieldName = "ID";
            this._coID.Name = "_coID";
            this._coID.OptionsColumn.AllowEdit = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(506, 12);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "保存";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // _coA
            // 
            this._coA.Caption = "A";
            this._coA.FieldName = "A";
            this._coA.Name = "_coA";
            this._coA.OptionsColumn.AllowEdit = false;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.DisplayFormat.FormatString = "c2";
            this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.EditFormat.FormatString = "c2";
            this.repositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.Mask.EditMask = "c2";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(593, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "请填写每小时的工资金额，通宵补贴为一个通宵需补贴的金额，在计算计时工资时，将以分钟为单位计算。";
            // 
            // frCaicDayMoney
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 345);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frCaicDayMoney";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计时工价设置";
            this.Load += new System.EventHandler(this.frCaicDayMoney_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _coDayMoney;
        private DevExpress.XtraGrid.Columns.GridColumn _coNightMoney;
        private DevExpress.XtraGrid.Columns.GridColumn _coLateAtNightMoney;
        private DevExpress.XtraGrid.Columns.GridColumn _coTXBT;
        private DevExpress.XtraGrid.Columns.GridColumn _coRemark;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private System.Windows.Forms.Label label1;
    }
}