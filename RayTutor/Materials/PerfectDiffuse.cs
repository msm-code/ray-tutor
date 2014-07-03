namespace RayTutor
{
    class PerfectDiffuse : IMaterial
    {
        ColorRgb materialColor;

        public PerfectDiffuse(ColorRgb materialColor)
        {
            this.materialColor = materialColor;
        }

        public ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            ColorRgb total = ColorRgb.Black;

            foreach (var light in hit.World.Lights)
            {
                Vector3 position = light.Sample();

                Vector3 inDirection = (position - hit.HitPoint).Normalized;
                double diffuseFactor = inDirection.Dot(hit.Normal);

                if (diffuseFactor < 0) { continue; }

                if (hit.World.AnyObstacleBetween(hit.HitPoint, position)) { continue; }

                total += light.Color * materialColor * diffuseFactor;
            }

            return total;
        }
    }
}
