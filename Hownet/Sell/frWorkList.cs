using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.Sell
{
    public partial class frWorkList : DevExpress.XtraEditors.XtraForm
    {
        public frWorkList()
        {
            InitializeComponent();
        }
        private int _materielID, _brandID;
        private int _colorID, _colorOneID, _colorTwoID, _sizeID;
        private string _Materiel, _Brand;
        private DataTable dt = new DataTable();
        public frWorkList(int MaterielID, int BrandID, string Materiel, string Brand, DataTable dtt)
            : this()
        {
            _materielID = MaterielID;
            _brandID = BrandID;
            _Materiel = Materiel;
            _Brand = Brand;
            dt = dtt;
        }
        private void frWorkList_Load(object sender, EventArgs e)
        {
            label1.Text = "款号：" + _Materiel + "  商标:" + _Brand + "";
            DataTable dtList = dt.Clone();
            DataTable ddd;

            int _tableTypeID = (int)BasicClass.Enums.TableType.Task;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["Amount"]) < 0)
                {
                    _colorID = Convert.ToInt32(dt.Rows[i]["ColorID"]);
                    _colorOneID = Convert.ToInt32(dt.Rows[i]["ColorOneID"]);
                    _colorTwoID = Convert.ToInt32(dt.Rows[i]["ColorTwoID"]);
                    _sizeID = Convert.ToInt32(dt.Rows[i]["SizeID"]);
                    ddd = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetWorkAmountByCS", new object[] {_materielID,_brandID,_tableTypeID,_colorID,_colorOneID,_colorTwoID,_sizeID }).Tables[0];
                    if (ddd.Rows.Count > 0)
                    {
                        dtList.Rows.Add(ddd.Rows[0].ItemArray);
                    }
                }
            }
            gridControl2.DataSource = dtList;
            _coColorID.ColumnEdit = BaseForm.RepositoryItem._reColor;
            _coSizeID.ColumnEdit = BaseForm.RepositoryItem._reSize;
            _coWorkingID.ColumnEdit = BaseForm.RepositoryItem._reWorking;
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                _colorID = int.Parse(gridView2.GetFocusedRowCellValue(_coColorID).ToString());
                _sizeID = int.Parse(gridView2.GetFocusedRowCellValue(_coSizeID).ToString());
                DataTable dtW = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllWorkTicketInfo, "GetWorking", new object[] {_materielID,_brandID,  _colorID, _sizeID }).Tables[0];
                gridControl3.DataSource = dtW;
            }
            catch { }
        }
    }
}