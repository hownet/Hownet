using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BasicClass
{
    public class DataSoure
    {
        public DataView getDS(string TypeName)
        {
            string bll = "";
            if (TypeName == "Measure")
            {
                bll = "Hownet.BLL.Measure";
               return Hownet.GetSerializeDS.GetDS(bll, "GetAllList", null).Tables[0].DefaultView;
            }

            else if (TypeName == "Company")
            {
                bll = "Hownet.BLL.Company";
                return  Hownet.GetSerializeDS.GetDS(bll, "GetList", new object[]{"TypeID=1"}).Tables[0].DefaultView;
            }
            else if (TypeName == "Product")
            {
                bll = "Hownet.BLL.Materiel";
                return Hownet.GetSerializeDS.GetDS(bll, "GetList", new object[] { "AttributeID=4" }).Tables[0].DefaultView;
            }
            else if (TypeName == "Brand")
            {
                bll = "Hownet.BLL.Materiel";
                return Hownet.GetSerializeDS.GetDS(bll, "GetList", new object[] { "AttributeID=5" }).Tables[0].DefaultView;
            }
            else if (TypeName == "Costs")
            {
                bll = "Hownet.BLL.OtherType";
                return Hownet.GetSerializeDS.GetDS(bll, "GetTypeList", new object[] { "费用类型" }).Tables[0].DefaultView;
            }
            else if (TypeName == "MiniEmp")
            {
                bll="Hownet.BLL.MiniEmp";
                return Hownet.GetSerializeDS.GetDS(bll,"GetWorkList",null).Tables[0].DefaultView;
            }
            //else if (TypeName == "CountryMoney")
            //{
            //    Hownet.BLL.QuotaPrice bllQP = new QuotaPrice();
            //    return bllQP.GetLookMoneyType();
            //}
            //else if (TypeName == "Payment")
            //{
            //    Hownet.BLL.QuotaPrice bllQP = new QuotaPrice();
            //    return bllQP.GetLookPayment();
            //}
            //else if (TypeName == "Fabrics")
            //{
            //    Hownet.BLL.Materiel bllMat = new Materiel();
            //    return bllMat.GetList("AttributeID=2").Tables[0].DefaultView;
            //}
            //else if (TypeName == "VerifyMan")
            //{
            //    Hownet.BLL.A_Users bllAU = new A_Users();
            //    return bllAU.GetAllList().Tables[0].DefaultView;
            //}
            //else if (TypeName == "SupplierF")
            //{
            //    Hownet.BLL.Company bllC = new Company();
            //    return bllC.GetLookList("(TypeID=6) And (MTID=0)");
            //}
            //else if (TypeName == "SupplierD")
            //{
            //    Hownet.BLL.Company bllC = new Company();
            //    return bllC.GetLookList("(TypeID=6) And (MTID>0)");
            //}
            //else if (TypeName == "BackAddress")
            //{
            //    Hownet.BLL.BackAddress bllB = new BackAddress();
            //    return bllB.GetList("(TypeID=1)").Tables[0].DefaultView;
            //}
            //else if (TypeName == "OutAddress")
            //{
            //    Hownet.BLL.BackAddress bllB = new BackAddress();
            //    return bllB.GetList("(TypeID=2)").Tables[0].DefaultView;
            //}
            //else if (TypeName == "Maker")
            //{
            //    Hownet.BLL.BackAddress bllB = new BackAddress();
            //    return bllB.GetList("(TypeID=3)").Tables[0].DefaultView;
            //}
            //else if (TypeName == "Depot")
            //{
            //    Hownet.BLL.Department bllD = new Department();
            //    return bllD.GetList("(DepartmentTypeID=-1)").Tables[0].DefaultView;
            //}
            //else if (TypeName == "ProductsType")
            //{
            //    Hownet.BLL.MaterielType bllMT = new MaterielType();
            //    return bllMT.GetList("AttributeID=1").Tables[0].DefaultView;
            //}
            //else if (TypeName == "Deparment")
            //{
            //    Hownet.BLL.Department bllD = new Department();
            //    return bllD.GetList("").Tables[0].DefaultView;
            //}
            else
            {
                DataView dv = new DataView();
                return dv;
            }

        }
        public DataView getParDS(string TypeName, object[] o)
        {
            string bll = "";
            if (TypeName == "PW")
            {
                bll = "Hownet.BLL.ProductWorkingMain";
                return Hownet.GetSerializeDS.GetDS(bll, "GetList", o).Tables[0].DefaultView;
            }
            else if (TypeName == "PWI")
            {
                bll = "Hownet.BLL.ProductWorkingInfo";
                return Hownet.GetSerializeDS.GetDS(bll, "GetBoxWork", o).Tables[0].DefaultView;
            }
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
        public int AddNew(string TypeName, string NewName)
        {
            int id = 0;
            
            return id;
        }
        public string DMember(string TypeName)
        {
            if (TypeName == "PW")
                return "Remark";
            else if (TypeName == "PWI")
            {
                return "WorkName";
            }
            else if (TypeName == "Costs")
                return "OtherTypeName";
            else
            {
                return "Name";
            }
        }
        public string VMember(string TypeName)
        {
            if (TypeName == "Costs")
                return "OtherTypeID";
            else
            {
                return "ID";
            }
        }
        public string ColName(string TypeName)
        {
            if (TypeName == "Measure")
            {
                return "单位-Name";
            }
            else if (TypeName == "Company")
            {
                return "客户-Name";
            }
            else if (TypeName == "SupplierF" || TypeName == "SupplierD")
            {
                return "供应商-Name";
            }
            else if (TypeName == "Product")
            {
                return "款号-Name";
            }
            else if (TypeName == "Brand")
            {
                return "商标-Name";
            }
            else if (TypeName == "VerifyMan")
            {
                return "审核人-TrueName";
            }
            else if (TypeName == "ProductsType")
            {
                return "款式类型-Name";
            }
            else if (TypeName == "Deparment")
            {
                return "所属部门-Name";
            }
            else if (TypeName == "PW")
                return "工艺单-Remark";
            if (TypeName == "Costs")
                return "类型-OtherTypeName";
            else if (TypeName == "PWI")
                return "工序-WorkName";
            else
            {
                return "名称-Name";
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

    }
}
