using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hownet.BaseContranl
{
    public  class AmountListClass
    {
        public static DataTable GetList(DataTable dtInfo)
        {
            DataTable dtColor = new DataTable();
            DataTable dtSize = new DataTable();
            DataTable dtColorList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllColor, "GetAllList", null).Tables[0];
            DataTable dtSizeList = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllSize, "GetAllList", null).Tables[0];
            dtColor.Columns.Add("ID", typeof(int));
            dtColor.Columns.Add("OneID", typeof(int));
            dtColor.Columns.Add("TwoID", typeof(int));
            dtColor.Columns.Add("ColorName", typeof(string));
            dtColor.Columns.Add("ColorOneName", typeof(string));
            dtColor.Columns.Add("ColorTwoName", typeof(string));
            dtSize.Columns.Add("ID", typeof(int));
            dtSize.Columns.Add("SizeName", typeof(string));
            dtSize.Columns.Add("Orders", typeof(int));
            DataRow dr;
            bool t = false;
            int ColorID, SizeID, ColorOneID, ColorTwoID;
            for (int r = 0; r < dtInfo.Rows.Count; r++)
            {
                t = false;
                for (int rr = 0; rr < dtColor.Rows.Count; rr++)
                {
                    if (Convert.ToInt32(dtInfo.Rows[r]["ColorID"]) == Convert.ToInt32(dtColor.Rows[rr]["ID"]) &&
                        Convert.ToInt32(dtInfo.Rows[r]["ColorOneID"]) == Convert.ToInt32(dtColor.Rows[rr]["OneID"]) &&
                        Convert.ToInt32(dtInfo.Rows[r]["ColorTwoID"]) == Convert.ToInt32(dtColor.Rows[rr]["TwoID"]))
                    {
                        t = true;
                        break;
                    }
                }
                if (!t)
                {
                    if (Convert.ToInt32(dtInfo.Rows[r]["ColorID"]) > 0)
                    {
                        dr = dtColor.NewRow();
                        dr["ID"] = dtInfo.Rows[r]["ColorID"];
                        dr["ColorName"] = dtColorList.Select("(ID=" + dtInfo.Rows[r]["ColorID"] + ")")[0]["Name"];
                        dr["OneID"] = dtInfo.Rows[r]["ColorOneID"];
                        if (Convert.ToInt32(dtInfo.Rows[r]["ColorOneID"]) > 0)
                            dr["ColorOneName"] = dtColorList.Select("(ID=" + dtInfo.Rows[r]["ColorOneID"] + ")")[0]["Name"];
                        else
                            dr["ColorOneName"] = string.Empty;
                        dr["TwoID"] = dtInfo.Rows[r]["ColorTwoID"];
                        if (Convert.ToInt32(dtInfo.Rows[r]["ColorTwoID"]) > 0)
                            dr["ColorTwoName"] = dtColorList.Select("(ID=" + dtInfo.Rows[r]["ColorTwoID"] + ")")[0]["Name"];
                        else
                            dr["ColorTwoName"] = string.Empty;
                        dtColor.Rows.Add(dr);
                    }
                }
            }
            for (int r = 0; r < dtInfo.Rows.Count; r++)
            {
                t = false;
                for (int rr = 0; rr < dtSize.Rows.Count; rr++)
                {
                    if (Convert.ToInt32(dtInfo.Rows[r]["SizeID"]) == Convert.ToInt32(dtSize.Rows[rr]["ID"]))
                    {
                        t = true;
                        break;
                    }
                }
                if (!t)
                {
                    if (Convert.ToInt32(dtInfo.Rows[r]["SizeID"]) > 0)
                        dtSize.Rows.Add(dtInfo.Rows[r]["SizeID"], dtSizeList.Select("(ID=" + dtInfo.Rows[r]["SizeID"] + ")")[0]["Name"], dtSizeList.Select("(ID=" + dtInfo.Rows[r]["SizeID"] + ")")[0]["Orders"]);
                }
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Color", typeof(string));
            dt.Rows.Add(dt.NewRow());
            dt.Rows[0]["Color"] = "颜色\\尺码";
            int i = 1;
            dtSize.DefaultView.Sort = "Orders";
            for (int n = 0; n < dtSize.DefaultView.Count; n++)
            {
                dt.Columns.Add("Columns" + i);
                dt.Rows[0][i] = dtSize.DefaultView[n]["SizeName"].ToString();
                i++;
            }
            for (int c = dt.Columns.Count; c < 8; c++)
            {
                dt.Columns.Add("Columns" + c);
            }
            dt.Columns.Add("SumNum");
            dt.Rows[0]["SumNum"] = "合计";
            dt.Columns.Add("ColorOne", typeof(string));
            dt.Columns.Add("ColorTwo", typeof(string));
            dt.Rows[0]["ColorOne"] = "位置颜色一";
            dt.Rows[0]["ColorTwo"] = "位置颜色二";
            i = 1;
            for (int n = 0; n < dtColor.Rows.Count; n++)
            {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[i][0] = dtColor.Rows[n]["ColorName"].ToString();
                dt.Rows[i][dt.Columns.Count - 2] = dtColor.Rows[n]["ColorOneName"].ToString();
                dt.Rows[i][dt.Columns.Count - 1] = dtColor.Rows[n]["ColorTwoName"].ToString();
                i++;
            }

            for (int r = 0; r < dtSize.DefaultView.Count; r++)
            {
                SizeID = Convert.ToInt32(dtSize.DefaultView[r]["ID"]);
                for (int c = 0; c < dtColor.Rows.Count; c++)
                {
                    ColorID = Convert.ToInt32(dtColor.Rows[c]["ID"]);
                    ColorOneID = Convert.ToInt32(dtColor.Rows[c]["OneID"]);
                    ColorTwoID = Convert.ToInt32(dtColor.Rows[c]["TwoID"]);
                    string sql = "(SizeID=" + SizeID + ") and (ColorID=" + ColorID + ") and (ColorOneID=" + ColorOneID + ") and (ColorTwoID=" + ColorTwoID + ")";
                    DataRow[] drs = dtInfo.Select(sql);
                    if (drs.Length > 0)
                    {
                        dt.Rows[c + 1][r + 1] = drs[0]["Amount"];
                    }
                }
            }
                dt.Rows.Add(dt.NewRow());
            return dt;
        }
    }
}
