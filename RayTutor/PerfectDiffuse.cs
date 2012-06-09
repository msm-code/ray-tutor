namespace RayTutor
{
    class PerfectDiffuse : IMaterial
    {
        ColorRgb materialColor;

        public PerfectDiffuse(ColorRgb materialColor)
        {
            this.materialColor = materialColor;
        }

        public ColorRgb Radiance(Raytracer tracer, PointLight light, HitInfo hit)
        {
            Vector3 inDirection = (light.Position - hit.HitPoint).Normalized;
            double diffuseFactor = inDirection.Dot(hit.Normal);

            if (diffuseFactor < 0) { return ColorRgb.Black; }

            return light.Color * materialColor * diffuseFactor;
        }
    }
}
