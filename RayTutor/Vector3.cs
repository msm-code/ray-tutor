using System;
namespace RayTutor
{
    struct Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3(double x, double y, double z)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Vector3 operator +(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);
        }

        public static Vector3 operator -(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);
        }

        public static Vector3 operator *(Vector3 vec, double val)
        {
            return new Vector3(vec.X * val, vec.Y * val, vec.Z * val);
        }

        public static Vector3 operator /(Vector3 vec, double val)
        {
            return new Vector3(vec.X / val, vec.Y / val, vec.Z / val);
        }

        public static Vector3 operator -(Vector3 vec)
        {
            return new Vector3(-vec.X, -vec.Y, -vec.Z);
        }

        public double Dot(Vector3 vec)
        {
            return (this.X * vec.X + this.Y * vec.Y + this.Z * vec.Z);
        }

        public static Vector3 Cross(Vector3 vecA, Vector3 vecB)
        {
            return new Vector3(vecA.Y * vecB.Z - vecA.Z * vecB.Y,
                vecA.Z * vecB.X - vecA.X * vecB.Z,
                vecA.X * vecB.Y - vecA.Y * vecB.X);
        }

        public static Vector3 Reflect(Vector3 vec, Vector3 normal)
        {
            double dot = normal.Dot(vec);
            return normal * dot * 2 - vec;
        }

        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.X < b.X ? a.X : b.X,
                a.Y < b.Y ? a.Y : b.Y,
                a.Z < b.Z ? a.Z : b.Z);
        }

        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.X < b.X ? b.X : a.X,
                a.Y < b.Y ? b.Y : a.Y,
                a.Z < b.Z ? b.Z : a.Z);
        }

        /*public static Vector3 ComponentMul(Vector3 a, Vector3 b)
        { return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z); }

        public static Vector3 ComponentDiv(Vector3 a, Vector3 b)
        { return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z); }*/

        public double Length
        { get { return Math.Sqrt(X * X + Y * Y + Z * Z); } }

        public double LengthSq
        { get { return X * X + Y * Y + Z * Z; } }

        public Vector3 Normalized
        { get { return this / this.Length; } }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "[{0},{1},{2}]", X, Y, Z);
        }
    }
}
