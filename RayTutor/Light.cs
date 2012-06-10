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

        public Ray SampleRay(Vector3 point)
        {
            Vector3 origin = area.SamplePoint();
            return new Ray(origin, point - origin);
        }
    }
}
