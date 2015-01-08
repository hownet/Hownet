using System;
using System.Collections.Generic;
using System.Data;
namespace BasicClass.Model
{
    /// <summary>
    /// 实体类SysTem 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SysTem
    {
        public SysTem()
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
        public string CompanyName
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string LinkMan
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
        /// 
        /// </summary>
        public string Fax
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string HDSerie
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Registration
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string BanKName
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
        public string BankUserName
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
        public bool Direct2Depot
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Sell4Depot
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool AutoClient
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool BoxOrPic
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int NumType
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Mobile
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int SellMoney
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool CustOder
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool NotPermissions
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DepotAllowNegative
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsChangedSales
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int BackDepotWorking
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int OderOne
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int OderTwo
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int OderThree
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool AutoCaicBoardWages
        {
            set;
            get;
        }
        /// <summary>
        /// 默认原料仓库
        /// </summary>
        public int DefaultRawDepot
        {
            set;
            get;
        }
        /// <summary>
        /// 自动收货的成品仓库
        /// </summary>
        public int DefaultDepot
        {
            set;
            get;
        }
        public bool IsShowMoney
        {
            set;
            get;
        }
        public bool CompanyByUser
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DoubleNotDefaultWTNum
        {
            set;
            get;
        }
        public int OrderDays
        {
            set;
            get;
        }
        public bool OrderNeedEat
        {
            set;
            get;
        }
        /// <summary>
        /// 是否检测未完成工序
        /// </summary>
        public bool IsCheckNoWork
        {
            set;
            get;
        }
        /// <summary>
        /// 是否允许普通用户修改数量
        /// </summary>
        public bool IsCanEditAmount
        {
            set;
            get;
        }
        /// <summary>
        /// 服务器是否自动关机
        /// </summary>
        public bool IsAutoClose
        {
            set;
            get;
        }
        /// <summary>
        /// 工资统计，是否不参考“参与统计选项”
        /// </summary>
        public bool IsTicketNotNeedCaic
        {
            set;
            get;
        }
        /// <summary>
        /// 是否显示已离职员工
        /// </summary>
        public bool IsShowOutEmp
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string DataVer
        {
            set;
            get;
        }
        /// <summary>
        /// 材料是否按单采购
        /// </summary>
        public bool MaterielByTask
        {
            set;
            get;
        }
        /// <summary>
        /// 读卡器上数量按工序累计
        /// </summary>
        public bool SumByWorking
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
    public class SysTemDTT
    {
        public SysTemDTT()
        { }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.SysTem> DataTableToList(DataTable dt)
        {
            List<Model.SysTem> modelList = new List<Model.SysTem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.SysTem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.SysTem();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    else
                    {
                        model.ID = 0;
                    }
                    model.CompanyName = dt.Rows[n]["CompanyName"].ToString();
                    model.LinkMan = dt.Rows[n]["LinkMan"].ToString();
                    model.Phone = dt.Rows[n]["Phone"].ToString();
                    model.Fax = dt.Rows[n]["Fax"].ToString();
                    model.HDSerie = dt.Rows[n]["HDSerie"].ToString();
                    model.Registration = dt.Rows[n]["Registration"].ToString();
                    model.BanKName = dt.Rows[n]["BanKName"].ToString();
                    model.BankNO = dt.Rows[n]["BankNO"].ToString();
                    model.BankUserName = dt.Rows[n]["BankUserName"].ToString();
                    model.Address = dt.Rows[n]["Address"].ToString();
                    if (dt.Rows[n]["Direct2Depot"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Direct2Depot"].ToString() == "1") || (dt.Rows[n]["Direct2Depot"].ToString().ToLower() == "true"))
                        {
                            model.Direct2Depot = true;
                        }
                        else
                        {
                            model.Direct2Depot = false;
                        }
                    }
                    if (dt.Rows[n]["Sell4Depot"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Sell4Depot"].ToString() == "1") || (dt.Rows[n]["Sell4Depot"].ToString().ToLower() == "true"))
                        {
                            model.Sell4Depot = true;
                        }
                        else
                        {
                            model.Sell4Depot = false;
                        }
                    }
                    if (dt.Rows[n]["AutoClient"].ToString() != "")
                    {
                        if ((dt.Rows[n]["AutoClient"].ToString() == "1") || (dt.Rows[n]["AutoClient"].ToString().ToLower() == "true"))
                        {
                            model.AutoClient = true;
                        }
                        else
                        {
                            model.AutoClient = false;
                        }
                    }
                    if (dt.Rows[n]["BoxOrPic"].ToString() != "")
                    {
                        if ((dt.Rows[n]["BoxOrPic"].ToString() == "1") || (dt.Rows[n]["BoxOrPic"].ToString().ToLower() == "true"))
                        {
                            model.BoxOrPic = true;
                        }
                        else
                        {
                            model.BoxOrPic = false;
                        }
                    }
                    if (dt.Rows[n]["NumType"].ToString() != "")
                    {
                        model.NumType = int.Parse(dt.Rows[n]["NumType"].ToString());
                    }
                    else
                    {
                        model.NumType = 0;
                    }
                    if (dt.Rows[n]["SellMoney"].ToString() != "")
                    {
                        model.SellMoney = int.Parse(dt.Rows[n]["SellMoney"].ToString());
                    }
                    else
                    {
                        model.SellMoney = 0;
                    }
                    if (dt.Rows[n]["CustOder"].ToString() != "")
                    {
                        if ((dt.Rows[n]["CustOder"].ToString() == "1") || (dt.Rows[n]["CustOder"].ToString().ToLower() == "true"))
                        {
                            model.CustOder = true;
                        }
                        else
                        {
                            model.CustOder = false;
                        }
                    }
                    if (dt.Rows[n]["NotPermissions"].ToString() != "")
                    {
                        if ((dt.Rows[n]["NotPermissions"].ToString() == "1") || (dt.Rows[n]["NotPermissions"].ToString().ToLower() == "true"))
                        {
                            model.NotPermissions = true;
                        }
                        else
                        {
                            model.NotPermissions = false;
                        }
                    }
                    model.Mobile = dt.Rows[n]["Mobile"].ToString();
                    if (dt.Rows[n]["DepotAllowNegative"].ToString() != "")
                    {
                        if ((dt.Rows[n]["DepotAllowNegative"].ToString() == "1") || (dt.Rows[n]["DepotAllowNegative"].ToString().ToLower() == "true"))
                        {
                            model.DepotAllowNegative = true;
                        }
                        else
                        {
                            model.DepotAllowNegative = false;
                        }
                    }
                    if (dt.Rows[n]["IsChangedSales"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsChangedSales"].ToString() == "1") || (dt.Rows[n]["IsChangedSales"].ToString().ToLower() == "true"))
                        {
                            model.IsChangedSales = true;
                        }
                        else
                        {
                            model.IsChangedSales = false;
                        }
                    }
                    if (dt.Rows[n]["BackDepotWorking"].ToString() != "")
                    {
                        model.BackDepotWorking = int.Parse(dt.Rows[n]["BackDepotWorking"].ToString());
                    }
                    else
                    {
                        model.BackDepotWorking = 0;
                    }
                    if (dt.Rows[n]["OderOne"].ToString() != "")
                    {
                        model.OderOne = int.Parse(dt.Rows[n]["OderOne"].ToString());
                    }
                    else
                    {
                        model.OderOne = 0;
                    }
                    if (dt.Rows[n]["OderTwo"].ToString() != "")
                    {
                        model.OderTwo = int.Parse(dt.Rows[n]["OderTwo"].ToString());
                    }
                    else
                    {
                        model.OderTwo = 0;
                    }
                    if (dt.Rows[n]["OderThree"].ToString() != "")
                    {
                        model.OderThree = int.Parse(dt.Rows[n]["OderThree"].ToString());
                    }
                    else
                    {
                        model.OderThree = 0;
                    }
                    if (dt.Rows[n]["AutoCaicBoardWages"].ToString() != "")
                    {
                        if ((dt.Rows[n]["AutoCaicBoardWages"].ToString() == "1") || (dt.Rows[n]["AutoCaicBoardWages"].ToString().ToLower() == "true"))
                        {
                            model.AutoCaicBoardWages = true;
                        }
                        else
                        {
                            model.AutoCaicBoardWages = false;
                        }
                    }
                    if (dt.Rows[n]["DefaultRawDepot"].ToString() != "")
                    {
                        model.DefaultRawDepot = int.Parse(dt.Rows[n]["DefaultRawDepot"].ToString());
                    }
                    else
                    {
                        model.DefaultRawDepot = 0;
                    }
                    if (dt.Rows[n]["IsShowMoney"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsShowMoney"].ToString() == "1") || (dt.Rows[n]["IsShowMoney"].ToString().ToLower() == "true"))
                        {
                            model.IsShowMoney = true;
                        }
                        else
                        {
                            model.IsShowMoney = false;
                        }
                    }
                    if (dt.Rows[n]["CompanyByUser"].ToString() != "")
                    {
                        if ((dt.Rows[n]["CompanyByUser"].ToString() == "1") || (dt.Rows[n]["CompanyByUser"].ToString().ToLower() == "true"))
                        {
                            model.CompanyByUser = true;
                        }
                        else
                        {
                            model.CompanyByUser = false;
                        }
                    }
                    if (dt.Rows[n]["DoubleNotDefaultWTNum"].ToString() != "")
                    {
                        model.DoubleNotDefaultWTNum = decimal.Parse(dt.Rows[n]["DoubleNotDefaultWTNum"].ToString());
                    }
                    else
                    {
                        model.DoubleNotDefaultWTNum = 0;
                    }
                    if (dt.Rows[n]["OrderDays"].ToString() != "")
                    {
                        model.OrderDays = int.Parse(dt.Rows[n]["OrderDays"].ToString());
                    }
                    if (dt.Rows[n]["OrderNeedEat"].ToString() != "")
                    {
                        if ((dt.Rows[n]["OrderNeedEat"].ToString() == "1") || (dt.Rows[n]["OrderNeedEat"].ToString().ToLower() == "true"))
                        {
                            model.OrderNeedEat = true;
                        }
                        else
                        {
                            model.OrderNeedEat = false;
                        }
                    }
                    if (dt.Rows[n]["IsCheckNoWork"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsCheckNoWork"].ToString() == "1") || (dt.Rows[n]["IsCheckNoWork"].ToString().ToLower() == "true"))
                        {
                            model.IsCheckNoWork = true;
                        }
                        else
                        {
                            model.IsCheckNoWork = false;
                        }
                    }
                    if (dt.Rows[n]["IsCanEditAmount"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsCanEditAmount"].ToString() == "1") || (dt.Rows[n]["IsCanEditAmount"].ToString().ToLower() == "true"))
                        {
                            model.IsCanEditAmount = true;
                        }
                        else
                        {
                            model.IsCanEditAmount = false;
                        }
                    }
                    if (dt.Rows[n]["IsAutoClose"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsAutoClose"].ToString() == "1") || (dt.Rows[n]["IsAutoClose"].ToString().ToLower() == "true"))
                        {
                            model.IsAutoClose = true;
                        }
                        else
                        {
                            model.IsAutoClose = false;
                        }
                    }
                    if (dt.Rows[n]["IsTicketNotNeedCaic"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsTicketNotNeedCaic"].ToString() == "1") || (dt.Rows[n]["IsTicketNotNeedCaic"].ToString().ToLower() == "true"))
                        {
                            model.IsTicketNotNeedCaic = true;
                        }
                        else
                        {
                            model.IsTicketNotNeedCaic = false;
                        }
                    }
                    if (dt.Rows[n]["IsShowOutEmp"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsShowOutEmp"].ToString() == "1") || (dt.Rows[n]["IsShowOutEmp"].ToString().ToLower() == "true"))
                        {
                            model.IsShowOutEmp = true;
                        }
                        else
                        {
                            model.IsShowOutEmp = false;
                        }
                    }
                    model.DataVer = dt.Rows[n]["DataVer"].ToString();
                    try
                    {
                        if (dt.Rows[n]["MaterielByTask"] != null && dt.Rows[n]["MaterielByTask"].ToString() != "")
                        {
                            if ((dt.Rows[n]["MaterielByTask"].ToString() == "1") || (dt.Rows[n]["MaterielByTask"].ToString().ToLower() == "true"))
                            {
                                model.MaterielByTask = true;
                            }
                            else
                            {
                                model.MaterielByTask = false;
                            }
                        }
                        if (dt.Rows[n]["SumByWorking"] != null && dt.Rows[n]["SumByWorking"].ToString() != "")
                        {
                            if ((dt.Rows[n]["SumByWorking"].ToString() == "1") || (dt.Rows[n]["SumByWorking"].ToString().ToLower() == "true"))
                            {
                                model.SumByWorking = true;
                            }
                            else
                            {
                                model.SumByWorking = false;
                            }
                        }
                    }
                    catch
                    {
                    }
                    model.A = int.Parse(dt.Rows[n]["A"].ToString());
                    modelList.Add(model);
                }
            }
            return modelList;
        }
    }
}

