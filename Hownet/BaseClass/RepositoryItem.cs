using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace Hownet.BaseForm
{
  public  class RepositoryItem
    {
      private static DataTable dt = new DataTable();
      private static DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
      private static DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
        static void reMeasure_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                DevExpress.XtraEditors.LookUpEdit ee = sender as DevExpress.XtraEditors.LookUpEdit;
               // DataView dv = (DataView)((sender as DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit).DataSource);
                DataView ddv = (DataView)(ee.Properties.DataSource);
                ddv.RowFilter = "IsEnd=0";
               // dt.DefaultView.RowFilter = "IsEnd=0";
            }
            catch (Exception ex)
            {
            }
        }

        static void reMeasure_QueryCloseUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dt.DefaultView.RowFilter = "";
        }
        public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reTableTypeID
        {
            get
            {
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                reMeasure.AutoHeight = false;
                reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "表单名", 100)});
                reMeasure.DisplayMember = "Name";
                reMeasure.NullText = "";
                reMeasure.Name = "_repositoryMeasure";
                reMeasure.ValueMember = "ID";
                reMeasure.NullText = "";
                reMeasure.DataSource =BasicClass.BaseTableClass.dtTableType.DefaultView;
                return reMeasure;
            }
        }
        public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reRemark(int TypeID)
        {
            string bll = "Hownet.BLL.Remark";
            dt = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(TableTypeID=" + TypeID + ")" }).Tables[0];
            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            reMeasure.AutoHeight = false;
            reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Remarks", "说明", 100)});
            reMeasure.DisplayMember = "Remarks";
            reMeasure.NullText = "";
            reMeasure.Name = "_repositoryMeasure";
            reMeasure.ValueMember = "ID";
            reMeasure.NullText = "";
            reMeasure.DataSource = dt.DefaultView;
            return reMeasure;
        }
      /// <summary>
      /// 款式
      /// </summary>
      /// <param name="TypeID"></param>
      /// <returns></returns>
        public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reMTID(int MTID)
        {
            string bll = "Hownet.BLL.MaterielType";
            if (MTID > 0)
                dt = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(AttributeID=" + MTID + ")" }).Tables[0];
            else
                dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            reMeasure.AutoHeight = false;
            reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Remarks", "类别", 100)});
            reMeasure.DisplayMember = "Name";
            reMeasure.NullText = "";
            reMeasure.Name = "_repositoryMeasure";
            reMeasure.ValueMember = "ID";
            reMeasure.NullText = "";
            reMeasure.DataSource = dt.DefaultView;
            return reMeasure;
        }
        /// <summary>
        /// 货位
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reDepotInfo(int DepotID)
        {
            string bll = "Hownet.BLL.Deparment";
            if (DepotID > 0)
                dt = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(ParentID=" + DepotID + ")" }).Tables[0];
            else
                dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            reMeasure.AutoHeight = false;
            reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "货位", 100)});
            reMeasure.DisplayMember = "Name";
            reMeasure.NullText = "";
            reMeasure.Name = "_repositoryMeasure";
            reMeasure.ValueMember = "ID";
            reMeasure.NullText = "";
            reMeasure.DataSource = dt.DefaultView;
            return reMeasure;
        }
      /// <summary>
      /// 计量单位
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reMeasure
      {
          get
          {
              dt = BasicClass.BaseTableClass.dtMeasure;
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "单位", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.NullText = "";
              reMeasure.DataSource =dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 包装方法
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _rePackingMethod
      {
          get
          {
              string bll = "Hownet.BLL.PackingMethod";
              dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "包装方法", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.NullText = "";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 客户
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reCompanyID
      {
          get
          {
              //int _userID = 0;
              //if (BasicClass.UserInfo.UserName != "Admin")
              //    _userID = BasicClass.UserInfo.UserID;
              dt = BasicClass.BaseTableClass.dtCompany;// BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "(TypeID=1)" }).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "客户", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 供应商
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reSupplier
      {
          get
          {
              //int _userID = 0;
              //if (BasicClass.UserInfo.UserName != "Admin")
              //    _userID = BasicClass.UserInfo.UserID;
              dt = BasicClass.BaseTableClass.dtSupplier;
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "供应商", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 裁片
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reCaiPian
      {
          get
          {
              string bll = "Hownet.BLL.CaiPian";
              dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "裁片名", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.NullText = "";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 成品款号
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reProduce
      {
          get
          {
              dt = BasicClass.BaseTableClass.dtFinished;// BasicClass.GetDataSet.GetDS(bll, "GetLookupList", new object[] { "(AttributeID=4)" }).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "款号", 100)});
              //reMeasure.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
              //  //new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", "Combo", null, true),
              //  new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", "New", null, true)});
              //_reProduce.Leave += new EventHandler(_reProduce_Leave);
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_reProduce";
              reMeasure.ValueMember = "ID";
              reMeasure.NullText = "";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
     
      /// <summary>
      /// 全部物料
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reAllMateriel
      {
          get
          {
              string bll = "Hownet.BLL.Materiel";
              dt = BasicClass.GetDataSet.GetDS(bll, "GetLookupList", new object[] { "(AttributeID<5)" }).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "物料", 100)});
              //reMeasure.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
              //  //new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", "Combo", null, true),
              //  new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", "New", null, true)});
              //_reProduce.Leave += new EventHandler(_reProduce_Leave);
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_reProduce";
              reMeasure.ValueMember = "ID";
              reMeasure.NullText = "";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      static void _reProduce_Leave(object sender, EventArgs e)
      {
          
      }

      /// <summary>
      /// 商标
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reBrand
      {
          get
          {
              string bll = "Hownet.BLL.Materiel";
              dt = BasicClass.GetDataSet.GetDS(bll, "GetLookupList", new object[] { "(AttributeID=5)" }).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "商标", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.NullText = "";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 物料属性
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reAttribute
      {
          get
          {
              string bll = "Hownet.BLL.Attribute";
              dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList",null).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "物料属性", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.NullText = "";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 商标
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reBow(string TypeName)
      {
          string bll = "Hownet.BLL.Materiel";
          dt = BasicClass.GetDataSet.GetDS(bll, "GetByTypeName", new object[] { TypeName }).Tables[0];
          DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
          reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
          reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
          reMeasure.AutoHeight = false;
          reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", TypeName, 100)});
          reMeasure.DisplayMember = "Name";
          reMeasure.NullText = "";
          reMeasure.Name = "_repositoryMeasure";
          reMeasure.ValueMember = "ID";
          reMeasure.NullText = "";
          reMeasure.DataSource = dt.DefaultView;
          return reMeasure;
      }
      /// <summary>
      /// 部门
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reDeparment
      {
          get
          {
              string bll = "Hownet.BLL.Deparment";
               dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "部门", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.NullText = "";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 职务
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reJobs
      {
          get
          {
              dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllDepartmentJobs, "GetAllList", null).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("JobsName", "职务", 100)});
              reMeasure.DisplayMember = "JobsName";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.NullText = "";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 员工
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reMiniEmp
      {
          get
          {
              dt = BasicClass.BaseTableClass.dtMiniEmp;
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "员工", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.NullText = "";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 工种
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reWorkType
      {
          get
          {
              string bll = "Hownet.BLL.WorkType";
               dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
               DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "工种", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_reMateriel";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 工种
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reUser
      {
          get
          {
              string bll = "Hownet.BLL.Users";
              dt = BasicClass.BaseTableClass.dtuser; //BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "用户", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_reMateriel";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 二位金额
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemTextEdit _re2Money
      {
          get
          {
              DevExpress.XtraEditors.Repository.RepositoryItemTextEdit re2Money = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
              re2Money.AutoHeight = false;
              re2Money.EditFormat.FormatString = "C4";
              re2Money.DisplayFormat.FormatString = "C4";
              re2Money.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
              re2Money.Mask.EditMask = "C4";
              re2Money.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
              re2Money.Mask.UseMaskAsDisplayFormat = true;
              re2Money.Name = "_reMoney";
              return re2Money;
          }
      }
      /// <summary>
      /// 数量显示
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemTextEdit _reNum
      {
          get
          {
              DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
              repositoryItemTextEdit1.AutoHeight = false;
              repositoryItemTextEdit1.DisplayFormat.FormatString = "0.####";
              repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
              repositoryItemTextEdit1.EditFormat.FormatString = "0.####";
              repositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
              repositoryItemTextEdit1.Mask.EditMask = "#########.####";
              repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
              repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
              return repositoryItemTextEdit1;
          }
      }
      /// <summary>
      /// 颜色
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reColor
      {
          get
          {
              string bll = "Hownet.BLL.Color";
              dt=BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "颜色", 100),
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Remark", "说明", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 非成品物料
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reMateriel
      {
          get
          {
              string bll = "Hownet.BLL.Materiel";
              dt = BasicClass.GetDataSet.GetDS(bll, "GetLookupList", new object[] { "(AttributeID<4)" }).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "款号", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.NullText = "";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 尺码
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reSize
      {
          get
          {
              string bll = "Hownet.BLL.Size";
              dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "尺码", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 生产制单编号
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reTaskNum
      {
          get
          {
              dt = BasicClass.GetDataSet.GetDS(BasicClass.Bllstr.bllProductTaskMain, "GetNumList", null).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              //reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              //reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Num", "编号", 100)});
              reMeasure.DisplayMember = "Num";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 生产计划编号
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _rePlanNum
      {
          get
          {
              dt = BasicClass.GetDataSet.GetDS("Hownet.BLL.ProductionPlan", "GetNumList", new object[] { "" }).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              //reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              //reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Num", "编号", 100)});
              reMeasure.DisplayMember = "Num";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              DataRow dr = dt.NewRow();
              dr["ID"] = 0;
              dr["Num"] = DBNull.Value;
              dt.Rows.InsertAt(dr, 0);
              reMeasure.DataSource = dt;
              return reMeasure;
          }
      }
      ///// <summary>
      ///// 使用方法
      ///// </summary>
      //public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reUse
      //{
      //    get
      //    {
      //        Hownet.BLL.UseInfo bllUI = new Hownet.BLL.UseInfo();
      //        DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMateriel = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
      //        reMateriel.AutoHeight = false;
      //        reMateriel.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
      //          new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UseName", "使用方法", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
      //        reMateriel.DisplayMember = "UseName";
      //        reMateriel.Name = "_reMateriel";
      //        reMateriel.ValueMember = "UseID";
      //        reMateriel.DataSource = bllUI.GetAllList().Tables[0];
      //        return reMateriel;
      //    }
      //}
      /// <summary>
      /// 全部工序
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reWorking
      {
          get
          {
              dt = BasicClass.BaseTableClass.dtWorking;// BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "工序名", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 普通工序
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reNotSpecialWorking
      {
          get
          {
              string bll = "Hownet.BLL.Working";
              dt = BasicClass.GetDataSet.GetDS(bll, "GetList", new object[] { "IsSpecial=0" }).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "工序名", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 执行情况
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reIsExc
      {
          get
          {
              DataTable dt = new DataTable();
              dt.Columns.Add("ID", typeof(int));
              dt.Columns.Add("Name", typeof(string));
              dt.Rows.Add(0, "尚未开始");
              dt.Rows.Add(1, "执行中");
              dt.Rows.Add(2, "已完成");
              dt.Rows.Add(3, "中止或取消");
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reType.AutoHeight = false;
              reType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "执行情况", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
              reType.DisplayMember = "Name";
              reType.Name = "_reMateriel";
              reType.ValueMember = "ID";
              reType.DataSource = dt;
              return reType;
          }
      }
      /// <summary>
      /// 执行情况
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reIsVerify
      {
          get
          {
              DataTable dt = new DataTable();
              dt.Columns.Add("Name", typeof(string));
              dt.Columns.Add("ID", typeof(int));
              dt.Rows.Add("", 0);
              dt.Rows.Add("未审核", 1);
              dt.Rows.Add("审核中", 2);
              dt.Rows.Add("已审核", 3);
              dt.Rows.Add("开始生产", 4);
              dt.Rows.Add("待确认", 5);
              dt.Rows.Add("已过帐", 6);
              dt.Rows.Add("合并生产", 7);
              dt.Rows.Add("已完成", 9);
              dt.Rows.Add("客户取消", 21);
              dt.Rows.Add("公司取消", 22);
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reType.AutoHeight = false;
              reType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "执行情况", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
              reType.DisplayMember = "Name";
              reType.Name = "_reMateriel";
              reType.ValueMember = "ID";
              reType.DataSource = dt;
              return reType;
          }
      }
      /// <summary>
      /// 性别
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reSex
      {
          get
          {
              DataTable dt = new DataTable();
              dt.Columns.Add("ID", typeof(int));
              dt.Columns.Add("Name", typeof(string));
              dt.Rows.Add(1, "男");
              dt.Rows.Add(2, "女");
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reType.AutoHeight = false;
              reType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "性别", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
              reType.DisplayMember = "Name";
              reType.Name = "_reMateriel";
              reType.ValueMember = "ID";
              reType.DataSource = dt;
              return reType;
          }
      }
      /// <summary>
      /// 颜色色系
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reColorType
      {
          get
          {
              DataTable dtType = new DataTable();
              dtType.Columns.Add("ID", typeof(int));
              dtType.Columns.Add("Name", typeof(string));
              dtType.Rows.Add(1, "浅色");
              dtType.Rows.Add(2, "中色");
              dtType.Rows.Add(3, "深色");
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reType.AutoHeight = false;
              reType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "色系", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
              reType.DisplayMember = "Name";
              reType.Name = "_reMateriel";
              reType.ValueMember = "ID";
              reType.DataSource = dtType;
              return reType;
          }
      }
      /// <summary>
      /// 国家
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reCountry
      {
          get
          {
              DataTable dtType = new DataTable();
              dtType.Columns.Add("ID", typeof(int));
              dtType.Columns.Add("Name", typeof(string));
              dtType.Rows.Add(0, "中国");
              dtType.Rows.Add(1, "其他");
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reType.AutoHeight = false;
              reType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "国家", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
              reType.DisplayMember = "Name";
              reType.Name = "_reMateriel";
              reType.ValueMember = "ID";
              reType.DataSource = dtType;
              return reType;
          }
      }
      /// <summary>
      /// 尺寸部位
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reSizePart
      {
          get
          {
              string bll = "Hownet.BLL.SizePart";
              DataTable dt = BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "尺寸部位", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.NullText = "";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView; ;
              return reMeasure;
          }
      }
      /// <summary>
      /// 规格
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reSpec
      {
          get
          {
              DataTable dt = BasicClass.BaseTableClass.dtSpec;// BasicClass.GetDataSet.GetDS(bll, "GetAllList", null).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "规格", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.NullText = "";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView; ;
              return reMeasure;
          }
      }
      /// <summary>
      /// 部门类型
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reDepartmentType
      {
          get
          {
              string bll = "Hownet.BLL.OtherType";
              dt = BasicClass.GetDataSet.GetDS(bll, "GetTypeList", new object[] { "部门类型" }).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "部门类型", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 费用类型
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reCosts
      {
          get
          {
              string bll = "Hownet.BLL.OtherType";
              dt = BasicClass.GetDataSet.GetDS(bll, "GetTypeList", new object[] { "费用类型" }).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "费用类型", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
   /// <summary>
   /// 计薪方法
   /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _rePayID
      {
          get
          {
              string bll = "Hownet.BLL.OtherType";
              dt = BasicClass.GetDataSet.GetDS(bll, "GetTypeList", new object[] { "计薪方式" }).Tables[0];
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reMeasure.QueryPopUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryPopUp);
              reMeasure.QueryCloseUp += new System.ComponentModel.CancelEventHandler(reMeasure_QueryCloseUp);
              reMeasure.AutoHeight = false;
              reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "计薪方式", 100)});
              reMeasure.DisplayMember = "Name";
              reMeasure.NullText = "";
              reMeasure.Name = "_repositoryMeasure";
              reMeasure.ValueMember = "ID";
              reMeasure.DataSource = dt.DefaultView;
              return reMeasure;
          }
      }
      /// <summary>
      /// 单位类别
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reMeasureType
      {
          get
          {
              DataTable dtType = new DataTable();
              dtType.Columns.Add("ID", typeof(int));
              dtType.Columns.Add("Name", typeof(string));
              dtType.Rows.Add(1, "数值");
              dtType.Rows.Add(2, "长度");
              dtType.Rows.Add(3, "重量");
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reType.AutoHeight = false;
              reType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "单位类别", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
              reType.DisplayMember = "Name";
              reType.Name = "_reMateriel";
              reType.ValueMember = "ID";
              reType.DataSource = dtType;
              return reType;
          }
      }
      ///// <summary>
      ///// 特殊工序
      ///// </summary>
      //public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reSpecialWorking
      //{
      //    get
      //    {
      //        Hownet.BLL.Working bllWork = new Hownet.BLL.Working();
      //        DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
      //        reMeasure.AutoHeight = false;
      //        reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
      //          new DevExpress.XtraEditors.Controls.LookUpColumnInfo("WorkingName", "工序名", 100)});
      //        reMeasure.DisplayMember = "WorkingName";
      //        reMeasure.Name = "_repositoryMeasure";
      //        reMeasure.ValueMember = "WorkingID";
      //        DataTable dt = bllWork.GetList("(IsSpecial = 1)").Tables[0];
      //        dt.DefaultView.Sort = "WorkingID";
      //        reMeasure.DataSource = dt;
      //        return reMeasure;
      //    }
      //}
      ///// <summary>
      ///// 非特殊工序
      ///// </summary>
      //public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reNotSpecialWorking
      //{
      //    get
      //    {
      //        Hownet.BLL.Working bllWork = new Hownet.BLL.Working();
      //        DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reMeasure = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
      //        reMeasure.AutoHeight = false;
      //        reMeasure.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
      //          new DevExpress.XtraEditors.Controls.LookUpColumnInfo("WorkingName", "工序名", 100)});
      //        reMeasure.DisplayMember = "WorkingName";
      //        reMeasure.Name = "_repositoryMeasure";
      //        reMeasure.ValueMember = "WorkingID";
      //        DataTable dt = bllWork.GetList("(IsSpecial = 0)").Tables[0];
      //        dt.DefaultView.Sort = "WorkingID";
      //        reMeasure.DataSource = dt;
      //        return reMeasure;
      //    }
      //}
      /// <summary>
      /// 使用方法
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reUse
      {
          get
          {
              DataTable dt = new DataTable();
              dt.Columns.Add("ID", typeof(int));
              dt.Columns.Add("Name", typeof(string));
              dt.Rows.Add(1, "按主色");
              dt.Rows.Add(2, "按插色一");
              dt.Rows.Add(3, "按插色二");
              dt.Rows.Add(4, "按尺码");
              dt.Rows.Add(5, "按主色+尺码");
              dt.Rows.Add(6, "按插色一+尺码");
              dt.Rows.Add(7, "按插色二+尺码");
              dt.Rows.Add(8, "按主色+插色一");
              dt.Rows.Add(9, "按主色+插色二");
              dt.Rows.Add(10, "按插色一+插色二");
              dt.Rows.Add(11, "按主色+插色一+插色二");
              dt.Rows.Add(12, "都不用");
              dt.Rows.Add(13, "特殊配色");
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reType.AutoHeight = false;
              reType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "使用方法", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
              reType.DisplayMember = "Name";
              reType.NullText = "";
              reType.Name = "_reMateriel";
              reType.ValueMember = "ID";
              reType.DataSource = dt;
              return reType;
          }
      }
      /// <summary>
      /// 加班情况
      /// </summary>
      public static DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit _reWorkDay
      {
          get
          {
              DataTable dt = new DataTable();
              dt.Columns.Add("ID", typeof(int));
              dt.Columns.Add("Name", typeof(string));
              dt.Rows.Add(2, "通宵");
              dt.Rows.Add(3, "午连班");
              dt.Rows.Add(4, "晚连班");
              DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
              reType.AutoHeight = false;
              reType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "加班情况", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
              reType.DisplayMember = "Name";
              reType.Name = "_reMateriel";
              reType.ValueMember = "ID";
              reType.DataSource = dt;
              return reType;
          }
      }

    }
}
