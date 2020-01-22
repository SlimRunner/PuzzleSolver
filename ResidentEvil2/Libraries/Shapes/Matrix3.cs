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
            for (int i = 0; i < MATRIX_SIZE; ++i)
            {
                for (int j = 0; j < MATRIX_SIZE; ++j)
                {
                    mtx[i, j] = other[i, j];
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

        public float this[int col, int row]
        {
            get
            {
                return mtx[col, row];
            }
        }

        /// <summary>
        /// Sets the last row of indices to a valid affine transformation
        /// </summary>
        public void MakeAffine()
        {
            mtx[2, 0] = 0;
            mtx[2, 1] = 0;
            mtx[2, 2] = 1;
        }

        /// <summary>
        /// Resets the matrix back to a flat affine matrix (no change)
        /// </summary>
        public void ResetMatrix()
        {
            for (int i = 0; i < MATRIX_SIZE; ++i)
            {
                for (int j = 0; j < MATRIX_SIZE; ++j)
                {
                    mtx[i, j] = i == j ? 1 : 0;
                }
            }

            //not needed
            //MakeAffine();
        }

        /// <summary>
        /// Clears current transformations 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
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
        /// Applies a translation to the matrix. This function assumes your matrix is affine.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ApplyTranslation(float x, float y)
        {
            mtx[0, 2] += x;
            mtx[1, 2] += y;
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

        public Float2 GetTransformation(Float2 vector)
        {
            Float2 retval = new Float2(
                mtx[0, 0] * vector.X + mtx[0, 1] * vector.Y + mtx[0, 2],
                mtx[1, 0] * vector.X + mtx[1, 1] * vector.Y + mtx[1, 2]);

            return retval;
        }
    }
}
