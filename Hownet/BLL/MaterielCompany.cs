using System;
using System.Data;
using System.Collections.Generic;
using Hownet.Model;
using System.ComponentModel;
namespace Hownet.BLL
{
    /// <summary>
    /// MaterielCompany
    /// </summary>
    public partial class MaterielCompany
    {
        public MaterielCompany()
        { }
        #region  Method
        private readonly string bll = "Hownet.BLL.MaterielCompany";


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public BindingList<Hownet.Model.MaterielCompany> GetModelList(string strWhere)
        {
            string strDS = BasicClass.GetDataSet.GetStringList(bll, "GetList", new object[] { strWhere });
            return DataTableToList(strDS);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public BindingList<Hownet.Model.MaterielCompany> DataTableToList(string strDS)
        {
            BindingList<Hownet.Model.MaterielCompany> modelList = new BindingList<Hownet.Model.MaterielCompany>();
            if (strDS.Length > 10)
            {
                Hownet.Model.MaterielCompany model;
                string[] ss = strDS.Split('й');
                string[] sc;
                for (int i = 0; i < ss.Length-1; i++)
                {
                    model = new Hownet.Model.MaterielCompany();
                    sc = ss[i].Split('ж');
                    if (sc[0] != string.Empty)
                    {
                        model.A = Convert.ToInt32(sc[0]);
                        model.ID = Convert.ToInt32(sc[1]);
                        model.MaterielID = int.Parse(sc[2]);
                        model.CompanyID = int.Parse(sc[3]);
                        model.CompanySN = sc[4];
                        model.CompanyRemark = sc[5];
                        model.Price = decimal.Parse(sc[6]);
                        model.Remark = sc[7];
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }


        #endregion  Method
    }
}

