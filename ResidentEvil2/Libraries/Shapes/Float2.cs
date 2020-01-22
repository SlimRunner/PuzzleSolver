using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResidentEvil2.Libraries.Shapes
{
    class Float2
    {
        public enum CoordSys
        {
            CS_RECTANGULAR = 0,
            CS_POLAR = 1
        }

        #region PROPERTIES
        public float X { get; set; }
        public float Y { get; set; }

        public float Magnitude => (float)Math.Sqrt(X * X + Y * Y);
        public float Angle
            => (Y + X != 0 ? (float)Math.Atan2(Y, X) : throw new Exception("undefined")); //NOTE: consider throwing error when both x and y are 0

        #endregion !properties

        #region CONSTRUCTORS
        /// <summary>
        /// Initializes the vector to a zero-vector.
        /// </summary>
        public Float2()
        {
            X = 0;
            Y = 0;
        }

        public Float2(Float2 other)
        {
            X = other.X;
            Y = other.Y;
        }

        /// <summary>
        /// Initializes the indices of a 2-vector with rectangular or polar values.
        /// </summary>
        /// <param name="x">The first index of the vector, or the magnitude when using polar values.</param>
        /// <param name="y">The second index of the vector, or the angle when using polar values.</param>
        /// <param name="valType">Determines the type of values the initialization process will use to create the vector.</param>
        public Float2(float x, float y, CoordSys valType = CoordSys.CS_RECTANGULAR)
        {
            switch (valType)
            {
                case CoordSys.CS_RECTANGULAR:
                    X = x;
                    Y = y;
                    break;
                case CoordSys.CS_POLAR:
                    X = x * (float)Math.Cos(y);
                    Y = x * (float)Math.Sin(y);
                    break;
                default:
                    break;
            }
        }

        #endregion !constructors

        #region MUTATORS
        public void SetRect(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void SetPolar(float radius, float theta)
        {
            X = radius * (float)Math.Cos(theta);
            Y = radius * (float)Math.Sin(theta);
        }

        #endregion !mutators

        #region ACCESSORS
        //Nothing yet

        #endregion !accessors

        #region OPERATORS
        public static Float2 operator +(Float2 lhs)
            => new Float2(lhs);

        public static Float2 operator -(Float2 lhs)
            => new Float2(-lhs.X, -lhs.Y);

        public static Float2 operator +(Float2 lhs, Float2 rhs)
            => new Float2(lhs.X + rhs.X, lhs.Y + rhs.Y);

        public static Float2 operator -(Float2 lhs, Float2 rhs)
            => new Float2(lhs.X - rhs.X, lhs.Y - rhs.Y);

        public static Float2 operator *(Float2 lhs, Float2 rhs)
            => new Float2(lhs.X * rhs.X, lhs.Y * rhs.Y);

        public static Float2 operator /(Float2 lhs, Float2 rhs)
            => new Float2(lhs.X / rhs.X, lhs.Y / rhs.Y);

        #endregion !operators

        public static float GetLength(Float2 vec1, Float2 vec2)
        {
            return (vec1 - vec2).Magnitude;
        }
    }
}
