using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Hownet.BaseContranl
{
  public  class BroculosDrawing : DrawingArea
    {
        private string _str = "“—…Û∫À ";
        public string StrText
        {
            set
            {
                _str = value;
                OnDraw();
            }
        }
        protected override void OnDraw()
        {
            Point textPosition = new Point(40, 20);
            DrawText(_str);
        }
    }
}
