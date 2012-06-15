using System;
namespace RayTutor
{
    class Phong : IMaterial
    {
        ITexture texture;
        double diffuseCoeff;
        double specularExponent;

        public Phong(ColorRgb materialColor, double diffuse, double exponent)
            : this(new ConstColor(materialColor), diffuse, exponent) { }

        public Phong(ITexture texture, double diffuse, double exponent)
        {
            this.texture = texture;
            this.diffuseCoeff = diffuse;
            this.specularExponent = exponent;
        }

        public ColorRgb Radiance(Raytracer tracer, LightInfo light, HitInfo hit)
        {
            Vector3 inDirection = (light.Position - hit.HitPoint).Normalized;
            double lambertFactor = inDirection.Dot(hit.Normal);

            if (lambertFactor < 0) { return ColorRgb.Black; }

            if (hit.World.AnyObstacleBetween(hit.HitPoint, light.Position)) { return ColorRgb.Black; }

            ColorRgb materialColor = texture.Get(hit);
            ColorRgb result = light.Color * materialColor * lambertFactor * diffuseCoeff;

            double phongFactor = PhongFactor(inDirection, hit.Normal, -hit.Ray.Direction);

            if (phongFactor != 0)
            { result += materialColor * phongFactor; }

            return result;
        }

        double PhongFactor(Vector3 inDirection, Vector3 normal, Vector3 toCameraDirection)
        {
            Vector3 reflected = Vector3.Reflect(inDirection, normal);
            double cosAngle = reflected.Dot(toCameraDirection);

            if (cosAngle <= 0) { return 0; }

            return Math.Pow(cosAngle, specularExponent);
        }
    }
}
