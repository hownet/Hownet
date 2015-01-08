using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

/// <summary>
/// Use this for drawing custom graphics and text with transparency.
/// Inherit from DrawingArea and override the OnDraw method.
/// </summary>
namespace Hownet.BaseContranl
{
    abstract public class DrawingArea : Panel
    {
        /// <summary>
        /// Drawing surface where graphics should be drawn.
        /// Use this member in the OnDraw method.
        /// </summary>
        protected Graphics graphics;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT 
                return cp;
            }
        }

        public DrawingArea()
        {
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.graphics = e.Graphics;
            this.graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            OnDraw();
        }

        /// <summary>
        /// Override this method in subclasses for drawing purposes.
        /// </summary>
        abstract protected void OnDraw();

        #region DrawText helper functions
        protected void DrawText(string StrText)
        {
            try
            {
                DrawingArea.DrawText(this.graphics, StrText);
            }
            catch //(Exception ex)
            {
            }
        }

        static public void DrawText(Graphics graphics, string StrText)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Font font = new Font("ËÎÌו", 14f, FontStyle.Bold);
            PointF rotatePoint = new PointF(50, 30);
            SizeF size = graphics.MeasureString(StrText, font);
            Matrix myMatrix = new Matrix();
            myMatrix.RotateAt(30, rotatePoint, MatrixOrder.Append);
            graphics.Transform = myMatrix;
            graphics.DrawString(StrText, font, new SolidBrush(Color.Red), rotatePoint.X - size.Width / 2, rotatePoint.Y - size.Height / 2);
            myMatrix = new Matrix();
            graphics.Transform = myMatrix;

            Pen p1 = new Pen(Color.Red, 2);
            p1.LineJoin = LineJoin.Miter;
            graphics.DrawLine(p1, new Point(20, 0), new Point(95, 42));
            graphics.DrawLine(p1, new Point(8, 20), new Point(20, 0));
            graphics.DrawLine(p1, new Point(8, 20), new Point(83, 62));
            graphics.DrawLine(p1, new Point(95, 42), new Point(83, 62));
        }
        #endregion
    }
}