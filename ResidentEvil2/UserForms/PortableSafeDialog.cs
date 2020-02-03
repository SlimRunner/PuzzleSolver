using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ResidentEvil2.Libraries.Shapes;

namespace ResidentEvil2.UserForms
{
    public partial class PortableSafeDialog : Form
    {
        private const int NODECOUNT = 8;

        private Circle[] node_ring;
        private float ringScalar;
        private float nodeRadius;

        public PortableSafeDialog()
        {
            InitializeComponent();

            nodeRadius = 10;
            ringScalar = Math.Min(CanvasSafeUpper.Width, CanvasSafeUpper.Height) / 2 - nodeRadius * 2;
            node_ring = GetRing(NODECOUNT, 1, (float)Math.PI / NODECOUNT);
        }

        #region EVENTS
        private void CanvasSafeUpper_Paint(object sender, PaintEventArgs e)
        {
            DrawOutputRing(e);
        }

        private void CanvasSafeUpper_MouseMove(object sender, MouseEventArgs e)
        {
            bool imageChanged = false;

            for (int i = 0; i < NODECOUNT; ++i)
            {
                if (node_ring[i].IsPointInside(e.Location, true))
                {
                    if (node_ring[i].brush != Brushes.Red)
                    {
                        node_ring[i].brush = Brushes.Red;
                        imageChanged = true;
                    }
                }
                else
                {
                    if (node_ring[i].brush != Brushes.White)
                    {
                        node_ring[i].brush = Brushes.White;
                        imageChanged = true;
                    }
                }
            }

            if (imageChanged) CanvasSafeUpper.Invalidate();
        }

        private void CanvasSafeUpper_Resize(object sender, EventArgs e)
        {
            ringScalar = Math.Max(Math.Min(CanvasSafeUpper.Width, CanvasSafeUpper.Height) / 2 - nodeRadius * 2, 0);
            foreach (Circle shape in node_ring)
            {
                shape.DiscardTransformation();
                shape.MoveMatrix(CanvasSafeUpper.Width / 2, CanvasSafeUpper.Height / 2);
                shape.ScaleMatrix(ringScalar, ringScalar);
            }
        }

        #endregion !events

        #region DRAWING
        private void DrawOutputRing(PaintEventArgs e)
        {
            Bitmap backbuffer = new Bitmap(CanvasSafeUpper.Width, CanvasSafeUpper.Height);
            Graphics gf = Graphics.FromImage(backbuffer);
            
            foreach (Circle shape in node_ring)
            {
                shape.DrawTransformed(gf);
            }

            e.Graphics.DrawImage(backbuffer, 0, 0);

            gf.Dispose();
            backbuffer.Dispose();
        }

        private Circle[] GetRing(uint sides, float radius, float rotation)
        {
            Circle[] shapes = new Circle[sides];

            const double DOU_PI = Math.PI * 2;

            for (int i = 0; i < sides; ++i)
            {
                shapes[i] = new Circle
                {
                    Radius = new Float2(nodeRadius, nodeRadius)
                };
                shapes[i].Location.X = (float)Math.Cos((double)i / sides * DOU_PI + rotation) * radius;
                shapes[i].Location.Y = (float)Math.Sin((double)i / sides * DOU_PI + rotation) * radius;
                //shapes[i].Scale(ringScalar, ringScalar);
                //shapes[i].Move(CanvasSafeUpper.Width / 2, CanvasSafeUpper.Height / 2);
                shapes[i].MoveMatrix(CanvasSafeUpper.Width / 2, CanvasSafeUpper.Height / 2);
                shapes[i].ScaleMatrix(ringScalar, ringScalar);
            }

            return shapes;
        }

        #endregion !drawing

        
    }


}
