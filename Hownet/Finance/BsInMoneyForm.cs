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
    public partial class BsInMoneyForm : DevExpress.XtraEditors.XtraForm
    {
        public BsInMoneyForm()
        {
            InitializeComponent();
        }
        private int _id = 0;
        public BsInMoneyForm(int id)
            : this()
        {
            _id = id;
        }
        int _companyID = 0;
        int _comID = 0;
        BasicClass.cResult r = new BasicClass.cResult();
        public BsInMoneyForm(int CompanyID, int ID, BasicClass.cResult crr)
            : this()
        {
            _comID = CompanyID;
            _id = ID;
            r = crr;
        }
        string blMIOO = "Hownet.BLL.MoneyInOrOut";
        string blCL = "Hownet.BLL.CompanyLog";
        DataTable dtMain = new DataTable();
        DataTable dtMI = new DataTable();
        DataTable dtCL = new DataTable();
        BindingSource bs = new BindingSource();
        decimal last = 0;
        decimal chang = 0;
        decimal moeny = 0;
        bool t = false;
  
        int _typeID = (int)BasicClass.Enums.MoneyTableType.BackMoney;
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            this.Text = "�տͻ�����";
            lookUpEdit1.Properties.DataSource = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetMoneyKJKM", null).Tables[0];
            ShowValue();
            if (_id == 0)
            {
                if (_comID == 0)
                {
                    bs.PositionChanged += new EventHandler(bs_PositionChanged);
                    InData();
                    bs.Position = dtMain.Rows.Count - 1;
                }
                else
                {
                    bs.PositionChanged += new EventHandler(bs_PositionChanged);
                    dtMain.Columns.Add("ID", typeof(int));
                    dtMain.Rows.Add(dtMain.NewRow());
                    bs.DataSource = dtMain;
                   // ShowView(0);
                }
            }
            else
            {
                dtMain.Columns.Add("ID", typeof(int));
                dtMain.Rows.Add(_id);
                ShowView(0);
                bar1.Visible = false;
            }
            string per = BasicClass.BasicFile.GetPermissions(this.Text);
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Add).ToString()) == -1)
                _brAddNew.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Edit).ToString()) == -1)
                 _brSave.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.Del).ToString()) == -1)
                _barVerify.Enabled = false;
            if (per.IndexOf(((int)BasicClass.Enums.Operation.UnVerify).ToString()) == -1)
                _barUnVerify .Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }
        private void ShowValue()
        {
            _leCompany.FormName = (int)BasicClass.Enums.TableType.Company;
            _leCompany.DV = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] {"TypeID=1 " }).Tables[0].DefaultView;//OR TypeID=3
            _ltChangMoney.Mask = BasicClass.Enums.Mask.���~.ToString();
        }
        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowView(bs.Position);
        }
        private void InData()
        {
            dtMain = BasicClass.GetDataSet.GetDS(blMIOO, "GetIDList", new object[] {_typeID}).Tables[0];
            if (dtMain.Rows.Count == 0)
                dtMain.Rows.Add(dtMain.NewRow());
            bs.DataSource = dtMain;
        }
        /// <summary>
        ///// ��ʾ��ϸ��¼
        /// </summary>
        /// <param name="p"></param>
        void ShowView(int p)
        {
            #region �ƶ���ť
            _brFrist.Enabled = true;
            _brPrv.Enabled = true;
            _brNext.Enabled = true;
            _brLast.Enabled = true;
            if (bs.Position == 0)
            {
                _brFrist.Enabled = false;
                _brPrv.Enabled = false;
            }
            if (bs.Position == dtMain.Rows.Count - 1)
            {
                _brNext.Enabled = false;
                _brLast.Enabled = false;
            }
            #endregion
            t = false;
            try
            {
                if (dtMain.DefaultView[p]["ID"].ToString() != "")
                {
                    _id = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                    dtMI = BasicClass.GetDataSet.GetDS(blMIOO, "GetList", new object[] { "(ID=" + _id + ")" }).Tables[0];
                    last = decimal.Parse(dtMI.Rows[0]["LastMoney"].ToString());
                    chang = decimal.Parse(dtMI.Rows[0]["ChangMoney"].ToString());
                    moeny = decimal.Parse(dtMI.Rows[0]["Money"].ToString());
                }
                else
                {
                    _id = 0;
                    dtMI = BasicClass.GetDataSet.GetDS(blMIOO, "GetList", new object[] { "(ID=" + _id + ")" }).Tables[0];
                    DataRow dr = dtMI.NewRow();
                    dr["ID"] = 0;
                    dr["Num"] = 0;// BasicClass.GetDataSet.GetOne(blMIOO, "NewNum", new object[] { DateTime.Today, _typeID });
                    dr["DateTime"] = dr["FillDate"] = DateTime.Today;
                    dr["CompanyID"] = _comID;
                    dr["VerifyMan"] = _companyID = 0;
                    dr["Remark"] = "";
                    dr["Money"] = dr["LastMoney"] = dr["ChangMoney"] = dr["KJKMID"] = dr["Fees"] = textEdit1.EditValue = last = chang = moeny = 0;
                    dr["TypeID"] = (int)BasicClass.Enums.MoneyTableType.BackMoney;
                    dr["FillMan"] = BasicClass.UserInfo.UserID;
                    dr["IsVerify"] = 1;
                    dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                    dr["KJKMID"] = 130;
                    dr["A"] = 1;
                    dtMI.Rows.Add(dr);
                }
                _leCompany.IsNotCanEdit = t = int.Parse(dtMI.Rows[0]["IsVerify"].ToString()) == 3;
                _ldDate.val = dtMI.Rows[0]["DateTime"];
                _ltChangMoney.val = dtMI.Rows[0]["ChangMoney"].ToString();
                _leCompany.editVal = dtMI.Rows[0]["CompanyID"];
                _barDel.Enabled = _ltChangMoney.IsCanEdit = _barVerify.Enabled = _brSave.Enabled = !t;
                _brAddNew.Enabled = _barUnVerify.Enabled = (t && p == dtMain.Rows.Count - 1);
                _typeID = Convert.ToInt32(dtMI.Rows[0]["TypeID"]);
                checkEdit1.Checked = (_typeID == (int)BasicClass.Enums.MoneyTableType.SellBack);
                lookUpEdit1.EditValue = Convert.ToInt32(dtMI.Rows[0]["KJKMID"]);
                textEdit1.EditValue = dtMI.Rows[0]["Fees"];
                memoEdit1.EditValue = dtMI.Rows[0]["Remark"];
                SetLoanText();
                if(_comID>0)
                {
                    _companyID = int.Parse(_leCompany.editVal.ToString());
                    if (!t && _companyID > 0)
                    {
                        DataTable dttt = BasicClass.GetDataSet.GetDS(blMIOO, "GetLastMoney", new object[] { _companyID }).Tables[0];
                        if (dttt.Rows.Count > 0)
                            last = decimal.Parse(dttt.Rows[0]["Money"].ToString());
                        else
                            last = 0;
                    }
                    SetLoanText();
                }
                textEdit2.Text = "SKD-" + dtMI.Rows[0]["Num"].ToString().PadLeft(3, '0');
            }
            catch (Exception ex)
            {

            }
        }
        #region ��¼�ƶ�
        /// <summary>
        /// �׼�¼
        /// </summary>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveFirst();
        }
        /// <summary>
        /// ��һ��
        /// </summary>
        private void barLargeButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MovePrevious();
        }
        /// <summary>
        /// ��һ��
        /// </summary>
        private void _brNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveNext();
        }
        /// <summary>
        /// β��¼
        /// </summary>
        private void _brLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveLast();
        }
        /// <summary>
        /// �µ�
        /// </summary>
        private void _brAddNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dtMain.Rows.Add(dtMain.NewRow());
            bs.Position = dtMain.Rows.Count - 1;
        }
        #endregion
        /// <summary>
        /// ����
        /// </summary>
        private void _brSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool f = _id == 0;
            Save();
            if (f)
            {
                int d = bs.Position;
                InData();
                bs.Position = d;
                ShowView(d);
            }
        }
        private void Save()
        {
            chang = decimal.Parse(_ltChangMoney.EditVal.ToString());
            if (checkEdit1.Checked)
                _typeID = (int)BasicClass.Enums.MoneyTableType.SellBack;
            else
                _typeID = (int)BasicClass.Enums.MoneyTableType.BackMoney;
            if (_companyID == 0 || chang == 0)
            {
                XtraMessageBox.Show("û��ѡ��ͻ���û��ʵ�ʽ�");
                return;
            }
            DataTable dttt = BasicClass.GetDataSet.GetDS(blMIOO, "GetLastMoney", new object[] { _companyID }).Tables[0];
            if (dttt.Rows.Count > 0)
                last = decimal.Parse(dttt.Rows[0]["Money"].ToString());
            else
                last = 0;
            moeny = last - chang;
            dtMI.Rows[0]["LastMoney"] = last;
            dtMI.Rows[0]["ChangMoney"] = chang;
            dtMI.Rows[0]["Money"] = moeny;
            dtMI.Rows[0]["CompanyID"] = _companyID;
            dtMI.Rows[0]["DateTime"] = _ldDate.val;
            dtMI.Rows[0]["TypeID"] = _typeID;
            dtMI.Rows[0]["KJKMID"] = lookUpEdit1.EditValue;
            dtMI.Rows[0]["Fees"] = textEdit1.EditValue;
            dtMI.Rows[0]["Remark"] = memoEdit1.EditValue;
            if (_id == 0)
            {
                dtMI.Rows[0]["Num"] = BasicClass.GetDataSet.GetOne(blMIOO, "NewNum", new object[] { Convert.ToDateTime(_ldDate.val), _typeID });
                dtMI.Rows[0]["ID"] = dtMain.Rows[bs.Position]["ID"] = _id = BasicClass.GetDataSet.Add(blMIOO, dtMI);
            }
            else
            {
                BasicClass.GetDataSet.UpData(blMIOO, dtMI);
            }
            _leCompany.IsNotCanEdit = true;
            _ltChangMoney.IsCanEdit = false;
            textEdit2.Text = "SKD-" + dtMI.Rows[0]["Num"].ToString().PadLeft(3, '0');
        }
        private void _brExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void SetLoanText()
        {

            chang = decimal.Parse(_ltChangMoney.EditVal.ToString());
            moeny=last-chang;
            _lbCMoney.Text = "��д��" + BasicClass.BasicFile.CmycurD(chang);
            _lbLast.Text = "�ͻ��� " + _leCompany.valStr + " ��ǰǷ�" + last.ToString("C2");
            _lbMoney.Text = "�ۼ����λ��" + chang.ToString("C2")+"Ԫ";
            _lbLoan.Text = "��Ƿ�" + moeny.ToString("C2") + "Ԫ";
        }

        private void _ltChangMoney_EditValueChanged(object val)
        {
            SetLoanText();
        }

        private void _leCompany_EditValueChanged(object val, string text)
        {
            _companyID = int.Parse(val.ToString());
            if (!t&&_companyID>0)
            {
                DataTable dttt = BasicClass.GetDataSet.GetDS(blMIOO, "GetLastMoney", new object[] { _companyID }).Tables[0];
                if (dttt.Rows.Count > 0)
                    last = decimal.Parse(dttt.Rows[0]["Money"].ToString());
                else
                    last = 0;
            }
            SetLoanText();
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (DialogResult.No == MessageBox.Show("�Ƿ�ȷ�����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                return;
            Save();
            t = true;
            dtMI.Rows[0]["VerifyMan"] = BasicClass.UserInfo.UserID;
            dtMI.Rows[0]["VerifyDate"] = DateTime.Today;
            dtMI.Rows[0]["IsVerify"] = 3;
            BasicClass.GetDataSet.UpData(blMIOO, dtMI);
            dtCL = BasicClass.GetDataSet.GetDS(blCL, "GetList", new object[] { "ID=0" }).Tables[0];
            DataRow dr = dtCL.NewRow();
            dr["ID"] = 0;
            dr["CompanyID"] = _companyID;
            dr["DateTime"] = dtMI.Rows[0]["DateTime"];
            dr["LastMoney"] = dtMI.Rows[0]["LastMoney"];
            dr["ChangMoney"] = chang;
            dr["Money"] = last -chang;
            dr["TypeID"] = _typeID; ;
            dr["TableID"] = _id;
            dr["NowMoneyTypeID"] = 0;
            dr["NowMoney"] = 0;
            dr["NowReta"] = 1;
            dr["A"] = 1;
            dtCL.Rows.Add(dr);
            BasicClass.GetDataSet.Add(blCL, dtCL);
            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllCompany, "UpLastMoney", new object[] { _companyID, (last - chang) });
            _barVerify.Enabled = _brSave.Enabled =_barDel.Enabled=  false;
            _brAddNew.Enabled = _barUnVerify.Enabled = (bs.Position == dtMain.Rows.Count - 1);
            DataTable dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(ID=" + lookUpEdit1.EditValue + ")" }).Tables[0];
            if (dtKM.Rows.Count > 0)
            {
                dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) + chang - Convert.ToDecimal(textEdit1.EditValue);
                BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
            }
            //dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(CompanyID=" + _companyID + ")" }).Tables[0];
            //if (dtKM.Rows.Count > 0)
            //{
            //    dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) - chang;
            //    BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
            //}
            string bllMI = "Hownet.BLL.MoneyList";
            decimal lastMoney = Convert.ToDecimal(BasicClass.GetDataSet.GetOne(bllMI, "GetLastMoney", new object[] { Convert.ToInt32(lookUpEdit1.EditValue) }));
            DataTable dtMII = BasicClass.GetDataSet.GetDS(bllMI, "GetList", new object[] { "1=2" }).Tables[0];
            DataRow ddr = dtMII.NewRow();
            ddr["KJKMID"] = Convert.ToInt32(lookUpEdit1.EditValue);
            ddr["DateTime"] = dtMI.Rows[0]["DateTime"];
            ddr["InMoney"] = chang - Convert.ToDecimal(textEdit1.EditValue);
            ddr["OutMoney"] = 0;
            ddr["Money"] = lastMoney + chang - Convert.ToDecimal(textEdit1.EditValue);
            ddr["TableID"] = _id;
            ddr["TypeID"] = (int)BasicClass.Enums.MoneyTableType.BackMoney;
            ddr["Remark"] = "�տͻ���" + _leCompany.valStr + " ����" + memoEdit1.Text; 
            dtMII.Rows.Add(ddr);
            BasicClass.GetDataSet.Add(bllMI, dtMII);
            string bllMain = "Hownet.BLL.CW_KJFL";
            string bllInfo = "Hownet.BLL.CW_KJFLInfo";


            DataTable dtPZ = BasicClass.GetDataSet.GetDS(bllMain, "GetList", new object[] { "(ID= 0 )" }).Tables[0];
            DataRow drr = dtPZ.NewRow();
            drr["A"] = 3;
            drr["ID"] = 0;
            DateTime dtNow = Convert.ToDateTime(dtMI.Rows[0]["DateTime"]);

            drr["���"] = BasicClass.GetDataSet.GetOne(bllMain, "NewNum", new object[] { dtNow, 1 });
            drr["��"] = dtNow.Year;
            drr["��"] = dtNow.Month.ToString().PadLeft(2, '0');
            drr["��"] = dtNow.Day.ToString().PadLeft(2, '0');
            drr["ʱ��"] = dtNow.ToString("yyyy-MM-dd");
            drr["����"] = string.Empty;
            drr["�Ƶ���"] = BasicClass.UserInfo.TrueName;
            drr["���"] = BasicClass.UserInfo.TrueName;
            drr["����"] = BasicClass.UserInfo.TrueName;
            drr["��������"] = BasicClass.UserInfo.TrueName;
            drr["�Ƶ���ID"] = BasicClass.UserInfo.UserID;
            drr["���ID"] = 0;
            drr["����ID"] = 0;
            drr["��������ID"] = 0;
            drr["�Ƶ�����"] = dtNow;
            drr["�������"] = dtNow;
            drr["��������"] = dtNow;
            drr["�ʻ�"] = lookUpEdit1.Text ;
            drr["�ʻ�ID"] = Convert.ToInt32(lookUpEdit1.EditValue);
            drr["TypeID"] = 1;
            drr["TableID"] = _id;
            dtPZ.Rows.Add(drr);
            int _PZID = BasicClass.GetDataSet.Add(bllMain, dtPZ);

            DataTable dtInfo = BasicClass.GetDataSet.GetDS(bllInfo, "GetList", new object[] { "(ID= 0 )" }).Tables[0];
            DataRow drrrr = dtInfo.NewRow();
            drrrr["A"] = 3;
            drrrr["ID"] = 0;
            drrrr["MainID"] = _PZID;
            drrrr["�������"] = "Ӧ���ʿ�";
            drrrr["���"] = chang;
            drrrr["������"] = DBNull.Value;
            drrrr["�������ID"] = 82;
            drrrr["��Ŀ����"] = string.Empty;
            drrrr["��Ŀ����ID"] = 0;
            drrrr["�ͻ��������"] = string.Empty;
            drrrr["�ͻ�"] = string.Empty;
            drrrr["���"] = string.Empty;
            drrrr["������"] = string.Empty;
            drrrr["��ע"] = "�տͻ���" + _leCompany.valStr + " ����";
            drrrr["�ͻ�����ID"] = 0;
            drrrr["�ͻ�ID"] = 0;
            drrrr["���ID"] = 0;
            drrrr["������ID"] = 0;
            drrrr["������Ŀ"] = _leCompany.valStr;
            drrrr["������ĿID"] = _companyID;
            drrrr["��������"] = string.Empty;
            drrrr["��������ID"] = 0;
            dtInfo.Rows.Add(drrrr);
            BasicClass.GetDataSet.Add(bllInfo, dtInfo);
            if (Convert.ToDecimal(textEdit1.EditValue) > 0)
            {
                dtInfo.Rows[0]["ID"] = 0;
                dtInfo.Rows[0]["���"] = Convert.ToDecimal(textEdit1.EditValue);
                dtInfo.Rows[0]["�������"] = "�������";
                dtInfo.Rows[0]["�������ID"] = 117;
                dtInfo.Rows[0]["������Ŀ"] = _leCompany.valStr;
                dtInfo.Rows[0]["������ĿID"] = _companyID;
                BasicClass.GetDataSet.Add(bllInfo, dtInfo);
            }
            r.ChangeText("1");
        }

        private void _barUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (int.Parse(BasicClass.GetDataSet.GetOne(blCL, "CanUnVerify", new object[] { _companyID, (int)(BasicClass.Enums.MoneyTableType.BackMoney), _id }).ToString()) > 0)
            {
                XtraMessageBox.Show("����֮�����к���ҵ��������������");
                return;
            }
            if (Convert.ToInt32(lookUpEdit1.EditValue) > 0)
            {
                if (int.Parse(BasicClass.GetDataSet.GetOne("Hownet.BLL.MoneyList", "CanUnVerify", new object[] { Convert.ToInt32(lookUpEdit1.EditValue), (int)(BasicClass.Enums.MoneyTableType.OutMoney), _id }).ToString()) > 0)
                {
                    XtraMessageBox.Show("����֮�����к���ҵ��������������");
                    return;
                }
            }
            string bllMain = "Hownet.BLL.CW_KJFL";
            string bllInfo = "Hownet.BLL.CW_KJFLInfo";
            BasicClass.GetDataSet.ExecSql("Hownet.BLL.MoneyList", "DeleteLog", new object[] { Convert.ToInt32(lookUpEdit1.EditValue), (int)(BasicClass.Enums.MoneyTableType.BackMoney), _id });
            DataTable dtPZ = BasicClass.GetDataSet.GetDS(bllMain, "GetList", new object[] { "(TableID=" + _id + ") And (TypeID=0)" }).Tables[0];
            if (dtPZ.Rows.Count > 0)
            {
                BasicClass.GetDataSet.ExecSql(bllMain, "Delete", new object[] { Convert.ToInt32(dtPZ.Rows[0]["ID"]) });
                BasicClass.GetDataSet.ExecSql(bllInfo, "DeleteByMainID", new object[] { Convert.ToInt32(dtPZ.Rows[0]["ID"]) });
            }

            t = false;
            dtMI.Rows[0]["VerifyMan"] = 0;
            dtMI.Rows[0]["VerifyDate"] = DateTime.Parse("1900-1-1");
            dtMI.Rows[0]["IsVerify"] = 1;
            BasicClass.GetDataSet.UpData(blMIOO, dtMI);
            BasicClass.GetDataSet.ExecSql(blCL, "DeleteLog", new object[] { _companyID, (int)(BasicClass.Enums.MoneyTableType.BackMoney), _id });
            _barVerify.Enabled = _brSave.Enabled = _barDel.Enabled = true;
            _brAddNew.Enabled = _barUnVerify.Enabled = false;
            DataTable dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(ID=" + lookUpEdit1.EditValue + ")" }).Tables[0];
            if (dtKM.Rows.Count > 0)
            {
                dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) - chang + Convert.ToDecimal(textEdit1.EditValue);
                BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
            }
            //dtKM = BasicClass.GetDataSet.GetDS("Hownet.BLL.Bas_KJKM", "GetList", new object[] { "(CompanyID=" + _companyID + ")" }).Tables[0];
            //if (dtKM.Rows.Count > 0)
            //{
            //    dtKM.Rows[0]["Money"] = Convert.ToDecimal(dtKM.Rows[0]["Money"]) + chang;
            //    BasicClass.GetDataSet.UpData("Hownet.BLL.Bas_KJKM", dtKM);
            //}
        }

        private void _barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("�Ƿ����ɾ����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                BasicClass.GetDataSet.ExecSql(blMIOO, "Delete", new object[] { _id});
                if (dtMain.Rows.Count > 1)
                {
                    dtMain.Rows.RemoveAt(bs.Position);
                    _brAddNew.Enabled = (t && bs.Position == dtMain.Rows.Count - 1);
                }
                else
                {
                    InData();
                    ShowView(0);
                }
            }
        }

        private void barButtonItem1_ItemClick_2(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!t)
            {
                XtraMessageBox.Show("����˺��ٴ�ӡ���ݣ�");
                return;
            }
            int _id = int.Parse(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllSysTem, "GetMaxId", null).ToString()) - 1;
            DataSet ds = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSysTem, "GetList", new object[] { "(ID=" + _id + ")" });
            if (ds.Tables[0].Rows.Count == 0)
            {
                XtraMessageBox.Show("�����ƹ�˾��Ϣ��");
                //Form fr = new BaseForm.UserBaseSetForm();
                //fr.ShowDialog();
                return;
            }
            ds.Tables[0].TableName = "Main";
            ds.Tables["Main"].Columns.Add("LastMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("BackMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("NowMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("NewMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("DXMoney", typeof(string));
            ds.Tables["Main"].Columns.Add("ComName", typeof(string));
            ds.Tables["Main"].Columns.Add("Date", typeof(string));
            ds.Tables["Main"].Columns.Add("Num", typeof(string));
            ds.Tables["Main"].Columns.Add("UserName", typeof(string));
            ds.Tables["Main"].Columns.Add("SumAmount", typeof(string));
            ds.Tables["Main"].Columns.Add("SumBoxAmount", typeof(string));
            ds.Tables["Main"].Columns.Add("SumMoney", typeof(string));
            //ds.Tables["Main"].Columns.Add("Mobile", typeof(string));
            ds.Tables["Main"].Columns.Add("MainRemark", typeof(string));
            ds.Tables["Main"].Columns.Add("BackDate", typeof(string));
            ds.Tables["Main"].Columns.Add("LastDate", typeof(string));
            ds.Tables["Main"].Columns.Add("���", typeof(string));
            ds.Tables["Main"].Columns.Add("�Ƶ�", typeof(string));
            DataTable dtUser = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllUsers, "GetAllList", null).Tables[0];

            ds.Tables[0].Rows[0]["LastMoney"] = last.ToString("C2") + "Ԫ��";
            ds.Tables[0].Rows[0]["BackMoney"] = chang.ToString("C2") + "Ԫ��";
           // ds.Tables[0].Rows[0]["NowMoney"] = money.ToString("C2") + "Ԫ��";
            ds.Tables[0].Rows[0]["NewMoney"] = (last - chang ).ToString("C2") + "Ԫ��";
            ds.Tables[0].Rows[0]["DXMoney"] = BasicClass.BasicFile.CmycurD(chang);
            ds.Tables[0].Rows[0]["ComName"] = _leCompany.valStr;
            ds.Tables[0].Rows[0]["Date"] = ((DateTime)(_ldDate.val)).ToString("yyyy��MM��dd��");
           // ds.Tables[0].Rows[0]["Num"] = dtPS.Rows[0]["Num"].ToString().PadLeft(3, '0');// _ltNum.val;
            ds.Tables[0].Rows[0]["UserName"] = BasicClass.UserInfo.TrueName;
            ds.Tables[0].Rows[0]["MainRemark"] = memoEdit1.EditValue;
            ds.Tables[0].Rows[0]["HDSerie"] = lookUpEdit1.Text;
            ds.Tables[0].Rows[0]["Num"] = textEdit2.Text;
            if (Convert.ToInt32(dtMI.Rows[0]["FillMan"]) > 0)
            {
                ds.Tables[0].Rows[0]["�Ƶ�"] = dtUser.Select("(ID=" + Convert.ToInt32(dtMI.Rows[0]["FillMan"]) + ")")[0]["TrueName"];
            }
            if (Convert.ToInt32(dtMI.Rows[0]["VerifyMan"]) > 0)
            {
                ds.Tables[0].Rows[0]["���"] = dtUser.Select("(ID=" + Convert.ToInt32(dtMI.Rows[0]["VerifyMan"]) + ")")[0]["TrueName"];
            }
            BaseForm.PrintClass.PrintInMoney(ds);
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
                lookUpEdit1.EditValue = 0;
        }

    }
}