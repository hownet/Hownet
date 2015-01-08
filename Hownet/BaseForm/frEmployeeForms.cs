using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using Hownet.Model;

namespace Hownet.BaseForm
{
    public partial class frEmployeeForms : DevExpress.XtraEditors.XtraForm
    {
        public frEmployeeForms()
        {
            InitializeComponent();
        }
        BasicClass.cResult cr = new BasicClass.cResult();
        int _id = 0;
        int _operation = 0;
        DataTable dt = new DataTable();
    //    DataTable dtPro = new DataTable();
     //   DataTable dtCity = new DataTable();
    //    DataTable dtCou = new DataTable();
        DataTable dtWT = new DataTable();
        DataTable dtOld = new DataTable();
        modEmployee mod=new modEmployee();
        bllEmployee bllEmp = new bllEmployee();
        string _picName = string.Empty;
        string bllCDM = "Hownet.BLL.CaicDayMoney";
        int _bedID = 0;
        int _tableID = 0;

        public frEmployeeForms(BasicClass.cResult r, int ID, int Operation)
            : this()
        {
            cr = r;
            _id = ID;
            _operation = Operation;
   
        }
        private void frEmployeeForms_Load(object sender, EventArgs e)
        {
            dtWT = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkType, "GetAllList", null).Tables[0];
            ucCheckComboBox2.DataDt = ucCheckComboBox1.DataDt = dtWT;
            ucCheckComboBox2.DisplayMember = ucCheckComboBox1.DisplayMember = "Name";
            ucCheckComboBox2.ValueMember = ucCheckComboBox1.ValueMember = "ID";
            _sbSave.Enabled = (_operation != (int)BasicClass.Enums.Operation.View);
         //   dtPro = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllChinaZone, "GetList", new object[] { "(OrderNum=2)" }).Tables[0];
         //   dtCity = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllChinaZone, "GetList", new object[] { "(OrderNum=3)" }).Tables[0];
        //    dtCou = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllChinaZone, "GetList", new object[] { "(OrderNum=4)" }).Tables[0];
            SetLab();
            //MyPrintDocument.PrinterSettings.PrinterName = "Zebra  LP2443";
            //MyPrintDocument.DefaultPageSettings.Landscape = false;
            _teBoardWages.EditValue = 0;
         //   dt = dtOld.Clone();
            if (_id > 0)
            {
                dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetList", new object[] { "(ID=" + _id + ")" }).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    mod = bllEmp.DataTableToList(dt)[0];
                }
                _ltSn.val = mod.Sn;
                _ltName.val = mod.Name;
                _leJSR.editVal = mod.IntroducerID;
                _ltIdentityCard.val = mod.IdentityCard;
                _reSex.EditValue = mod.Sex;
                _leProvince.editVal = mod.Province;
                _leCity.editVal = mod.City;
                _leCounty.editVal = mod.County;
                _ltAddress.val = mod.Address;
                _ltPhone.val = mod.Phone;
                _ldAcc.val = mod.AccDate;
                _lePayType.editVal = mod.PayID;
                if (mod.DimDate == DateTime.Parse("1900-1-1"))
                    _ldDim.val = null;
                else
                    _ldDim.val = mod.DimDate;
                _leBed.editVal = _bedID = mod.BedID;
                _leTable.editVal = _tableID = mod.TableID;
                _leDepartment.editVal = mod.DepartmentID;
                _leDegree.editVal = mod.DegreeID;
                _lePolity.editVal = mod.PolityID;
                _teSOSMan.EditValue = mod.SOSMan;
                _teSOSPhone.EditValue = mod.SOSPhone;
                _ltNowAddress.val = mod.NowAddress;
                _teRoyalty.EditValue = mod.Royalty;
                _teLastMoney.EditValue = mod.LassMoney;
                _picName = mod.Image;
                pictureEdit1.EditValue = BasicClass.ZipJpg.ShowPic(mod.Image);
                _ltIDCard.val = mod.IDCardID.ToString();
                ucCheckComboBox1.Values = mod.WorkTypeID;
                ucCheckComboBox2.Values = mod.DefaultWorkType;
                _teNeedDeposit.EditVal = mod.NeedDeposit;
                _teDeposit.EditVal = mod.Deposit;
                _teBoardWages.EditValue = mod.BoardWages;
                if (mod.HeTongDate.Year==1|| mod.HeTongDate == DateTime.Parse("1900-1-1"))
                    _ldHeTongDate.val = null;
                else
                    _ldHeTongDate.val = mod.HeTongDate;
                _teHeTongAmount.EditVal = mod.HeTongAmount;
                if (mod.HeTongDQDate.Year == 1 || mod.HeTongDQDate == DateTime.Parse("1900-1-1"))
                    _ldHeTongDQDate.val = null;
                else
                    _ldHeTongDQDate.val = mod.HeTongDQDate;
                if ( mod.PayID == 49 || mod.PayID == 50)
                {
                    lookUpEdit1.EditValue = Convert.ToInt32(mod.LassMoney);
                    lookUpEdit1.Visible = true;
                    _teLastMoney.Visible = _teRoyalty.Visible = false;
                }
                else if (mod.PayID == 48)
                {
                    lookUpEdit1.Visible = false;
                    _teLastMoney.Visible = true;
                    _teRoyalty.Visible = false;
                }
                else
                {
                    lookUpEdit1.Visible = false;
                    _teLastMoney.Visible = _teRoyalty.Visible = false;
                }
                _ceIsCaicTiCheng.Checked = mod.IsCaicTiCheng;
                _teMaxAmountDay.EditValue = mod.MaxAmountDay;
                // _teDeposit.IsCanEdit = (Convert.ToInt32(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllMiniEmp, "GetCaicPayCount", new object[] {_id })) == 0);
                if (mod.DimDate > Convert.ToDateTime("1900-1-1"))
                    _teDeposit.IsCanEdit = true;
                _teBankAccountName.EditValue = mod.BankAccountName;
                _teBankName.EditValue = mod.BankName;
                _teBankNO.EditValue = mod.BankNO;
            }
            else
            {
                for (int i = 0; i < panel1.Controls.Count; i++)
                {
                    try
                    {
                        BaseContranl.LabAndText lt = panel1.Controls[i] as BaseContranl.LabAndText;

                    }
                    catch { }
                    try
                    {
                        BaseContranl.LabAndLookupEdit ll = panel1.Controls[i] as BaseContranl.LabAndLookupEdit;

                    }
                    catch { }
                }
                string sn = BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllMiniEmp, "GetMaxSn", null).ToString();
                try
                {
                    _ltSn.val = (int.Parse(sn) + 1).ToString();
                }
                catch { }
                _teRoyalty.EditValue = mod.Royalty = 0;
                _teLastMoney.EditValue = mod.LassMoney = 0;
                _teNeedDeposit.EditVal = mod.NeedDeposit = 0;
                _teDeposit.EditVal = mod.Deposit = 0;
                _teMaxAmountDay.EditValue = mod.MaxAmountDay = 0;
                _teLastMoney.Visible = _teRoyalty.Visible = lookUpEdit1.Visible = false;
                _teBankAccountName.EditValue = _teBankName.EditValue = _teBankNO.EditValue = string.Empty;
            }
            _ltName.Focus();

        }
        private void SetLab()
        {
            _leBed.FormName = (int)BasicClass.Enums.TableType.Bed;
            _leTable.FormName = (int)BasicClass.Enums.TableType.MeaTable;
            _leProvince.FormName = _leCity.FormName = _leCounty.FormName = (int)BasicClass.Enums.TableType.Zone;
            _leJSR.FormName = (int)BasicClass.Enums.TableType.Employee;
            _leDegree.FormName = (int)BasicClass.Enums.TableType.Degree;
            _leDepartment.FormName = (int)BasicClass.Enums.TableType.Deparment;
            _lePolity.FormName = (int)BasicClass.Enums.TableType.Polity;
            _leProvince.FormName = _leCity.FormName = _leCounty.FormName = (int)BasicClass.Enums.TableType.Zone;
            _leProvince.DV =BasicClass.BaseTable.dtPro.DefaultView;
            _leCounty.DV = BasicClass.BaseTable.dtCou.DefaultView;
            _leCity.DV = BasicClass.BaseTable.dtCity.DefaultView;
            _lePayType.FormName = (int)BasicClass.Enums.TableType.PayType;
            _teNeedDeposit.Mask = _teDeposit.Mask = BasicClass.Enums.Mask.金額.ToString();
            _teHeTongAmount.Mask = BasicClass.Enums.Mask.整數.ToString();
            lookUpEdit1.Properties.DataSource = BasicClass.GetDataSet.GetDS(bllCDM, "GetLookupList", null).Tables[0];
            lookUpEdit1.EditValue = 0;
          //  lookUpEdit1.ToolTip = lookUpEdit1.Text;
        }

        private void _leProvince_EditValueChanged(object val, string text)
        {
            BasicClass.BaseTable.dtCity.DefaultView.RowFilter = "(ParentID=" + int.Parse(val.ToString()) + ") ";
            _leCity.DV = BasicClass.BaseTable.dtCity.DefaultView;
            if (int.Parse(val.ToString()) != mod.Province)
                _leCity.editVal = 0;
        }

        private void _leCity_EditValueChanged(object val, string text)
        {
            BasicClass.BaseTable.dtCou.DefaultView.RowFilter = "(ParentID=" + int.Parse(val.ToString()) + ") ";
            _leCounty.DV = BasicClass.BaseTable.dtCou.DefaultView;
            if (int.Parse(val.ToString()) != mod.City)
                _leCounty.editVal = 0;
        }

        private void _sbSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                _id = 0;
                for (int i = 0; i < panel1.Controls.Count; i++)
                {
                    try
                    {
                        BaseContranl.LabAndText lt = panel1.Controls[i] as BaseContranl.LabAndText;
                        lt.EditVal = string.Empty;
                    }
                    catch { }
                    try
                    {
                        BaseContranl.LabAndLookupEdit ll = panel1.Controls[i] as BaseContranl.LabAndLookupEdit;
                        ll.editVal = 0;
                    }
                    catch { }
                }
                string sn = BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllMiniEmp, "GetMaxSn", null).ToString();
                try
                {
                    _ltSn.val = (int.Parse(sn) + 1).ToString();
                }
                catch { }
                ucCheckComboBox1.Values = string.Empty;
                ucCheckComboBox2.Values = string.Empty;
                _teRoyalty.EditValue = mod.Royalty = 0;
                _teLastMoney.EditValue = mod.LassMoney = 0;
                _teNeedDeposit.EditVal = mod.NeedDeposit = 0;
                _teDeposit.EditVal = mod.Deposit = 0;
                _teLastMoney.Visible = _teRoyalty.Visible = lookUpEdit1.Visible = false;
            }
        }
        private bool Save()
        {
            mod.ID = _id;
            try
            {
                mod.Sn = Convert.ToInt32(_ltSn.val.Trim()).ToString();
            }
            catch
            {
                mod.Sn = _ltSn.val.Trim();//.Replace("'", "''");
            }
            mod.Name = _ltName.val.Trim();//.Replace("'", "''");
            if (mod.Sn.Trim() == string.Empty)
            {
                XtraMessageBox.Show("请填写编号！");
                return false;
            }
            if (mod.Name.Trim() == string.Empty)
            {
                XtraMessageBox.Show("请填写员工姓名！");
                return false;
            }

            if (mod.ID == 0)
            {
                DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetList", new object[] { "(Sn='" + mod.Sn + "') And (IsEnd=0)" }).Tables[0];
                if (dttt.Rows.Count > 0)
                {
                    XtraMessageBox.Show("编号：" + mod.Sn + "已被员工" + dttt.Rows[0]["Name"].ToString() + "使用");
                    return false;
                }
            }
            else
            {
                if (_ldDim.val == null)
                {
                    DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetList", new object[] { "(Sn='" + mod.Sn + "') And (ID<>" + _id + ") And (IsEnd=0)" }).Tables[0];
                    if (dttt.Rows.Count > 0)
                    {
                        XtraMessageBox.Show("编号：" + mod.Sn + "已被员工" + dttt.Rows[0]["Name"].ToString() + "使用");
                        return false;
                    }
                }
            }
            mod.IntroducerID = int.Parse(_leJSR.editVal.ToString());
            mod.IdentityCard = _ltIdentityCard.val;
            mod.Sex = int.Parse(_reSex.EditValue.ToString());
            mod.Province = int.Parse(_leProvince.editVal.ToString());
            mod.City = int.Parse(_leCity.editVal.ToString());
            mod.County = int.Parse(_leCounty.editVal.ToString());
            mod.Address = _ltAddress.val;
            mod.Phone = _ltPhone.val;
            if (_ldAcc.val != null)
                mod.AccDate = DateTime.Parse(_ldAcc.val.ToString());
            else
                mod.AccDate = BasicClass.GetDataSet.GetDateTime().Date;
            mod.WorkTypeID = ucCheckComboBox1.Values;
            mod.DefaultWorkType = ucCheckComboBox2.Values;
            mod.PayID = int.Parse(_lePayType.editVal.ToString());

            mod.BedID = int.Parse(_leBed.editVal.ToString());
            mod.TableID = int.Parse(_leTable.editVal.ToString());
            mod.DepartmentID = int.Parse(_leDepartment.editVal.ToString());
            mod.DegreeID = int.Parse(_leDegree.editVal.ToString());
            mod.PolityID = int.Parse(_lePolity.editVal.ToString());
            mod.SOSMan = _teSOSMan.Text;
            mod.SOSPhone = _teSOSPhone.Text;
            mod.NowAddress = _ltNowAddress.val;
            try
            {
                if (_teLastMoney.EditValue != null)
                    mod.LassMoney = (decimal)_teLastMoney.EditValue;
                else
                    mod.LassMoney = 0;
            }
            catch
            {
                mod.LassMoney = 0;
            }
            try
            {
                if (_teRoyalty.EditValue != null)
                    mod.Royalty = (decimal)_teRoyalty.EditValue;
                else
                    mod.Royalty = 0;
            }
            catch
            {
                mod.Royalty = 0;
            }

            try
            {
                if (_ltIDCard.EditVal.ToString() != string.Empty)
                    mod.IDCardID = long.Parse(_ltIDCard.val);
                else
                    mod.IDCardID = 0;
            }
            catch (Exception ex)
            {
                mod.IDCardID = 0;
            }
            if (mod.BedID > 0 && mod.BedID != _bedID)
            {
                DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetIsUse", new object[] { mod.BedID }).Tables[0];
                if (dttt.Rows.Count > 0)
                {
                    if (!(Convert.ToInt32(dttt.Rows[0][0]) > Convert.ToInt32(dttt.Rows[0][1])))
                    {
                        XtraMessageBox.Show("该宿舍员工已安排满员！");
                        return false;
                    }
                }
            }
            if (mod.TableID > 0 && mod.TableID != _tableID)
            {
                DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetIsUse", new object[] { mod.TableID }).Tables[0];
                if (dttt.Rows.Count > 0)
                {
                    if (!(Convert.ToInt32(dttt.Rows[0][0]) > Convert.ToInt32(dttt.Rows[0][1])))
                    {
                        XtraMessageBox.Show("该餐桌员工已安排满员！");
                        return false;
                    }
                }
            }
            if (mod.IDCardID != 0)
            {
                DataTable dttt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetList", new object[] { "(IDCardID=" + mod.IDCardID + ") And (ID<>" + _id + ") And ((DimDate IS NULL) OR (DimDate = CONVERT(DATETIME, '1900-01-01 00:00:00', 102)))" }).Tables[0];
                if (dttt.Rows.Count > 0)
                {
                    XtraMessageBox.Show("ID卡号：" + mod.IDCardID + "已被员工" + dttt.Rows[0]["Name"].ToString() + "使用");
                    return false;
                }
            }
            if (_ldDim.val != null)
                mod.DimDate = (DateTime)_ldDim.val;
            else
                mod.DimDate = DateTime.Parse("1900-1-1");
            if (mod.DimDate > DateTime.Parse("1900-1-1"))
            {
                mod.IsEnd = 1;
                mod.IDCardID = 0;
                mod.TableID = 0;
                mod.BedID = 0;
            }
            else
            {
                mod.IsEnd = 0;
            }
            if ( mod.PayID == 49 || mod.PayID == 50)
            {
                mod.LassMoney = Convert.ToInt32(lookUpEdit1.EditValue);
            }
            try
            {
                mod.NeedDeposit = Convert.ToDecimal(_teNeedDeposit.EditVal);
            }
            catch
            {
                mod.NeedDeposit = 0;
            }
            try
            {
                mod.Deposit = Convert.ToDecimal(_teDeposit.EditVal);
            }
            catch
            {
                mod.Deposit = 0;
            }
            mod.IsCaicTiCheng = _ceIsCaicTiCheng.Checked;
            if (_id > 0)
            {
                if (mod.Image.Trim() != string.Empty && mod.Image.Trim() != _picName.Trim())
                {
                    BasicClass.FileUpDown.DelFile(mod.Image);
                    BasicClass.FileUpDown.DelFile("Mini" + mod.Image);
                }
            }
            if (_picName.Trim() != "")
            {
                BasicClass.FileUpDown.UpFile(BasicClass.BasicFile.Dir + _picName, _picName);
                //BasicClass.FileUpDown.UpFile(BasicClass.BasicFile.Dir + "Mini" + _picName, "Mini" + _picName);
            }
            if (_ldHeTongDate.val != null)
                mod.HeTongDate = (DateTime)_ldHeTongDate.val;
            else
                mod.HeTongDate = DateTime.Parse("1900-1-1");
            mod.HeTongAmount = _teHeTongAmount.val;
            if (_ldHeTongDQDate.val != null)
                mod.HeTongDQDate = (DateTime)_ldHeTongDQDate.val;
            else
                mod.HeTongDQDate = DateTime.Parse("1900-1-1");
            mod.Image = _picName;
            mod.A = 1;
            mod.FillDate =BasicClass.GetDataSet.GetDateTime().Date;
            mod.FillUser = BasicClass.UserInfo.UserID;
            mod.BoardWages = Convert.ToDecimal(_teBoardWages.EditValue);
            mod.Remark = "";
            mod.MaxAmountDay = Convert.ToInt32(_teMaxAmountDay.EditValue);

            mod.BankAccountName = _teBankAccountName.EditValue.ToString().Trim(); ;
           mod.BankName = _teBankName.EditValue.ToString().Trim(); ;
            mod.BankNO = _teBankNO.EditValue.ToString().Trim();
            //if (dt.Rows.Count > 0)
            //    mod.Deposit = Convert.ToDecimal(dt.Rows[0]["Deposit"]);
            //else
            //    mod.Deposit = 0;
            List<modEmployee> li = new List<modEmployee>();
            li.Add(mod);
            DataTable dtt = BasicClass.ToDataTable.ListToDataTable<modEmployee>(li);
            dtt.TableName = "Emp";
            if (_id == 0)
                _id = mod.ID = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllMiniEmp, dtt);
            else
                BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllMiniEmp, dtt);
            cr.ChangeText("1");
            BasicClass.GetDataSet.SetDataTable();
            return true;
        }
        private void _sbPrint_Click(object sender, EventArgs e)
        {

            if (Save())
            {
                this.Close();
            }
        }

        private void _sbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (pictureEdit1.Image != null)
            {
                string aa = pictureEdit1.Image.RawFormat.ToString();
                byte[] bb = (byte[])pictureEdit1.EditValue;
                _picName = DateTime.Now.ToString("yyMMddhhmmss") + DateTime.Now.Millisecond.ToString() + ".jpg";
                FileStream stream = new FileStream(BasicClass.BasicFile.Dir + _picName, FileMode.OpenOrCreate);
                stream.Write(bb, 0, bb.Length);
              //  BasicClass.FileUpDown.GenThumbnail(stream, BasicClass.BasicFile.Dir + "Mini" + _picName);
                stream.Close();
                stream.Dispose();
            }
            else
            {
                _picName = "";
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //文字右对齐
            StringFormat sf = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            //划虚线
            Pen ftqGoal = new Pen(Color.Black, 1);
            ftqGoal.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            //StringAlignment.Center;或者：StringAlignment.Far;或者：StringAlignment.Near; 
            Brush brush = new SolidBrush(Color.Black);//画刷 
            Brush brred = new SolidBrush(Color.Red);//
            Font titleFont = new Font("黑体", 24, FontStyle.Bold);//标题字体 
            Font font = new Font("Consolas", 14, FontStyle.Regular);//数字0字有斜线的字体:WST_Ital,  01 DigitGraphics , 00 Starmap Truetype,Consolas,
            Font IDfont = new Font("Consolas", 12, FontStyle.Regular);
            //Font font = new Font("WST_Engl", 8);//正文字体 
            Font headerFont = new Font("黑体", 12, FontStyle.Bold);//列名标题 
            Font footerFont = new Font("Arial", 8);//页脚显示页数的字体 
            Font upLineFont = new Font("Arial", 9, FontStyle.Bold);//当header分两行显示的时候，上行显示的字体。 
            Font underLineFont = new Font("Arial", 8);//当header分两行显示的时候，下行显示的字

            e.Graphics.DrawString("编号："+_ltSn.val, font, brush, 5, 10);
            e.Graphics.DrawString("姓名："+_ltName.val , font, brush, 5, 35);
            e.Graphics.DrawString("ID卡号：" , font, brush, 5, 60);
            e.Graphics.DrawString(_ltIDCard.val, IDfont, brush, 5, 85);
        }
        private void PrintLabes()
        {
            try
            {
                //MyPrintDocument.PrintController = new System.Drawing.Printing.StandardPrintController();
                //this.MyPrintDocument.Print();//直接打印
            }
            catch { }
        }

        private void ucCheckComboBox1_EditValueChanged(object val, string text)
        {
            if (val != null && val.ToString().Length > 0)
            {
                string[] ss = val.ToString().Split(',');
                if (ss.Length > 0)
                {
                    decimal deposit = 0;
                    DataRow[] drs;
                    for (int i = 0; i < ss.Length; i++)
                    {
                        drs = dtWT.Select("(ID=" + Convert.ToInt32(ss[i]) + ")");
                        if (deposit < Convert.ToDecimal(drs[0]["Deposit"]))
                            deposit = Convert.ToDecimal(drs[0]["Deposit"]);
                    }
                    _teNeedDeposit.EditVal = deposit;
                }
                else
                {

                }

            }
        }

        private void _lePayType_EditValueChanged(object val, string text)
        {
            int aa = Convert.ToInt32(val);
            if ( aa == 49 || aa == 50)
            {
                lookUpEdit1.Visible = true;
                _teLastMoney.Visible = _teRoyalty.Visible = false;
            }
            else if (aa == 48)
            {
                lookUpEdit1.Visible = false;
                _teLastMoney.Visible = true;
                _teRoyalty.Visible = false;
            }
            else
            {
                lookUpEdit1.Visible = false;
                _teLastMoney.Visible = _teRoyalty.Visible = true;
            }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            this.lookUpEdit1.ToolTip = this.lookUpEdit1.Text;
        }

        private void ucCheckComboBox2_EditValueChanged(object val, string text)
        {
            if (val != null && val.ToString().Length > 0)
            {
                string[] ss = val.ToString().Split(',');
                if (ss.Length > 0)
                {
                    decimal deposit = 0;
                    DataRow[] drs;
                    for (int i = 0; i < ss.Length; i++)
                    {
                        drs = dtWT.Select("(ID=" + Convert.ToInt32(ss[i]) + ")");
                        if (deposit < Convert.ToDecimal(drs[0]["Deposit"]))
                            deposit = Convert.ToDecimal(drs[0]["Deposit"]);
                    }
                    _teNeedDeposit.EditVal = deposit;
                }
                else
                {

                }

            }
        }

        private void _teHeTongAmount_EditValueChanged(object val)
        {
            if (_ldHeTongDate.val != null)
            {
                try
                {
                    _ldHeTongDQDate.val = ((DateTime)_ldHeTongDate.val).AddYears(Convert.ToInt32(_teHeTongAmount.val));
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Save();
            DataTable dtEmp = new DataTable();
            dtEmp.Columns.Add("Name", typeof(string));
            dtEmp.Columns.Add("DepartmentName", typeof(string));
            dtEmp.Columns.Add("WorkTypeName", typeof(string));
            dtEmp.Columns.Add("QR", typeof(string));
            dtEmp.TableName = "Emp";
            for (int i = 0; i < dt.DefaultView.Count; i++)
            {
                dtEmp.Rows.Add(mod.Name, _leDepartment.valStr, ucCheckComboBox1.Text, mod.Sn);
            }
            BaseForm.PrintClass.PrintEMP(dtEmp);
        }

    }
}