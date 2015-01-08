using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Hownet.BaseContranl
{
    public class DataSoure
    {
        public DataView getDS(int TypeName)
        {
           // string bll = "";
            switch (TypeName)
            {
                case (int)BasicClass.Enums.TableType.Measure:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMeasure, "GetAllList", null).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.Company:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "TypeID=1" }).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.Product:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "AttributeID=4" }).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.Brand:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] { "(AttributeID=5)" }).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.Costs:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllPayColumnsSet, "GetTypeList", null).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.MiniEmp:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetWorkList", null).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.Supplier:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "TypeID=2" }).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.Task:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetNumList", null).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.Deparment:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetAllList", null).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.Employee:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMiniEmp, "GetAllList", null).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.Polity:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetTypeList", new object[] { "政治面貌" }).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.Degree:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetTypeList", new object[] { "学历" }).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.PayType:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllOtherType, "GetTypeList", new object[] { "计薪方式" }).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.Processing:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "TypeID=3" }).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.PackingMethod:
                    {
                        return BasicClass.GetDataSet.GetDS("Hownet.BLL.PackingMethod", "GetAllList", null).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.Bed:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetInfoListByTypeName", new object[] { "宿舍" }).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.MeaTable:
                    {
                        return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetInfoListByTypeName", new object[] { "食堂" }).Tables[0].DefaultView;
                    }
                case (int)BasicClass.Enums.TableType.JGC:
                    {
                       return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllCompany, "GetList", new object[] { "(TypeID=3)" }).Tables[0].DefaultView;
                    }
                default:
                    {
                        DataView dv = new DataView();
                        return dv;
                    }
            }
        }
        public DataView getParDS(int TypeName, object[] o)
        {
            string bll = "";
            if (TypeName ==(int)BasicClass.Enums.TableType.PW)
            {
                bll = "Hownet.BLL.ProductWorkingMain";
                return BasicClass.GetDataSet.GetDS(bll, "GetList", o).Tables[0].DefaultView;
            }
            else if (TypeName ==(int)BasicClass.Enums.TableType.PWI)
            {
                bll = "Hownet.BLL.ProductWorkingInfo";
                return BasicClass.GetDataSet.GetDS(bll, "GetBoxWork", o).Tables[0].DefaultView;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.BOM)
            {
                DataTable dt=BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMaterielStructMain, "GetList", o).Tables[0];
                return dt.DefaultView;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Deparment)
            {
                return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetList", o).Tables[0].DefaultView;
            }
            //else if (TypeName == (int)BasicClass.Enums.TableType.LeiLiao)
            //{
            //    return BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetNumListByDeparmentID", o).Tables[0].DefaultView;
            //}
            else
            {
                DataView dv = new DataView();
                return dv;
            }
        }
        public DataTable ClientOK()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add(5,"等待客戶確認");
            dt.Rows.Add(6,"客戶確認通過");
            dt.Rows.Add(7,"客戶取消本單");
            dt.Rows.Add(8, "公司取消本單");
            return dt;
        }
        public DataTable UpTypeID()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add(1, "請求方");
            dt.Rows.Add(2, "客戶方");
            return dt;
        }
        public int AddNew(int TypeName, string NewName)
        {
            int id = 0;
            if (TypeName == (int)BasicClass.Enums.TableType.Product)
            {
                DataTable dtt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllMateriel, "GetList", new object[] {"(ID=0)" }).Tables[0];
                dtt.Rows.Clear();
                DataRow dr = dtt.NewRow();
                dr["ID"] = dr["A"] = dr["IsEnd"] = 0;
                dr["Name"] = NewName;
                dr["MeasureID"] =dr["SecondMeasureID"]= 0;
                dr["Sn"] = BasicClass.GetChinese.GetChineseSpell(NewName);
                dr["Remark"] = dr["Image"] = "";
                dr["Conversion"] =dr["IsUse"]= 1;
                dr["TypeID"] = dr["AttributeID"] = 4;
                dtt.Rows.Add(dr);
                id = BasicClass.GetDataSet.Add(BasicClass.Bllstr.bllMateriel, dtt);
            }
            return id;
        }
        public string DMember(int TypeName)
        {
            if (TypeName == (int)BasicClass.Enums.TableType.PW)
                return "Remark";
            else if (TypeName == (int)BasicClass.Enums.TableType.PWI)
            {
                return "WorkName";
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Task||TypeName==(int)BasicClass.Enums.TableType.LeiLiao)
            {
                return "Num";
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.BOM)
            {
                return "Ver";
            }
            else
            {
                return "Name";
            }
        }
        public string VMember(int TypeName)
        {
            if (TypeName == (int)BasicClass.Enums.TableType.BOM)
            {
                return "MainID";
            }
                return "ID";
        }
        public List<strColumns> ColName(int TypeName)
        {
            List<strColumns> li = new List<strColumns>();
            strColumns sc = new strColumns();
            if (TypeName == (int)BasicClass.Enums.TableType.Measure)
            {
                sc.Caption = "单位";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Company)
            {
                sc.Caption = "客户";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Processing)
            {
                sc.Caption = "加工商";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Supplier)
            {
                sc.Caption = "供应商";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Product)
            {
                sc.Caption = "款号";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Brand)
            {
                sc.Caption = "商标";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Deparment)
            {
                sc.Caption = "所属部门";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.PW)
            {
                sc.Caption = "工艺单";
                sc.ColumnName = "Remark";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.PWI)
            {
                sc.Caption = "工序";
                sc.ColumnName = "WorkName";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Costs)
            {
                sc.Caption = "费用类型";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Task||TypeName==(int)BasicClass.Enums.TableType.LeiLiao)
            {
                sc.Caption = "计划单编号";
                sc.ColumnName = "Num";
                li.Add(sc);
                sc = new strColumns();
                sc.Caption = "裁剪床号";
                sc.ColumnName = "BedNO";
                li.Add(sc);
                sc = new strColumns();
                sc.Caption = "款号";
                sc.ColumnName = "MaterielName";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Deparment)
            {
                sc.Caption = "部门";
                sc.ColumnName = "MaterielName";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Zone)
            {
                sc.Caption = "Zone";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.MiniEmp)
            {
                sc.Caption = "员工姓名";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Degree)
            {
                sc.Caption = "学历";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Polity)
            {
                sc.Caption = "政治面貌";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.BOM)
            {
                sc.Caption = "单用量";
                sc.ColumnName = "Ver";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.PayType)
            {
                sc.Caption = "计薪方法";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.PackingMethod)
            {
                sc.Caption = "包装方法";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.Bed)
            {
                sc.Caption = "宿舍";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else if (TypeName == (int)BasicClass.Enums.TableType.MeaTable)
            {
                sc.Caption = "餐桌";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
            else
            {
                sc.Caption = "名称";
                sc.ColumnName = "Name";
                li.Add(sc);
                return li;
            }
        }
        public static DataTable NeedIsVerify
        {
            get
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Rows.Add(1, "申請中");
                dt.Rows.Add(2, "審核未通過");
                dt.Rows.Add(3, "已審核未采購");
                dt.Rows.Add(4, "采購中");
                dt.Rows.Add(5, "采購已完成");
                return dt;
            }
        }
        public class strColumns
        {
            public strColumns()
            { }
            public string Caption
            {
                set;
                get;
            }
            public string ColumnName
            {
                set;
                get;
            }
        }
    }
}
