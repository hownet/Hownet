using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hownet
{
    public class OpenFormClass
    {
        public static bool CheckFormIsOpen(string formName)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == formName.Substring(formName.LastIndexOf('.') + 1))
                {
                    f.Visible = true;
                    f.Activate();
                    return true;
                }
            }
            return false;
        }
    }
}
