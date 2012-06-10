namespace RayTutor
{
    class Rectangle : IShape
    {
        Vector2 center;
        Vector2 halfSize;
        double rotation;

        public Rectangle(Vector2 center, Vector2 halfSize, double rotation)
        {
            this.center = center;
            this.halfSize = halfSize;
            this.rotation = rotation;
        }

        public bool Contains(Vector2 point)
        {
            Vector2 dist = point - center;
            Vector2 rotated = Vector2.Rotate(dist, -this.rotation);

            if (rotated.X < -halfSize.X || rotated.X > halfSize.X ||
                rotated.Y < -halfSize.Y || rotated.Y > halfSize.Y)
            { return false; }
            return true;
        }
    }
}
