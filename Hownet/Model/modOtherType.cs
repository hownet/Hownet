using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hownet.Model
{

    [Serializable]
    public class modOtherType
    {
        public modOtherType()
        { }
        #region Model
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int TypeID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            set;
            get;
        }
        public int A
        {
            set;
            get;
        }
        #endregion Model

    }
    public class OtherTypeOTT
    {
        public OtherTypeOTT()
        { }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<modOtherType> DataTableToList(DataTable dt)
        {
            List<modOtherType> modelList = new List<modOtherType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                modOtherType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new modOtherType();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    model.Name = dt.Rows[n]["Name"].ToString();
                    if (dt.Rows[n]["TypeID"].ToString() != "")
                    {
                        model.TypeID = int.Parse(dt.Rows[n]["TypeID"].ToString());
                    }
                    model.Value = dt.Rows[n]["Value"].ToString();
                    model.A = int.Parse(dt.Rows[n]["A"].ToString());
                    modelList.Add(model);
                }
            }
            return modelList;
        }
    }
}
