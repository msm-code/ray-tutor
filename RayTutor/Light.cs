namespace RayTutor
{
    class Light
    {
        IArea area;

        public Light(IArea area, ColorRgb color)
        {
            this.area = area;
            this.Color = color;
        }

        public ColorRgb Color { get; set; }

        public Vector3 Sample(Vector3 point)
        {
            Vector3 origin = area.SamplePoint();
            return origin;
        }
    }
}
