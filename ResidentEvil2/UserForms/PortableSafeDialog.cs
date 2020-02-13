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
        private Float2 ringScalar;
        private float nodeRadius;

        private Circle[] node_grid;
        private Float2 gridScalar;

        public PortableSafeDialog()
        {
            InitializeComponent();

            nodeRadius = 10;
            //ringScalar = Math.Min(CanvasSafeUpper.Width, CanvasSafeUpper.Height) / 2 - nodeRadius * 2;
            ringScalar = 
                new Float2(
                    Math.Max(CanvasSafeUpper.Width / 2 - nodeRadius * 2, 0),
                    Math.Max(CanvasSafeUpper.Height / 2 - nodeRadius * 2, 0));
            node_ring = GetRing(NODECOUNT, 1, (float)Math.PI / NODECOUNT);

            gridScalar = new Float2(CanvasSafeUpper.Width / 2, CanvasSafeUpper.Height / 2);
            node_grid = GetGrid(NODECOUNT, 2, 1);
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
            ringScalar.SetRect(
                Math.Max(CanvasSafeUpper.Width / 2 - nodeRadius * 2, 0),
                Math.Max(CanvasSafeUpper.Height / 2 - nodeRadius * 2, 0));
            gridScalar.SetRect(CanvasSafeUpper.Width / 2, CanvasSafeUpper.Height / 2);

            Matrix3 ringTrans = new Matrix3();
            ringTrans.AddTranslation(CanvasSafeUpper.Width / 2, CanvasSafeUpper.Height / 2);
            ringTrans.AddScale(ringScalar.X, ringScalar.Y);

            foreach (Circle shape in node_ring)
            {
                shape.DiscardTransformation();
                shape.SetMatrix(ringTrans);
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

        private Circle[] GetRing(uint nodeCount, float radius, float rotation)
        {
            const double DOU_PI = Math.PI * 2;

            Circle[] shapes = new Circle[nodeCount];
            Matrix3 shapeTrans = new Matrix3();
            shapeTrans.AddTranslation(CanvasSafeUpper.Width / 2, CanvasSafeUpper.Height / 2);
            shapeTrans.AddScale(ringScalar.X, ringScalar.Y);

            for (int i = 0; i < nodeCount; ++i)
            {
                shapes[i] = new Circle
                {
                    Radius = new Float2(nodeRadius, nodeRadius)
                };
                shapes[i].Location.X = (float)Math.Cos((double)i / nodeCount * DOU_PI + rotation) * radius;
                shapes[i].Location.Y = (float)Math.Sin((double)i / nodeCount * DOU_PI + rotation) * radius;
                shapes[i].SetMatrix(shapeTrans);
            }

            return shapes;
        }

        private Circle[] GetGrid(uint nodeCount, int cols, float size)
        {
            if (cols < 0)
                throw new ArgumentException("cols cannot be negative");

            Circle[] shapes = new Circle[nodeCount];
            int rowMax = ((int)nodeCount - 1) / cols;
            int colMax = cols - 1;

            Matrix3 shapeTrans = new Matrix3();
            shapeTrans.AddTranslation(CanvasSafeUpper.Width / 2, CanvasSafeUpper.Height / 2);
            shapeTrans.AddScale(gridScalar.X, gridScalar.Y);

            size *= 2;

            for (int i = 0; i < nodeCount; ++i)
            {
                shapes[i] = new Circle
                {
                    Radius = new Float2(nodeRadius, nodeRadius)
                };
                shapes[i].Location.X = (float)(i % cols) / colMax * size - 1;
                shapes[i].Location.Y = (float)(i / cols) / rowMax * size - 1;
                shapes[i].SetMatrix(shapeTrans);
            }

            return shapes;
        }

        #endregion !drawing

        
    }


}
