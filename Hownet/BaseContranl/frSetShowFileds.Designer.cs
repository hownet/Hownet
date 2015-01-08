namespace Hownet.BaseContranl
{
    partial class frSetShowFileds
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._coA = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coFileds = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coWidth = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coIsShow = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this._coOrderBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.simpleButton1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.simpleButton2);
            this.splitContainer1.Size = new System.Drawing.Size(484, 61);
            this.splitContainer1.SplitterDistance = 238;
            this.splitContainer1.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleButton1.Location = new System.Drawing.Point(0, 0);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(238, 61);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "保存";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleButton2.Location = new System.Drawing.Point(0, 0);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(242, 61);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "取消";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 61);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(484, 464);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._coA,
            this._coID,
            this._coFileds,
            this._coCName,
            this._coWidth,
            this._coIsShow,
            this._coUserID,
            this._coTypeID,
            this._coOrderBy,
            this.gridColumn10});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 40;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // _coA
            // 
            this._coA.Caption = "A";
            this._coA.FieldName = "A";
            this._coA.Name = "_coA";
            this._coA.OptionsColumn.AllowEdit = false;
            // 
            // _coID
            // 
            this._coID.Caption = "ID";
            this._coID.FieldName = "ID";
            this._coID.Name = "_coID";
            this._coID.OptionsColumn.AllowEdit = false;
            // 
            // _coFileds
            // 
            this._coFileds.Caption = "字段";
            this._coFileds.FieldName = "Fileds";
            this._coFileds.Name = "_coFileds";
            this._coFileds.OptionsColumn.AllowEdit = false;
            // 
            // _coCName
            // 
            this._coCName.Caption = "显示名称";
            this._coCName.FieldName = "CName";
            this._coCName.Name = "_coCName";
            this._coCName.Visible = true;
            this._coCName.VisibleIndex = 0;
            // 
            // _coWidth
            // 
            this._coWidth.Caption = "宽度";
            this._coWidth.FieldName = "Width";
            this._coWidth.Name = "_coWidth";
            this._coWidth.Visible = true;
            this._coWidth.VisibleIndex = 1;
            // 
            // _coIsShow
            // 
            this._coIsShow.Caption = "是否显示";
            this._coIsShow.FieldName = "IsShow";
            this._coIsShow.Name = "_coIsShow";
            this._coIsShow.Visible = true;
            this._coIsShow.VisibleIndex = 2;
            // 
            // _coUserID
            // 
            this._coUserID.Caption = "UserID";
            this._coUserID.FieldName = "UserID";
            this._coUserID.Name = "_coUserID";
            this._coUserID.OptionsColumn.AllowEdit = false;
            // 
            // _coTypeID
            // 
            this._coTypeID.Caption = "TypeID";
            this._coTypeID.FieldName = "TypeID";
            this._coTypeID.Name = "_coTypeID";
            this._coTypeID.OptionsColumn.AllowEdit = false;
            // 
            // _coOrderBy
            // 
            this._coOrderBy.Caption = "显示顺序";
            this._coOrderBy.FieldName = "OrderBy";
            this._coOrderBy.Name = "_coOrderBy";
            this._coOrderBy.Visible = true;
            this._coOrderBy.VisibleIndex = 3;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "gridColumn10";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            // 
            // frSetShowFileds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 525);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frSetShowFileds";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "设置显示字段";
            this.Load += new System.EventHandler(this.frSetShowFileds_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _coA;
        private DevExpress.XtraGrid.Columns.GridColumn _coID;
        private DevExpress.XtraGrid.Columns.GridColumn _coFileds;
        private DevExpress.XtraGrid.Columns.GridColumn _coCName;
        private DevExpress.XtraGrid.Columns.GridColumn _coWidth;
        private DevExpress.XtraGrid.Columns.GridColumn _coIsShow;
        private DevExpress.XtraGrid.Columns.GridColumn _coUserID;
        private DevExpress.XtraGrid.Columns.GridColumn _coTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn _coOrderBy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
    }
}