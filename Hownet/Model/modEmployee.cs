using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hownet.Model
{
    /// <summary>
    /// 实体类Employee 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class modEmployee
    {
        public modEmployee()
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
        /// 介绍人
        /// </summary>
        public int IntroducerID
        {
            set;
            get;
        }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdentityCard
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Sex
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sn
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Province
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set;
            get;
        }
        /// <summary>
        /// 到职日期
        /// </summary>
        public DateTime AccDate
        {
            set;
            get;
        }
        /// <summary>
        /// 工种
        /// </summary>
        public string WorkTypeID
        {
            set;
            get;
        }
        /// <summary>
        /// 工资种类
        /// </summary>
        public int PayID
        {
            set;
            get;
        }
        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime DimDate
        {
            set;
            get;
        }
        /// <summary>
        /// 床铺
        /// </summary>
        public int BedID
        {
            set;
            get;
        }
        /// <summary>
        /// 餐桌
        /// </summary>
        public int TableID
        {
            set;
            get;
        }
        /// <summary>
        /// 部门
        /// </summary>
        public int DepartmentID
        {
            set;
            get;
        }
        /// <summary>
        /// 学位
        /// </summary>
        public int DegreeID
        {
            set;
            get;
        }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public int PolityID
        {
            set;
            get;
        }
        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string SOSPhone
        {
            set;
            get;
        }
        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string SOSMan
        {
            set;
            get;
        }
        /// <summary>
        /// 现居住地
        /// </summary>
        public string NowAddress
        {
            set;
            get;
        }
        /// <summary>
        /// 填写日期
        /// </summary>
        public DateTime FillDate
        {
            set;
            get;
        }
        /// <summary>
        /// 填写人
        /// </summary>
        public int FillUser
        {
            set;
            get;
        }
        /// <summary>
        /// 固定或保底工资
        /// </summary>
        public decimal LassMoney
        {
            set;
            get;
        }
        /// <summary>
        /// 保底或提成比例
        /// </summary>
        public decimal Royalty
        {
            set;
            get;
        }
        /// <summary>
        /// 照片
        /// </summary>
        public string Image
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsUse
        {
            set;
            get;
        }
        /// <summary>
        /// 市
        /// </summary>
        public int City
        {
            set;
            get;
        }
        /// <summary>
        /// 县
        /// </summary>
        public int County
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public long IDCardID
        {
            set;
            get;
        }
        public int IsEnd
        {
            set;
            get;
        }
        public decimal Deposit
        {
            set;
            get;
        }
        public decimal NeedDeposit
        {
            set;
            get;
        }
        public string DefaultWorkType
        {
            set;
            get;
        }
        public decimal BoardWages
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime HeTongDate
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string HeTongAmount
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime HeTongDQDate
        {
            set;
            get;
        }
        public bool IsCaicTiCheng
        {
            set;
            get;
        }
        public int MaxAmountDay
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string BankNO
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string BankAccountName
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string BankName
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

    public class bllEmployee
    {
        public bllEmployee()
        {
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<modEmployee> DataTableToList(DataTable dt)
        {
            List<modEmployee> modelList = new List<modEmployee>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                modEmployee model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new modEmployee();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    else
                    {
                        model.ID = 0;
                    }
                    model.Name = dt.Rows[n]["Name"].ToString();
                    if (dt.Rows[n]["IntroducerID"].ToString() != "")
                    {
                        model.IntroducerID = int.Parse(dt.Rows[n]["IntroducerID"].ToString());
                    }
                    else
                    {
                        model.IntroducerID = 0;
                    }
                    model.IdentityCard = dt.Rows[n]["IdentityCard"].ToString();
                    if (dt.Rows[n]["Sex"].ToString() != "")
                    {
                        model.Sex = int.Parse(dt.Rows[n]["Sex"].ToString());
                    }
                    else
                    {
                        model.Sex = 0;
                    }
                    model.Sn = dt.Rows[n]["Sn"].ToString();
                    if (dt.Rows[n]["Province"].ToString() != "")
                    {
                        model.Province = int.Parse(dt.Rows[n]["Province"].ToString());
                    }
                    else
                    {
                        model.Province = 0;
                    }
                    model.Address = dt.Rows[n]["Address"].ToString();
                    model.Phone = dt.Rows[n]["Phone"].ToString();
                    if (dt.Rows[n]["AccDate"].ToString() != "")
                    {
                        model.AccDate = DateTime.Parse(dt.Rows[n]["AccDate"].ToString());
                    }
                    else
                    {
                        model.AccDate = DateTime.Parse("1900-1-1");
                    }
                    model.WorkTypeID = dt.Rows[n]["WorkTypeID"].ToString();
                    if (dt.Rows[n]["PayID"].ToString() != "")
                    {
                        model.PayID = int.Parse(dt.Rows[n]["PayID"].ToString());
                    }
                    else
                    {
                        model.PayID = 0;
                    }
                    if (dt.Rows[n]["DimDate"].ToString() != "")
                    {
                        model.DimDate = DateTime.Parse(dt.Rows[n]["DimDate"].ToString());
                    }
                    else
                    {
                        model.DimDate = DateTime.Parse("1900-1-1");
                    }
                    if (dt.Rows[n]["BedID"].ToString() != "")
                    {
                        model.BedID = int.Parse(dt.Rows[n]["BedID"].ToString());
                    }
                    else
                    {
                        model.BedID = 0;
                    }
                    if (dt.Rows[n]["TableID"].ToString() != "")
                    {
                        model.TableID = int.Parse(dt.Rows[n]["TableID"].ToString());
                    }
                    else
                    {
                        model.TableID = 0;
                    }
                    if (dt.Rows[n]["DepartmentID"].ToString() != "")
                    {
                        model.DepartmentID = int.Parse(dt.Rows[n]["DepartmentID"].ToString());
                    }
                    else
                    {
                        model.DepartmentID = 0;
                    }
                    if (dt.Rows[n]["DegreeID"].ToString() != "")
                    {
                        model.DegreeID = int.Parse(dt.Rows[n]["DegreeID"].ToString());
                    }
                    else
                    {
                        model.DegreeID = 0;
                    }
                    if (dt.Rows[n]["PolityID"].ToString() != "")
                    {
                        model.PolityID = int.Parse(dt.Rows[n]["PolityID"].ToString());
                    }
                    else
                    {
                        model.PolityID = 0;
                    }
                    model.SOSPhone = dt.Rows[n]["SOSPhone"].ToString();
                    model.SOSMan = dt.Rows[n]["SOSMan"].ToString();
                    model.NowAddress = dt.Rows[n]["NowAddress"].ToString();
                    if (dt.Rows[n]["FillDate"].ToString() != "")
                    {
                        model.FillDate = DateTime.Parse(dt.Rows[n]["FillDate"].ToString());
                    }
                    else
                    {
                        model.FillDate = DateTime.Parse("1900-1-1");
                    }
                    if (dt.Rows[n]["FillUser"].ToString() != "")
                    {
                        model.FillUser = int.Parse(dt.Rows[n]["FillUser"].ToString());
                    }
                    else
                    {
                        model.FillUser = 0;
                    }
                    if (dt.Rows[n]["LassMoney"].ToString() != "")
                    {
                        model.LassMoney = decimal.Parse(dt.Rows[n]["LassMoney"].ToString());
                    }
                    else
                    {
                        model.LassMoney = 0;
                    }
                    if (dt.Rows[n]["Royalty"].ToString() != "")
                    {
                        model.Royalty = decimal.Parse(dt.Rows[n]["Royalty"].ToString());
                    }
                    else
                    {
                        model.Royalty = 0;
                    }
                    model.Image = dt.Rows[n]["Image"].ToString();
                    if (dt.Rows[n]["IsUse"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsUse"].ToString() == "1") || (dt.Rows[n]["IsUse"].ToString().ToLower() == "true"))
                        {
                            model.IsUse = true;
                        }
                        else
                        {
                            model.IsUse = false;
                        }
                    }
                    if (dt.Rows[n]["City"].ToString() != "")
                    {
                        model.City = int.Parse(dt.Rows[n]["City"].ToString());
                    }
                    else
                    {
                        model.City = 0;
                    }
                    if (dt.Rows[n]["County"].ToString() != "")
                    {
                        model.County = int.Parse(dt.Rows[n]["County"].ToString());
                    }
                    else
                    {
                        model.County = 0;
                    }
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    model.IDCardID = Int64.Parse(dt.Rows[n]["IDCardID"].ToString());
                    if (dt.Rows[n]["IsEnd"].ToString() != "")
                    {
                        model.IsEnd = int.Parse(dt.Rows[n]["IsEnd"].ToString());
                    }
                    else
                    {
                        model.IsEnd = 0;
                    }
                    if (dt.Rows[n]["Deposit"].ToString() != "")
                    {
                        model.Deposit = decimal.Parse(dt.Rows[n]["Deposit"].ToString());
                    }
                    else
                    {
                        model.Deposit = 0;
                    }
                    if (dt.Rows[n]["NeedDeposit"].ToString() != "")
                    {
                        model.NeedDeposit = decimal.Parse(dt.Rows[n]["NeedDeposit"].ToString());
                    }
                    else
                    {
                        model.NeedDeposit = 0;
                    }
                    model.DefaultWorkType = dt.Rows[n]["DefaultWorkType"].ToString(); 
                    if (dt.Rows[n]["BoardWages"] != null && dt.Rows[n]["BoardWages"].ToString() != "")
                    {
                        model.BoardWages = decimal.Parse(dt.Rows[n]["BoardWages"].ToString());
                    }
                    if (dt.Rows[n]["HeTongDate"] != null && dt.Rows[n]["HeTongDate"].ToString() != "")
                    {
                        model.HeTongDate = DateTime.Parse(dt.Rows[n]["HeTongDate"].ToString());
                    }
                    model.HeTongAmount = dt.Rows[n]["HeTongAmount"].ToString();
                    if (dt.Rows[n]["HeTongDQDate"] != null && dt.Rows[n]["HeTongDQDate"].ToString() != "")
                    {
                        model.HeTongDQDate = DateTime.Parse(dt.Rows[n]["HeTongDQDate"].ToString());
                    }
                    if (dt.Rows[n]["IsCaicTiCheng"] != null && dt.Rows[n]["IsCaicTiCheng"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsCaicTiCheng"].ToString() == "1") || (dt.Rows[n]["IsCaicTiCheng"].ToString().ToLower() == "true"))
                        {
                            model.IsCaicTiCheng = true;
                        }
                        else
                        {
                            model.IsCaicTiCheng = false;
                        }
                    }
                    if (dt.Rows[n]["MaxAmountDay"] != null && dt.Rows[n]["MaxAmountDay"].ToString() != "")
                    {
                        model.MaxAmountDay = int.Parse(dt.Rows[n]["MaxAmountDay"].ToString());
                    }
                    model.BankNO = dt.Rows[n]["BankNO"].ToString();
                    model.BankAccountName = dt.Rows[n]["BankAccountName"].ToString();
                    model.BankName = dt.Rows[n]["BankName"].ToString();
                    model.A = int.Parse(dt.Rows[n]["A"].ToString());
                    modelList.Add(model);
                }
            }
            return modelList;
        }
    }

}

