using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hownet.BaseContranl
{
    public partial class ucMenu : DevExpress.XtraEditors.XtraUserControl
    {
        public ucMenu()
        {
            InitializeComponent();
        }
        public delegate void ClickButtonHandler(BasicClass.cResult cr, string MenuTag);
        public event ClickButtonHandler ClickButtonChanged;
        BasicClass.cResult r = new BasicClass.cResult();


        /// <summary>
        /// 是否显示首单
        /// </summary>
        public bool IsShowFrist
        {
            set
            {
                _barFrist.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示上一单菜单
        /// </summary>
        public bool IsShowPrv
        {
            set
            {
                _barPrv.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示下一菜单
        /// </summary>
        public bool IsShowNext
        {
            set
            {
                _barNext.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示最后一个菜单
        /// </summary>
        public bool IsShowLast
        {
            set
            {
                _barLast.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示新建菜单
        /// </summary>
        public bool IsShowAddNew
        {
            set{

                _barNew.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示编辑菜单
        /// </summary>
        public bool IsShowEdit
        {
            set
            {
                _barEdit.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示删除菜单
        /// </summary>
        public bool IsShowDel
        {
            set
            {
                _barDel.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示删除明细菜单
        /// </summary>
        public bool IsShowDelInfo
        {
            set
            {
                _barDelInfo.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示删除单据菜单
        /// </summary>
        public bool IsShowDelTable
        {
            set
            {
                _barDelTable.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示保存菜单
        /// </summary>
        public bool IsShowSave
        {
            set
            {
                _barSave.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示审核菜单
        /// </summary>
        public bool IsShowVerify
        {
            set
            {
                _barVerify.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示弃审菜单
        /// </summary>
        public bool IsShowUnVerify
        {
            set
            {
                _barUnVerify.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示打印菜单
        /// </summary>
        public bool IsShowPrint
        {
            set
            {
                _barPrint.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示导出Excel菜单
        /// </summary>
        public bool IsShowExcel
        {
            set
            {
                _barExcel.Enabled = value;
            }
        }

        /// <summary>
        /// 是否显示帮助菜单
        /// </summary>
        public bool IsShowHelp
        {
            set
            {
                _barHelp.Enabled = value;
            }
        }
        private void ClickButton(BasicClass.cResult cr, string MenuTag)
        {
            if (ClickButtonChanged != null)
                ClickButtonChanged(cr, MenuTag);
        }
        private void _barFrist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ClickButton(r, e.Item.Tag.ToString());
        }
    }
}
