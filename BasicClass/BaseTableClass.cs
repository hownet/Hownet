using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BasicClass
{
    public class BaseTableClass
    {
        public static DataTable dtTableType
        {
            get
            {
                DataTable dt = new DataTable();
                dt.TableName = "BaseTableType";
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("ID", typeof(int));


                dt.Rows.Add("生产制单", 1);
                dt.Rows.Add("客户", 2);

                dt.Rows.Add("款号", 3);
                dt.Rows.Add("商标", 4);
                dt.Rows.Add("员工", 5);
                dt.Rows.Add("费用类型", 6);
                dt.Rows.Add("供应商", 7);
                dt.Rows.Add("成品入库", 8);
                dt.Rows.Add("部门", 9);
                dt.Rows.Add("工种", 10);
                dt.Rows.Add("订薪方式", 11);
                dt.Rows.Add("学历", 12);
                dt.Rows.Add("政治面貌", 13);

                dt.Rows.Add("省份", 15);
                dt.Rows.Add("单位", 16);
                dt.Rows.Add("工艺单主表", 17);
                dt.Rows.Add("工艺单明细", 18);
                dt.Rows.Add("床号", 19);
                dt.Rows.Add("就餐桌号", 20);
                dt.Rows.Add("销售开单", 21);
                dt.Rows.Add("销售退货", 22);
                dt.Rows.Add("采购订单", 23);
                dt.Rows.Add("采购收货", 24);
                dt.Rows.Add("采购退货", 25);
                dt.Rows.Add("生产领料", 26);
                dt.Rows.Add("客户订单主表", 27);
                dt.Rows.Add("客户订单", 28);
                dt.Rows.Add("派工单", 29);
                dt.Rows.Add("BOM", 30);
                dt.Rows.Add("加工单", 31);
                dt.Rows.Add("加工商", 32);
                return dt;
            }
        }
        private static DataTable _dtuser;
        public static DataTable dtuser
        {
            set
            {
                _dtuser = value;
            }
            get
            {
                if (_dtuser == null)
                {
                    _dtuser = BasicClass.GetDataSet.GetDS("Hownet.BLL.Users", "GetViewList", null).Tables[0];
                }
                return _dtuser;
            }
        }
        #region 员工简表
        private static DataTable _dtMiniEmp;
        /// <summary>
        /// 员工简表
        /// </summary>
        public static DataTable dtMiniEmp
        {
            set
            {
                _dtMiniEmp = value;
            }
            get
            {
                if (_dtMiniEmp == null)
                {
                    _dtMiniEmp = BasicClass.GetDataSet.GetDS("Hownet.BLL.MiniEmp", "GetViewList", null).Tables[0];
                }
                return _dtMiniEmp;
            }
        }
        /// <summary>
        /// 刷新员工简表
        /// </summary>
        public static void ReMiniEmp()
        {
            _dtMiniEmp = BasicClass.GetDataSet.GetDS("Hownet.BLL.MiniEmp", "GetViewList", null).Tables[0];
        }
        #endregion

        #region 计量单位
        private static DataTable _dtMeasure;
        /// <summary>
        /// 计量单位
        /// </summary>
        public static DataTable dtMeasure
        {
            set
            {
                _dtMeasure = value;
            }
            get
            {
                if (_dtMeasure == null)
                {
                    _dtMeasure = BasicClass.GetDataSet.GetDS("Hownet.BLL.Measure", "GetAllList", null).Tables[0];
                }
                return _dtMeasure;
            }
        }
        /// <summary>
        /// 刷新计量单位
        /// </summary>
        public static void ReMeasure()
        {
            _dtMeasure = BasicClass.GetDataSet.GetDS("Hownet.BLL.Measure", "GetAllList", null).Tables[0];
        }
        #endregion
        #region 供应商
        private static DataTable _dtSupplier;
        /// <summary>
        /// 供应商
        /// </summary>
        public static DataTable dtSupplier
        {
            set
            {
                _dtSupplier = value;
            }
            get
            {
                if (_dtSupplier == null)
                {
                    _dtSupplier = BasicClass.GetDataSet.GetBySql("Select 1 as A,* From Company where (TypeID=2)");
                }
                return _dtSupplier;
            }
        }
        /// <summary>
        /// 刷新供应商
        /// </summary>
        public static void ReSupplier()
        {
            dtSupplier = BasicClass.GetDataSet.GetBySql("Select 1 as A,* From Company where (TypeID=2)");
        }
        #endregion
        #region 客户
        private static DataTable _dtCompany;
        /// <summary>
        /// 客户
        /// </summary>
        public static DataTable dtCompany
        {
            set
            {
                _dtCompany = value;
            }
            get
            {
                if (_dtCompany == null)
                {
                    _dtCompany = BasicClass.GetDataSet.GetBySql("Select 1 as A,* From Company where (TypeID=1)");
                }
                return _dtCompany;
            }
        }
        /// <summary>
        /// 刷新客户
        /// </summary>
        public static void ReCompany()
        {
            _dtCompany = BasicClass.GetDataSet.GetBySql("Select 1 as A,* From Company where (TypeID=1)");
        }
        #endregion

        #region 颜色
        private static DataTable _dtColor;
        /// <summary>
        /// 颜色
        /// </summary>
        public static DataTable dtColor
        {
            set
            {
                _dtColor = value;
            }
            get
            {
                if (_dtColor == null)
                {
                    _dtColor = BasicClass.GetDataSet.GetDS("Hownet.BLL.Color", "GetAllList", null).Tables[0];
                }
                return _dtColor;
            }
        }
        /// <summary>
        /// 刷新颜色
        /// </summary>
        public static void ReColor()
        {
            _dtColor = BasicClass.GetDataSet.GetDS("Hownet.BLL.Color", "GetAllList", null).Tables[0];
        }

        #endregion

        #region 尺码
        private static DataTable _dtSize;
        public static DataTable dtSize
        {
            set
            {
                _dtSize = value;
            }
            get
            {
                if (_dtSize == null)
                {
                    _dtSize = BasicClass.GetDataSet.GetDS("Hownet.BLL.Size", "GetAllList", null).Tables[0];
                }
                return _dtSize;
            }
        }
        public static void ReSize()
        {
            _dtSize = BasicClass.GetDataSet.GetDS("Hownet.BLL.Size", "GetAllList", null).Tables[0];
        }
        #endregion

        #region 普通工序，未包含计时工序
        private static DataTable _dtWorking;
        /// <summary>
        /// 普通工序，未包含计时工序
        /// </summary>
        public static DataTable dtWorking
        {
            set
            {
                _dtWorking = value;
            }
            get
            {
                if (_dtWorking == null)
                {
                    _dtWorking = BasicClass.GetDataSet.GetDS("Hownet.BLL.Working", "GetList", new object[] { "(WorkTypeID>-1)" }).Tables[0];
                }
                return _dtWorking;
            }
        }
        /// <summary>
        /// 刷新普通工序
        /// </summary>
        public static void ReWorking()
        {
            _dtWorking = BasicClass.GetDataSet.GetDS("Hownet.BLL.Working", "GetList", new object[] { "(WorkTypeID>-1)" }).Tables[0];
        }
        #endregion

        #region 规格
        private static DataTable _dtSpec;
        /// <summary>
        /// 规格
        /// </summary>
        public static DataTable dtSpec
        {
            set
            {
                _dtSpec = value;
            }
            get
            {
                if (_dtSpec == null)
                {
                    _dtSpec = BasicClass.GetDataSet.GetDS("Hownet.BLL.Specification", "GetAllList", null).Tables[0];
                }
                return _dtSpec;
            }
        }
        /// <summary>
        /// 刷新规格
        /// </summary>
        public static void ReSpec()
        {
            _dtSpec = BasicClass.GetDataSet.GetDS("Hownet.BLL.Specification", "GetAllList", null).Tables[0];

        }

        #endregion


        #region 所有物料
        private static DataTable _dtAllMateriel;
        /// <summary>
        /// 所有物料
        /// </summary>
        public static DataTable dtAllMateriel
        {
            set
            {
                _dtAllMateriel = value;
            }
            get
            {
                if (_dtAllMateriel == null)
                {
                    _dtAllMateriel = BasicClass.GetDataSet.GetDS("Hownet.BLL.Materiel", "GetListAndMeasure", null).Tables[0];
                    DataRow dr = _dtAllMateriel.NewRow();
                    dr["ID"] = 0;
                    dr["Name"] = string.Empty;
                    _dtAllMateriel.Rows.InsertAt(dr, 0);
                }
                return _dtAllMateriel;
            }
        }
        public static void ReAllMateriel()
        {
            _dtAllMateriel = BasicClass.GetDataSet.GetDS("Hownet.BLL.Materiel", "GetListAndMeasure", null).Tables[0];
        }

        #endregion
        #region 款号
        private static DataTable _dtFinished;
        /// <summary>
        /// 款号
        /// </summary>
        public static DataTable dtFinished
        {
            set
            {
                _dtFinished = value;
            }
            get
            {
                if (_dtFinished == null)
                {
                    _dtFinished = BasicClass.GetDataSet.GetDS("Hownet.BLL.Materiel", "GetList", new object[] { "(AttributeID=4)" }).Tables[0];
                }
                return _dtFinished;
            }
        }
        public static void ReFinished()
        {
            _dtFinished = BasicClass.GetDataSet.GetDS("Hownet.BLL.Materiel", "GeList", new object[] { "(AttributeID=4)" }).Tables[0];
        }
        #endregion

        #region 非成品物料

        private static DataTable _dtMateriel;
        /// <summary>
        /// 非成品物料
        /// </summary>
        public static DataTable dtMateriel
        {
            set
            {
                _dtMateriel = value;
            }
            get
            {
                if(_dtMateriel==null)
                {
                    ReMateriel();// _dtMateriel = BasicClass.GetDataSet.GetDS("Hownet.BLL.Materiel", "GetList", new object[] { "(AttributeID<>4)" }).Tables[0];
                }
                return _dtMateriel;
            }
        }
        public static void ReMateriel()
        {
            _dtMateriel = BasicClass.GetDataSet.GetDS("Hownet.BLL.Materiel", "GetList", new object[] { "(AttributeID<>4)" }).Tables[0];
            
        }
        #endregion

        #region 商标
        private static DataTable _dtBrand;
        /// <summary>
        /// 商标
        /// </summary>
        public static DataTable dtBrand
        {
            set
            {
                _dtBrand = value;
            }
            get
            {
                if(_dtBrand==null)
                {
                    ReBrand();
                }
                return _dtBrand;
            }
        }
        /// <summary>
        /// 刷新商标
        /// </summary>
        public static void ReBrand()
        {
            _dtBrand = BasicClass.GetDataSet.GetDS("Hownet.BLL.Materiel", "GetList", new object[] { "(AttributeID=5)" }).Tables[0];
        }
        #endregion
        #region 加工商
        private static DataTable _dtProcessing;
        /// <summary>
        /// 加工商
        /// </summary>
        public static DataTable dtProcessing
        {
            set
            {
                _dtProcessing = value;
            }
            get
            {
                if (_dtProcessing == null)
                {
                    _dtProcessing = BasicClass.GetDataSet.GetBySql("Select 1 as A, * From Company where (TypeID=3)");
                }
                return _dtProcessing;
            }
        }
        /// <summary>
        /// 刷新供应商
        /// </summary>
        public static void ReProcessing()
        {
            dtProcessing = BasicClass.GetDataSet.GetBySql("Select 1 as A,* From Company where (TypeID=3)");
        }
        #endregion

        #region 仓库
        private static DataTable _dtDepot;
        /// <summary>
        /// 加工商
        /// </summary>
        public static DataTable dtDepot
        {
            set
            {
                _dtDepot = value;
            }
            get
            {
                if (_dtDepot == null)
                {
                    _dtDepot = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeList", new object[] { "仓库" }).Tables[0];
                }
                return _dtDepot;
            }
        }
        /// <summary>
        /// 刷新供应商
        /// </summary>
        public static void ReDeppot()
        {
            dtDepot = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDeparment, "GetTypeList", new object[] { "仓库" }).Tables[0];
        }
        #endregion
    }
}
