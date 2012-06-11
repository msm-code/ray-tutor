namespace RayTutor
{
    class Mandelbrot : IShape
    {
        const int MaxIterations = 10;

        double scale;
        double rotation;

        public Mandelbrot(double scale, double rotation)
        {
            this.scale = scale;
            this.rotation = rotation;
        }

        public bool Contains(Vector2 point)
        {
            point /= scale;
            point = Vector2.Rotate(point, -rotation);

            Vector2 z = point;

            for (int i = 0; i < MaxIterations; i++)
            {
                z = new Vector2(z.X * z.X - z.Y * z.Y,
                    2 * z.X * z.Y) + point;

                if (z.X * z.X + z.Y * z.Y > 4) { return false; }
            }

            return true;
        }
    }
}
