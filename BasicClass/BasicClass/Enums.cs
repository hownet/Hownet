using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BasicClass
{
    public class Enums
    {
        /// <summary>
        /// 表單名
        /// </summary>
        public enum TableType
        {
            /// <summary>
            /// 生产制单
            /// </summary>
            Task = 1,
            /// <summary>
            /// 客户
            /// </summary>
            Company = 2,
            /// <summary>
            /// 款号
            /// </summary>
            Product = 3,
            /// <summary>
            /// 商标
            /// </summary>
            Brand = 4,
            /// <summary>
            /// 员工
            /// </summary>
            MiniEmp = 5,
            /// <summary>
            /// 费用类型
            /// </summary>
            Costs = 6,
            /// <summary>
            /// 供应商
            /// </summary>
            Supplier = 7,
            /// <summary>
            /// 成品入库
            /// </summary>
            P2D = 8,
            /// <summary>
            /// 部门
            /// </summary>
            Deparment = 9,
            /// <summary>
            /// 工种
            /// </summary>
            WorkType = 10,
            /// <summary>
            /// 订薪方式
            /// </summary>
            PayType = 11,
            /// <summary>
            /// 学历
            /// </summary>
            Degree = 12,
            /// <summary>
            /// 政治面貌
            /// </summary>
            Polity = 13,
            /// <summary>
            /// HR员工
            /// </summary>
            Employee = 14,
            /// <summary>
            /// 省份
            /// </summary>
            Zone = 15,
            /// <summary>
            /// 单位
            /// </summary>
            Measure = 16,
            /// <summary>
            /// 工艺单主表
            /// </summary>
            PW = 17,
            /// <summary>
            /// 工艺单明细
            /// </summary>
            PWI = 18,
            /// <summary>
            /// 床号
            /// </summary>
            Bed = 19,
            /// <summary>
            /// 就餐桌号
            /// </summary>
            MeaTable = 20,
            /// <summary>
            /// 销售开单
            /// </summary>
            Sell = 21,
            /// <summary>
            /// 销售退货
            /// </summary>
            SellBack = 22,
            /// <summary>
            /// 采购订单
            /// </summary>
            Stock = 23,
            /// <summary>
            /// 采购收货
            /// </summary>
            StockBack = 24,
            /// <summary>
            /// 采购退货
            /// </summary>
            StockBackSupp = 25,
            /// <summary>
            /// 车间领料
            /// </summary>
            TaskLinLiao = 26,
            /// <summary>
            /// 客户订单主表
            /// </summary>
            SalesMain = 27,
            /// <summary>
            /// 客户订单
            /// </summary>
            SalesOne = 28,
            /// <summary>
            /// 派工单
            /// </summary>
            ShippingNotice = 29,
            /// <summary>
            /// BOM
            /// </summary>
            BOM = 30,
            /// <summary>
            /// 加工单
            /// </summary>
            SellProcess = 31,
            /// <summary>
            /// 加工商
            /// </summary>
            Processing = 32,
            /// <summary>
            /// 物料类型
            /// </summary>
            MaterielType = 33,
            /// <summary>
            /// 裁片
            /// </summary>
            CaiPian = 34,
            /// <summary>
            /// 物料
            /// </summary>
            Materiel = 35,
            /// <summary>
            /// 属性
            /// </summary>
            Attribute = 36,
            /// <summary>
            /// 班组生产任务
            /// </summary>
            TaskToDep = 37,
            /// <summary>
            /// 样版单
            /// </summary>
            Sample = 38,
            /// <summary>
            /// 库存盘点
            /// </summary>
            WMSInventoryInfo = 39,
            /// <summary>
            /// 包装方法
            /// </summary>
            PackingMethod = 40,
            /// <summary>
            /// 生产计划
            /// </summary>
            ProductionPlan = 41,
            /// <summary>
            /// 生产领料
            /// </summary>
            LeiLiao = 42,
            /// <summary>
            /// 请购单
            /// </summary>
            NeedStock = 43,
            /// <summary>
            /// 半成品外发加工领料
            /// </summary>
            StockLinLiao = 44,
            /// <summary>
            /// 仓库调拨单
            /// </summary>
            StorageAllocation = 45,
            /// <summary>
            /// 成品入包装部
            /// </summary>
            P2Pack = 46,
            /// <summary>
            /// 委外领料
            /// </summary>
            WWLinLiao = 47,
            /// <summary>
            /// 成品采购订单
            /// </summary>
            FinishedStock=48,
            /// <summary>
            /// 成品采购收货
            /// </summary>
            FinishedSBack=49,
            /// <summary>
            /// 加工厂
            /// </summary>
            JGC=50,
            /// <summary>
            /// 生产领料
            /// </summary>
            LinLiao=60,
            /// <summary>
            /// 物料加工
            /// </summary>
            ProcessingTask=61,
            /// <summary>
            /// 外协加工领料
            /// </summary>
            _外协加工领料=62,
            /// <summary>
            /// 外协加工收货
            /// </summary>
            _外协加工收货=63,
            /// <summary>
            /// 采购部收货
            /// </summary>
            _采购部收货=64,
            /// <summary>
            /// 采购部入仓库
            /// </summary>
            _采购部入仓库=65,

            _缝制要求=66,
            _针步=67,
            _针距=68,
            _针宽=69,
            _制单入库=70
        }
        /// <summary>
        /// 需要公式的类型
        /// </summary>
        public enum Formula
        {
            押金 = 1,
            补贴 = 2
        }
        public enum CompanyType
        {
            客户 = 1,
            供应商 = 2,
            外加工商 = 3
        }
        public enum AmountType
        {
            原始数量 = 1,
            未完成数量 = 2,
            完成数量 = 3,
            未分配到班组数量=4
        }
        /// <summary>
        /// 表單名
        /// </summary>
        public enum NumPrefix
        {
            /// <summary>
            /// 報價
            /// </summary>
            樣衣報價單 = 1,
            /// <summary>
            /// 樣衣
            /// </summary>
            樣衣單 = 2,
            /// <summary>
            /// 訂單
            /// </summary>
            生產訂單 = 3,
            /// <summary>
            /// 布料計劃單
            /// </summary>
            Fabric = 4,
            /// <summary>
            /// 布料采購單
            /// </summary>
            布料采購單 = 5,
            /// <summary>
            /// 布料采購收貨單
            /// </summary>
            布料采購收貨 = 6,
            /// <summary>
            /// 打版單
            /// </summary>
            打版單=7
        }
        /// <summary>
        /// 金額往來表單名
        /// </summary>
        public enum MoneyTableType
        {

            /// <summary>
            /// 采购收货
            /// </summary>
            Back = 1,
            /// <summary>
            /// 还供应商货款
            /// </summary>
            OutMoney = 2,
            /// <summary>
            /// 销售开单
            /// </summary>
            Sell = 3,
            /// <summary>
            /// 收客户货款
            /// </summary>
            BackMoney = 4,
            /// <summary>
            /// 销售退货
            /// </summary>
            SellBack = 5,
            /// <summary>
            /// 采购退货
            /// </summary>
            StockBackSupp = 6,
            /// <summary>
            /// 加工厂领料
            /// </summary>
            ProcessingLinLiao=7,
            /// <summary>
            /// 加工厂来成品
            /// </summary>
            Processing2Depot=8,
            /// <summary>
            /// 付加工厂货款
            /// </summary>
            OutProcessing=9,
            KJFL=11,
            KJFLInMoney=12,
            Vouchers=13
        }
        public enum Attribute
        {
            原材料 = 1,
            半成品 = 2,
            外加工 = 3,
            成品 = 4,
            商标 = 5,
            工具=6
        }
        public enum Operation
        {
            View = 0,
            Add = 1,
            Edit = 2,
            Del = 3,
            Verify = 4,
            Print = 5,
            /// <summary>
            /// 过帐
            /// </summary>
            Posting = 6,
            UnVerify=7,
            Money=8,
            UnPosting=9,
            /// <summary>
            /// 請求審核
            /// </summary>
            NeedVerify = 11,
            /// <summary>
            /// 審核未通過
            /// </summary>
            NoVerify = 12,
            /// <summary>
            /// 普通信息
            /// </summary>
            Common = 13


        }
        public enum IsEnd
        {
            未完成 = 1,
            已完成 = 2,
            公司中止 = 3,
            供應商中止 = 4,
            其它原因中止 = 5
        }
        public enum IsVerify
        {
            未审核 = 1,
            审核中 = 2,
            已审核 = 3,
            开始生产 = 4,
            待确认 = 5,
            确认通过 = 6,
            合并生产 = 7,
            已完成 = 9,
            开始备料 = 10,
            客户取消 = 21,
            公司取消 = 22,
            已过帐 = 31
        }
        public enum Mask
        {
            整數,
            金額,
            匯率
        }
        public enum PlanUseRep
        {
            使用原仓存 = 1,
            使用采购余量 = 2,
            已申购数量 = 3,
            已采购数量 = 4,
            车间已领用数量 = 5,
            库存已备料数量 = 6
        }
        public enum OtherType
        {
            系统设置 = 900,
            用户界面 = 901,
            风格=902
        }

    }
}
