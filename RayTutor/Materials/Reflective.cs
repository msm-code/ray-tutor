namespace RayTutor
{
    class Reflective : IMaterial
    {
        IMaterial direct;
        double reflectivity;
        ColorRgb reflectionColor;

        public Reflective(ColorRgb materialColor, double diffuse, double specular, double exponent, double reflectivity)
        {
            this.direct = new Phong(materialColor, diffuse, specular, exponent);
            this.reflectivity = reflectivity;
            this.reflectionColor = materialColor;
        }

        public Reflective(ColorRgb materialColor, double diffuse, double specular, double exponent, double reflectivity, bool foo)
        {
            this.direct = new Phong(new Checker(), diffuse, specular, exponent);
            this.reflectivity = reflectivity;
            this.reflectionColor = materialColor;
        }

        public ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            Vector3 toCameraDirection = -hit.Ray.Direction;

            ColorRgb luminance = direct.Shade(tracer, hit);
            Vector3 reflectedVector = Vector3.Reflect(toCameraDirection, hit.Normal);
            Ray reflectedRay = new Ray(hit.HitPoint, reflectedVector);

            luminance += tracer.TraceRay(hit.World, reflectedRay, hit.Depth) *
                reflectionColor *
                reflectivity;

            return luminance;
        }
    }
}
