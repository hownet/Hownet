using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Pay
{
    public partial class frOrdering : DevExpress.XtraEditors.XtraForm
    {
        public frOrdering()
        {
            InitializeComponent();
        }
        string bll = "Hownet.BLL.OrderingList";
        DateTime dt1;
        DateTime dt2;
        DataTable dtEmp = new DataTable();
        DataTable dtDep = new DataTable();
        DataTable dtList = new DataTable();
        private void frOrdering_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = dateEdit2.EditValue = BasicClass.GetDataSet.GetDateTime().Date.AddDays(BasicClass.BasicFile.liST[0].OrderDays);
            dtEmp = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetAllList", null).Tables[0];
            DataRow dr = dtEmp.NewRow();
            dr["ID"] = 0;
            dr["Name"] = string.Empty;
            dtEmp.Rows.Add(dr);
            dtEmp.DefaultView.Sort = "ID";
            _leEmployeeID.Properties.DataSource = dtEmp.DefaultView;
            _leEmployeeID.EditValue = 0;
            dtDep = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetAllList", null).Tables[0];
            DataRow drr = dtDep.NewRow();
            drr["ID"] = 0;
            drr["Name"] = string.Empty;
            dtDep.Rows.Add(drr);
            dtDep.DefaultView.Sort = "ID";
            _leDeparment.Properties.DataSource = dtDep.DefaultView;
            _leDeparment.EditValue = 0;
            _reDeparmentID.DataSource = dtDep;
            _reEmployee.DataSource = dtEmp;
           string per = BasicClass.BasicFile.GetPermissions(this.Text);
           if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
               simpleButton3.Enabled = false;
           if (per.IndexOf(((int)BasicClass.Enums.Operation.Verify).ToString()) == -1)
               simpleButton4.Enabled = false;
           dateEdit3.EditValue = BasicClass.GetDataSet.GetDateTime();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GetOrderList();
        }
        private void GetOrderList()
        {
            dt1 = Convert.ToDateTime(dateEdit1.EditValue).AddHours(-1);
            dt2 = Convert.ToDateTime(dateEdit2.EditValue).AddDays(1);
            dtList = BasicClass.GetDataSet.GetDS(bll, "GetOderList", new object[] { dt1, dt2, Convert.ToInt32(_leDeparment.EditValue), Convert.ToInt32(_leEmployeeID.EditValue) }).Tables[0];
            gridControl1.DataSource = dtList;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                advBandedGridView1.ExportToXls(fileName);
                OpenFile(fileName);
            }
        }
        private string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            string name = Application.ProductName;
            int n = name.LastIndexOf(".") + 1;
            if (n > 0) name = name.Substring(n, name.Length - n);
            dlg.Title = "导出" + title;
            dlg.FileName = name;
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }
        private void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("是否打开刚才导出的文档？", "导出Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "未找到导出的文档", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(_leEmployeeID.EditValue) == 0)
            {
                XtraMessageBox.Show("请选择员工！");
                return;
            }
            if (!dateEdit1.EditValue.Equals( dateEdit2.EditValue))
            {
                XtraMessageBox.Show("只能对一天时间操作！");
                return;
            }
            DateTime dtSet=Convert.ToDateTime(dateEdit2.EditValue);
            DateTime dtTody = BasicClass.GetDataSet.GetDateTime();
            if (Convert.ToInt32( dtTody.TimeOfDay.ToString().Substring(0, 2)) > 19)
            {
                XtraMessageBox.Show("时间太晚，不能继续！");
                return;
            }
            if (BasicClass.BasicFile.liST[0].OrderDays > 0)
            {
                if (dtSet < dtTody)
                {
                    DataTable dtTem = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetList", new object[] { "(ID=" + _leEmployeeID.EditValue + ")" }).Tables[0];
                    if (Convert.ToDateTime(dtTem.Rows[0]["AccDate"]).Date < dtSet)
                    {

                        XtraMessageBox.Show("只能对以后的日期进行操作！");
                        return;
                    }
                }
            }
            int _oderCount = 0;
            DataTable dtt = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] {"(EmployeeID="+_leEmployeeID.EditValue+") And (DateTime='"+Convert.ToDateTime(dateEdit1.EditValue)+"')" }).Tables[0];
            if (dtt.Rows.Count == 0)
            {
                if (radioButton2.Checked)
                {
                    XtraMessageBox.Show("没有订餐记录！");
                    return;
                }
                else
                {
                    if ((!checkBox3.Checked) && (!checkBox4.Checked) && (!checkBox5.Checked))
                    {
                        XtraMessageBox.Show("请选择订哪一餐饭！");
                        return;
                    }
                    DataRow dr = dtt.NewRow();
                    dr[0] = 1;
                    dr[1] = 0;
                    dr[2] = _leEmployeeID.EditValue;
                    dr[3] = dateEdit1.EditValue;
                    if (checkBox3.Checked)
                    {
                        dr[4] = 1;
                        dr[6] = dtTody;
                        _oderCount += 1;
                    }
                    else
                    {
                        dr[4] = 0;
                        dr[6] = Convert.ToDateTime("1900-1-1");
                    }
                    dr[5] = 0;
                    dr[7] = Convert.ToDateTime("1900-1-1");
                    if (checkBox4.Checked)
                    {
                        dr[8] = 1;
                        dr[10] = dtTody;
                        _oderCount += 1;
                    }
                    else
                    {
                        dr[8] = 0;
                        dr[10] = Convert.ToDateTime("1900-1-1");
                    }
                    dr[9] = 0;
                    dr[11] = Convert.ToDateTime("1900-1-1");
                    if (checkBox5.Checked)
                    {
                        dr[12] = 1;
                        dr[14] = dtTody;
                        _oderCount += 1;
                    }
                    else
                    {
                        dr[12] = 0;
                        dr[14] = Convert.ToDateTime("1900-1-1");
                    }
                    dr[13] = 0;
                    dr[15] = Convert.ToDateTime("1900-1-1");
                    dr[16] = _oderCount;
                    dr[17] = 0;
                    dtt.Rows.Add(dr);
                    BasicClass.GetDataSet.Add(bll, dtt);
                }
            }
            else
            {
                if (radioButton1.Checked)
                {
                    if (checkBox3.Checked)
                    {
                        dtt.Rows[0][4] = 1;
                        dtt.Rows[0][6] = dtTody;
                    }
                    if (checkBox4.Checked)
                    {
                        dtt.Rows[0][8] = 1;
                        dtt.Rows[0][10] = dtTody;
                    }
                    if (checkBox5.Checked)
                    {
                        dtt.Rows[0][12] = 1;
                        dtt.Rows[0][14] = dtTody;
                    }
                }
                else
                {
                    if (checkBox3.Checked)
                    {
                        dtt.Rows[0][4] = 0;
                        dtt.Rows[0][6] = Convert.ToDateTime("1900-1-1");
                    }
                    if (checkBox4.Checked)
                    {
                        dtt.Rows[0][8] = 0;
                        dtt.Rows[0][10] = Convert.ToDateTime("1900-1-1");
                    }
                    if (checkBox5.Checked)
                    {
                        dtt.Rows[0][12] = 0;
                        dtt.Rows[0][14] = Convert.ToDateTime("1900-1-1");
                    }
                }
                dtt.Rows[0]["OrderCount"] = Convert.ToInt32(dtt.Rows[0][4]) + Convert.ToInt32(dtt.Rows[0][8]) + Convert.ToInt32(dtt.Rows[0][12]);
                dtt.AcceptChanges();
                BasicClass.GetDataSet.UpData(bll, dtt);
            }
            GetOrderList();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (BasicClass.BasicFile.liST[0].OrderNeedEat)
            {
                BasicClass.GetDataSet.ExecSql("Hownet.BLL.OrderingList", "CaicMoney", new object[] { BasicClass.GetDataSet.GetDateTime().Date });
            }
            else
            {
                BasicClass.GetDataSet.ExecSql("Hownet.BLL.OrderingList", "CaicMoneyNoEat", new object[] { BasicClass.GetDataSet.GetDateTime().Date });
            }
            GetOrderList();
        }
        //打印午餐桌位图
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            DataSet dsOrderList = BasicClass.GetDataSet.GetDS(bll, "GetTabeList", new object[] { dateEdit3.DateTime.Date, radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value.ToString() });
            //MiniEmp.Name, MiniEmp.TableID
            //MiniEmp.TableID, Deparment.CountEmployee, COUNT(OrderingList.ID) AS CountEmp, TabName 
            //  MAX(Deparment.CountEmployee) AS Expr1, OtherType.Name
            DataTable dtt = new DataTable();
            dtt.Columns.Add("餐桌名", typeof(string));//餐桌名
            dtt.Columns.Add("TableID", typeof(int));//餐桌ID
            dtt.Columns.Add("MaxEmp", typeof(int));//可容纳人数
            dtt.Columns.Add("CountEmp", typeof(int));//已安排人数
            int MaxEmp=Convert.ToInt32(dsOrderList.Tables[2].Rows[0]["Expr1"]);
            for (int i = 0; i <MaxEmp ; i++)
            {
                dtt.Columns.Add("员工：" + (i + 1).ToString(), typeof(string));
            }
            DataRow[] drs;
            for (int i = 0; i < dsOrderList.Tables[1].Rows.Count; i++)
            {
                DataRow dr = dtt.NewRow();
                dr["餐桌名"] = dsOrderList.Tables[1].Rows[i]["TabName"];
                dr["TableID"] = dsOrderList.Tables[1].Rows[i]["TableID"];
                dr["MaxEmp"] = dsOrderList.Tables[1].Rows[i]["CountEmployee"];
                dr["CountEmp"] = dsOrderList.Tables[1].Rows[i]["CountEmp"];
                drs = dsOrderList.Tables[0].Select("(TableID=" + dr["TableID"] + ")");
                for (int j = 0; j < drs.Length; j++)
                {
                    dr[4 + j] = drs[j]["Name"];
                }
                dtt.Rows.Add(dr);
            }
            DataSet ds = new DataSet();
            DataTable dtMain = new DataTable();
            dtMain.Columns.Add("Date", typeof(string));
            dtMain.Rows.Add(dateEdit3.DateTime.ToString("yyyy年MM月dd日") + radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Description);
            dtMain.TableName = "Main";
            dtt.TableName = "Info";
            ds.Tables.Add(dtMain);
            ds.Tables.Add(dtt);
            BaseForm.PrintClass.OrderList(ds);
            //gridControl2.DataSource = dtt;
            //gridView1.Columns["TableID"].Visible = false;
            //gridView1.Columns["MaxEmp"].Visible = false;
            //gridView1.Columns["CountEmp"].Visible = false;
            //string fileName = ShowSaveFileDialog("Excel文档", "Microsoft Excel|*.xls");
            //if (fileName != "")
            //{
            //    gridView1.ExportToXls(fileName);
            //    OpenFile(fileName);
            //}
        }
    }
}