using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FastReport;

namespace Hownet.BaseForm
{
    public class PrintClass
    {
        public static void PrintSellTable(DataSet ds ,int A4A5   )
        { 
            Report report = new Report();
            if(A4A5==0)
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\SellTable.frx");
            else if(A4A5==1)
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\SellTableA4.frx");
            else if(A4A5==2)
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\SellTableA5.frx");
            report.RegisterData(ds);
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
           report.Show();
          //report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintInMoney(DataSet ds)
        {
            Report report = new Report();

             report.Load(BasicClass.BasicFile.AppDir + @"\Report\InMoney.frx");
            report.RegisterData(ds);
           // report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            report.Show();
            // report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintOutMoney(DataSet ds)
        {
            Report report = new Report();

            report.Load(BasicClass.BasicFile.AppDir + @"\Report\OutMoney.frx");
            report.RegisterData(ds);
          //   report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            report.Show();
           //  report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintSellInfoTable(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\SellInfoTable.frx");
            report.RegisterData(ds);
            //report.GetDataSource("Info").Enabled = true;
            //report.GetDataSource("Main").Enabled = true;
            report.Show();
           // report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintStockSellTable(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\StockSell.frx");
            report.RegisterData(ds);
            //report.GetDataSource("Info").Enabled = true;
            //report.GetDataSource("Main").Enabled = true;
            report.Show();
           //  report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintStockToDep(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\StockToDep.frx");
            report.RegisterData(ds);
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            report.Show();
           // report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintFinishedStockToDep(DataSet ds,bool IsDesign)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\FinishedStockToDep.frx");
            report.RegisterData(ds);
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            if(!IsDesign)
             report.Show();
            else
            report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintFinishedStockToDepNoPrice(DataSet ds, bool IsDesign)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\FinishedStockToDepNoPrice.frx");
            report.RegisterData(ds);
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            if (!IsDesign)
                report.Show();
            else
                report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintSellBack(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\SellBackTable.frx");
            report.RegisterData(ds);
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
             report.Show();
           // report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintLinLiao(DataSet ds,bool IsA4)
        {
            Report report = new Report();
            if(!IsA4)
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\LinLiao.frx");
            else
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\LinLiaoA4.frx");
            report.RegisterData(ds);
             report.GetDataSource("Info").Enabled = true;
             report.GetDataSource("Main").Enabled = true;
          report.Show();
        //   report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintWWLinLiao(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\WWLinLiao.frx");
            report.RegisterData(ds);
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            report.Show();
            // report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintStockTable(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\StockTable.frx");
            report.RegisterData(ds);
            //report.GetDataSource("Info").Enabled = true;
            //report.GetDataSource("Main").Enabled = true;
            //report.GetDataSource("dtTem").Enabled = true;
            report.Show();
            //report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintNeedStock(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\NeedStockTable.frx");
            report.RegisterData(ds);
            //report.GetDataSource("Info").Enabled = true;
            //report.GetDataSource("Main").Enabled = true;
            report.Show();
            //report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintSellProess(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\SellProess.frx");
            report.RegisterData(ds);
           // report.GetDataSource("Info").Enabled = true;
           // report.GetDataSource("Main").Enabled = true;
            report.Show();
           // report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintSellInfo(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\SellInfo.frx");
            report.RegisterData(ds);
            //report.GetDataSource("Info").Enabled = true;
            //report.GetDataSource("Main").Enabled = true;
            report.Show();
            //report.Design();
           report.Dispose();
            GC.Collect();
        }
        public static void PrintSBackInfo(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\FinedSBackInfo.frx");
            report.RegisterData(ds);
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            report.Show();
            //report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintSellColorSum(DataSet ds)
        {
            Report report = new Report();
             report.Load(BasicClass.BasicFile.AppDir + @"\Report\SellInfoSumColor.frx");
            report.RegisterData(ds);
            report.GetDataSource("dttInfo").Enabled = true;
            report.GetDataSource("dttMain").Enabled = true;
              report.Show();
            //report.Design();
            report.Dispose();
            GC.Collect();
        }

        public static void PrintTaskTicket(DataSet ds,bool IsDesign)
        {
            Report report = new Report();
            if (BasicClass.BasicFile.liST[0].CustOder)
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\TicketCustOder.frx");
            else
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\Ticket.frx");
            report.RegisterData(ds);

            if (!IsDesign)
            {
                report.Show();
            }
            else
            {
                report.GetDataSource("MainDt").Enabled = true;
                report.GetDataSource("Ticket").Enabled = true;
                report.Design();
            }
            report.Dispose();
            GC.Collect();
        }
        public static void PrintTaskInfo(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\TicketInfo.frx");
            report.RegisterData(ds);
            report.Show();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintTaskInfoCard(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\TicketInfoCard.frx");
            report.RegisterData(ds);
           // report.GetDataSource("Ticket").Enabled = true;
            report.Show();
           // report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintTaskGroup(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\TicketGroup.frx");
            report.RegisterData(ds);
          //  report.GetDataSource("Ticket").Enabled = true;
           // report.GetDataSource("MainDt").Enabled = true;
            report.Show();
           // report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintPayCosts(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\frPayCosts.frx");
            report.RegisterData(ds);
            //report.GetDataSource("Main").Enabled = true;
            //report.GetDataSource("Info").Enabled = true;
            report.Show();
            //report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintTicketLine(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\TicketLine.frx");
            report.RegisterData(ds);
            report.Show();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintDay(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\DayReport.frx");
            report.RegisterData(ds);
            report.Show();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintDaySum(DataTable dt, bool IsInfo,bool IsShowPrice)
        {
            Report report = new Report();
            if (IsInfo)
            {
                if (IsShowPrice)
                {
                    report.Load(BasicClass.BasicFile.AppDir + @"\Report\SumTicketPrice.frx");
                }
                else
                {
                    report.Load(BasicClass.BasicFile.AppDir + @"\Report\SumTicket.frx");
                }
            }
            else
            {
                if (IsShowPrice)
                {
                    report.Load(BasicClass.BasicFile.AppDir + @"\Report\InfoTicket.frx");
                }
                else
                {
                    report.Load(BasicClass.BasicFile.AppDir + @"\Report\InfoTicket.frx");
                }
            }
            report.RegisterData(dt, "dtSum");
            //report.GetDataSource("dtSum").Enabled = true;
            report.Show();
            //report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintPayLine(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\PayLineReport.frx");
            report.RegisterData(ds);
            report.GetDataSource("PayLine").Enabled = true;
            report.Show();
           // report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintEmpPay(DataSet ds,bool IsAndBuTie)
        {
            Report report = new Report();
            if(IsAndBuTie)
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\PayPriceReport.frx");
            else
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\PayPriceReportNoBuTie.frx");
            report.RegisterData(ds);
            report.GetDataSource("PayLine").Enabled = true;
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("NoDefault").Enabled = true;
            report.Show();
            //report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintLabes(DataTable dt)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\repLabel.frx");
            report.RegisterData(dt,"Labs");
          //  report.GetDataSource("Labs").Enabled = true;
            report.Show();
           // report.Design();
            report.Dispose();
        }
        public static void PrintBigLabes(DataTable dt)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\repBigLabel.frx");
            report.RegisterData(dt, "Labs");
            //  report.GetDataSource("Labs").Enabled = true;
            report.Show();
             //report.Design();
            report.Dispose();
        }
        public static void PrintCompanyMoney(int TypeID, DataSet  ds    )
        {
            Report report = new Report();
            if (TypeID == 1)
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\CompanyMoney.frx");
            else if (TypeID == 2)
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\SupplierMoney.frx");
            else if (TypeID == 3)
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\ProcessingMoney.frx");
            report.RegisterData(ds);
            report.RegisterData(ds.Tables[0], "CompanyMoney");//.Enabled = true;
          //  report.GetDataSource("CompanyMoney").Enabled = true;
          //  report.GetDataSource("Tem").Enabled = true;
            report.Show();
            //report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintCompanyMoneyList(int TypeID, DataTable dt)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\CompanyMoneyList.frx");
            report.RegisterData(dt, "CompanyMoneyList");
            //report.GetDataSource("CompanyMoneyList").Enabled = true;
            report.Show();
            //report.Design();
            report.Dispose();
        }
        public static void BOM(DataTable dt)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\BOM.frx");
            report.RegisterData(dt, "BOM");
            report.GetDataSource("BOM").Enabled = true;
            report.Show();
           // report.Design();
            report.Dispose();
        }
        public static void PrintBOMPrice(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\BomPrice.frx");
            report.RegisterData(ds);
            report.GetDataSource("Main").Enabled = true;
            report.GetDataSource("Info").Enabled = true;

             report.Show();
             //report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void ProductWorking(DataTable dt)
        {
            Report report = new Report();
            if (BasicClass.BasicFile.liST[0].CustOder)
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\ProductWorkingCustOder.frx");
            else
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\ProductWorking.frx");
            report.RegisterData(dt, "Work");
          //  report.GetDataSource("Work").Enabled = true;
            report.Show();
            report.Dispose();
        }
        public static void PrintFinishedWorking(DataSet ds)
        {
            try
            {
                Report report = new Report();
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\finishedWorking.frx");
                report.RegisterData(ds);
                report.GetDataSource("Main").Enabled = true;
                report.GetDataSource("Info").Enabled = true;
                 report.Show();
               // report.Design();
                report.Dispose();
                GC.Collect();
            }
            catch
            {
                
            }
        }
        public static void Sample(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\Samples.frx");
            report.RegisterData(ds);
            //report.GetDataSource("dtT").Enabled = true;
            //report.GetDataSource("dtM").Enabled = true;
            //report.GetDataSource("dtI").Enabled = true;
            //report.GetDataSource("dtMat").Enabled = true;
            //report.GetDataSource("TaskPrint").Enabled = true;
            //report.GetDataSource("dtImage").Enabled = true;
           // report.Show();
            // report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void TaskBom(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\TaskDemand.frx");
            report.RegisterData(ds);
          //  report.GetDataSource("Main").Enabled = true;
          //  report.GetDataSource("AmountInfo").Enabled = true;
          //  report.GetDataSource("dtDemand").Enabled = true;
          //  report.GetDataSource("SizePart").Enabled = true;
          //  report.GetDataSource("SizeDemand").Enabled = true;
          //  report.GetDataSource("CSDemand").Enabled = true;
          //  report.GetDataSource("dtNoCS").Enabled = true;
            report.Show();
          //  report.Design();
            report.Dispose();
        }

        public static void PrintTaskAmount(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\TaskAmount.frx");
            report.RegisterData(ds);
              report.GetDataSource("Main").Enabled = true;
              report.GetDataSource("AmountInfo").Enabled = true;
              report.GetDataSource("dtLR").Enabled = true;
              report.GetDataSource("dtAR").Enabled = true;
           // report.Show();
              report.Design();
            report.Dispose();
        }
        public static void PrintTaskt(DataSet ds,int Rows)
        {
            Report report = new Report();
            if(Rows<15)
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\frTaskToDeparment.frx");
            else
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\frTaskToDeparment18.frx");
            report.RegisterData(ds);
            report.GetDataSource("Main").Enabled = true;
            report.GetDataSource("AmountInfo").Enabled = true;
            report.GetDataSource("dtAR").Enabled = true;
            report.GetDataSource("BOM").Enabled = true;
            report.Show();
           //  report.Design();
            report.Dispose();
        }
        public static void TaskReport(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\TaskReport.frx");
            report.RegisterData(ds);
            report.GetDataSource("Main").Enabled = true;
            report.GetDataSource("AmountInfo").Enabled = true;
            report.GetDataSource("dtDemand").Enabled = true;
            report.GetDataSource("SizePart").Enabled = true;
            report.GetDataSource("SizeDemand").Enabled = true;
            report.GetDataSource("CSDemand").Enabled = true;
            report.GetDataSource("dtNoCS").Enabled = true;
            report.Show();
          //  report.Design();
            report.Dispose();
        }
        public static void TaskReportLine(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\TaskReportLine.frx");
            report.RegisterData(ds);
            report.GetDataSource("Main").Enabled = true;
            report.GetDataSource("AmountInfo").Enabled = true;
            report.GetDataSource("Demand").Enabled = true;
            report.Show();
            //  report.Design();
            report.Dispose();
        }
        public static void TaskForm(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\TaskForm.frx");
            report.RegisterData(ds);
            //report.GetDataSource("Main").Enabled = true;
            //report.GetDataSource("AmountInfo").Enabled = true;
            //report.GetDataSource("dtDemand").Enabled = true;
            //report.GetDataSource("SizePart").Enabled = true;
            report.Show();
            //report.Design();
            report.Dispose();
        }
        public static void MaterielStructure(DataSet ds)
        {
            Report report = new Report();

            report.Load(BasicClass.BasicFile.AppDir + @"\Report\frMaterielStructure.frx");
            report.RegisterData(ds);
            //report.GetDataSource("Main").Enabled = true;
            //report.GetDataSource("Info").Enabled = true;
            //report.Design();
            report.Show();
            report.Dispose();
        }
        public static void OrderList(DataSet ds)
        {
            Report report = new Report();

            report.Load(BasicClass.BasicFile.AppDir + @"\Report\frOrderList.frx");
            report.RegisterData(ds);
            report.GetDataSource("Main").Enabled = true;
            report.GetDataSource("Info").Enabled = true;
          //  report.Design();
            report.Show();
            report.Dispose();
        }
        public static void WorkEnd(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\WorkEnd.frx");
            report.RegisterData(ds);
            //report.GetDataSource("Main").Enabled = true;
            //report.GetDataSource("Info").Enabled = true;
            //report.Design();
            report.Show();
            report.Dispose();
        }
        public static void ExpreSF(DataTable dt,string ExpressType)
        {
            Report report = new Report();
            if(ExpressType=="顺丰")
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\SF.frx");
            else if (ExpressType == "快捷")
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\KJ.frx");
            else if (ExpressType == "申通")
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\ST.frx");
            else if (ExpressType == "速尔")
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\Sure.frx");
            else if (ExpressType == "中通")
                report.Load(BasicClass.BasicFile.AppDir + @"\Report\ZT.frx");
            report.RegisterData(dt, "Exp");
            // report.GetDataSource("Exp").Enabled = true;
            report.Show();
           //  report.Design();
            report.Dispose();
        }
        public static void KQInfo(DataSet ds)
        {
            Report report = new Report();
            report.Load(System.Windows.Forms.Application.StartupPath + @"\Report\DayInfo.frx");
            report.RegisterData(ds);
            //report.GetDataSource("dtList").Enabled = true;
            //report.GetDataSource("dtMain").Enabled = true;
            //report.Design();
            report.Show();
            report.Dispose();
        }
        public static void KQSum(DataSet ds)
        {
            Report report = new Report();
            report.Load(System.Windows.Forms.Application.StartupPath + @"\Report\DaySum.frx");
            report.RegisterData(ds);
            //report.GetDataSource("dtList").Enabled = true;
            //report.GetDataSource("dtMain").Enabled = true;
            // report.Design();
            report.Show();
            report.Dispose();
        }
        public static void PrintP2DTable(DataSet ds, bool IsDesign)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\P2D.frx");
            report.RegisterData(ds);
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            if (!IsDesign)
                report.Show();
            else
                report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintPack2DTable(DataSet ds, bool IsDesign)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\Pack2D.frx");
            report.RegisterData(ds);
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            if (!IsDesign)
                report.Show();
            else
                report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintP2PackTable(DataSet ds, bool IsDesign)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\P2Pack.frx");
            report.RegisterData(ds);
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            if (!IsDesign)
                report.Show();
            else
                report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintSaleslist(DataSet ds, bool IsDesign)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\frSalesList.frx");
            report.RegisterData(ds);
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            report.GetDataSource("List").Enabled = true;
            if (!IsDesign)
                report.Show();
            else
                report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintSelllist(DataSet ds, bool IsDesign)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\frSellList.frx");
            report.RegisterData(ds);
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            report.GetDataSource("List").Enabled = true;
            if (!IsDesign)
                report.Show();
            else
                report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintSalesAlllist(DataSet ds, bool IsDesign)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\frSalesAllList.frx");
            report.RegisterData(ds);
            report.GetDataSource("Info").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            report.GetDataSource("List").Enabled = true;
            if (!IsDesign)
                report.Show();
            else
                report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void PrintQR(DataTable dt)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\QRLabel.frx");
            report.RegisterData(dt, "Labs");
            // report.GetDataSource("Labs").Enabled = true;
            report.Show();
          //   report.Design();
            report.Dispose();
        }
        /// <summary>
        /// 物料二维码
        /// </summary>
        /// <param name="dt"></param>
        public static void PrintMaterielQR(DataTable dt)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\MaterielQR.frx");
            report.RegisterData(dt, "dt");
          //  report.GetDataSource("dt").Enabled = true;
            report.Show();
           //  report.Design();
            report.Dispose();
        }
        public static void PrintEMP(DataTable dt)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\frEMP.frx");
            report.RegisterData(dt, "Emp");
            report.GetDataSource("Emp").Enabled = true;
            report.Show();
            // report.Design();
            report.Dispose();
        }
        public static void PrintKJFL(DataSet ds)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\frKJFL.frx");
            report.RegisterData(ds);
            //report.GetDataSource("Main").Enabled = true;
           // report.GetDataSource("Info").Enabled = true;
            report.Show();
          //  report.Design();
            report.Dispose();
        }
        public static void PrintInLabes(DataTable dt,bool IsPrint)
        {
            Report report = new Report();
             report.Load(BasicClass.BasicFile.AppDir + @"\Report\frInLab.frx");
            report.RegisterData(dt, "Labs");

            if (IsPrint)
            {
                report.Show();
            }
            else
            {
                report.GetDataSource("Labs").Enabled = true;
                report.Design();
            }
            report.Dispose();
        }
        public static void PrintSamply(DataSet ds, bool IsDesign)
        {
            Report report = new Report();
            report.Load(BasicClass.BasicFile.AppDir + @"\Report\frSample.frx");
            report.RegisterData(ds);
            report.GetDataSource("CA").Enabled = true;
            report.GetDataSource("Main").Enabled = true;
            report.GetDataSource("SML").Enabled = true;
            if (!IsDesign)
                report.Show();
            else
                report.Design();
            report.Dispose();
            GC.Collect();
        }
        public static void Tem(DataSet ds)
        {
            Report report = new Report();
            report.RegisterData(ds);
            report.GetDataSource("Ticket").Enabled = true;
            report.Design();
            report.Dispose();
        }
    }
}
