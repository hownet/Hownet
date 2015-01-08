using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using Hownet.BaseForm;
using BasicClass;

namespace Hownet.Stock
{
    public partial class WaiXieOutForm : DevExpress.XtraEditors.XtraForm
    {
        public WaiXieOutForm()
        {
            InitializeComponent();
        }
        private DataTable dtMInfo = new DataTable();//外加工单明细
        public WaiXieOutForm(DataTable dt)
            : this()
        {
            dtMInfo = dt;
        }
        private string bllSB = "Hownet.BLL.StockBack";
        private string blSBI = "Hownet.BLL.StockBackInfo";
        DataTable dtPS = new DataTable();
  //      DataTable dtPSO = new DataTable();
        DataTable dtPSI = new DataTable();
        DataTable dtJGC = new DataTable();

        ArrayList li = new ArrayList();
        ArrayList liNeed = new ArrayList();

        BindingSource bs = new BindingSource();
        bool _IsVerify = false;
        private DataTable dtMain = new DataTable();//外加工单主表
        
        //private DataTable dtNeed = new DataTable();//物料需求表
        int _MainID = 0;
        int _taskID = 0;
        private void WaiXieOutForm_Load(object sender, EventArgs e)
        {
            bs.PositionChanged += new EventHandler(bs_PositionChanged);
            _loDepot.Properties.DataSource = BasicClass.BaseTableClass.dtDepot;
            _loCom.Properties.DataSource = BasicClass.BaseTableClass.dtProcessing;
            _coNeedColorID.ColumnEdit = _coNeedColorOneID.ColumnEdit = _coNeedColorTwoID.ColumnEdit = _coMInfoColorID.ColumnEdit = _coMInfoColorOne.ColumnEdit = _coMInfoColorTwo.ColumnEdit = RepositoryItem._reColor;
            _coNeedSizeID.ColumnEdit = _coMInfoSizeID.ColumnEdit = RepositoryItem._reSize;
            _coNeedMaterielID.ColumnEdit = _coMInfoMaterielID.ColumnEdit = RepositoryItem._reMateriel;
            _coNeedMeasureID.ColumnEdit = _coMInfoMeasureID.ColumnEdit = RepositoryItem._reMeasure;
            _coBackAmount.ColumnEdit = _coAmount.ColumnEdit = _coMInfoAmount.ColumnEdit = _coMInfoNotAmount.ColumnEdit = _coNeedNeedAmount.ColumnEdit = _coNeedNotAmount.ColumnEdit = RepositoryItem._reNum;
            if (dtMInfo.Rows.Count > 0)
            {
                dtMain.Columns.Add("ID", typeof(int));
                dtMain.Rows.Add(dtMain.NewRow());
                _barFrist.Enabled = _barLast.Enabled = _barNext.Enabled = _barPve.Enabled = barSubItem1.Enabled = false;
                ShowValue(0, true);
            }
            else
            {
                InData();

                bs.Position = dtMain.Rows.Count - 1;
                if (bs.Position == 0)
                    ShowValue(0,false);
            }
        }


        void bs_PositionChanged(object sender, EventArgs e)
        {
            ShowValue(bs.Position,false);
        }
        /// <summary>
        /// 为主导航条绑定值
        /// </summary>
        private void InData()
        {

            dtMain = BasicClass.GetDataSet.GetDS(bllSB, "GetIDList", new object[] { (int)Enums.TableType.ProcessingTask }).Tables[0];
            if (dtMain.Rows.Count == 0)
                dtMain.Rows.Add(dtMain.NewRow());
            bs.DataSource = dtMain;
        }
        /// <summary>
        /// 显示当前主表记录详细情况
        /// </summary>
        /// <param name="p">记录序号</param>
        private void ShowValue(int p,bool f)
        {
            _IsVerify = false;
            li.Clear();
            if (dtMain.DefaultView[p]["ID"].ToString() != "")
            {
                _MainID = int.Parse(dtMain.DefaultView[p]["ID"].ToString());
                dtPS = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=" + _MainID + ")" }).Tables[0];

            }
            else
            {
                dtPS = BasicClass.GetDataSet.GetDS(bllSB, "GetList", new object[] { "(ID=0)" }).Tables[0];
                DataRow dr = dtPS.NewRow();
                dr["CompanyID"] = dr["ID"] = dr["DepotID"] = dr["UpData"] = dr["VerifyMan"] = dr["Money"] = _MainID = 0;
                dr["FillDate"] = dr["DataTime"] = BasicClass.GetDataSet.GetDateTime().Date;
                dr["FillMan"] = BasicClass.UserInfo.UserID;
                dr["IsVerify"] = 1;
                dr["VerifyDate"] = DateTime.Parse("1900-1-1");
                dr["Remark"] = "";
                dr["Num"] = 0;// BasicClass.GetDataSet.GetOne(bllSB, "NewNum", new object[] { DateTime.Today });
                dr["LastMoney"] = 0;
                dr["BackMoney"] = 0;
                dr["BackDate"] = DateTime.Parse("1900-1-1");
                dr["TaskID"] = _taskID = 0;
                dr["DeparmentType"] = 0;
                //  money = 0;
                dtPS.Rows.Add(dr);
                _barAddTable.Enabled = false;
            }
            _IsVerify = (int.Parse((dtPS.Rows[0]["IsVerify"]).ToString()) == 3);
            _laNum.Text = "WXCK-" + Convert.ToDateTime(dtPS.Rows[0]["DateTime"]).ToString("yyyyMMdd") + dtPS.Rows[0]["Num"].ToString().PadLeft(3, '0'); ;
            dateEdit1.EditValue = Convert.ToDateTime(dtPS.Rows[0]["DateTime"]);
            _loCom.EditValue = dtPS.Rows[0]["CompanyID"];
            textEdit2.Text = dtPS.Rows[0]["Remark"].ToString();
            _loDepot.EditValue = dtPS.Rows[0]["DepotID"];
            _barAddTable.Enabled = _barUnVerify.Enabled = _IsVerify;
            _barAddInfo.Enabled = _gvMI.OptionsBehavior.Editable = _gvNeed.OptionsBehavior.Editable = _barSave.Enabled = _barEdit.Enabled = _barDel.Enabled = _barVerify.Enabled = !_IsVerify;
            if (_MainID == 0)
                _barVerify.Enabled = false;
            //_coMInfoAmount.Caption = "总加工量";
            //_coMInfoNotAmount.Caption = "未完成数量";
            //_coMInfoNowAmount.Caption = "总加工数量";
            //_coMInfoNowAmount.VisibleIndex = 5;
            _gvMI.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            _gcNeed.UseEmbeddedNavigator = !_IsVerify;
            if (!f)
                dtMInfo = BasicClass.GetDataSet.GetDS(blSBI, "GetList", new object[] { "(MainID=" + _MainID + ")" }).Tables[0];
            _gcMI.DataSource = dtMInfo;
            dtPSI = BasicClass.GetDataSet.GetDS(blSBI, "GetList", new object[] { "(MainID=" + _MainID * -1 + ")" }).Tables[0];
            _gcNeed.DataSource = dtPSI;
            linkLabel1.Visible = _IsVerify;
            if (p != dtMain.Rows.Count - 1)
                _barUnVerify.Enabled = false;
        }
        /// <summary>
        /// 取消已选颜色，设置为0
        /// </summary>
        private void _reColor_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag.ToString() == "Del")
            {
                _gvMI.SetFocusedValue(0);
            }
        }
        /// <summary>
        /// 单无格改变后，更新A字段值，如果是物料列，设置当前行的单位名称值
        /// </summary>
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coMInfoA)
            {
                if (_gvMI.GetFocusedRowCellValue(_coMInfoA).ToString() == "1")
                    _gvMI.SetFocusedRowCellValue(_coMInfoA, 2);
                else if (_gvMI.GetFocusedRowCellValue(_coMInfoA).ToString() == "0")
                    _gvMI.SetFocusedRowCellValue(_coMInfoA, 3);
            }
            if (e.Column == _coMInfoNowAmount && e.Value != null)
            {
                _gvMI.SetFocusedRowCellValue(_coMInfoNotAmount, decimal.Parse(_gvMI.GetFocusedRowCellValue(_coMInfoTemAmount).ToString()) - decimal.Parse(e.Value.ToString()));
            }
            if (e.Column == _coMInfoMaterielID)
            {
                _gvMI.SetFocusedRowCellValue(_coMInfoMeasureID, bllMat.GetModel(int.Parse(e.Value.ToString())).DefaultMeasureID);
            }
            if (e.Column == _coMInfoAmount && e.Value != null && modMM.IsVerify == 1)
            {
                _gvMI.SetFocusedRowCellValue(_coMInfoNotAmount, e.Value);
            }
            //Hownet.BLL.BaseFile.MaterielDemandClass bllMDC = new Hownet.BLL.BaseFile.MaterielDemandClass();
            //DataTable dt = bllMDC.GetNeedAmount((DataTable)(_gcMI.DataSource), modMM.ID);
            //_gcNeed.DataSource = dt;
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            _gvNeed.CloseEditor();
            _gvMI.CloseEditor();
            if (_gvMI.RowCount > 0 && _gvNeed.RowCount == 0)
            {
                CalcNeed();
            }
            if (_loCom.EditValue != null)
                modMM.CompanyID = int.Parse(_loCom.EditValue.ToString());
            else
                return;
            if (_loDepot.EditValue != null)
                modMM.DepotID = int.Parse(_loDepot.EditValue.ToString());
            else
                return;
            modMM.DepotExecutant = BasicClass.UserInfo.UserID;
            modMM.DateTime = (DateTime)(dateEdit1.EditValue);
            modMM.LastDateTime = (DateTime)(dateEdit2.EditValue);
            modMM.Remark = textEdit2.Text.Trim();
            modMM.IsVerify = 1;
            modMM.VerifyDateTime = null;
            modMM.VerifyManID = 0;
            if (modMM.ID == 0)
            {
                modMM.Num = bllMM.GetNewNum((DateTime)(dateEdit1.EditValue));
                modMM.ID = bllMM.Add(modMM);
            }
            else
                bllMM.Update(modMM);
            bllMI.Save((DataTable)(_gcMI.DataSource),modMM.ID,li);
            li.Clear();
            //bllMNA.DelWhere(modMM.ID);
            bllMNA.Save((DataTable)(_gcNeed.DataSource),modMM.ID,liNeed);
            int p = bs.Position;
            InData();
            bs.Position = p;
            if (p == dtMain.Rows.Count - 1)
                ShowValue(p,false);
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (modMM.ID != 0)
                return;
            Hownet.BLL.ProduceTaskMain bllPTM = new Hownet.BLL.ProduceTaskMain();
            li.Clear();
            string tem = buttonEdit1.Text;
            string[] split = tem.Split(new Char[] { ',' });
            foreach (string a in split)
            {
                if (a.Trim() != "")
                    li.Add(bllPTM.GetID(a.Trim()).ToString());
            }
            cResult r = new cResult();
            r.TextChanged += new TextChangedHandler(r_TextChanged);
            Form fr = new ERP.WuKong.PeiLiaoTem(r, li);
            fr.ShowDialog();
        }
        void r_TextChanged(string s)
        {
            Hownet.BLL.BaseFile.PeiLiao PL = new Hownet.BLL.BaseFile.PeiLiao();
            if (s.Length > 0)
            {
                s = s.Remove(s.Length - 1);
                this.buttonEdit1.Text = s;
                li.Clear();
                string[] split = s.Split(new Char[] { ',' });
                foreach (string a in split)
                {
                    if (a.Trim() != "")
                        li.Add(a.Trim());
                }
                buttonEdit1.Text = PL.ID2Num(s);
                dtMInfo = PL.GetWaiXie(li);
                _gcMI.DataSource = dtMInfo;
                _coMInfoAmount.Visible = true;
                _coMInfoAmount.Caption = "总需求量";
                _coMInfoNotAmount.Caption = "未加工数量";
                _coMInfoNowAmount.Caption = "本次数量";
                _coMInfoNowAmount.VisibleIndex = 7;
            }
        }
        #region 记录移动
        private void _barFrist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveFirst();
        }

        private void _barPve_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MovePrevious();
        }

        private void _barNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveNext();
        }

        private void _barLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.MoveLast();
        }

       private void _barExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion
        private void _barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form fr = new DepPeiLiaoForm(1, modMM.ID);
            fr.ShowDialog();
        }

        private void _barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private void _gvMI_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            _gvMI.SetFocusedRowCellValue(_coMInfoColorID, 0);
            _gvMI.SetFocusedRowCellValue(_coMInfoColorOne, 0);
            _gvMI.SetFocusedRowCellValue(_coMInfoColorTwo, 0);
            _gvMI.SetFocusedRowCellValue(_coMInfoSizeID, 0);
            _gvMI.SetFocusedRowCellValue(_coMInfoAmount, 0);
            _gvMI.SetFocusedRowCellValue(_coMInfoNotAmount, 0);
            _gvMI.SetFocusedRowCellValue(_coMInfoTemAmount, 0);
            _gvMI.SetFocusedRowCellValue(_coMInfoNowAmount, 0);
            _gvMI.SetFocusedRowCellValue(_coMInfoRemark, "");
            _gvMI.SetFocusedRowCellValue(_coMInfoA, 3);
            _gvMI.SetFocusedRowCellValue(_coMInfoDemandID, 0);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bs.AddNew();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CalcNeed();
        }
        private void CalcNeed()
        {
             Hownet.BLL.BaseFile.MaterielDemandClass bllMDC = new Hownet.BLL.BaseFile.MaterielDemandClass();
            DataTable dt = bllMDC.GetNeedAmount((DataTable)(_gcMI.DataSource), modMM.ID);
            _gcNeed.DataSource = dt;
        }
        private void _gvMI_MouseUp(object sender, MouseEventArgs e)
        {
            if (modMM.IsVerify!=3)
            {
                if (e.Button == MouseButtons.Right)
                    DoShowMenu(_gvMI.CalcHitInfo(new Point(e.X, e.Y)));
            }
        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void _barVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (((DataTable)(_gcMI.DataSource)).GetChanges() != null || ((DataTable)(_gcNeed.DataSource)).GetChanges() != null)
                Save();
            modMM.IsVerify = 3;
            modMM.VerifyDateTime = DateTime.Today;
            modMM.VerifyManID = BasicClass.UserInfo.UserID;
            bllMM.Update(modMM);
            ShowValue(bs.Position, false);
        }

        private void _barUnVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bllMM.CountOut(modMM.ID) > 0)
            {
                XtraMessageBox.Show("本单已有后续操作，不能弃审");
                return;
            }
            modMM.IsVerify = 1;
            modMM.VerifyDateTime = DateTime.Parse("1900-1-1");
            modMM.VerifyManID = 0;
            bllMM.Update(modMM);
            ShowValue(bs.Position, false);
        }

        private void _barDelInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                if (_gvMI.FocusedRowHandle > -1)
                {
                    li.Add(_gvMI.GetFocusedRowCellValue(_coMInfoID).ToString());
                    _gvMI.DeleteRow(_gvMI.FocusedRowHandle);
                    CalcNeed();
                }
            }
        }

        private void _barDelTable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogResult.Yes == XtraMessageBox.Show("是否真的删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                for (int i = 0; i < _gvNeed.RowCount; i++)
                {
                    if (_gvNeed.GetRowCellValue(i, _coNeedID).ToString() != "")
                        bllMNA.Delete(int.Parse(_gvNeed.GetRowCellValue(i, _coNeedID).ToString()));
                }
                for (int i = 0; i < _gvMI.RowCount; i++)
                {
                    if (_gvMI.GetRowCellValue(i, _coMInfoID).ToString() != "")
                        bllMI.Delete(int.Parse(_gvMI.GetRowCellValue(i, _coMInfoID).ToString()));
                }
                bllMM.Delete(modMM.ID);
                InData();
                bs.Position = dtMain.Rows.Count - 1;
                if (bs.Position == 0 && dtMInfo.Rows.Count == 0)
                {
                    ShowValue(0, false);
                }
                if (dtMInfo.Rows.Count > 0)
                    this.Close();
            }
        }

        private void _gcNeed_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.Tag.ToString() == "Del" && _gvNeed.GetFocusedRowCellValue(_coNeedID).ToString() != "")
                liNeed.Add(_gvNeed.GetFocusedRowCellValue(_coNeedID).ToString());
        }

        private void _gvNeed_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column != _coNeedA)
            {
                if (_gvNeed.GetFocusedRowCellValue(_coNeedA).ToString() == "1")
                    _gvNeed.SetFocusedRowCellValue(_coNeedA, 2);
            }
            if (e.Column == _coNeedNeedAmount)
                _gvNeed.SetFocusedRowCellValue(_coNeedNotAmount, e.Value);
        }

        private void _barAddInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _gvMI.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
        }
    }
}