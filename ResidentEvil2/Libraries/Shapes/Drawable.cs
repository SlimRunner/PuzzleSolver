using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

namespace ResidentEvil2.Libraries.Shapes
{
    class Drawable
    {
        public Pen pen { get; set; }
        public Brush brush { get; set; }
        public float wer;

        public Drawable()
        {
            pen = Pens.Black;
            brush = Brushes.White;
        }

        public Drawable(Pen p, Brush b)
        {
            pen = p;
            brush = b;
        }
    }
}
