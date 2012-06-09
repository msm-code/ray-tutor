namespace RayTutor
{
    class PerfectReflective : IMaterial
    {
        IMaterial direct;
        double reflectivity;
        ColorRgb reflectionColor;

        public PerfectReflective(ColorRgb materialColor, double diffuse, double exponent, double reflectivity)
        {
            this.direct = new Phong(materialColor, diffuse, exponent);
            this.reflectivity = reflectivity;
            this.reflectionColor = materialColor;
        }

        public ColorRgb Radiance(Raytracer tracer, PointLight light, HitInfo hit)
        {
            Vector3 toCameraDirection = -hit.Ray.Direction;

            ColorRgb luminance = direct.Radiance(tracer, light, hit);
            Vector3 reflectedVector = Vector3.Reflect(toCameraDirection, hit.Normal);
            Ray reflectedRay = new Ray(hit.HitPoint, reflectedVector);

            double reflectionFactor = PerfectReflectionFactor(reflectivity, hit.Normal, reflectedVector);
            double lambertFactor = toCameraDirection.Dot(hit.Normal);

            luminance += tracer.TraceRay(hit.World, reflectedRay, hit.Depth) *
                reflectionColor *
                reflectionFactor * lambertFactor;

            return luminance;
        }

        double PerfectReflectionFactor(double reflectivity, Vector3 normal, Vector3 reflectedVector)
        {
            return reflectivity / normal.Dot(reflectedVector);
        }
    }
}
