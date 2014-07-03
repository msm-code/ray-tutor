using System;
namespace RayTutor
{
    class Phong : IMaterial
    {
        ITexture texture;
        double diffuseCoeff;
        double specular;
        double specularExponent;

        public Phong(ColorRgb materialColor, double diffuse, double specular, double exponent)
            : this(new ConstColor(materialColor), diffuse, specular, exponent) { }

        public Phong(ITexture texture, double diffuse, double specular, double exponent)
        {
            this.texture = texture;
            this.diffuseCoeff = diffuse;
            this.specular = specular;
            this.specularExponent = exponent;
        }
        
        public ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            ColorRgb total = ColorRgb.Black;

            foreach (var light in hit.World.Lights)
            {
                Vector3 position = light.Sample();

                Vector3 inDirection = (position - hit.HitPoint).Normalized;
                double lambertFactor = inDirection.Dot(hit.Normal);

                if (lambertFactor < 0) { continue; }

                if (hit.World.AnyObstacleBetween(hit.HitPoint, position)) { continue; }

                ColorRgb materialColor = texture.Get(hit);
                ColorRgb result = light.Color * materialColor * lambertFactor * diffuseCoeff;

                double phongFactor = PhongFactor(inDirection, hit.Normal, -hit.Ray.Direction) * specular;

                if (phongFactor != 0)
                { result += materialColor * phongFactor; }

                total += result;
            }

            return total;
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
