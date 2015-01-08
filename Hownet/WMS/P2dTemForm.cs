using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Collections;
using DevExpress.XtraGrid.Views.BandedGrid;
using BasicClass;

namespace Hownet.WMS
{
    public partial class P2dTemForm : DevExpress.XtraEditors.XtraForm
    {
        public P2dTemForm()
        {
            InitializeComponent();
        }
        cResult r = new cResult();
        int _mainID = 0;
        int _taskID = 0;
        DataTable dtInfo = new DataTable();
        public P2dTemForm(cResult rs, DataTable dt,int TaskID,int MainID)
            : this()
        {
            r = rs;
            dtInfo = dt;
            _taskID = TaskID;
            _mainID = MainID;
        }
        ArrayList ColorList = new ArrayList();
        ArrayList SizeList = new ArrayList();
        ArrayList ColorOneList = new ArrayList();
        ArrayList ColorTwoList = new ArrayList();
        ArrayList SizeNameList = new ArrayList();
        //string materielName = "";
        private void SellTemForm_Load(object sender, EventArgs e)
        {
            InData();
        }
        private DataTable ShowInfo(int TaskID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Color", typeof(string));
            //该生产单的明细数量
            DataTable dtTInfo = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllAmountInfo, "GetList", new object[] { "(MainID=" + TaskID + ") And (TableTypeID=" + (int)BasicClass.Enums.TableType.Task + ")" }).Tables[0];
            dt.Rows.Add(dt.NewRow());
            dt.Rows[0]["Color"] = "颜色\\尺码";//添加颜色字段
            //该生产单的颜色列表
            DataTable dtSize = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllAmountInfo, "GetSize", new object[] { TaskID, (int)BasicClass.Enums.TableType.Task }).Tables[0];

            int i = 1;

            ColorList.Clear();
            ColorOneList.Clear();
            ColorTwoList.Clear();
            SizeList.Clear();
            ColorList.Add(0);
            SizeList.Add(0);
            ColorOneList.Add(0);
            ColorTwoList.Add(0);
            advBandedGridView2.Bands.Clear();
            advBandedGridView2.Columns.Clear();
            advBandedGridView2.OptionsView.ShowColumnHeaders = false;//清空表

            for (int s = 0; s < dtSize.Rows.Count; s++)//以尺码循环，添加列，每个尺码添加两列，并将尺码名称添加到SizeNameList，将尺码 ID添加到SizeList
            {
                int m = 0;
                if (i == 1)
                    m = 1;
                else
                    m = i * 2 - 1;
                dt.Columns.Add("Columns" + i);
                dt.Columns.Add("Columnss" + i);
                dt.Rows[0][m] = dtSize.Rows[s][0].ToString();
                SizeList.Add(int.Parse(dtSize.Rows[s][1].ToString()));
                SizeNameList.Add(dtSize.Rows[s][0].ToString());
                i++;
            }
            dt.Columns.Add("SumNum", typeof(string));//添加在线合计列
            dt.Rows[0]["SumNum"] = "在线";
            dt.Columns.Add("GoSum", typeof(string));//添加入库合计列
            dt.Rows[0]["GoSum"] = "入库";
            dt.Columns.Add("ColorOne", typeof(string));
            dt.Columns.Add("ColorTwo", typeof(string));
            dt.Rows[0]["ColorOne"] = "插色一";
            dt.Rows[0]["ColorTwo"] = "插色二";
            for (int j = 0; j < dt.Columns.Count - 4; j++)//为advBandedGridView2添加 Bands
            {
                advBandedGridView2.Bands.Add();
                advBandedGridView2.Columns.Add();
                advBandedGridView2.Columns[j].FieldName = dt.Columns[j].ColumnName;
                advBandedGridView2.Bands[j].Columns.Add((advBandedGridView2.Columns[j]) as BandedGridColumn);
                if (j > 0)
                {
                    if ((j % 2) == 0)
                    {
                        advBandedGridView2.Bands[j].Caption = "入库";
                    }
                    else
                    {
                        advBandedGridView2.Bands[j].Caption = "在线";
                        advBandedGridView2.Columns[j].OptionsColumn.AllowEdit = false;
                    }
                }
                else
                {
                    advBandedGridView2.Bands[j].Caption = "颜色";
                    advBandedGridView2.Columns[j].OptionsColumn.AllowEdit = false;
                }

                advBandedGridView2.Bands[j].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                advBandedGridView2.Columns[j].Visible = true;
                advBandedGridView2.Columns[j].Width = 60;
            }
            int bCount = advBandedGridView2.Bands.Count;//将每个尺码的在线、入库两个Brand添加到尺码名称的Brand下，组成多维表头
            for (int j = 0; j < SizeNameList.Count; j++)
            {
                advBandedGridView2.Bands.Add();
                advBandedGridView2.Bands[bCount + j].Caption = SizeNameList[j].ToString();
                advBandedGridView2.Bands[bCount + j].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            for (int j = 0; j < SizeNameList.Count; j++)
            {
                advBandedGridView2.Bands[bCount - j].Children.AddRange(new GridBand[] { advBandedGridView2.Bands[1], advBandedGridView2.Bands[2] });
            }
            bCount = advBandedGridView2.Bands.Count;
            i = 0;
            for (int j = dt.Columns.Count - 4; j < dt.Columns.Count; j++)
            {
                advBandedGridView2.Bands.Add();
                advBandedGridView2.Columns.Add();
                advBandedGridView2.Columns[j].FieldName = dt.Columns[j].ColumnName;
                advBandedGridView2.Bands[bCount + i].Caption = dt.DefaultView[0][j].ToString();
                advBandedGridView2.Bands[bCount + i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                advBandedGridView2.Bands[bCount + i].Columns.Add((advBandedGridView2.Columns[j]) as BandedGridColumn);
                advBandedGridView2.Columns[j].Visible = true;
                advBandedGridView2.Columns[j].Width = 60;
                advBandedGridView2.Columns[j].OptionsColumn.AllowEdit = false;
                i++;
            }
            advBandedGridView2.Bands.Add();
            advBandedGridView2.Bands[advBandedGridView2.Bands.Count - 1].Caption = "合计";
            advBandedGridView2.Bands[advBandedGridView2.Bands.Count - 1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            advBandedGridView2.Bands[advBandedGridView2.Bands.Count - 1].Children.AddRange(new GridBand[] { advBandedGridView2.Bands[bCount], advBandedGridView2.Bands[bCount + 1] });
            DataTable dtcolor = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllAmountInfo, "GetSumColor", new object[] { TaskID, (int)BasicClass.Enums.TableType.Task }).Tables[0];
            i = 1;
            for (int c = 0; c < dtcolor.Rows.Count; c++)
            {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[i][0] = dtcolor.Rows[c][0].ToString();
                dt.Rows[i][dt.Columns.Count - 4] = dtcolor.Rows[c][7].ToString();//未入库总数
                dt.Rows[i][dt.Columns.Count - 2] = dtcolor.Rows[c][3].ToString();
                dt.Rows[i][dt.Columns.Count - 1] = dtcolor.Rows[c][5].ToString();
                ColorList.Add(int.Parse(dtcolor.Rows[c][1].ToString()));
                ColorOneList.Add(int.Parse(dtcolor.Rows[c][4].ToString()));
                ColorTwoList.Add(int.Parse(dtcolor.Rows[c][6].ToString()));
                i++;
            }
            for (int r = 1; r < SizeList.Count; r++)
            {
                for (int c = 1; c < ColorList.Count; c++)
                {
                    int m = 0;
                    if (r == 1)
                        m = 1;
                    else
                        m = r * 2 - 1;
                    //modDTI.ColorID = int.Parse(ColorList[c].ToString());
                    //modDTI.SizeID = int.Parse(SizeList[r].ToString());
                    //modDTI.ColorOneID = int.Parse(ColorOneList[c].ToString());
                    //modDTI.ColorTwoID = int.Parse(ColorTwoList[c].ToString());
                    //modDTI.MainID = TaskID;

                    string sql = "(SizeID=" + SizeList[r] + ") and (ColorID=" + ColorList[c] + ") and (ColorOneID=" + ColorOneList[c] + ") and (ColorTwoID=" + ColorTwoList[c] + ") And (MainID=" + _taskID + ")";
                    DataRow[] drs = dtTInfo.Select(sql);
                    if (drs.Length > 0)
                        dt.Rows[c][m] = drs[0]["NotAmount"];
                    //dt.Rows[c][m] = bllPTI.GetNotAmount(modPTI);
                    if (dtInfo.Rows.Count > 0)
                    {
                        for (int x = 0; x < dtInfo.Rows.Count; x++)
                        {
                            if (dtInfo.DefaultView[x]["ColorID"].ToString() == ColorList[c].ToString() && dtInfo.DefaultView[x]["SizeID"].ToString() == SizeList[r].ToString() && dtInfo.DefaultView[x]["ColorOneID"].ToString() == ColorOneList[c].ToString() && dtInfo.DefaultView[x]["ColorTwoID"].ToString() == ColorTwoList[c].ToString())
                            {
                                dt.Rows[c][m + 1] = dtInfo.DefaultView[x]["Amount"];
                            }
                        }
                    }
                }
            }
            dt.Rows.RemoveAt(0);
            return dt;
        }
        void InData()
        {
            gridControl2.DataSource = ShowInfo(_taskID);
            for (int i = 0; i < advBandedGridView2.RowCount; i++)
            {
                if (advBandedGridView2.GetRowCellValue(i, advBandedGridView2.Columns[0]).ToString() != string.Empty)
                {
                    SumRow(i);
                }
            }
            SumAmount();
        }

        private void advBandedGridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.ColumnHandle < advBandedGridView2.Columns.Count - 4)
            {
                int amount = 0;
                if (e.Value.ToString() != string.Empty)
                {
                    try
                    {
                        amount = int.Parse(e.Value.ToString());
                        if (amount > int.Parse(advBandedGridView2.GetRowCellValue(e.RowHandle, advBandedGridView2.Columns[e.Column.ColumnHandle - 1]).ToString()))
                        {
                            if (DialogResult.No == XtraMessageBox.Show("超过原有数量，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            {
                                advBandedGridView2.SetRowCellValue(e.RowHandle, e.Column, string.Empty);
                                return;
                            }
                        }
                        SumRow(e.RowHandle);
                        SumAmount();
                    }
                    catch
                    {
                        XtraMessageBox.Show("只能填整数！");
                        advBandedGridView2.SetRowCellValue(e.RowHandle, e.Column, string.Empty);
                    }
                }
            }
        }
        private void SumRow(int rowHandle)
        {
            int amount = 0;
            for (int i = 2; i < advBandedGridView2.Columns.Count - 4; i++)
            {
                if ((i % 2) == 0)
                {
                    if (advBandedGridView2.GetRowCellValue(rowHandle, advBandedGridView2.Columns[i]).ToString() != string.Empty)
                    {
                        amount = amount + int.Parse(advBandedGridView2.GetRowCellValue(rowHandle, advBandedGridView2.Columns[i]).ToString());
                    }
                }
            }
            advBandedGridView2.SetRowCellValue(rowHandle, advBandedGridView2.Columns[advBandedGridView2.Columns.Count - 3], amount);
        }
        private void SumAmount()
        {
            int amount = 0;
            for (int i = 0; i < advBandedGridView2.RowCount; i++)
            {
                if (advBandedGridView2.GetRowCellValue(i, "GoSum").ToString() != string.Empty)
                {
                    amount = amount + int.Parse(advBandedGridView2.GetRowCellValue(i, "GoSum").ToString());
                }
            }
            labelControl2.Text = "合计： " + amount.ToString() + "件";
        }

        private void _buOK_Click(object sender, EventArgs e)
        {
            DataTable dtTm = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetList", new object[] { "(ID=" + _taskID + ")" }).Tables[0];
            int materielID = int.Parse(dtTm.Rows[0]["MaterielID"].ToString());
            int BrandID = int.Parse(dtTm.Rows[0]["BrandID"].ToString());
            DataTable dt = (DataTable)(gridControl2.DataSource);
            int colorID = 0;
            int colorOneID = 0;
            int colorTwoID = 0;
            int amount=0;
            dtInfo.Rows.Clear();
            for (int i = 1; i < ColorList.Count; i++)
            {
                colorID = int.Parse(ColorList[i].ToString());
                if (ColorOneList[i].ToString() != string.Empty)
                    colorOneID = int.Parse(ColorOneList[i].ToString());
                if (ColorTwoList[i].ToString() != string.Empty)
                    colorTwoID = int.Parse(ColorTwoList[i].ToString());
                for (int m = 1; m < SizeList.Count; m++)
                {
                    if (dt.DefaultView[i - 1][m * 2].ToString() != string.Empty)
                    {
                        string strAmount = dt.DefaultView[i - 1][m * 2].ToString();
                        amount = int.Parse(strAmount);
                        if (amount != 0)
                        {
                            DataRow dr = dtInfo.NewRow();
                            dr["ID"] = 0;
                            dr["MainID"] = _mainID;
                            dr["MaterielID"] = materielID;
                            dr["ColorID"] = colorID;
                            dr["ColorOneID"] = colorOneID;
                            dr["ColorTwoID"] = colorTwoID;
                            dr["SizeID"] = SizeList[m].ToString();
                            dr["BrandID"] = BrandID;
                            dr["MListID"] = 0;
                            dr["Remark"] = "";
                            dr["TaskID"] = _taskID;
                            dr["DeparmentID"] = 0;
                            dr["Amount"] = amount;
                            dr["A"] = 3;
                            //dr["MaterielName"] = materielName;
                            //dr["ColorName"] = bllMA.GetModel(colorID).MaterielAttributeName;
                            //dr["SizeName"] = bllMA.GetModel(int.Parse(SizeList[m].ToString())).MaterielAttributeName;
                            dtInfo.Rows.Add(dr);
                        }
                    }
                }
            }
            //dtInfo.Rows.Add(dtInfo.NewRow());
            //dtInfo.Rows[dtInfo.Rows.Count - 1]["MainID"] = rowID;
            r.RowChang(dtInfo);
            this.Close();
        }

        private void _buCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}