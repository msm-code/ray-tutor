namespace RayTutor
{
    class PointLight
    {
        public PointLight(Vector3 position, ColorRgb color, double brightness)
        {
            this.Position = position;
            this.Color = color;
            this.Brightness = 1;
        }

        public PointLight(Vector3 position, ColorRgb color)
            : this(position, color, 1) { }

        public Vector3 Position { get; private set; }
        public ColorRgb Color { get; private set; }
        public double Brightness { get; private set; }
    }
}
