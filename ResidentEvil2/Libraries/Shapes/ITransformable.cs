using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidentEvil2.Libraries.Shapes
{
    interface ITransformable
    {
        /*
         * https://en.wikipedia.org/wiki/Transformation_matrix#/media/File:2D_affine_transformation_matrix.svg
         */

        void Move(float x, float y);
        void Scale(float w, float h);
        void Rotate(float angle);
        void Shear(float horz, float vert);
        void Reflect(bool x, bool y);
        void Transform(Matrix3 matrix);
        void SetMatrix();
        Matrix3 ApplyMatrix();
    }
}
