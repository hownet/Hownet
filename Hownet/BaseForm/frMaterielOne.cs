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
    public partial class frMaterielOne : DevExpress.XtraEditors.XtraForm
    {
        public frMaterielOne()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _ID = 0;
        int _TypeID = 0;
        int _attributeID = 0;
        int _MCID = 0;
        DataTable dtMateriel = new DataTable();
        DataTable dtOld = new DataTable();
        string bll = BasicClass.Bllstr.bllMateriel;
        string fileName = string.Empty;
        string oldFileName = string.Empty;
        DataTable dtMC = new DataTable();
        DataRow drMC;
        public frMaterielOne(BasicClass.cResult cr, int ID,DataTable dt)
            : this()
        {
            r = cr;
            _ID = ID;
            dtOld = dt;
            _sbCancel.Visible = _sbSaveAndContinue.Visible = _sbSaveAndExit.Visible = false;
        }
        public frMaterielOne(BasicClass.cResult cr, int ID, DataTable dt,int TypeID,int AttributeID)
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
            try
            {
                dtMateriel = dtOld.Clone();
                _leMTID.Properties.DataSource = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielType, "GetList", new object[] { "IsEnd=0" }).Tables[0];
                _leMTID.EditValue = _TypeID;
                _leMeasureID.Properties.DataSource = BasicClass.BaseTableClass.dtMeasure;
                if (_ID == 0)
                {
                    this.Text = "添加物料";
                    _teName.EditValue = _teSn.EditValue = string.Empty;
                    pictureEdit1.EditValue = null;
                    _teSn.Text = (dtOld.Rows.Count + 1).ToString().PadLeft(3, '0');
                }
                else
                {
                    this.Text = "编辑物料";
                    dtMateriel.Rows.Add(dtOld.Select("(ID=" + _ID + ")")[0].ItemArray);
                    _teName.EditValue = dtMateriel.Rows[0]["Name"];
                    _teSn.EditValue = dtMateriel.Rows[0]["Sn"];
                    _teMeRemark.EditValue = dtMateriel.Rows[0]["Remark"];
                    _leMeasureID.EditValue = int.Parse(dtMateriel.Rows[0]["MeasureID"].ToString());
                    _teChengBengJ.EditValue = Convert.ToDecimal(dtMateriel.Rows[0]["ChengBengJ"]);
                    fileName = oldFileName = dtMateriel.Rows[0]["Image"].ToString();
                    ShowPic(fileName);
                }
                dtMC = BasicClass.GetDataSet.GetDS("Hownet.BLL.MaterielCompany", "GetList", new object[] { "(MaterielID=" + _ID + ")" }).Tables[0];
                gridControl1.DataSource = dtMC;
                lookUpEdit1.Properties.DataSource = BasicClass.BaseTableClass.dtSupplier;
                _coCompanyID.ColumnEdit = RepositoryItem._reSupplier;
            }
            catch (Exception ex)
            {

            }
        }
        private bool Save()
        {
            if (_teSn.Text.Trim().Length == 0 || _teName.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("物料编号、名称不能为空！");
                return false;
            }
            if (_leMeasureID.EditValue == null || int.Parse(_leMeasureID.EditValue.ToString()) == 0)
            {
                XtraMessageBox.Show("请选择物料的默认计量单位 !");
                return false;
            }
            if (pictureEdit1.EditValue != null)
            {
                byte[] bb = (byte[])pictureEdit1.EditValue;//更新后，保存图片，生成新的缩略图，并更新Image字段为新文件名
                fileName = BasicClass.FileUpDown.SavePic(bb);
            }
            else
            {
                fileName = string.Empty;
            }
            dtMateriel.Rows.Clear();
            string sqlWhere = " (ID <> " + _ID + ")  And ((Sn = '" + _teSn.Text.Trim() + "') OR (Name = '" + _teName.Text.Trim() + "')) ";
            DataRow[] drs = dtOld.Select(sqlWhere);
            if (drs.Length > 0)//如果有同色號或同名稱、同英文名的記錄
            {
                //如果色號、名稱、英文名都相同，且已標記為被刪除，則取消刪除
                if (int.Parse(drs[0]["IsEnd"].ToString()) > 0 && drs[0]["Sn"].Equals(_teSn.Text.Trim()) && drs[0]["Name"].Equals(_teName.Text.Trim()))
                {
                    drs[0]["IsEnd"] = 0;
                    dtOld.AcceptChanges();
                    dtMateriel.Rows.Add(drs[0].ItemArray);
                    BasicClass.GetDataSet.UpData(bll, dtMateriel);
                    return true;
                }
                else//如果不是，或只有部份字段相同，則提示有重復
                {
                    XtraMessageBox.Show("编号或名称重复！");
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
            dr["TypeID"] = _leMTID.EditValue;
            dr["Remark"] = _teMeRemark.Text.Trim();
            dr["SecondMeasureID"] = dr["Conversion"] = dr["IsUse"] = 0;
            dr["Image"] = fileName;
            dr["AttributeID"] = _attributeID;
            dr["Designers"] = 0;
            if (_teChengBengJ.Text.Trim() != string.Empty)
                dr["ChengBengJ"] = _teChengBengJ.EditValue;
            else
                dr["ChengBengJ"] = 0;
            dtMateriel.Rows.Add(dr);
            if (_ID == 0)
            {
                dr["ID"] =_ID= BasicClass.GetDataSet.Add(bll, dtMateriel);
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
            int a = 0;
            DataTable dtMCTem = dtMC.Clone();
            for (int i = 0; i < dtMC.Rows.Count; i++)
            {
                a = Convert.ToInt32(dtMC.Rows[i]["A"]);
                if (a > 1)
                {
                    dtMCTem.Rows.Clear();
                    dtMC.Rows[i]["MaterielID"] = _ID;
                    dtMCTem.Rows.Add(dtMC.Rows[i].ItemArray);
                    if (a == 3)
                    {
                        dtMC.Rows[i]["ID"] = BasicClass.GetDataSet.Add("Hownet.BLL.MaterielCompany", dtMCTem);
                    }
                    else if (a == 2)
                    {
                        BasicClass.GetDataSet.UpData("Hownet.BLL.MaterielCompany", dtMCTem);
                    }
                    dtMC.Rows[i]["A"] = 1;
                }
            }
            dtMC.AcceptChanges();
            dtOld.AcceptChanges();
            return true;
        }
        private void UpFile()
        {
            BasicClass.FileUpDown.DelFile(oldFileName);
            BasicClass.FileUpDown.DelFile("Mini" + oldFileName);
            BasicClass.FileUpDown.UpFile(BasicClass.BasicFile.Dir + fileName, fileName);
           // BasicClass.FileUpDown.UpFile(BasicClass.BasicFile.Dir + "Mini" + fileName, "Mini" + fileName);
        }

        private void _sbSaveAndContinue_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                r.RowChang(dtOld);
                _ID = 0;
                _teMeRemark.EditValue = _teName.EditValue = _teSn.EditValue = string.Empty;
                dtMateriel.Rows.Clear();
                _teSn.Text = (dtOld.Rows.Count + 1).ToString().PadLeft(3, '0');
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
            //if (DialogResult.Yes == XtraMessageBox.Show("是否不保存當前處理直接退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            //{
                
            //}
            this.Close();
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
                byte[] bb = (byte[])pictureEdit1.EditValue;//更新后，保存图片，生成新的缩略图，并更新Image字段为新文件名
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

        private void _teName_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                DoShowMenu(gridView1.CalcHitInfo(new Point(e.X, e.Y)));
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            //if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            //{
                popupMenu1.ShowPopup(Control.MousePosition);
            //}
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _MCID = -1;
            drMC = dtMC.NewRow();
            lookUpEdit1.EditValue = 0;
            _teCSN.EditValue = _teCPrice.EditValue = _teCR.EditValue = _teRemark.EditValue = string.Empty;
            panel1.Visible = true;
            _teCSN.Focus();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                _MCID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
                drMC = dtMC.Rows[gridView1.FocusedRowHandle];
                lookUpEdit1.EditValue = gridView1.GetFocusedRowCellValue(_coCompanyID);
                _teCSN.EditValue = gridView1.GetFocusedRowCellDisplayText(_coCompanySN);
                _teCR.EditValue = gridView1.GetFocusedRowCellDisplayText(_coCR);
                _teCPrice.EditValue = Convert.ToDecimal(gridView1.GetFocusedRowCellValue(_coPrice));
                _teRemark.EditValue = gridView1.GetFocusedRowCellDisplayText(_coRemark);
                panel1.Visible = true;
                _teCSN.Focus();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("提示", "是否真的删除？", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    _MCID = Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID));
                    if (_MCID > 0)
                    {
                        BasicClass.GetDataSet.ExecSql("Hownet.BLL.MaterielCompany", "Delete", new object[] { _MCID });
                    }
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    dtMC.AcceptChanges();
                }
            }
        }

        private void lookUpEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "Redo")
            {
                BasicClass.BaseTableClass.ReSupplier();
                lookUpEdit1.Properties.DataSource = BasicClass.BaseTableClass.dtSupplier;
            }
        }
        /// <summary>
        /// 确定MC修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            drMC["MaterielID"] = _ID;
            drMC["CompanyID"] = lookUpEdit1.EditValue;
            drMC["CompanySN"] = _teCSN.EditValue;
            drMC["CompanyRemark"] = _teCR.EditValue;
            drMC["Price"] = _teCPrice.EditValue;
            drMC["Remark"] = _teRemark.EditValue;
            if (_MCID < 0)
            {
                drMC["A"] = 3;
                dtMC.Rows.Add(drMC);
            }
            else if(_MCID>0)
            {
                drMC["A"] = 2;
            }
            panel1.Visible = false;
        }
        /// <summary>
        /// 放弃MC修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if(gridView1.FocusedRowHandle>-1)
            {
                int row = gridView1.FocusedRowHandle;
                int a = 0;
                DataTable dtMCTem = dtMC.Clone();
                for (int i = 0; i < dtMC.Rows.Count; i++)
                {
                    a = Convert.ToInt32(dtMC.Rows[i]["A"]);
                    if (a > 1)
                    {
                        dtMCTem.Rows.Clear();
                        dtMC.Rows[i]["MaterielID"] = _ID;
                        dtMCTem.Rows.Add(dtMC.Rows[i].ItemArray);
                        if (a == 3)
                        {
                            dtMC.Rows[i]["ID"] = BasicClass.GetDataSet.Add("Hownet.BLL.MaterielCompany", dtMCTem);
                        }
                        else if (a == 2)
                        {
                            BasicClass.GetDataSet.UpData("Hownet.BLL.MaterielCompany", dtMCTem);
                        }
                        dtMC.Rows[i]["A"] = 1;
                    }
                }
                r.ChangeText(gridView1.GetRowCellValue(row, _coID).ToString() + "," + gridView1.GetRowCellDisplayText(row, _coCompanyID));
                if (!_sbSaveAndContinue.Visible)
                    this.Close();
            }
        }
    }
}