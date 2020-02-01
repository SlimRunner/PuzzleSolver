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
            matrix_p.SetRotation(angle);
            Location = matrix_p.GetTransformation(Location);
            matrix_p.ResetMatrix();
        }

        public void Shear(float horz, float vert)
        {
            matrix_p.SetShear(horz, vert);
            Location = matrix_p.GetTransformation(Location);
            matrix_p.ResetMatrix();
        }

        public void Reflect(bool x, bool y)
        {
            matrix_p.SetScale(x ? -1 : 1, y ? -1 : 1);
            Location = matrix_p.GetTransformation(Location);
            matrix_p.ResetMatrix();
        }

        public void Transform(Matrix3 matrix)
        {
            matrix_p = new Matrix3(matrix);
            Location = matrix_p.GetTransformation(Location);
            matrix_p.ResetMatrix();
        }

        public void MoveMatrix(float x, float y)
        {
            matrix_p.AddTranslation(x, y);
        }

        public void ScaleMatrix(float w, float h)
        {
            matrix_p.AddScale(w, h);
        }

        public void RotateMatrix(float angle)
        {
            matrix_p.AddRotation(angle);
        }

        public void ShearMatrix(float horz, float vert)
        {
            matrix_p.AddShear(horz, vert);
        }

        public void ReflectMatrix(bool x, bool y)
        {
            matrix_p.AddScale(x ? -1 : 1, y ? -1 : 1);
        }

        public void SetMatrix(Matrix3 matrix)
        {
            matrix_p = new Matrix3(matrix);
        }

        public Matrix3 ApplyTransformation()
        {
            Matrix3 oldMatrix = new Matrix3(matrix_p);
            Location = matrix_p.GetTransformation(Location);
            matrix_p.ResetMatrix();
            return oldMatrix;
        }

        public void DiscardTransformation()
        {
            matrix_p.ResetMatrix();
        }

        #endregion !mutators

        #region METHODS
        public void Draw(Graphics gf)
        {
            gf.FillEllipse(brush, Location.X - Radius.X, Location.Y - Radius.Y, Radius.X * 2, Radius.Y * 2);
            gf.DrawEllipse(pen, Location.X - Radius.X, Location.Y - Radius.Y, Radius.X * 2, Radius.Y * 2);
        }

        public void DrawTransformed(Graphics gf)
        {
            Float2 transLoc = matrix_p.GetTransformation(Location);
            gf.FillEllipse(brush, transLoc.X - Radius.X, transLoc.Y - Radius.Y, Radius.X * 2, Radius.Y * 2);
            gf.DrawEllipse(pen, transLoc.X - Radius.X, transLoc.Y - Radius.Y, Radius.X * 2, Radius.Y * 2);
        }

        public bool IsPointInside(Float2 vec, bool useTransformation = true)
        {
            Float2 vector = new Float2(vec);
            Float2 boundary, transLoc;

            if (!useTransformation)
                transLoc = Location;
            else
                transLoc = matrix_p.GetTransformation(Location);

            boundary = Float2.Pow2((vector - transLoc) / Radius);
            return boundary.X + boundary.Y <= 1;
        }

        public bool IsPointInside(Point vec, bool useTransformation = false)
        {
            Float2 vector = new Float2(vec.X, vec.Y);
            Float2 boundary, transLoc;

            if (!useTransformation)
                transLoc = Location;
            else
                transLoc = matrix_p.GetTransformation(Location);

            boundary = Float2.Pow2((vector - transLoc) / Radius);
            return boundary.X + boundary.Y <= 1;
        }

        public bool IsPointInside(float x, float y, bool useTransformation = true)
        {
            //https://math.stackexchange.com/questions/76457/check-if-a-point-is-within-an-ellipse

            Float2 vector = new Float2(x, y);
            Float2 boundary, transLoc;

            if (!useTransformation)
                transLoc = Location;
            else
                transLoc = matrix_p.GetTransformation(Location);

            boundary = Float2.Pow2((vector - transLoc) / Radius);
            return boundary.X + boundary.Y <= 1;
        }

        #endregion !methods
    }
}
