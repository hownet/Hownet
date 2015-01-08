using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Finance
{
    public partial class frKJKM : DevExpress.XtraEditors.XtraForm
    {
        public frKJKM()
        {
            InitializeComponent();
        }
        string bll = "Hownet.BLL.Bas_KJKM";
        DataTable dt = new DataTable();
        BasicClass.cResult r = new BasicClass.cResult();
        int ID = 0;
        public frKJKM(BasicClass.cResult cr, int KMID)
            : this()
        {
            r = cr;
            ID = KMID;
        }
        private void frKJKM_Load(object sender, EventArgs e)
        {
            BasicClass.GetDataSet.ExecSql(bll, "UpParentID", null);
            dt = BasicClass.GetDataSet.GetDS(bll, "GetTreeList", null).Tables[0];
            treeList1.DataSource = dt;
            treeList1.Columns["A"].Visible = false;
            treeList1.Columns["ID"].Visible = false;
            treeList1.Columns["Remark"].Visible = false;
            treeList1.Columns["Orders"].Caption = "顺序号";
            treeList1.Columns["Name"].Caption = "会计科目名称";
            treeList1.Columns["Nums"].Caption = "科目编号";
            treeList1.Columns["Money"].Caption = "余额";
            treeList1.Columns["CompanyID"].Visible = false;
            treeList1.Columns["DebitMoney"].Visible = false;
            treeList1.Columns["CreditMoney"].Visible = false;
            treeList1.Columns["MoneyType"].Visible = false;
            // treeList1.Columns["编号"].Visible = true;
            treeList1.ExpandAll();
            if (ID == 0)
            {
               
                panel1.Visible = false;
            }
            else
            {
                panel1.Focus();
                textEdit1.Focus();
            }
            treeList1.OptionsBehavior.Editable = false;

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode.Level == 0)
            {
                XtraMessageBox.Show("顶级科目不能修改");
            }
            else
            {
                if (treeList1.FocusedNode == null)
                {
                    return;
                }
                BasicClass.cResult r = new BasicClass.cResult();
                r.RowChanged += new BasicClass.RowChangedHandler(r_RowChanged);
                object o = treeList1.FocusedNode.GetValue("Num");
                object j = treeList1.FocusedNode.ParentNode.GetValue("Num");
                Form fr = new frKJKMOne(r, dt, Convert.ToInt32(treeList1.FocusedNode.ParentNode.GetValue("Num")), Convert.ToInt32(treeList1.FocusedNode.GetValue("Num")));
                fr.ShowDialog();
            }
        }
        //添加
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            if (treeList1.FocusedNode.Level == 2)
            {
                XtraMessageBox.Show("最多只能三层。");
                return;
            }
            BasicClass.cResult r = new BasicClass.cResult();
            r.RowChanged += new BasicClass.RowChangedHandler(r_RowChanged);
            Form fr = new frKJKMOne(r, dt, Convert.ToInt32(treeList1.FocusedNode.GetValue("Num")), 0);
            fr.ShowDialog();
        }

        void r_RowChanged(DataTable dt)
        {
            dt = BasicClass.GetDataSet.GetDS(bll, "GetTreeList", null).Tables[0];
            treeList1.DataSource = dt;
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textEdit1.Text.Trim().Length > 0)
                {
                    try
                    {
                        string s = "%" + textEdit1.Text.Trim() + "%";
                        s = s.Replace("'", "''");
                        dt = BasicClass.GetDataSet.GetDS(bll, "GetFilterList", new object[] { s }).Tables[0];
                        dt.DefaultView.Sort = "Orders";
                        treeList1.DataSource = dt.DefaultView;
                        treeList1.Columns["A"].Visible = false;
                       // treeList1.Columns["ID"].Visible = false;
                       // treeList1.Columns["Remark"].Visible = false;
                        treeList1.Columns["Orders"].Caption = "顺序号";
                        treeList1.Columns["Name"].Caption = "会计科目名称";
                        treeList1.Columns["Nums"].Caption = "科目编号";
                        treeList1.Columns["Money"].Caption = "余额";
                        treeList1.Columns["CompanyID"].Visible = false;
                        treeList1.Columns["DebitMoney"].Visible = false;
                        treeList1.Columns["CreditMoney"].Visible = false;
                        treeList1.Columns["MoneyType"].Visible = false;
                        treeList1.ExpandAll();

                        treeList1.OptionsBehavior.Editable = false;
                        treeList1.Focus();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        private void treeList1_KeyDown(object sender, KeyEventArgs e)
        {
            if (ID ==0)
                return;
            if (e.KeyCode == Keys.Enter)
            {
                if (treeList1.FocusedNode != null)
                {
                    string s = treeList1.FocusedNode.GetValue("Name").ToString();
                    ID =Convert.ToInt32(treeList1.FocusedNode.GetValue("ID"));
                    int i = treeList1.FocusedNode.Level;
                    int _lID = 0;
                    string _lName = string.Empty;
                    if (i > 1)
                    {
                        treeList1.FocusedNode = treeList1.FocusedNode.ParentNode;
                        i = treeList1.FocusedNode.Level;
                        //s = treeList1.FocusedNode.GetValue("Name").ToString() + "-" + s;
                        _lName = treeList1.FocusedNode.GetValue("Name").ToString();
                        _lID = Convert.ToInt32(treeList1.FocusedNode.GetValue("ID"));
                    }
                    DataTable dtTem = new DataTable();
                    dtTem.Columns.Add("ID", typeof(string));
                    dtTem.Columns.Add("Name", typeof(string));
                    dtTem.Rows.Add(_lID, _lName);
                    dtTem.Rows.Add(ID, s);
                    
                    r.RowChang(dtTem);
                    this.Close();
                }
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}