namespace Hownet.BaseForm
{
    partial class frMaterielType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frMaterielType));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this._barAdd = new DevExpress.XtraBars.BarButtonItem();
            this._barEdit = new DevExpress.XtraBars.BarButtonItem();
            this._barDel = new DevExpress.XtraBars.BarButtonItem();
            this._barSave = new DevExpress.XtraBars.BarButtonItem();
            this._barPrint = new DevExpress.XtraBars.BarButtonItem();
            this._barExcel = new DevExpress.XtraBars.BarButtonItem();
            this._barExit = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this._sbOK = new DevExpress.XtraEditors.SimpleButton();
            this._teParent = new DevExpress.XtraEditors.TextEdit();
            this._teRemark = new DevExpress.XtraEditors.TextEdit();
            this._teName = new DevExpress.XtraEditors.TextEdit();
            this._teSn = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this._trID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this._trParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this._trSn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this._trName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this._trRemark = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this._coIsUse = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this._trIsEnd = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this._trA = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this._trAttributeID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._teParent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._teRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._teName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._teSn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this._barAdd,
            this._barEdit,
            this._barDel,
            this._barSave,
            this._barPrint,
            this._barExcel,
            this._barExit});
            this.barManager1.MaxItemId = 7;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barDel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barExcel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this._barExit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // _barAdd
            // 
            this._barAdd.Caption = "新增";
            this._barAdd.Id = 0;
            this._barAdd.ImageIndex = 6;
            this._barAdd.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this._barAdd.Name = "_barAdd";
            this._barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barAdd_ItemClick);
            // 
            // _barEdit
            // 
            this._barEdit.Caption = "编辑";
            this._barEdit.Id = 1;
            this._barEdit.ImageIndex = 0;
            this._barEdit.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2);
            this._barEdit.Name = "_barEdit";
            this._barEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barEdit_ItemClick);
            // 
            // _barDel
            // 
            this._barDel.Caption = "删除";
            this._barDel.Enabled = false;
            this._barDel.Id = 2;
            this._barDel.ImageIndex = 3;
            this._barDel.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D));
            this._barDel.Name = "_barDel";
            this._barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this._barDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barDel_ItemClick);
            // 
            // _barSave
            // 
            this._barSave.Caption = "保存";
            this._barSave.Id = 3;
            this._barSave.ImageIndex = 5;
            this._barSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this._barSave.Name = "_barSave";
            this._barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barSave_ItemClick);
            // 
            // _barPrint
            // 
            this._barPrint.Caption = "打印";
            this._barPrint.Id = 4;
            this._barPrint.ImageIndex = 4;
            this._barPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P));
            this._barPrint.Name = "_barPrint";
            this._barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barPrint_ItemClick);
            // 
            // _barExcel
            // 
            this._barExcel.Caption = "导出Excel";
            this._barExcel.Id = 5;
            this._barExcel.ImageIndex = 1;
            this._barExcel.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E));
            this._barExcel.Name = "_barExcel";
            this._barExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barExcel_ItemClick);
            // 
            // _barExit
            // 
            this._barExit.Caption = "关闭";
            this._barExit.Id = 6;
            this._barExit.ImageIndex = 2;
            this._barExit.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q));
            this._barExit.Name = "_barExit";
            this._barExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._barExit_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(568, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 431);
            this.barDockControlBottom.Size = new System.Drawing.Size(568, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 400);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(568, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 400);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "EditTableHS.png");
            this.imageList1.Images.SetKeyName(1, "ExportExcel.png");
            this.imageList1.Images.SetKeyName(2, "GroupInkClose.png");
            this.imageList1.Images.SetKeyName(3, "GroupMacroRows.png");
            this.imageList1.Images.SetKeyName(4, "PrintDialogAccess.png");
            this.imageList1.Images.SetKeyName(5, "SaveAndClose.png");
            this.imageList1.Images.SetKeyName(6, "TableInsertCellsDialog.png");
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this._sbOK);
            this.panelControl1.Controls.Add(this._teParent);
            this.panelControl1.Controls.Add(this._teRemark);
            this.panelControl1.Controls.Add(this._teName);
            this.panelControl1.Controls.Add(this._teSn);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(320, 31);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(248, 400);
            this.panelControl1.TabIndex = 4;
            // 
            // _sbOK
            // 
            this._sbOK.Location = new System.Drawing.Point(78, 133);
            this._sbOK.Name = "_sbOK";
            this._sbOK.Size = new System.Drawing.Size(75, 23);
            this._sbOK.TabIndex = 8;
            this._sbOK.Text = "确  定";
            this._sbOK.Click += new System.EventHandler(this._sbOK_Click);
            // 
            // _teParent
            // 
            this._teParent.Location = new System.Drawing.Point(54, 88);
            this._teParent.MenuManager = this.barManager1;
            this._teParent.Name = "_teParent";
            this._teParent.Properties.ReadOnly = true;
            this._teParent.Size = new System.Drawing.Size(182, 21);
            this._teParent.TabIndex = 7;
            // 
            // _teRemark
            // 
            this._teRemark.Location = new System.Drawing.Point(54, 64);
            this._teRemark.MenuManager = this.barManager1;
            this._teRemark.Name = "_teRemark";
            this._teRemark.Size = new System.Drawing.Size(182, 21);
            this._teRemark.TabIndex = 6;
            // 
            // _teName
            // 
            this._teName.Location = new System.Drawing.Point(54, 40);
            this._teName.MenuManager = this.barManager1;
            this._teName.Name = "_teName";
            this._teName.Size = new System.Drawing.Size(182, 21);
            this._teName.TabIndex = 5;
            // 
            // _teSn
            // 
            this._teSn.Location = new System.Drawing.Point(54, 16);
            this._teSn.MenuManager = this.barManager1;
            this._teSn.Name = "_teSn";
            this._teSn.Properties.ReadOnly = true;
            this._teSn.Size = new System.Drawing.Size(182, 21);
            this._teSn.TabIndex = 4;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(21, 91);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "上级：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(21, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "说明：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 43);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "名称：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(21, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "编号：";
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this._trID,
            this._trParentID,
            this._trSn,
            this._trName,
            this._trRemark,
            this._coIsUse,
            this._trIsEnd,
            this._trA,
            this._trAttributeID});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 31);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.DragNodes = true;
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsBehavior.ExpandNodesOnIncrementalSearch = true;
            this.treeList1.Size = new System.Drawing.Size(320, 400);
            this.treeList1.TabIndex = 5;
            this.treeList1.AfterDragNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList1_AfterDragNode);
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            this.treeList1.DoubleClick += new System.EventHandler(this.treeList1_DoubleClick);
            // 
            // _trID
            // 
            this._trID.Caption = "ID";
            this._trID.FieldName = "ID";
            this._trID.Name = "_trID";
            this._trID.OptionsColumn.AllowEdit = false;
            this._trID.OptionsColumn.AllowSort = false;
            // 
            // _trParentID
            // 
            this._trParentID.Caption = "ParentID";
            this._trParentID.FieldName = "ParentID";
            this._trParentID.Name = "_trParentID";
            this._trParentID.OptionsColumn.AllowEdit = false;
            this._trParentID.OptionsColumn.AllowSort = false;
            // 
            // _trSn
            // 
            this._trSn.Caption = "编号";
            this._trSn.FieldName = "Sn";
            this._trSn.Name = "_trSn";
            this._trSn.OptionsColumn.AllowSort = false;
            this._trSn.Visible = true;
            this._trSn.VisibleIndex = 1;
            this._trSn.Width = 80;
            // 
            // _trName
            // 
            this._trName.Caption = "类别名";
            this._trName.FieldName = "Name";
            this._trName.Name = "_trName";
            this._trName.OptionsColumn.AllowSort = false;
            this._trName.Visible = true;
            this._trName.VisibleIndex = 0;
            this._trName.Width = 98;
            // 
            // _trRemark
            // 
            this._trRemark.Caption = "说明";
            this._trRemark.FieldName = "Remark";
            this._trRemark.Name = "_trRemark";
            this._trRemark.OptionsColumn.AllowSort = false;
            this._trRemark.Visible = true;
            this._trRemark.VisibleIndex = 2;
            this._trRemark.Width = 121;
            // 
            // _coIsUse
            // 
            this._coIsUse.Caption = "IsUse";
            this._coIsUse.FieldName = "IsUse";
            this._coIsUse.Name = "_coIsUse";
            this._coIsUse.OptionsColumn.AllowEdit = false;
            this._coIsUse.OptionsColumn.AllowSort = false;
            // 
            // _trIsEnd
            // 
            this._trIsEnd.Caption = "IsEnd";
            this._trIsEnd.FieldName = "IsEnd";
            this._trIsEnd.Name = "_trIsEnd";
            this._trIsEnd.OptionsColumn.AllowEdit = false;
            this._trIsEnd.OptionsColumn.AllowSort = false;
            // 
            // _trA
            // 
            this._trA.Caption = "A";
            this._trA.FieldName = "A";
            this._trA.Name = "_trA";
            this._trA.OptionsColumn.AllowEdit = false;
            this._trA.OptionsColumn.AllowSort = false;
            // 
            // _trAttributeID
            // 
            this._trAttributeID.Caption = "AttributeID";
            this._trAttributeID.FieldName = "AttributeID";
            this._trAttributeID.Name = "_trAttributeID";
            this._trAttributeID.OptionsColumn.AllowEdit = false;
            // 
            // frMaterielType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 458);
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frMaterielType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物料类别";
            this.Load += new System.EventHandler(this.frColor_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frColor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._teParent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._teRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._teName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._teSn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem _barAdd;
        private DevExpress.XtraBars.BarButtonItem _barEdit;
        private DevExpress.XtraBars.BarButtonItem _barDel;
        private DevExpress.XtraBars.BarButtonItem _barSave;
        private DevExpress.XtraBars.BarButtonItem _barPrint;
        private DevExpress.XtraBars.BarButtonItem _barExcel;
        private DevExpress.XtraBars.BarButtonItem _barExit;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn _trID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn _trParentID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn _trSn;
        private DevExpress.XtraTreeList.Columns.TreeListColumn _trName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn _trRemark;
        private DevExpress.XtraTreeList.Columns.TreeListColumn _coIsUse;
        private DevExpress.XtraTreeList.Columns.TreeListColumn _trIsEnd;
        private DevExpress.XtraTreeList.Columns.TreeListColumn _trA;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit _teParent;
        private DevExpress.XtraEditors.TextEdit _teRemark;
        private DevExpress.XtraEditors.TextEdit _teName;
        private DevExpress.XtraEditors.TextEdit _teSn;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton _sbOK;
        private DevExpress.XtraTreeList.Columns.TreeListColumn _trAttributeID;
    }
}