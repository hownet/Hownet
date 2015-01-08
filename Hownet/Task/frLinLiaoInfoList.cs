using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Task
{
    public partial class frLinLiaoInfoList : DevExpress.XtraEditors.XtraForm
    {
        public frLinLiaoInfoList()
        {
            InitializeComponent();
        }
        int _PTMID = 0;
        int _InfoID = 0;
        int _ManID = 0;
        bool _IsVerify = false;
        string bllRL = "Hownet.BLL.RepertoryList";
        string bll = "Hownet.BLL.StockBackInfoList";
        string bllDep = "Hownet.BLL.Deparment";
        DataTable dtRL = new DataTable();
        DataTable dtDepInfo = new DataTable();
        BasicClass.cResult r = new BasicClass.cResult();
        int _MListID = 0;
        int _DepotID = 0;
        public frLinLiaoInfoList(int PTMID)
            : this()
        {
            _PTMID = PTMID;
        }
        public frLinLiaoInfoList(int MListID, int DepotID, int InfoID, int MainID,bool IsVerify, BasicClass.cResult cr)
            : this()
        {
            _MListID = MListID;
            _DepotID = DepotID;
            _InfoID = InfoID;
            _ManID = MainID;
            _IsVerify = IsVerify;
            r = cr;
        }
        DataTable dtSBIL = new DataTable();
        private void frLinLiaoInfoList_Load(object sender, EventArgs e)
        {
            dtSBIL = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(InfoID=" + _InfoID + ")" }).Tables[0];
            dtDepInfo = BasicClass.GetDataSet.GetDS(bllDep, "GetList", new object[] { "(ParentID=" + _DepotID + ")" }).Tables[0];
            gridControl1.DataSource = dtSBIL;
            gridView1.Columns["Amount"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           if(Convert.ToInt32 (e.KeyChar)==13)
           {
               int _ID = 0;
               try
               {
                   _ID = Convert.ToInt32(textBox1.Text.Trim());
                   textBox1.Text = string.Empty;
                   DataTable dtTem=new DataTable();
                   //if (_ID > 0)
                       dtTem = BasicClass.GetDataSet.GetDS(bllRL, "GetList", new object[] { "(QRID=" + _ID + ")" }).Tables[0];
                   //else
                   //    dtTem = BasicClass.GetDataSet.GetDS(bllRL, "GetList", new object[] { "(ID=" + _ID*-1 + ")" }).Tables[0];
                   
                   if (dtTem.Rows.Count ==1)
                   {

                       if(dtSBIL.Select("(NotAmount="+dtTem.Rows[0]["ID"]+")").Length>0)
                       {
                           XtraMessageBox.Show("该物料已被领走");
                           return;
                       }
                       if (dtDepInfo.Select("(ID=" + dtTem.Rows[0]["DepotInfoID"] + ")").Length > 0)
                       {
                           if (DialogResult.Yes == XtraMessageBox.Show("该物料确认出仓？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                           {
                               decimal _amount = 0;
                               DataRow dr = dtSBIL.NewRow();
                               dr["ID"] = 0;
                               dr["A"] = 1;
                               dr["InfoID"] = _InfoID;
                               _amount = Convert.ToDecimal(dtTem.Rows[0]["NotAmount"]);
                               dr["Amount"] = dtTem.Rows[0]["NotAmount"];
                               dr["Remark"] = string.Empty;
                               dr["IsOk"] = true;
                               dr["BatchNumBer"] = dtTem.Rows[0]["BatchNumBer"];
                               dr["NotAmount"] = dtTem.Rows[0]["ID"];
                               dr["SpecID"] = dtTem.Rows[0]["SpecID"];
                               dr["DepotInfoID"] = dtTem.Rows[0]["DepotInfoID"];
                               dr["MainID"] = _ManID;
                               dr["QRID"] = _ID;
                               DataTable dtt = dtSBIL.Clone();
                               dtt.Rows.Add(dr.ItemArray);
                               dtt.Rows[0]["ID"] = BasicClass.GetDataSet.Add(bll, dtt);
                               dtSBIL.Rows.Add(dtt.Rows[0].ItemArray);
                               r.ChangeText(_amount.ToString());
                           }
                       }
                       else
                       {
                           XtraMessageBox.Show("非所选择仓库的物料！");
                           return;
                       }
                   }
               }
               catch (Exception ex)
               {
                   XtraMessageBox.Show("条码号不正确");
                   return;
               }
           }
        }
    }
}