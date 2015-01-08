using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Hownet.BaseForm
{
    /// <summary>
    /// 进度窗体。
    /// </summary>
    public partial class FrmWaitDialog : Form
    {
        /// <summary>
        /// 创建一个进度窗体。
        /// </summary>
        /// <param name="caption">进度信息。</param>
        /// <param name="tile">标题信息。</param>
        public FrmWaitDialog(string caption,string tile)
        {
            InitializeComponent();
            this.labelCaption.Text = caption;
            if (tile != null && tile != string.Empty)
            {
                this.labelTitle.Text=tile;
            }
            this.TopMost = true;
            this.Show();
            this.Refresh();
        }

        /// <summary>
        /// 创建一个进度窗体。
        /// </summary>
        /// <param name="caption">进度信息。</param>
        public FrmWaitDialog(string caption)
            : this(caption, string.Empty)
        {
        }

        /// <summary>
        /// 进度提示信息。
        /// </summary>
        public string Caption
        {
            get { return this.labelCaption.Text; }
            set 
            { 
                this.labelCaption.Text = value;
                this.Refresh();
            }
        }

        private void FrmWaitDialog_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            LinearGradientBrush myBrush = new LinearGradientBrush(this.ClientRectangle, Color.White, Color.Pink, LinearGradientMode.Vertical);
            g.FillRectangle(myBrush, this.ClientRectangle);
        }
    }
}
