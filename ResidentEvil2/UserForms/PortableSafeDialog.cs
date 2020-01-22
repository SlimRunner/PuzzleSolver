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

        private PointF[] psafe_nodes_ring;
        private float ringScalar;

        private PointF[] psafe_nodes_grid;
        private float gridScalar;

        private Circle[] node_ring;

        public PortableSafeDialog()
        {
            InitializeComponent();

            psafe_nodes_ring = GetRegularPolygon(NODECOUNT, 1, (float)Math.PI / NODECOUNT);
            ringScalar = Math.Min(CanvasSafeUpper.Width, CanvasSafeUpper.Height);
            node_ring = GetRing(NODECOUNT, 1, (float)Math.PI / NODECOUNT);
        }

        #region EVENTS
        private void CanvasSafeUpper_Paint(object sender, PaintEventArgs e)
        {
            DrawOutputRing(e);
        }

        private void CanvasSafeUpper_Resize(object sender, EventArgs e)
        {
            ringScalar = Math.Min(CanvasSafeUpper.Width, CanvasSafeUpper.Height);
        }

        #endregion !events

        #region DRAWING
        private void DrawOutputRing(PaintEventArgs e)
        {
            PointF clCenter = new PointF(CanvasSafeUpper.Width / 2, CanvasSafeUpper.Height / 2);

            //e.Graphics.FillRectangle(Brushes.LightGray, e.Graphics.ClipBounds);
            /*foreach (PointF vertex in psafe_nodes_ring)
            {
                e.Graphics.DrawEllipse(Pens.Black, clCenter.X + vertex.X * clCenter.X - 6, clCenter.Y + vertex.Y * clCenter.Y - 6, 12, 12);
            }*/
            foreach (Circle shape in node_ring)
            {
                shape.Draw(e);
            }
        }

        private PointF[] GetRegularPolygon(uint sides, float radius, float rotation)
        {
            PointF[] nodes = new PointF[NODECOUNT];
            const double DOU_PI = Math.PI * 2;

            for (int i = 0; i < NODECOUNT; ++i)
            {
                nodes[i].X = (float)Math.Cos((double)i / NODECOUNT * DOU_PI + rotation) * radius;
                nodes[i].Y = (float)Math.Sin((double)i / NODECOUNT * DOU_PI + rotation) * radius;
            }

            return nodes;
        }

        private Circle[] GetRing(uint sides, float radius, float rotation)
        {
            Circle[] shapes = new Circle[NODECOUNT];

            const double DOU_PI = Math.PI * 2;

            for (int i = 0; i < NODECOUNT; ++i)
            {
                shapes[i] = new Circle
                {
                    Radius = new Float2(5, 5)
                };
                shapes[i].Location.X = (float)Math.Cos((double)i / NODECOUNT * DOU_PI + rotation) * radius;
                shapes[i].Location.Y = (float)Math.Sin((double)i / NODECOUNT * DOU_PI + rotation) * radius;
                shapes[i].Scale(20, 20);
                shapes[i].Move(150, 150);
            }

            return shapes;
        }

        private PointF[] GetPaddedGrid()
        {
            return null;
        }

        #endregion !drawing

    }


}
