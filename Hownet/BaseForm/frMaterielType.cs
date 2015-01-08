using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseForm
{
    public partial class frMaterielType : DevExpress.XtraEditors.XtraForm
    {
        public frMaterielType()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _id = 0;
        public frMaterielType(BasicClass.cResult cr, int ID)
            : this()
        {
            r = cr;
            _id = ID;
        }
        private BindingSource bs = new BindingSource();
        private DataTable dt = new DataTable();
        private bool _isEdit = false;
        private int _attributeID = 0;
        private string bll = "Hownet.BLL.MaterielType";
        private int _parentID = 0;
        private int _maxID = 0;
        private bool _isAdd = false;
        private int _oldParentID = 0;

        private void frColor_Load(object sender, EventArgs e)
        {
            dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
            treeList1.DataSource = dt;
            _maxID = int.Parse(dt.Rows[dt.Rows.Count - 1]["ID"].ToString()) + 1;
            treeList1.ExpandAll();
            //if (int.Parse(BasicClass.UserInfo.UserPU) == 1)
            //{
            //    _sbOK.Enabled=_barAdd.Enabled = _barDel.Enabled = _barSave.Enabled = _barEdit.Enabled = false;
            //}
        }
        private bool Save()
        {
            bool t = true;
            try
            {
                treeList1.CloseEditor();
                DataTable dtt = dt.Clone();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int a = int.Parse(dt.Rows[i]["A"].ToString());
                    if (a > 1)
                    {
                        dtt.Clear();
                        dtt.Rows.Add(dt.Rows[i].ItemArray);
                        if (a == 2)
                            BasicClass.GetDataSet.UpData(bll,  dtt);
                        else if (a == 3)
                            dt.Rows[i]["ID"] = BasicClass.GetDataSet.Add(bll, dtt);
                        dt.Rows[i]["A"] = 1;
                    }
                }
                _isEdit = false;
            }
            catch (Exception ex)
            {
                t = false;
            }
            return t;
        }
        private void _barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode != null)
            {
                _teParent.Text = treeList1.FocusedNode.GetValue(_trName).ToString();
                _teName.Text = "";
                _teRemark.Text = "";
                if (treeList1.FocusedNode.HasChildren)
                {
                    _teSn.Text = "0" + (Int64.Parse(treeList1.FocusedNode.LastNode.GetValue(_trSn).ToString()) + 1).ToString();
                }
                else
                {
                    _teSn.Text = treeList1.FocusedNode.GetValue(_trSn).ToString() + "01";
                }
                _sbOK.Enabled = true;
                _isAdd = true;
                _parentID = int.Parse(treeList1.FocusedNode.GetValue(_trID).ToString());
                _attributeID = int.Parse(treeList1.FocusedNode.GetValue(_trAttributeID).ToString());
            }
        }

        private void _barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode.Level > 0 &&int.Parse( treeList1.FocusedNode.GetValue(_trID).ToString())>10)
                _sbOK.Enabled = true;
        }

        private void _barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ////检测该记录是否被使用
            int rID = int.Parse(treeList1.FocusedNode.GetValue(_trID).ToString());
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                if (rID > 0)
                {
                    DataRow[] drs = dt.Select("(ID=" + rID + ")");
                    if (drs.Length > 0)
                    {
                        DataTable dttt = dt.Clone();
                        dttt.Rows.Add(drs[0]);
                        dttt.Rows[0]["IsEnd"] = 1;
                        BasicClass.GetDataSet.UpData(bll, dttt);
                    }
                }
            }
        }

        private void _barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            treeList1.CloseEditor();
            if (Save())
                XtraMessageBox.Show("保存成功");
        }

        private void _barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void _barExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void _barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_isEdit)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("记录月修改，是否保存？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    Save();
                }
                _isEdit = false;
            }
            this.Close();
        }

        private void frColor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isEdit)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("记录被修改，是否保存？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    Save();
                }
            }
            GetID();
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null)
            {
                treeList1.OptionsBehavior.DragNodes = (e.Node.Level > 0);
                _oldParentID = int.Parse(treeList1.FocusedNode.GetValue(_trParentID).ToString());
                _teName.Text = treeList1.FocusedNode.GetValue(_trName).ToString();
                _teRemark.Text = treeList1.FocusedNode.GetValue(_trRemark).ToString();
                _teSn.Text = treeList1.FocusedNode.GetValue(_trSn).ToString();
                if (treeList1.FocusedNode.Level > 0)
                    _teParent.Text = treeList1.FocusedNode.ParentNode.GetValue(_trName).ToString();
                else
                    _teParent.Text = "";
            }
            _sbOK.Enabled = false;
        }

        private void _sbOK_Click(object sender, EventArgs e)
        {
            string ssn = _teName.Text.Trim();
            if (ssn.Length == 0)
                return;
            if (ssn == "钢弓" || ssn == "透明背带" || ssn == "棉碗" || ssn == "成品肩带" || ssn == "胶骨")
            {
                XtraMessageBox.Show("不能再添加默认物料类别！");
                _teName.Text = string.Empty;
                return;
            }
            if (_isAdd)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = _maxID;
                dr["A"] = 3;
                dr["IsUse"] = false;
                dr["IsEnd"] = 0;
                dr["Sn"] = _teSn.Text.Trim();
                dr["Name"] = _teName.Text.Trim();
                dr["Remark"] = _teRemark.Text.Trim();
                dr["ParentID"] = _parentID;
                dr["AttributeID"] = _attributeID;
                dt.Rows.Add(dr);
                treeList1.DataSource = dt;
                treeList1.ExpandAll();
                _maxID += 1;
                _isAdd = false;
            }
            else
            {
                treeList1.FocusedNode.SetValue(_trName, _teName.Text.Trim());
                treeList1.FocusedNode.SetValue(_trRemark, _teRemark.Text.Trim());
                if (treeList1.FocusedNode.GetValue(_trA).ToString() == "1")
                    treeList1.FocusedNode.SetValue(_trA, 2);
            }
            _isEdit = true;
            _sbOK.Enabled = false;
        }
        private void treeList1_AfterDragNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            int a = int.Parse(treeList1.FocusedNode.GetValue(_trParentID).ToString());
            if (a == 0 || _attributeID != int.Parse(treeList1.FocusedNode.ParentNode.GetValue(_trAttributeID).ToString()))
            {
                treeList1.FocusedNode.SetValue(_trParentID, _oldParentID);
                treeList1.CloseEditor();
                treeList1.ClearNodes();
                treeList1.DataSource = dt;
                treeList1.ExpandAll();
            }
            else
            {
                e.Node.SetValue(_trAttributeID, e.Node.RootNode.GetValue(_trAttributeID));
                if (e.Node.GetValue(_trA).ToString() == "1")
                    e.Node.SetValue(_trA, 2);
                _isEdit = true;
            }
        }

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            if (_id != 0)
            {
                this.Close();
            }
        }
        private void GetID()
        {
            if (_id != 0)
            {
                Save();
                if (treeList1.FocusedNode!=null)
                {
                    int _mID = int.Parse(treeList1.FocusedNode.GetValue(_trID).ToString());
                    if (_mID > 0)
                    {
                        r.ChangeText(_mID.ToString());
                    }
                }
            }
        }
    }
}