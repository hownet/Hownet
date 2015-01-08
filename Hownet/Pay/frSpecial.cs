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
    public partial class frSpecial : DevExpress.XtraEditors.XtraForm
    {
        public frSpecial()
        {
            InitializeComponent();
        }
        BasicClass.cResult r = new BasicClass.cResult();
        int _PWIID =0;
        int _Group = 0;
        int _Orders = 0;
        int _MainID = 0;
        int _WorkingID = 0;
        int _OneAmount = 0;
        bool _IsCaiC = false;
        bool _IsCut = false;
        bool _IsTicket = false;
        bool _IsCanMove = false;
        string _WorkingName = string.Empty;
        DataTable dtColor = new DataTable();
        DataTable dtCompany = new DataTable();
        DataTable dtPWI = new DataTable();
        DataTable dtSize = new DataTable();
        public frSpecial(BasicClass.cResult cr, int pwiID,string WorkingName)
            : this()
        {
            r = cr;
            _PWIID = pwiID;
            _WorkingName = WorkingName;
        }
        private void frSpecial_Load(object sender, EventArgs e)
        {
            DataTable ddddt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductWorkingInfo, "GetList", new object[] { "(ID=" + _PWIID + ")" }).Tables[0];
            _Group = Convert.ToInt32(ddddt.Rows[0]["GroupBy"]);
            _Orders = Convert.ToInt32(ddddt.Rows[0]["Orders"]);
            _MainID = Convert.ToInt32(ddddt.Rows[0]["MainID"]);
            _IsCaiC = Convert.ToBoolean(ddddt.Rows[0]["IsCaiC"]);
            _IsCut = Convert.ToBoolean(ddddt.Rows[0]["IsCut"]);
            _WorkingID = Convert.ToInt32(ddddt.Rows[0]["WorkingID"]);
            _OneAmount = Convert.ToInt32(ddddt.Rows[0]["OneAmount"]);
            _IsTicket = Convert.ToBoolean(ddddt.Rows[0]["IsTicket"]);
            _IsCanMove = Convert.ToBoolean(ddddt.Rows[0]["IsCanMove"]);
            labelControl1.Text = _WorkingName + " 的普通工序结构：";
            _coWorkingID.ColumnEdit = BaseForm.RepositoryItem._reNotSpecialWorking;
            dtColor = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllColor, "GetAllList", null).Tables[0];
            DataRow dr = dtColor.NewRow();
            dr["ID"] = 0;
            dr["Name"] = string.Empty;
            dtColor.Rows.Add(dr);
            dtColor.DefaultView.Sort = "ID";
            _reColor.DataSource = dtColor.DefaultView;

            dtCompany = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "(TypeID=1)" }).Tables[0];
            DataRow drr = dtCompany.NewRow();
            drr["ID"] = 0;
            drr["Name"] = string.Empty;
            dtCompany.Rows.Add(drr);
            dtCompany.DefaultView.Sort = "ID";
            _reCompany.DataSource = dtCompany.DefaultView;

            dtSize = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSize, "GetAllList", null).Tables[0];
            DataRow ddrr = dtSize.NewRow();
            ddrr["ID"] = 0;
            ddrr["Name"] = string.Empty;
            dtSize.Rows.Add(ddrr);
            dtSize.DefaultView.Sort = "ID";
            _reSize.DataSource = dtSize.DefaultView;


            dtPWI = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductWorkingInfo, "GetList", new object[] { "(PWIID=" + _PWIID + ")" }).Tables[0];
            DataRow ddr = dtPWI.NewRow();
            ddr["MainID"] = -1;
            ddr["PWIID"] = _PWIID;
            ddr["GroupBy"] = _Group;
            ddr["Orders"] = _Orders;
            ddr["A"] = 0;
            ddr["ID"] = ddr["CompanyID"] = ddr["ColorID"] = ddr["WorkingID"] = 0;
            ddr["MainID"] = _MainID * -1;
            ddr["IsCaiC"] = ddr["IsCut"] = true;
            ddr["SpecialWork"] = _WorkingID;
            ddr["IsTicket"] = _IsTicket;
            ddr["OneAmount"] = _OneAmount;
            ddr["IsCanMove"] = _IsCanMove;
            dtPWI.Rows.Add(ddr.ItemArray);
            dtPWI.Rows.Add(ddr.ItemArray);
            gridControl1.DataSource = dtPWI;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridView1.OptionsBehavior.Editable = (e.FocusedRowHandle < gridView1.RowCount - 1);
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle == gridView1.RowCount - 2)
            {
                DataRow ddr = dtPWI.NewRow();
                ddr["MainID"] = -1;
                ddr["PWIID"] = _PWIID;
                ddr["GroupBy"] = _Group;
                ddr["Orders"] = _Orders;
                ddr["A"] = 0;
                ddr["ID"] = ddr["CompanyID"] = ddr["ColorID"] = ddr["WorkingID"] = 0;
                ddr["MainID"] = _MainID * -1;
                ddr["IsCaiC"] = ddr["IsCut"] = true;
                ddr["SpecialWork"] = _WorkingID;
                ddr["IsTicket"] = _IsTicket;
                ddr["OneAmount"] = _OneAmount;
                ddr["IsCanMove"] = _IsCanMove;
                dtPWI.Rows.Add(ddr.ItemArray);
            }
            if (e.Column != _coA)
            {
                if (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 0)
                    gridView1.SetFocusedRowCellValue(_coA, 3);
                else if (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coA)) == 1)
                    gridView1.SetFocusedRowCellValue(_coA, 2);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            dtPWI.AcceptChanges();
            if (CheckRepeat())
                return;
            DataTable dtt = dtPWI.Clone();
            int a = 0;
            for (int i = 0; i < dtPWI.Rows.Count - 2; i++)
            {
                a = Convert.ToInt32(dtPWI.Rows[i]["A"]);
                if (a > 1)
                {
                    dtt.Rows.Clear();
                    dtt.Rows.Add(dtPWI.Rows[i].ItemArray);
                    if (a == 2)
                        BasicClass.GetDataSet.UpData(BasicClass.Bllstr.bllProductWorkingInfo, dtt);
                    else if (a == 3)
                        dtPWI.Rows[i]["ID"] = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllProductWorkingInfo, dtt);
                    dtPWI.Rows[i]["A"] = 1;
                }
            }
            r.ChangeText("OK");
        }
        private bool CheckRepeat()
        {
            bool t = false;
            for (int i = 0; i < gridView1.RowCount - 3; i++)
            {
                for (int j = i + 1; j < gridView1.RowCount - 2; j++)
                {
                    string aaa = gridView1.GetRowCellValue(i, _coColorID).ToString() + "-" + gridView1.GetRowCellValue(i, _coCompanyID).ToString() + "-" + gridView1.GetRowCellValue(i, _coWorkingID).ToString();
                    string bb = gridView1.GetRowCellValue(j, _coColorID).ToString() + "-" + gridView1.GetRowCellValue(j, _coCompanyID).ToString() + "-" + gridView1.GetRowCellValue(j, _coWorkingID).ToString();
                    if ((gridView1.GetRowCellValue(i, _coColorID).ToString() + "-" + gridView1.GetRowCellValue(i, _coCompanyID).ToString() + "-" + gridView1.GetRowCellValue(i, _coWorkingID).ToString()) ==
                        (gridView1.GetRowCellValue(j, _coColorID).ToString() + "-" + gridView1.GetRowCellValue(j, _coCompanyID).ToString() + "-" + gridView1.GetRowCellValue(j, _coWorkingID).ToString()))
                    {
                        XtraMessageBox.Show("不能有重复的记录！");
                        return true;
                    }
                }
            }
            return t;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle > -1 && gridView1.FocusedRowHandle < gridView1.RowCount - 2)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("删除后，已打印工票将不能正常录入，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    if (DialogResult.Yes == XtraMessageBox.Show("删除后，已打印工票将不能正常录入，请再次确认是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {

                        if (Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)) > 0)
                        {
                            if (Convert.ToInt32(BasicClass.GetDataSet.GetOne(BasicClass.Bllstr.bllProductWorkingInfo, "GetPWIDUseCount", new object[] { Convert.ToInt32(gridView1.GetFocusedRowCellValue(_coID)) })) > 0)
                            {
                                XtraMessageBox.Show("该工序已有生成工票，不能被删除！");
                                return;
                            }
                            BasicClass.GetDataSet.ExecSql(BasicClass.Bllstr.bllProductWorkingInfo, "Delete", new object[] { int.Parse(gridView1.GetFocusedRowCellValue(_coID).ToString()) });
                        }
                        gridView1.DeleteRow(gridView1.FocusedRowHandle);
                        dtPWI.AcceptChanges();
                    }
                }

            }
        }

    }
}