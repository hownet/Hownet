using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hownet.Model;

namespace Hownet.Pay
{
    public partial class frCatering : DevExpress.XtraEditors.XtraForm
    {
        public frCatering()
        {
            InitializeComponent();
        }
        List<modOtherType> list;
        OtherTypeOTT bllOT = new OtherTypeOTT();
        DataTable dt = new DataTable();
        bool _IsEdit = false;
        private void frCatering_Load(object sender, EventArgs e)
        {
            list = bllOT.DataTableToList(BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetTypeList", new object[] { "伙食扣费" }).Tables[0]);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name == "早餐")
                {
                    _teZhaoCan.EditValue = Convert.ToDecimal(list[i].Value);
                }
                else if (list[i].Name == "午餐")
                { 
                    _teWuCan.EditValue = Convert.ToDecimal(list[i].Value); 
                }
                else if (list[i].Name == "晚餐")
                {
                    _teWanCan.EditValue = Convert.ToDecimal(list[i].Value);
                }
                else if (list[i].Name == "订餐后未吃")
                {
                    _teNotEat.EditValue = Convert.ToDecimal(list[i].Value);
                }
                else if (list[i].Name == "未订餐吃饭")
                {
                    _teNotOrder.EditValue = Convert.ToDecimal(list[i].Value);
                }
                else if (list[i].Name == "以下要扣费")
                {
                    checkEdit1.Checked = (list[i].Value == "1");
                    checkEdit1.Tag = list[i].Value;
                }
                else if(list[i].Name=="计时不扣正常餐费")
                {
                    checkEdit2.Checked=(list[i].Value=="1");
                    checkEdit2.Tag = list[i].Value;
                }
                else if (list[i].Name == "允许违规次数")
                {
                    textEdit1.EditValue = list[i].Value;
                }
                else if (list[i].Name == "就餐成功")
                {
                    textEdit2.EditValue = list[i].Value;
                }
                else if (list[i].Name == "重复就餐")
                {
                    textEdit3.EditValue = list[i].Value;
                }
                else if (list[i].Name == "未订吃饭")
                {
                    textEdit4.EditValue = list[i].Value;
                }
            }
            if (!BasicClass.BasicFile.liST[0].OrderNeedEat)
            {
                 _teNotEat.Enabled = _teNotOrder.Enabled = checkEdit1.Enabled = textEdit1.Enabled =textEdit2.Enabled=textEdit4.Enabled=textEdit3.Enabled= false;//_teZhaoCan.Enabled =
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name == "早餐")
                {
                    if (Convert.ToDecimal(list[i].Value) != Convert.ToDecimal(_teZhaoCan.EditValue))
                    {
                        _IsEdit = true;
                        list[i].Value = _teZhaoCan.EditValue.ToString();
                    }
                }
                else if (list[i].Name == "午餐")
                {
                    if (Convert.ToDecimal(list[i].Value) != Convert.ToDecimal(_teWuCan.EditValue))
                    {
                        _IsEdit = true;
                        list[i].Value = _teWuCan.EditValue.ToString();
                    }
                }
                else if (list[i].Name == "晚餐")
                {
                    if (Convert.ToDecimal(list[i].Value) != Convert.ToDecimal(_teWanCan.EditValue))
                    {
                        _IsEdit = true;
                        list[i].Value = _teWanCan.EditValue.ToString();
                    }
                }
                else if (list[i].Name == "订餐后未吃")
                {
                    if (Convert.ToDecimal(list[i].Value) != Convert.ToDecimal(_teNotEat.EditValue))
                    {
                        _IsEdit = true;
                        list[i].Value = _teNotEat.EditValue.ToString();
                    }
                }
                else if (list[i].Name == "未订餐吃饭")
                {
                    if (Convert.ToDecimal(list[i].Value) != Convert.ToDecimal(_teNotOrder.EditValue))
                    {
                        _IsEdit = true;
                        list[i].Value = _teNotOrder.EditValue.ToString();
                    }
                }
                else if (list[i].Name == "允许违规次数")
                {
                    if (Convert.ToInt32(list[i].Value) != Convert.ToInt32(textEdit1.EditValue))
                    {
                        _IsEdit = true;
                        list[i].Value = textEdit1.EditValue.ToString();
                    }
                }
                else if (list[i].Name == "以下要扣费")
                {
                    if (checkEdit1.Checked)
                        checkEdit1.Tag = "1";
                    else
                        checkEdit1.Tag = "0";
                    if (list[i].Value != checkEdit1.Tag.ToString())
                    {
                        _IsEdit = true;
                        if (checkEdit1.Checked)
                            list[i].Value = "1";
                        else
                            list[i].Value = "0";
                    }
                }
                else if (list[i].Name == "计时不扣正常餐费")
                {
                    if (checkEdit2.Checked)
                        checkEdit2.Tag = "1";
                    else
                        checkEdit2.Tag = "0";
                    if (list[i].Value != checkEdit2.Tag.ToString())
                    {
                        _IsEdit = true;
                        if (checkEdit2.Checked)
                            list[i].Value = "1";
                        else
                            list[i].Value = "0";
                    }
                }
                else if (list[i].Name == "就餐成功")
                {
                    if (list[i].Value != textEdit2.Text.Trim())
                    {
                        _IsEdit = true;
                        list[i].Value = textEdit2.Text.Trim();
                    }
                }
                else if (list[i].Name == "重复就餐")
                {
                    if (list[i].Value != textEdit3.Text.Trim())
                    {
                        _IsEdit = true;
                        list[i].Value = textEdit3.Text.Trim();
                    }
                }
                else if (list[i].Name == "未订吃饭")
                {
                    if (list[i].Value != textEdit4.Text.Trim())
                    {
                        _IsEdit = true;
                        list[i].Value = textEdit4.Text.Trim();
                    }
                }
            }
            if (_IsEdit)
            {
                DataTable dtt = BasicClass.ToDataTable.ListToDataTable<modOtherType>(list);
                dtt.TableName = "dt";
                DataTable dtttt = dtt.Clone();
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    dtttt.Rows.Clear();
                    dtttt.Rows.Add(dtt.Rows[i].ItemArray);
                    BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllOtherType, dtttt);
                }
              //  BasicClass.GetDataSet.ExecSql("Hownet.BLL.OrderingList", "CaicMoney", new object[] { BasicClass.GetDataSet.GetDateTime().Date.AddDays(-1) });
                _IsEdit = false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}