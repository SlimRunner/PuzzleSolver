using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidentEvil2.Libraries.Shapes
{
    class Circle : Drawable, ITransformable
    {
        #region MEMBER DATA
        public Float2 Location { get; set; }
        public Float2 Radius { get; set; }

        private Matrix3 matrix_p;

        #endregion !member data

        #region CONSTRUCTORS
        public Circle()
        {
            Location = new Float2();
            Radius = new Float2();
            matrix_p = new Matrix3();
        }

        #endregion !constructors

        #region MUTATORS
        public void Move(float x, float y)
        {
            matrix_p.SetTranslation(x, y);
            Location = matrix_p.GetTransformation(Location);
            matrix_p.ResetMatrix();
        }

        public void Scale(float w, float h)
        {
            matrix_p.SetScale(w, h);
            Location = matrix_p.GetTransformation(Location);
            matrix_p.ResetMatrix();
        }

        public void Rotate(float angle)
        {
            throw new NotImplementedException();
        }

        public void Shear(float horz, float vert)
        {
            throw new NotImplementedException();
        }

        public void Reflect(bool x, bool y)
        {
            throw new NotImplementedException();
        }

        public void Transform(Matrix3 matrix)
        {
            matrix_p = new Matrix3(matrix);
        }

        public Matrix3 SetMatrix()
        {
            throw new NotImplementedException();
        }

        public Matrix3 ApplyMatrix()
        {
            throw new NotImplementedException();
        }

        #endregion !mutators

        #region METHODS
        public void Draw(PaintEventArgs e)
        {
            e.Graphics.FillEllipse(brush, Location.X - Radius.X, Location.Y - Radius.Y, Radius.X * 2, Radius.Y * 2);
            e.Graphics.DrawEllipse(pen, Location.X - Radius.X, Location.Y - Radius.Y, Radius.X * 2, Radius.Y * 2);
        }

        public void DrawTransformed(PaintEventArgs e)
        {
            Float2 point1 = new Float2(Location.X - Radius.X, Location.Y - Radius.Y), point2 = new Float2(Radius.X * 2, Radius.Y * 2);

            matrix_p.Transform(ref point1);
            matrix_p.Transform(ref point2);

            e.Graphics.FillEllipse(brush, point1.X, point1.Y, point2.X, point2.Y);
            e.Graphics.DrawEllipse(pen, point1.X, point1.Y, point2.X, point2.Y);
        }

        public bool IsPointInside(Float2 vec, bool useTransformation = true)
        {
            Float2 vector = useTransformation ? matrix_p.GetTransformation(vec) : new Float2(vec);

            return Float2.GetLength(vector, Location) <= Radius.Magnitude;
        }

        #endregion !methods
    }
}
