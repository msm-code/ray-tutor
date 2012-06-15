using System;

namespace RayTutor
{
    class Transparent : IMaterial
    {
        Phong direct;
        double refraction;
        double transmission;
        double specular;
        ColorRgb baseColor;

        public Transparent(ColorRgb color,
            double diffuse, 
            double exponent, 
            double specular, 
            double refraction, 
            double transmission)
        {
            this.direct = new Phong(color, diffuse, exponent);
            this.transmission = transmission;
            this.baseColor = color;
            this.specular = specular;
            this.refraction = refraction;
        }

        public ColorRgb Radiance(Raytracer tracer, LightInfo light, HitInfo hit)
        {
            ColorRgb final = direct.Radiance(tracer, light, hit);

            Vector3 normal = hit.Normal;
            Vector3 toCameraDirection = -hit.Ray.Direction;

            Vector3 reflectionDirection = Vector3.Reflect(toCameraDirection, normal);
            ColorRgb reflectionColor = GetSpecularColor(normal, reflectionDirection, toCameraDirection);
            Ray reflectedRay = new Ray(hit.HitPoint, reflectionDirection);

            if (IsTotalInternalReflection(hit))
            {
                final += tracer.TraceRay(hit.World, reflectedRay, hit.Depth);
            }
            else
            {
                Vector3 transmissionDirection;
                ColorRgb transmissionColor = GetTransmissionColor(normal, out transmissionDirection, toCameraDirection);
                Ray transmittedRay = new Ray(hit.HitPoint, transmissionDirection);

                final += reflectionColor *
                    tracer.TraceRay(hit.World, reflectedRay, hit.Depth) *
                    Math.Abs(hit.Normal.Dot(reflectionDirection));

                final += transmissionColor *
                    tracer.TraceRay(hit.World, transmittedRay, hit.Depth) *
                    Math.Abs(hit.Normal.Dot(transmissionDirection));
            }

            return final;
        }

        public ColorRgb GetTransmissionColor(Vector3 normal, out Vector3 transmissionDirection, Vector3 toCameraDirection)
        {
            double cosNormalToCamera = normal.Dot(toCameraDirection);
            double eta = refraction;

            if (cosNormalToCamera < 0)
            {
                cosNormalToCamera = -cosNormalToCamera;
                normal = -normal;
                eta = 1 / eta;
            }

            double cosTransmission = Math.Sqrt(1 - (1 - cosNormalToCamera * cosNormalToCamera) / (eta * eta));

            transmissionDirection = -toCameraDirection / eta -
                normal * (cosTransmission - cosNormalToCamera / eta);

            return ColorRgb.White * transmission / (eta * eta * Math.Abs(normal.Dot(transmissionDirection)));
        }

        public bool IsTotalInternalReflection(HitInfo hit)
        {
            Vector3 toCameraDirection = -hit.Ray.Direction;
            double cosNormalToCamera = hit.Normal.Dot(toCameraDirection);
            double eta = refraction;

            if (cosNormalToCamera < 0)
            { eta = 1 / eta; }

            return (1 - (1 - cosNormalToCamera * cosNormalToCamera) / (eta * eta) < 0);
        }

        public ColorRgb GetSpecularColor(Vector3 normal, Vector3 inDirection, Vector3 outDirection)
        {
            return (baseColor * specular) / normal.Dot(inDirection);
        }
    }
}
