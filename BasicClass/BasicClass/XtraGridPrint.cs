using System;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using System.Xml.Serialization;
using System.Drawing;

namespace Hownet
{
    class XtraGridPrint
    {

        //*********************字段*********************
        private string pageHeaderName = "";
        private string pageFooterName = "";
        private bool isPrintPage = true;
        private bool isPrintDate = true;
        private int headerLocation = 1;
        private int footerLocation = 1;
        private int pageLocation = 3;
        private int dateLocation = 5;
        private bool enableEditPage = true;
        private System.Drawing.Font pageHeaderFont = null;
        private System.Drawing.Font pageFooterFont = null;
        private System.Drawing.Printing.Margins devMargins = null;
        private System.Drawing.Printing.PaperKind devPaperKind = System.Drawing.Printing.PaperKind.A4;//可以自定义修改纸张
        private PageHeaderArea PHA = new DevExpress.XtraPrinting.PageHeaderArea();
        private PageFooterArea PFA = new DevExpress.XtraPrinting.PageFooterArea();
        //*********************字段*********************


        //*********************属性*********************
        
        /// <summary>
        /// 标题
        /// </summary>
        public string PageHeaderName
        {
            get
            {
                return pageHeaderName;
            }
            set
            {
                pageHeaderName = value;
            }
        }

        /// <summary>
        ///页脚 
        /// </summary>
        public string PageFooterName
        {
            get
            {
                return pageFooterName;
            }
            set
            {
                pageFooterName = value;
            }
        }

        //是否打印页数
        public bool IsPrintPage
        {
            get
            {
                return isPrintPage;
            }
            set
            {
                isPrintPage = value;
            }
        }


        //是否打印时间
        public bool IsPrintDate
        {
            get
            {
                return isPrintDate;
            }
            set
            {
                isPrintDate = value;
            }
        }

        //header的位置（左，中，右）
        public int HeaderLocation
        {
            get
            {
                return headerLocation;
            }
            set
            {
                headerLocation = value;
            }
        }

        //footer的位置（左，中，右）
        public int FooterLocation
        {
            get
            {
                return footerLocation;
            }
            set
            {
                footerLocation = value;
            }
        }


        //页数的位置
        public int PageLocation
        {
            get
            {
                return pageLocation;
            }
            set
            {
                pageLocation = value;
            }
        }


        //时间的位置
        public int DateLocation
        {
            get
            {
                return dateLocation;
            }
            set
            {
                dateLocation = value;
            }
        }

        //是否允许编辑页面
        public bool EnableEditPage
        {
            get
            {
                return enableEditPage;
            }
            set
            {
                enableEditPage = value;
            }
        }

        //标题字体
        public System.Drawing.Font PageHeaderFont
        {
            get
            {
                return pageHeaderFont;
            }
            set
            {
                pageHeaderFont = value;
            }
        }


        //页脚字体
        public System.Drawing.Font PageFooterFont
        {
            get
            {
                return pageFooterFont;
            }
            set
            {
                pageFooterFont = value;
            }
        }


        //页边距
        public System.Drawing.Printing.Margins DevMargins
        {
            get
            {
                return devMargins;
            }
            set
            {
                devMargins = value;
            }
        }


        //纸张类型
        public System.Drawing.Printing.PaperKind DevPaperKind
        {
            get
            {
                return devPaperKind;
            }
            set
            {
                devPaperKind = value;
            }
        }
        //*********************字段*********************


        private void PageHeaderFooterSettings()
        {
            PHA.Content.Clear();
            PFA.Content.Clear();
            string[] stringsPHA = new string[] { "", "", "" };
            string[] stringsPFA = new string[] { "", "", "" };
            switch (headerLocation)
            {
                case 0: stringsPHA[0] = pageHeaderName;
                    break;
                case 1: stringsPHA[1] = pageHeaderName;
                    break;
                case 2: stringsPHA[2] = pageHeaderName;
                    break;
                default: stringsPHA[1] = pageHeaderName;
                    break;
            };
            switch (footerLocation)
            {
                case 0: stringsPFA[0] = pageFooterName;
                    break;
                case 1: stringsPFA[1] = pageFooterName;
                    break;
                case 2: stringsPFA[2] = pageFooterName;
                    break;
                default: stringsPFA[1] = pageFooterName;
                    break;
            };
            if (isPrintPage)
            {
                switch (pageLocation)
                {
                    case 0: stringsPHA[0] = stringsPHA[0] + "[Page # of Pages #]";
                        break;
                    case 1: stringsPHA[1] = stringsPHA[1] + "[Page # of Pages #]";
                        break;
                    case 2: stringsPHA[2] = stringsPHA[2] + "[Page # of Pages #]";
                        break;
                    case 3: stringsPFA[2] = stringsPFA[2] + "[Page # of Pages #]";
                        break;
                    case 4: stringsPFA[1] = stringsPFA[1] + "[Page # of Pages #]";
                        break;
                    case 5: stringsPFA[0] = stringsPFA[0] + "[Page # of Pages #]";
                        break;
                    default: stringsPFA[2] = stringsPFA[2] + "[Page # of Pages #]";
                        break;
                };
            }
            if (isPrintDate)
            {
                switch (dateLocation)
                {
                    case 0: stringsPHA[0] = stringsPHA[0] + "[Date Printed]";
                        break;
                    case 1: stringsPHA[1] = stringsPHA[1] + "[Date Printed]";
                        break;
                    case 2: stringsPHA[2] = stringsPHA[0] + "[Date Printed]";
                        break;
                    case 3: stringsPFA[2] = stringsPFA[2] + "[Date Printed]";
                        break;
                    case 4: stringsPFA[1] = stringsPFA[1] + "[Date Printed]";
                        break;
                    case 5: stringsPFA[0] = stringsPFA[0] + "[Date Printed]";
                        break;
                    default: stringsPFA[0] = stringsPFA[0] + "[Date Printed]";
                        break;
                };
            }
            PHA.Content.AddRange(stringsPHA);
            PFA.Content.AddRange(stringsPFA);
            if (pageHeaderFont != null)
            {
                try
                {
                    PHA.Font = pageHeaderFont;
                }
                catch
                {
                    PHA.Font = new Font(new System.Drawing.FontFamily("宋体"), 20, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                }
            }
            else
            {
                PHA.Font = new Font(new System.Drawing.FontFamily("宋体"), 20, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            }
            if (pageFooterFont != null)
            {
                try
                {
                    PFA.Font = pageFooterFont;
                }
                catch
                {
                    PFA.Font = new Font(new System.Drawing.FontFamily("宋体"), 9, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                }
            }
        }

        public void ShowDevPreview(DevExpress.XtraPrinting.IPrintable printComponent)
        {
            PageHeaderFooterSettings();
            PrintingSystem ps = new PrintingSystem();
            PrintableComponentLink pc = new PrintableComponentLink();
            pc.Component = printComponent;
            pc.Landscape = true;
            //是否指定页边距尺寸
            if (devMargins != null)
                pc.Margins = devMargins;
            //是否定义纸张
            if (devPaperKind != System.Drawing.Printing.PaperKind.A4)
                pc.PaperKind = devPaperKind;
            //标题和页脚的显示
            pc.PageHeaderFooter = new PageHeaderFooter(PHA, PFA);
            pc.CreateDocument(ps);
            pc.EnablePageDialog = enableEditPage;
            pc.ShowPreview();
        }
    }
}