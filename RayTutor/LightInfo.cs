namespace RayTutor
{
    class LightInfo
    {
        public LightInfo(Vector3 position, ColorRgb color)
        {
            this.Position = position;
            this.Color = color;
        }

        public Vector3 Position { get; private set; }
        public ColorRgb Color { get; private set; }
    }
}
