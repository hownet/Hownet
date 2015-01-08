using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.SystemSet
{
    public partial class UserOneForm : DevExpress.XtraEditors.XtraForm
    {
        public UserOneForm()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int ID = 0;
        DataTable dtUser = new DataTable();
        public UserOneForm(BasicClass.cResult r, int id)
            : this()
        {
            this.r = r;
            this.ID = id;
        }
        BasicClass.PassWord bllPW = new BasicClass.PassWord();
        private void UserOneForm_Load(object sender, EventArgs e)
        {

            DataTable dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetList", new object[] { "ParentID=0" }).Tables[0];
            _loDepartment.Properties.DataSource =dt;
            if (ID > 0)
            {
                this.Text = "编辑用户资料";
                dtUser = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetList", new object[] { "(ID=" + ID + ")" }).Tables[0];
                _teSn.EditValue = dtUser.Rows[0]["Name"];
                _teUserName.EditValue = dtUser.Rows[0]["TrueName"];
                _loDepartment.EditValue = dtUser.Rows[0]["DepartmentID"];
                _loJobs.EditValue = dtUser.Rows[0]["JobsID"];
                simpleButton1.Text = "修改";
                //ShowDepartment(ID);
            }
            else if(ID==0)
            {
                this.Text = "添加新用户";
                simpleButton1.Text = "保存";
                textEdit1.Enabled = false;
                dtUser = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetList", new object[] { "(ID=0)" }).Tables[0];
                dtUser.Rows.Add(dtUser.NewRow());
                dtUser.Rows[0]["ID"] = 0;
                dtUser.Rows[0]["A"] = 1;
            }
            else if (ID < 0)
            {
                this.Text = "修改密码";
                dtUser = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetList", new object[] { "(ID=" + ID*-1 + ")" }).Tables[0];
                _teSn.EditValue = dtUser.Rows[0]["Name"];
                _teUserName.EditValue = dtUser.Rows[0]["TrueName"];
                _loDepartment.EditValue = dtUser.Rows[0]["DepartmentID"];
                _loJobs.EditValue = dtUser.Rows[0]["JobsID"];
                simpleButton1.Text = "修改";
                _teSn.Enabled =_loDepartment.Enabled=_loJobs.Enabled= false;
            }
        }

        private void _loDepartment_EditValueChanged(object sender, EventArgs e)
        {
            if (_loDepartment.EditValue != null)
            {
                _loJobs.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDepartmentJobs, "GetList", new object[] { "(DepartmentID= " + int.Parse(_loDepartment.EditValue.ToString()) + ")" }).Tables[0];
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                gridView1.CloseEditor();
                //byte[] bb;
                if (ID > -1)
                {
                    if (_teSn.Text.Trim() == "" || _teUserName.Text.Trim() == "")
                    {
                        XtraMessageBox.Show("用户编号、用户名不能为空");
                        return;
                    }
                }
                string pass;
                if (ID > 0)
                {
                    pass = bllPW.Encrypt(textEdit1.Text.Trim(), "howneter");
                    byte[] bb = Convert.FromBase64String(pass);
                    if (!((bool)(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllUsers, "CheckUser", new object[] { ID, bb }))))
                    {
                        XtraMessageBox.Show("原密码错误！");
                        return;
                    }
                }
                else if (ID == 0)
                {
                    if (_tePass.Text.Trim().Length == 0 || !_tePass.EditValue.Equals(_te2Pass.EditValue))
                    {
                        XtraMessageBox.Show("新密码不能为空且两次密码要相同，请重新设置密码！");
                        _te2Pass.EditValue = _tePass.EditValue = null;
                        return;
                    }
                }
                else if (ID < 0)
                {
                    pass = bllPW.Encrypt(textEdit1.Text.Trim(), "howneter");
                    byte[] bb = Convert.FromBase64String(pass);
                    if (!((bool)(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllUsers, "CheckUser", new object[] { ID * -1, bb }))))
                    {
                        XtraMessageBox.Show("原密码错误！");
                        return;
                    }
                    DataTable dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetList", new object[] { "(ID=" + ID * -1 + ")" }).Tables[0];
                    if (dtTem.Rows.Count > 0)
                    {
                        if (_tePass.Text.Trim().Length > 0)
                        {
                            if (!_tePass.EditValue.Equals(_te2Pass.EditValue))
                            {
                                XtraMessageBox.Show("两次密码不一样，请重新设置密码！");
                                _te2Pass.EditValue = _tePass.EditValue = null;
                                return;
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("请输入新密码");
                            return;
                        }
                        dtTem.Rows[0]["Password"] = Convert.FromBase64String(bllPW.Encrypt(_tePass.Text.Trim(), "howneter"));
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllUsers, dtTem);
                        XtraMessageBox.Show("密码已修改，下次登录请使用新密码！");
                        this.Close();
                        return;
                    }
                }
                if (_tePass.Text.Trim().Length > 0)
                {
                    if (!_tePass.EditValue.Equals(_te2Pass.EditValue))
                    {
                        XtraMessageBox.Show("两次密码不一样，请重新设置密码！");
                        _te2Pass.EditValue = _tePass.EditValue = null;
                        this.Close();
                    }
                }
                //if (_loJobs.EditValue == null || _loJobs.EditValue.ToString() == "0")
                //{
                //    XtraMessageBox.Show("请选择该用户所属部门职务！");
                //    return;
                //}
                dtUser.Rows[0]["Name"] = _teSn.EditValue.ToString().Trim();
                dtUser.Rows[0]["TrueName"] = _teUserName.EditValue.ToString().Trim();
                dtUser.Rows[0]["ID"] = ID;
                if (BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetList", new object[] { "(Name='" + _teSn.EditValue.ToString().Trim() + "') AND (ID <> " + ID + ")" }).Tables[0].Rows.Count > 0)
                {
                    XtraMessageBox.Show("编号已存在");
                    _teSn.Focus();
                    return;
                }
                if (ID == 0)
                {
                    dtUser.Rows[0]["Password"] = Convert.FromBase64String(bllPW.Encrypt(_tePass.Text.Trim(), "howneter"));
                }
                else
                {
                    if (_tePass.Text.Trim().Length > 0)
                        dtUser.Rows[0]["Password"] = Convert.FromBase64String(bllPW.Encrypt(_tePass.Text.Trim(), "howneter"));
                }
                if (_loDepartment.EditValue != null)
                    dtUser.Rows[0]["DepartmentID"] = int.Parse(_loDepartment.EditValue.ToString());
                else
                    dtUser.Rows[0]["DepartmentID"] = 0;
                if (_loJobs.EditValue != null)
                    dtUser.Rows[0]["JobsID"] = int.Parse(_loJobs.EditValue.ToString());
                else
                    dtUser.Rows[0]["JobsID"] = 0;
                dtUser.Rows[0]["Phone"] = "";
                dtUser.Rows[0]["Email"] = "";
                dtUser.Rows[0]["State"] = 0;
                if (ID == 0)
                {
                    dtUser.Rows[0]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllUsers, dtUser);
                }
                else
                {
                    //modAU.Password = bllAU.GetModel(ID).Password;
                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllUsers, dtUser);
                }
                //SaveUserAtDepartment(modUI.UserInfoID);
                r.ChangeText("1");
                this.Close();
            }
            catch (Exception ex)
            {
                this.Close();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _loDepartment_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "New")
            {
                //cResult r = new cResult();
                //r.TextChanged += new TextChangedHandler(r_TextChanged);
                //Form fr = new ERP.BaseFile.frDepartment(r, -1);
                //fr.ShowDialog();
            }
        }

        void r_TextChanged(string s)
        {
            //string[] ss = s.Split(',');
            //_loDepartment.Properties.DataSource = bllDep.GetList("").Tables[0];
            //_loDepartment.EditValue = int.Parse(ss[0]);
            //_loJobs.EditValue = int.Parse(ss[1]);
        }
        //private void ShowDepartment(int UserID)
        //{
        //    gridControl1.DataSource = bllDep.GetTypeList("仓库").Tables[0];
        //    for (int i = 0; i < dtUAD.Rows.Count; i++)
        //    {
        //        for (int r = 0; r < gridView1.RowCount; r++)
        //        {
        //            if (gridView1.GetRowCellValue(r, _coID).ToString() == dtUAD.Rows[i]["DepartmentID"].ToString())
        //            {
        //                gridView1.SetRowCellValue(r, _coT, true);
        //                gridView1.SetRowCellValue(r, _coA, 1);
        //                break;
        //            }
        //        }
        //    }
        //    gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView1_CellValueChanged);
        //}

        //void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    if (e.Column != _coA)
        //    {
        //        if (gridView1.GetFocusedRowCellValue(_coA).ToString() == "1")
        //            gridView1.SetFocusedRowCellValue(_coA, 2);
        //        else if (gridView1.GetFocusedRowCellValue(_coA).ToString() == "0")
        //            gridView1.SetFocusedRowCellValue(_coA, 3);
        //    }
        //}
        //private void SaveUserAtDepartment(int UserID)
        //{
        //    Hownet.Model.UserAtDepartment modUAD;
        //    for (int i = 0; i < gridView1.RowCount; i++)
        //    {
        //        int a=int.Parse( gridView1.GetRowCellValue(i,_coA).ToString());
        //        if (a == 2 && (!(bool)(gridView1.GetRowCellValue(i, _coT))))
        //        {
        //            modUAD = new Hownet.Model.UserAtDepartment();
        //            modUAD.DepartmentID = int.Parse(gridView1.GetRowCellValue(i, _coID).ToString());
        //            modUAD.UserID = UserID;
        //            bllUAD.Delete(modUAD);
        //        }
        //        else if (a == 3 && ((bool)(gridView1.GetRowCellValue(i, _coT))))
        //        {
        //            modUAD = new Hownet.Model.UserAtDepartment();
        //            modUAD.DepartmentID = int.Parse(gridView1.GetRowCellValue(i, _coID).ToString());
        //            modUAD.UserID = ID;
        //            bllUAD.Add(modUAD);
        //        }

        //    }
        //}
    }
}