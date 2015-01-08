using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseContranl
{
   public class BaseFormClass
    {
       public static string ShowSaveFileDialog(string title, string filter,string fileName)
       {
           SaveFileDialog dlg = new SaveFileDialog();
           string name = fileName;
           int n = name.LastIndexOf(".") + 1;
           if (n > 0) name = name.Substring(n, name.Length - n);
           dlg.Title = "导出" + title;
           dlg.FileName = name;
           dlg.Filter = filter;
           if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
           return "";
       }
       public static string ShowSaveFileDialog(string title, string filter)
       {
           SaveFileDialog dlg = new SaveFileDialog();
           string name = Application.ProductName;
           int n = name.LastIndexOf(".") + 1;
           if (n > 0) name = name.Substring(n, name.Length - n);
           dlg.Title = "导出" + title;
           dlg.FileName = name;
           dlg.Filter = filter;
           if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
           return "";
       }
        public static void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("是否打开刚才导出的文档？", "导出Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( "未找到导出的文档", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
