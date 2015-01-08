using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;

namespace Hownet.BaseForm
{
    public partial class frFinishedOne : DevExpress.XtraEditors.XtraForm
    {
        public frFinishedOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _ID = 0;
        int _TypeID = 0;
        int _attributeID = 0;
        DataTable dtMateriel = new DataTable();
        DataTable dtOld = new DataTable();
       DataTable dtEmp = new DataTable();
        string bll = BasicClass.Bllstr.bllMateriel;
        string fileName = string.Empty;
        string oldFileName = string.Empty;
        public frFinishedOne(BasicClass.cResult cr, int ID, DataTable dt, int TypeID, int AttributeID)
            : this()
        {
            r = cr;
            _ID = ID;
            dtOld = dt;
            _TypeID = TypeID;
            _attributeID = AttributeID;
        }
        private void frColorOne_Load(object sender, EventArgs e)
        {
             dtMateriel = dtOld.Clone();
             _leMTID.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielType, "GetList", new object[] { "AttributeID=4 And  IsEnd=0" }).Tables[0];
             _leMTID.EditValue = _TypeID;
             _leMeasureID.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMeasure, "GetList", new object[] {"IsEnd=0" }).Tables[0];
             dtEmp= BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetListByDepTypeID", new object[] { 45}).Tables[0];
             dtEmp.Rows.Add(0, "");
             dtEmp.DefaultView.Sort = "ID";
             lookUpEdit1.Properties.DataSource = dtEmp.DefaultView;
             lookUpEdit1.EditValue = 0;
            if (_ID == 0)
            {
                this.Text = "增加款号：";
                _teName.EditValue = _teSn.EditValue = string.Empty;
                pictureEdit1.EditValue = null;
            }
            else
            {
                this.Text = "编辑款号：";
                dtMateriel.Rows.Add(dtOld.Select("(ID=" + _ID + ")")[0].ItemArray);
                _teName.EditValue = dtMateriel.Rows[0]["Name"];
                _teSn.EditValue = dtMateriel.Rows[0]["Name"];
                _teMeRemark.EditValue = dtMateriel.Rows[0]["Remark"];
                _leMeasureID.EditValue = int.Parse(dtMateriel.Rows[0]["MeasureID"].ToString());
                _teChengBengJ.EditValue = Convert.ToDecimal(dtMateriel.Rows[0]["ChengBengJ"]);
                _teLingShouJia.EditValue = Convert.ToDecimal(dtMateriel.Rows[0]["LingShouJia"]);
                _teTiaoMaH.EditValue = dtMateriel.Rows[0]["TiaoMaH"].ToString();
                _teYiJiDaiLiJia.EditValue = Convert.ToDecimal(dtMateriel.Rows[0]["YiJiDaiLiJia"]);
                fileName = oldFileName = dtMateriel.Rows[0]["Image"].ToString();
                ShowPic(fileName);
            }
        }
        private bool Save()
        {
            if (_teSn.Text.Trim().Length == 0 )
            {
                XtraMessageBox.Show("款号不能为空！");
                return false;
            }
            if (_leMeasureID.EditValue == null || int.Parse(_leMeasureID.EditValue.ToString()) == 0)
            {
                XtraMessageBox.Show("请选择默认的计量单位!");
                return false;
            }
            if (pictureEdit1.EditValue != null)
            {
                //byte[] bb = (byte[])pictureEdit1.EditValue;//更新后，保存图片，生成新的缩略图，并更新Image字段为新文件名
                //fileName = BasicClass.FileUpDown.SavePic(bb);
            }
            else
            {
                fileName = string.Empty;
            }
            _teName.Text = _teSn.Text;
            dtMateriel.Rows.Clear();
            string sqlWhere = " (ID <> " + _ID + ")  And ((Sn = '" + _teSn.Text.Trim() + "') OR (Name = '" + _teName.Text.Trim() + "')) ";
            DataRow[] drs = dtOld.Select(sqlWhere);
            if (drs.Length > 0)//如果有同色號或同名稱、同英文名的記錄
            {
                //如果色號、名稱、英文名都相同，且已標記為被刪除，則取消刪除
                if (int.Parse(drs[0]["IsEnd"].ToString())>0 && drs[0]["Sn"].Equals(_teSn.Text.Trim()) && drs[0]["Name"].Equals(_teName.Text.Trim()))
                {
                    drs[0]["IsEnd"] = 0;
                    dtOld.AcceptChanges();
                    dtMateriel.Rows.Add(drs[0].ItemArray);
                    BasicClass.GetDataSet.UpData(bll, dtMateriel);
                    return true;
                }
                else//如果不是，或只有部份字段相同，則提示有重復
                {
                    XtraMessageBox.Show("款号重复！");
                    return false;
                }
            }
            DataRow dr = dtMateriel.NewRow();
            dr["A"] = 1;
            dr["ID"] = _ID;
            dr["Sn"] = _teSn.Text.Trim();
            dr["Name"] = _teName.Text.Trim();
            dr["IsEnd"] = 0;
            dr["MeasureID"] = _leMeasureID.EditValue;
            dr["TypeID"] = Convert.ToInt32(_leMTID.EditValue);
            dr["Remark"] = _teMeRemark.Text.Trim();
            dr["SecondMeasureID"] = dr["Conversion"] = dr["IsUse"] = 0;
            dr["Image"] = fileName;
            dr["AttributeID"] = _attributeID;
            dr["Designers"] = lookUpEdit1.EditValue;
            if (_teChengBengJ.Text.Trim() != string.Empty)
                dr["ChengBengJ"] = _teChengBengJ.EditValue;
            else
                dr["ChengBengJ"] = 0;
            if (_teLingShouJia.Text.Trim() != string.Empty)
                dr["LingShouJia"] = _teLingShouJia.EditValue;
            else
                dr["LingShouJia"] = 0;
            dr["TiaoMaH"] = _teTiaoMaH.EditValue;
            if (_teYiJiDaiLiJia.Text.Trim() != string.Empty)
                dr["YiJiDaiLiJia"] = _teYiJiDaiLiJia.EditValue;
            else
                dr["YiJiDaiLiJia"] = 0;
            dtMateriel.Rows.Add(dr);
            if (_ID == 0)
            {
               dr["ID"]=_ID= BasicClass.GetDataSet.Add(bll, dtMateriel);
               dtOld.Rows.Add(dr.ItemArray);
            }
            else
            {
                BasicClass.GetDataSet.UpData(bll, dtMateriel);
                drs = dtOld.Select("(ID=" + _ID + ")");
                drs[0].ItemArray = dr.ItemArray;
                if (fileName != string.Empty && fileName != oldFileName)
                {
                    //Thread t = new Thread(new ThreadStart(this.ShowFlashForm));
                    //t.Start();
                    UpFile();
                    //t.Abort();
                    //t.Join();
                }
            }
            dtOld.AcceptChanges();

            return true;
        }
        private void UpFile()
        {
            BasicClass.FileUpDown.DelFile(oldFileName);
            BasicClass.FileUpDown.UpFile(BasicClass.BasicFile.Dir + fileName, fileName);
        }

        private void _sbSaveAndContinue_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                r.RowChang(dtOld);
                _ID = 0;
                _teMeRemark.EditValue = _teName.EditValue = _teSn.EditValue = string.Empty;
                dtMateriel.Rows.Clear();
            }
        }

        private void _sbSaveAndExit_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                r.RowChang(dtOld);
                this.Close();
            }
        }

        private void _sbCancel_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否不保存当前处理直接退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                this.Close();
            }
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            if (pictureEdit1.EditValue != null)
            {
                try
                {
                    if (!BasicClass.BasicFile.FileExists(fileName))
                        BasicClass.FileUpDown.DownLoad(fileName, BasicClass.BasicFile.Dir + fileName);
                }
                catch { }
                Form fr = new ShowPic(fileName);
                
                fr.ShowDialog();
            }
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (pictureEdit1.EditValue != null)
            {
              //  byte[] bb = (byte[])pictureEdit1.EditValue;//更新后，保存图片，生成新的缩略图，并更新Image字段为新文件名
                byte[] bb = BasicClass.FileUpDown.ImageToByteArray((Image)pictureEdit1.EditValue);
               // pictureEdit1.Image
                fileName = BasicClass.FileUpDown.SavePic(bb);
            }
            else
            {
                fileName = string.Empty;
            }
        }
        private void ShowPic(string picName)
        {
            if (picName.Trim() != "")
            {
                if (!BasicClass.BasicFile.FileExists("Mini" + picName))
                    BasicClass.FileUpDown.DownLoad("Mini" + picName, BasicClass.BasicFile.Dir + "Mini" + picName);
                pictureEdit1.EditValue = BasicClass.FileUpDown.getPicEditValue("Mini" + picName);
            }
            else
            {
                pictureEdit1.EditValue = null;
            }
        }
    }
}