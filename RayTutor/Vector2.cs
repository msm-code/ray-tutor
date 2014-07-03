using System;
namespace RayTutor
{
    struct Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2(double x, double y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector2 operator +(Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(vec1.X + vec2.X, vec1.Y + vec2.Y);
        }

        public static Vector2 operator -(Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(vec1.X - vec2.X, vec1.Y - vec2.Y);
        }

        public static Vector2 operator *(Vector2 vec, double val)
        {
            return new Vector2(vec.X * val, vec.Y * val);
        }

        public static Vector2 operator /(Vector2 vec, double val)
        {
            return vec * (1 / val);
        }

        public static Vector2 operator -(Vector2 vec)
        {
            return new Vector2(-vec.X, -vec.Y);
        }

        public double Dot(Vector2 vec)
        {
            return (this.X * vec.X + this.Y * vec.Y);
        }

        public static Vector2 Rotate(Vector2 vector, double rotation)
        {
            return new Vector2(
                vector.X * Math.Cos(rotation) - vector.Y * Math.Sin(rotation),
                vector.Y * Math.Cos(rotation) + vector.X * Math.Sin(rotation));
        }

        public double Length
        { get { return Math.Sqrt(X * X + Y * Y); } }

        public double LengthSq
        { get { return X * X + Y * Y; } }

        public Vector2 Normalized
        { get { return this / this.Length; } }
    }
}
