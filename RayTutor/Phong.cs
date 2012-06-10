using System;
namespace RayTutor
{
    class Phong : IMaterial
    {
        ColorRgb materialColor;
        double diffuseCoeff;
        double specularExponent;

        public Phong(ColorRgb materialColor, double diffuse, double exponent)
        {
            this.materialColor = materialColor;
            this.diffuseCoeff = diffuse;
            this.specularExponent = exponent;
        }

        public ColorRgb Radiance(Raytracer tracer, LightInfo light, HitInfo hit)
        {
            Vector3 inDirection = (light.Position - hit.HitPoint).Normalized;
            double lambertFactor = inDirection.Dot(hit.Normal);

            if (lambertFactor < 0) { return ColorRgb.Black; }

            ColorRgb result = light.Color * materialColor * lambertFactor * diffuseCoeff;

            double phongFactor = PhongFactor(inDirection, hit.Normal, -hit.Ray.Direction);

            if (phongFactor > 0)
            { result += materialColor * Math.Pow(phongFactor, specularExponent); }

            return result;
        }

        double PhongFactor(Vector3 inDirection, Vector3 normal, Vector3 toCameraDirection)
        {
            Vector3 reflected = Vector3.Reflect(inDirection, normal);
            return reflected.Dot(toCameraDirection);
        }
    }
}
