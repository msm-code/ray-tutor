using System;
namespace RayTutor.Transformations
{
    class Matrix
    {
        double[] data;

        private Matrix(double[] data)
        { this.data = data; }

        double this[int x, int y]
        {
            get { return data[y * 4 + x]; }
            set { data[y * 4 + x] = value; }
        }

        public static Matrix Identity
        {
            get
            {
                return new Matrix(new double[] {
                    1, 0, 0, 0,
                    0, 1, 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1});
            }
        }

        public static Matrix Translate(double x, double y, double z)
        {
            return new Matrix(new double[] {
                1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1});
        }

        public static Matrix Scale(double x, double y, double z)
        {
            return new Matrix(new double[] {
                x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1});
        }

        public static Matrix RotateX(double angle)
        {
            return new Matrix(new double[] {
                1, 0,               0,                0,
                0, Math.Cos(angle), -Math.Sin(angle), 0,
                0, Math.Sin(angle), Math.Cos(angle),  0,
                0, 0,               0,                1});
        }

        public static Matrix RotateY(double angle)
        {
            return new Matrix(new double[] {
                Math.Cos(angle),  0, Math.Sin(angle),  0,
                0,                1, 0,                0,
                -Math.Sin(angle), 0, Math.Cos(angle),  0,
                0,                0, 0,                1});
        }

        public static Matrix RotateZ(double angle)
        {
            return new Matrix(new double[] {
                Math.Cos(angle), -Math.Sin(angle), 0, 0,
                Math.Sin(angle), Math.Cos(angle),  0, 0,
                0,               0,                1, 0,
                0,               0,                0, 1});
        }

        public static Matrix operator *(Matrix m0, Matrix m1)
        {
            Matrix result = Matrix.Identity;
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 3; y++)
                {
                    double total = 0;
                    for (int i = 0; i < 4; i++)
                        total += m0[i, y] * m1[x, i];

                    result[x, y] = total;
                }
            return result;
        }

        public Vector3 TransformPoint(Vector3 p)
        {
            return new Vector3(
                this[0, 0] * p.X + this[1, 0] * p.Y + this[2, 0] * p.Z + this[3, 0],
                this[0, 1] * p.X + this[1, 1] * p.Y + this[2, 1] * p.Z + this[3, 1],
                this[0, 2] * p.X + this[1, 2] * p.Y + this[2, 2] * p.Z + this[3, 2]);
        }

        public Vector3 TransformVector(Vector3 v)
        {
            return new Vector3(
                this[0, 0] * v.X + this[1, 0] * v.Y + this[2, 0] * v.Z,
                this[0, 1] * v.X + this[1, 1] * v.Y + this[2, 1] * v.Z,
                this[0, 2] * v.X + this[1, 2] * v.Y + this[2, 2] * v.Z);
        }

        public Vector3 TransformNormal(Vector3 n)
        {
            return new Vector3(
                this[0, 0] * n.X + this[0, 1] * n.Y + this[0, 2] * n.Z,
                this[1, 0] * n.X + this[1, 1] * n.Y + this[1, 2] * n.Z,
                this[2, 0] * n.X + this[2, 1] * n.Y + this[2, 2] * n.Z);
        }
    }
}
