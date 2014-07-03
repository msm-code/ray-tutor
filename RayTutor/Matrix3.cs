using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTutor
{
    class Matrix3
    {
        double[,] matrix;

        public Matrix3(double[,] matrix)
        {
            this.matrix = matrix;
        }

        public Matrix3 Inversed()
        {
            double[,] result = new double[3, 3];

            double determinant =
                matrix[0, 0] * (matrix[1, 1] * matrix[2, 2] - matrix[2, 1] * matrix[1, 2]) -
                matrix[1, 0] * (matrix[0, 1] * matrix[2, 2] - matrix[2, 1] * matrix[0, 2]) +
                matrix[2, 0] * (matrix[0, 1] * matrix[1, 2] - matrix[1, 1] * matrix[0, 2]);
            double invDet = 1 / determinant;

            result[0, 0] = (matrix[1, 1] * matrix[2, 2] - matrix[2, 1] * matrix[1, 2]) * invDet;
            result[1, 0] = (matrix[2, 0] * matrix[1, 2] - matrix[1, 0] * matrix[2, 2]) * invDet;
            result[2, 0] = (matrix[1, 0] * matrix[2, 1] - matrix[2, 0] * matrix[1, 1]) * invDet;

            result[0, 1] = (matrix[2, 1] * matrix[0, 2] - matrix[0, 1] * matrix[2, 2]) * invDet;
            result[1, 1] = (matrix[0, 0] * matrix[2, 2] - matrix[2, 0] * matrix[0, 2]) * invDet;
            result[2, 1] = (matrix[2, 0] * matrix[0, 1] - matrix[0, 0] * matrix[2, 1]) * invDet;

            result[0, 2] = (matrix[0, 1] * matrix[1, 2] - matrix[1, 1] * matrix[0, 2]) * invDet;
            result[1, 2] = (matrix[1, 0] * matrix[0, 2] - matrix[0, 0] * matrix[1, 2]) * invDet;
            result[2, 2] = (matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1]) * invDet;

            return new Matrix3(result);
        }

        public double this[int x, int y]
        {
            get { return matrix[x, y]; }
        }

        public static Vector3 operator *(Matrix3 mat, Vector3 v)
        {
            double[,] m = mat.matrix;

            return new Vector3(
                m[0, 0] * v.X + m[1, 0] * v.Y + m[2, 0] * v.Z,
                m[0, 1] * v.X + m[1, 1] * v.Y + m[2, 1] * v.Z,
                m[0, 2] * v.X + m[1, 2] * v.Y + m[2, 2] * v.Z);
        }
    }
}
