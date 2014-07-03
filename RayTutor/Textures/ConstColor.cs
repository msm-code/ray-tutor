namespace RayTutor
{
    class ConstColor : ITexture
    {
        ColorRgb color;

        public ConstColor(ColorRgb color)
        {
            this.color = color;
        }

        public ColorRgb Get(HitInfo hitInfo)
        {
            return color;
        }
    }
}
