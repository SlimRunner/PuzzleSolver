using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidentEvil2.Libraries.Shapes
{
    class Matrix3
    {
        const int MATRIX_SIZE = 3;
        private float[,] mtx;

        #region CONSTRUCTORS
        /// <summary>
        /// Initializes the matrix to a flat matrix (no change).
        /// </summary>
        public Matrix3()
        {
            mtx = new float[MATRIX_SIZE, MATRIX_SIZE];
            ResetMatrix();
        }

        public Matrix3(Matrix3 other)
        {
            mtx = new float[MATRIX_SIZE, MATRIX_SIZE];

            for (int row = 0; row < MATRIX_SIZE; ++row)
            {
                for (int col = 0; col < MATRIX_SIZE; ++col)
                {
                    mtx[row, col] = other[row, col];
                }
            }
        }

        public Matrix3(float[,] indices)
        {
            mtx = new float[MATRIX_SIZE, MATRIX_SIZE];
            SetMatrix(indices);
        }

        public Matrix3(params float[] indices)
        {
            mtx = new float[MATRIX_SIZE, MATRIX_SIZE];
            SetMatrix(indices);
        }

        #endregion !constructors

        #region MUTATORS
        /// <summary>
        /// Resets the matrix back to its default state (no change)
        /// </summary>
        public void ResetMatrix()
        {
            for (int row = 0; row < MATRIX_SIZE; ++row)
            {
                for (int col = 0; col < MATRIX_SIZE; ++col)
                {
                    mtx[row, col] = row == col ? 1 : 0;
                }
            }
        }

        /// <summary>
        /// Sets the last row of indices so that the matrix represents a homogeneous coordinate system.
        /// </summary>
        public void MakeAffine()
        {
            mtx[2, 0] = 0;
            mtx[2, 1] = 0;
            mtx[2, 2] = 1;
        }

        /// <summary>
        /// Resets matrix and sets it according to a translation given.
        /// </summary>
        /// <param name="x">Value that represents a translation in the x axis.</param>
        /// <param name="y">Value that represents a translation in the y axis.</param>
        public void SetTranslation(float x, float y)
        {
            ResetMatrix();
            mtx[0, 2] = x;
            mtx[1, 2] = y;
        }

        public void SetScale(float xScale, float yScale)
        {
            ResetMatrix();
            mtx[0, 0] = xScale;
            mtx[0, 1] = 0;
            mtx[1, 0] = 0;
            mtx[1, 1] = yScale;
        }

        public void SetRotation(float angle)
        {
            ResetMatrix();
            mtx[0, 0] = (float)Math.Cos(angle);
            mtx[0, 1] = (float)Math.Sin(angle);
            mtx[1, 0] = (float)-Math.Sin(angle);
            mtx[1, 1] = (float)Math.Cos(angle);
        }

        public void SetShear(float xAngle, float yAngle)
        {
            ResetMatrix();
            mtx[0, 0] = 1;
            mtx[0, 1] = (float)Math.Tan(xAngle);
            mtx[1, 0] = (float)Math.Tan(yAngle);
            mtx[1, 1] = 1;
        }

        /// <summary>
        /// Applies a translation to the matrix.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddTranslation(float x, float y)
        {
            Matrix3 trans = new Matrix3();
            trans[0, 2] = x;
            trans[1, 2] = y;

            MultiplyRTL(trans);
        }

        public void AddScale(float xScale, float yScale)
        {
            Matrix3 scale = new Matrix3();
            scale[0, 0] = xScale;
            scale[0, 1] = 0;
            scale[1, 0] = 0;
            scale[1, 1] = yScale;

            MultiplyRTL(scale);
        }

        public void AddRotation(float angle)
        {
            /* Matrix multiplication
             * a  b  c       p  q  r       ap+dq  as+dt  
             * d  e  f   *   s  t  u   =   bp+eq  bs+et  
             * g  h  i       v  w  x           
             */

            Matrix3 rot = new Matrix3();
            rot[0, 0] = (float)Math.Cos(angle);
            rot[0, 1] = (float)Math.Sin(angle);
            rot[1, 0] = (float)-Math.Sin(angle);
            rot[1, 1] = (float)Math.Cos(angle);

            MultiplyRTL(rot);
        }

        public void AddShear(float xAngle, float yAngle)
        {
            Matrix3 shear = new Matrix3();
            shear[0, 0] = 1;
            shear[0, 1] = (float)Math.Tan(xAngle);
            shear[1, 0] = (float)Math.Tan(yAngle);
            shear[1, 1] = 1;

            MultiplyRTL(shear);
        }

        public void SetMatrix(float[,] indices)
        {
            if (indices.Rank != 2)
                throw new ArgumentException("The number of dimensions used to initialize Matrix3 is not correct", "indices");
            if (indices.GetLength(0) != 3 && indices.GetLength(1) != 3)
                throw new ArgumentException("The array passed is not 3x3", "indices");

            mtx[0, 0] = indices[0, 0];
            mtx[0, 1] = indices[0, 1];
            mtx[0, 2] = indices[0, 2];

            mtx[1, 0] = indices[1, 0];
            mtx[1, 1] = indices[1, 1];
            mtx[1, 2] = indices[1, 2];

            mtx[2, 0] = indices[2, 0];
            mtx[2, 1] = indices[2, 1];
            mtx[2, 2] = indices[2, 2];
        }

        public void SetMatrix(params float[] indices)
        {
            if (indices.GetLength(0) != 9)
                throw new ArgumentException("The param list passed doesn't match the size of Matrix3", "indices");

            /* Index indentification
             * 0  1  2
             * 3  4  5
             * 6  7  8
             */

            mtx[0, 0] = indices[0];
            mtx[0, 1] = indices[1];
            mtx[0, 2] = indices[2];

            mtx[1, 0] = indices[3];
            mtx[1, 1] = indices[4];
            mtx[1, 2] = indices[5];

            mtx[2, 0] = indices[6];
            mtx[2, 1] = indices[7];
            mtx[2, 2] = indices[8];
        }

        public void Transform(ref Float2 vector)
        {
            vector.X = mtx[0, 0] * vector.X + mtx[0, 1] * vector.Y + mtx[0, 2];
            vector.Y = mtx[1, 0] * vector.X + mtx[1, 1] * vector.Y + mtx[1, 2];
        }

        private void MultiplyRTL(Matrix3 lhs)
        {
            /* Matrix multiplication (Applies from right to left)
             * A = A*B
             *    A             B
             * a  b  c       p  q  r       ap+dq+gr  as+dt+gu  av+dw+gx
             * d  e  f   *   s  t  u   =   bp+eq+hr  bs+et+hu  bv+ew+hx
             * g  h  i       v  w  x       cp+fq+ir  cs+ft+iu  cv+fw+ix
             * 
             * 
             * A = B*A
             *    B             A
             * p  q  r       a  b  c       ap+dq+gr  bp+eq+hr  cp+fq+ir
             * s  t  u   *   d  e  f   =   as+dt+gu  bs+et+hu  cs+ft+iu
             * v  w  x       g  h  i       av+dw+gx  bv+ew+hx  cv+fw+ix
             * 
             * https://matrixcalc.org/en/
             */

            mtx[0, 0] = mtx[0, 0] * lhs[0, 0] + mtx[1, 0] * lhs[0, 1] + mtx[2, 0] * lhs[0, 2]; //  x  x  x
            mtx[0, 1] = mtx[0, 1] * lhs[0, 0] + mtx[1, 1] * lhs[0, 1] + mtx[2, 1] * lhs[0, 2]; //  .  .  .
            mtx[0, 2] = mtx[0, 2] * lhs[0, 0] + mtx[1, 2] * lhs[0, 1] + mtx[2, 2] * lhs[0, 2]; //  .  .  .

            mtx[1, 0] = mtx[0, 0] * lhs[1, 0] + mtx[1, 0] * lhs[1, 1] + mtx[2, 0] * lhs[1, 2]; //  .  .  .
            mtx[1, 1] = mtx[0, 1] * lhs[1, 0] + mtx[1, 1] * lhs[1, 1] + mtx[2, 1] * lhs[1, 2]; //  x  x  x
            mtx[1, 2] = mtx[0, 2] * lhs[1, 0] + mtx[1, 2] * lhs[1, 1] + mtx[2, 2] * lhs[1, 2]; //  .  .  .

            mtx[2, 0] = mtx[0, 0] * lhs[2, 0] + mtx[1, 0] * lhs[2, 1] + mtx[2, 0] * lhs[2, 2]; //  .  .  .
            mtx[2, 1] = mtx[0, 1] * lhs[2, 0] + mtx[1, 1] * lhs[2, 1] + mtx[2, 1] * lhs[2, 2]; //  .  .  .
            mtx[2, 2] = mtx[0, 2] * lhs[2, 0] + mtx[1, 2] * lhs[2, 1] + mtx[2, 2] * lhs[2, 2]; //  x  x  x
        }

        #endregion !mutators

        #region ACCESSORS
        public bool IsAffine()
        {
            return mtx[2, 0] == 0 && mtx[2, 1] == 0 && mtx[2, 2] == 1;
        }

        public Float2 GetTransformation(Float2 vector)
        {
            Float2 retval = new Float2(
                mtx[0, 0] * vector.X + mtx[0, 1] * vector.Y + mtx[0, 2],
                mtx[1, 0] * vector.X + mtx[1, 1] * vector.Y + mtx[1, 2]);

            return retval;
        }

        #endregion accessors

        #region OPERATORS
        public float this[int row, int col]
        {
            get
            {
                return mtx[row, col];
            }

            private set
            {
                mtx[row, col] = value;
            }
        }

        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {
            Matrix3 retval = new Matrix3();

            /* Matrix multiplication
             * a  b  c       p  q  r       ap+dq+gr  as+dt+gu  av+dw+gx
             * d  e  f   *   s  t  u   =   bp+eq+hr  bs+et+hu  bv+ew+hx
             * g  h  i       v  w  x       cp+fq+ir  cs+ft+iu  cv+fw+ix
             */

            retval[0, 0] = lhs[0, 0] * rhs[0, 0] + lhs[1, 0] * rhs[0, 1] + lhs[2, 0] * rhs[0, 2]; //  x  x  x
            retval[0, 1] = lhs[0, 0] * rhs[1, 0] + lhs[1, 0] * rhs[1, 1] + lhs[2, 0] * rhs[1, 2]; //  .  .  .
            retval[0, 2] = lhs[0, 0] * rhs[2, 0] + lhs[1, 0] * rhs[2, 1] + lhs[2, 0] * rhs[2, 2]; //  .  .  .

            retval[1, 0] = lhs[0, 1] * rhs[0, 0] + lhs[1, 1] * rhs[0, 1] + lhs[2, 1] * rhs[0, 2]; //  .  .  .
            retval[1, 1] = lhs[0, 1] * rhs[1, 0] + lhs[1, 1] * rhs[1, 1] + lhs[2, 1] * rhs[1, 2]; //  x  x  x
            retval[1, 2] = lhs[0, 1] * rhs[2, 0] + lhs[1, 1] * rhs[2, 1] + lhs[2, 1] * rhs[2, 2]; //  .  .  .

            retval[2, 0] = lhs[0, 2] * rhs[0, 0] + lhs[1, 2] * rhs[0, 1] + lhs[2, 2] * rhs[0, 2]; //  .  .  .
            retval[2, 1] = lhs[0, 2] * rhs[1, 0] + lhs[1, 2] * rhs[1, 1] + lhs[2, 2] * rhs[1, 2]; //  .  .  .
            retval[2, 2] = lhs[0, 2] * rhs[2, 0] + lhs[1, 2] * rhs[2, 1] + lhs[2, 2] * rhs[2, 2]; //  x  x  x

            return retval;
        }

        #endregion !operators

    }
}
