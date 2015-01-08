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
    public partial class frZaiYao : DevExpress.XtraEditors.XtraForm
    {
        public frZaiYao()
        {
            InitializeComponent();
        }
        string bll = "Hownet.BLL.Bas_LBXM";
        DataTable dt = new DataTable();
        BasicClass.cResult r = new BasicClass.cResult();
        int ID = 0;
        public frZaiYao(BasicClass.cResult cr, int KMID)
            : this()
        {
            r = cr;
            ID = KMID;
        }
        private void frKJKM_Load(object sender, EventArgs e)
        {
            if (ID == 0)
            {
                dt = BasicClass.GetDataSet.GetDS(bll, "GetTreeList", null).Tables[0];
                treeList1.DataSource = dt;
                treeList1.Columns["A"].Visible = false;
                treeList1.Columns["IDS"].Visible = false;
                treeList1.Columns["Name"].Caption = "项目名称";

                // treeList1.Columns["编号"].Visible = true;
                treeList1.ExpandAll();
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
                XtraMessageBox.Show("顶级项目不能修改");
            }
            else
            {
                if (treeList1.FocusedNode == null)
                {
                    return;
                }
                BasicClass.cResult r = new BasicClass.cResult();
                r.RowChanged += new BasicClass.RowChangedHandler(r_RowChanged);
                Form fr = new frZaiYaoOne(r, dt, Convert.ToInt32(treeList1.FocusedNode.ParentNode.GetValue("ID")), Convert.ToInt32(treeList1.FocusedNode.GetValue("ID")));
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
            BasicClass.cResult r = new BasicClass.cResult();
            r.RowChanged += new BasicClass.RowChangedHandler(r_RowChanged);
            Form fr = new frZaiYaoOne(r, dt, Convert.ToInt32(treeList1.FocusedNode.GetValue("ID")), 0);
            fr.ShowDialog();
        }

        void r_RowChanged(DataTable dtt)
        {
            dt = dtt;
            treeList1.DataSource = dt;
            treeList1.ExpandAll();
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
                        treeList1.DataSource = dt.DefaultView;
                        treeList1.Columns["A"].Visible = false;
                      //  treeList1.Columns["IDS"].Visible = false;
                        treeList1.Columns["Name"].Caption = "项目名称";

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
            if (ID == 0)
                return;
            if (e.KeyCode == Keys.Enter)
            {
                if (treeList1.FocusedNode != null)
                {
                    string s = treeList1.FocusedNode.GetValue("Name").ToString();
                    ID =Convert.ToInt32(treeList1.FocusedNode.GetValue("ID"));
                    int i = treeList1.FocusedNode.Level;
                    while (i > 1)
                    {
                        treeList1.FocusedNode = treeList1.FocusedNode.ParentNode;
                        i = treeList1.FocusedNode.Level;
                        s = treeList1.FocusedNode.GetValue("Name").ToString() + "-" + s;
                    }
                    DataTable dtTem = new DataTable();
                    dtTem.Columns.Add("ID", typeof(string));
                    dtTem.Columns.Add("Name", typeof(string));
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