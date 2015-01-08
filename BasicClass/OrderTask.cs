using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace BasicClass
{
   public class OrderTask
    {
       public static DataTable ShowTemInfo(int MainID, int TableTypeID)
       {
           string bllAI="Hownet.BLL.AmountInfo";
           Hownet.Model.AmountInfo modAI = new Hownet.Model.AmountInfo();

           DataTable dtInfo = new DataTable();
           //  if(AmountTypeID<4)
           dtInfo = BasicClass.GetDataSet.GetDS(bllAI, "GetList", new object[] { "(MainID=" + MainID + ") And (TableTypeID= " + TableTypeID + ")" }).Tables[0];

           DataTable dtColor = BasicClass.GetDataSet.GetDS(bllAI, "GetSumColor", new object[] { MainID, TableTypeID }).Tables[0];
           DataTable dtSize = BasicClass.GetDataSet.GetDS(bllAI, "GetSize", new object[] { MainID, TableTypeID }).Tables[0];
           DataTable dt = new DataTable();
           dt.Columns.Add("Color", typeof(string));
           dt.Rows.Add(dt.NewRow());
           dt.Rows[0]["Color"] = "颜色\\尺码";
           int i = 1;
           ArrayList ColorList = new ArrayList();
           ArrayList SizeList = new ArrayList();
           ArrayList ColorOneList = new ArrayList();
           ArrayList ColorTwoList = new ArrayList();
           ColorList.Clear();
           ColorOneList.Clear();
           ColorTwoList.Clear();
           SizeList.Clear();
           ColorList.Add(0);
           SizeList.Add(0);
           ColorOneList.Add(0);
           ColorTwoList.Add(0);
           for (int n = 0; n < dtSize.Rows.Count; n++)
           {
               dt.Columns.Add("Columns" + i);
               dt.Rows[0][i] = dtSize.Rows[n]["SizeName"].ToString();
               SizeList.Add(int.Parse(dtSize.Rows[n]["SizeID"].ToString()));
               i++;
           }
           for (int c = dt.Columns.Count; c < 13; c++)
           {
               dt.Columns.Add("Columns" + c);
           }
           dt.Columns.Add("SumNum");
           dt.Rows[0]["SumNum"] = "合计";
           dt.Columns.Add("ColorOne", typeof(string));
           dt.Columns.Add("ColorTwo", typeof(string));
           dt.Columns.Add("MainID", typeof(int));
           dt.Rows[0]["ColorOne"] = "位置颜色一";
           dt.Rows[0]["ColorTwo"] = "位置颜色二";
           dt.Rows[0]["MainID"] = MainID;
           i = 1;
           for (int n = 0; n < dtColor.Rows.Count; n++)
           {
               dt.Rows.Add(dt.NewRow());
               dt.Rows[i][0] = dtColor.Rows[n]["ColorName"].ToString();
               dt.Rows[i][dt.Columns.Count - 3] = dtColor.Rows[n]["ColorOneName"].ToString();
               dt.Rows[i][dt.Columns.Count - 2] = dtColor.Rows[n]["ColorTwoName"].ToString();
               dt.Rows[i]["MainID"] = MainID;
               ColorList.Add(int.Parse(dtColor.Rows[n]["ColorID"].ToString()));
               ColorOneList.Add(int.Parse(dtColor.Rows[n]["ColorOneID"].ToString()));
               ColorTwoList.Add(int.Parse(dtColor.Rows[n]["ColorTwoID"].ToString()));
               i++;
           }
           for (int r = 1; r < SizeList.Count; r++)
           {
               modAI.SizeID = int.Parse(SizeList[r].ToString());
               for (int c = 1; c < ColorList.Count; c++)
               {
                   modAI.ColorID = int.Parse(ColorList[c].ToString());
                   modAI.ColorOneID = int.Parse(ColorOneList[c].ToString());
                   modAI.ColorTwoID = int.Parse(ColorTwoList[c].ToString());
                   modAI.MainID = MainID;
                   string sql = "(SizeID=" + modAI.SizeID + ") and (ColorID=" + modAI.ColorID + ") and (ColorOneID=" + modAI.ColorOneID + ") and (ColorTwoID=" + modAI.ColorTwoID + ")";
                   DataRow[] drs = dtInfo.Select(sql);
                   if (drs.Length > 0)
                   {
                           dt.Rows[c][r] = drs[0]["Amount"];
                   }
               }
           }
           return dt;
       }
       public static DataTable ShowPSInfo(int PSOID, int TableTypeID)
       {
           string bllAI = "Hownet.BLL.ProduceSellInfo";
           Hownet.Model.AmountInfo modAI = new Hownet.Model.AmountInfo();

           DataTable dtInfo = new DataTable();
           //  if(AmountTypeID<4)
           dtInfo = BasicClass.GetDataSet.GetDS(bllAI, "GetList", new object[] { "(PSOID=" + PSOID + ")" }).Tables[0];

           DataTable dtColor = BasicClass.GetDataSet.GetDS(bllAI, "GetSumColor", new object[] { PSOID }).Tables[0];
           DataTable dtSize = BasicClass.GetDataSet.GetDS(bllAI, "GetSize", new object[] { PSOID }).Tables[0];
           DataTable dt = new DataTable();
           dt.Columns.Add("Color", typeof(string));
           dt.Rows.Add(dt.NewRow());
           dt.Rows[0]["Color"] = "颜色\\尺码";
           int i = 1;
           ArrayList ColorList = new ArrayList();
           ArrayList SizeList = new ArrayList();
           ArrayList ColorOneList = new ArrayList();
           ArrayList ColorTwoList = new ArrayList();
           ColorList.Clear();
           ColorOneList.Clear();
           ColorTwoList.Clear();
           SizeList.Clear();
           ColorList.Add(0);
           SizeList.Add(0);
           ColorOneList.Add(0);
           ColorTwoList.Add(0);
           for (int n = 0; n < dtSize.Rows.Count; n++)
           {
               dt.Columns.Add("Columns" + i);
               dt.Rows[0][i] = dtSize.Rows[n]["SizeName"].ToString();
               SizeList.Add(int.Parse(dtSize.Rows[n]["SizeID"].ToString()));
               i++;
           }
           for (int c = dt.Columns.Count; c < 13; c++)
           {
               dt.Columns.Add("Columns" + c);
           }
           dt.Columns.Add("SumNum");
           dt.Rows[0]["SumNum"] = "合计";
           dt.Columns.Add("ColorOne", typeof(string));
           dt.Columns.Add("ColorTwo", typeof(string));
           dt.Columns.Add("MainID", typeof(int));
           dt.Rows[0]["ColorOne"] = "位置颜色一";
           dt.Rows[0]["ColorTwo"] = "位置颜色二";
           dt.Rows[0]["MainID"] = PSOID;
           i = 1;
           for (int n = 0; n < dtColor.Rows.Count; n++)
           {
               dt.Rows.Add(dt.NewRow());
               dt.Rows[i][0] = dtColor.Rows[n]["ColorName"].ToString();
               dt.Rows[i][dt.Columns.Count - 3] = dtColor.Rows[n]["ColorOneName"].ToString();
               dt.Rows[i][dt.Columns.Count - 2] = dtColor.Rows[n]["ColorTwoName"].ToString();
               dt.Rows[i]["MainID"] = PSOID;
               ColorList.Add(int.Parse(dtColor.Rows[n]["ColorID"].ToString()));
               ColorOneList.Add(int.Parse(dtColor.Rows[n]["ColorOneID"].ToString()));
               ColorTwoList.Add(int.Parse(dtColor.Rows[n]["ColorTwoID"].ToString()));
               i++;
           }
           for (int r = 1; r < SizeList.Count; r++)
           {
               modAI.SizeID = int.Parse(SizeList[r].ToString());
               for (int c = 1; c < ColorList.Count; c++)
               {
                   modAI.ColorID = int.Parse(ColorList[c].ToString());
                   modAI.ColorOneID = int.Parse(ColorOneList[c].ToString());
                   modAI.ColorTwoID = int.Parse(ColorTwoList[c].ToString());
                   modAI.MainID = PSOID;
                   string sql = "(SizeID=" + modAI.SizeID + ") and (ColorID=" + modAI.ColorID + ") and (ColorOneID=" + modAI.ColorOneID + ") and (ColorTwoID=" + modAI.ColorTwoID + ")";
                   DataRow[] drs = dtInfo.Select(sql);
                   if (drs.Length > 0)
                   {
                       dt.Rows[c][r] = drs[0]["Amount"];
                   }
               }
           }
           return dt;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="TableTypeID">表类型</param>
       /// <param name="AmountType">数量类型，1为制单数量，2为已发货数量，3为未发货数量</param>
       /// <returns></returns>
       public static DataTable GetSalesList(int MainID, int TableTypeID, int AmountType)
       {
           string bllAI = "Hownet.BLL.AmountInfo";
           Hownet.Model.AmountInfo modAI = new Hownet.Model.AmountInfo();

           DataTable dtInfo = new DataTable();
           //  if(AmountTypeID<4)
           dtInfo = BasicClass.GetDataSet.GetDS(bllAI, "GetList", new object[] { "(MainID=" + MainID + ") And (TableTypeID= " + TableTypeID + ")" }).Tables[0];

           DataTable dtColor = BasicClass.GetDataSet.GetDS(bllAI, "GetSumColor", new object[] { MainID, TableTypeID }).Tables[0];
           DataTable dtSize = BasicClass.GetDataSet.GetDS(bllAI, "GetSize", new object[] { MainID, TableTypeID }).Tables[0];
           DataTable dt = new DataTable();
           dt.Columns.Add("Color", typeof(string));
           dt.Rows.Add(dt.NewRow());
           dt.Rows[0]["Color"] = "颜色\\尺码";
           int i = 1;
           ArrayList ColorList = new ArrayList();
           ArrayList SizeList = new ArrayList();
           ArrayList ColorOneList = new ArrayList();
           ArrayList ColorTwoList = new ArrayList();
           ColorList.Clear();
           ColorOneList.Clear();
           ColorTwoList.Clear();
           SizeList.Clear();
           ColorList.Add(0);
           SizeList.Add(0);
           ColorOneList.Add(0);
           ColorTwoList.Add(0);
           for (int n = 0; n < dtSize.Rows.Count; n++)
           {
               dt.Columns.Add("Columns" + i);
               dt.Rows[0][i] = dtSize.Rows[n]["SizeName"].ToString();
               SizeList.Add(int.Parse(dtSize.Rows[n]["SizeID"].ToString()));
               i++;
           }
           for (int c = dt.Columns.Count; c < 13; c++)
           {
               dt.Columns.Add("Columns" + c);
           }
           dt.Columns.Add("SumNum");
           dt.Rows[0]["SumNum"] = "合计";
           dt.Columns.Add("ColorOne", typeof(string));
           dt.Columns.Add("ColorTwo", typeof(string));
           dt.Columns.Add("MainID", typeof(int));
           dt.Rows[0]["ColorOne"] = "位置颜色一";
           dt.Rows[0]["ColorTwo"] = "位置颜色二";
           dt.Rows[0]["MainID"] = MainID;
           i = 1;
           for (int n = 0; n < dtColor.Rows.Count; n++)
           {
               dt.Rows.Add(dt.NewRow());
               dt.Rows[i][0] = dtColor.Rows[n]["ColorName"].ToString();
               dt.Rows[i][dt.Columns.Count - 3] = dtColor.Rows[n]["ColorOneName"].ToString();
               dt.Rows[i][dt.Columns.Count - 2] = dtColor.Rows[n]["ColorTwoName"].ToString();
               dt.Rows[i]["MainID"] = MainID;
               ColorList.Add(int.Parse(dtColor.Rows[n]["ColorID"].ToString()));
               ColorOneList.Add(int.Parse(dtColor.Rows[n]["ColorOneID"].ToString()));
               ColorTwoList.Add(int.Parse(dtColor.Rows[n]["ColorTwoID"].ToString()));
               i++;
           }
           for (int r = 1; r < SizeList.Count; r++)
           {
               modAI.SizeID = int.Parse(SizeList[r].ToString());
               for (int c = 1; c < ColorList.Count; c++)
               {
                   modAI.ColorID = int.Parse(ColorList[c].ToString());
                   modAI.ColorOneID = int.Parse(ColorOneList[c].ToString());
                   modAI.ColorTwoID = int.Parse(ColorTwoList[c].ToString());
                   modAI.MainID = MainID;
                   string sql = "(SizeID=" + modAI.SizeID + ") and (ColorID=" + modAI.ColorID + ") and (ColorOneID=" + modAI.ColorOneID + ") and (ColorTwoID=" + modAI.ColorTwoID + ")";
                   DataRow[] drs = dtInfo.Select(sql);
                   if (drs.Length > 0)
                   {
                      // dt.Rows[c][r] = drs[0]["Amount"];
                       if (AmountType == 1)
                           dt.Rows[c][r] = drs[0]["Amount"];
                       else if (AmountType == 2)
                           dt.Rows[c][r] = drs[0]["NotAmount"];
                   }
               }
           }
           return dt;
       }
    }
}
