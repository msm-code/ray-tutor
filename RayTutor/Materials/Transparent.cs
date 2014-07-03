using System;

namespace RayTutor
{
    class Transparent : IMaterial
    {
        Phong direct;
        double refraction;
        double reflection;
        double transmission;
        double specular;
        ColorRgb baseColor;

        public Transparent(ColorRgb color, double diffuse, double specular,
            double exponent, double reflection, double refraction, double transmission)
        {
            this.direct = new Phong(color, diffuse, specular, exponent);
            this.transmission = transmission;
            this.baseColor = color;
            this.reflection = reflection;
            this.specular = specular;
            this.refraction = refraction;
        }

        public ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            ColorRgb final = direct.Shade(tracer, hit);

            Vector3 toCameraDirection = -hit.Ray.Direction;
            double cosIncidentAngle = hit.Normal.Dot(toCameraDirection);
            double eta = cosIncidentAngle > 0 ? refraction : 1 / refraction;
            double refractionCoeff = FindRefractionCoeff(eta, cosIncidentAngle);

            Ray reflectedRay = new Ray(hit.HitPoint, Vector3.Reflect(toCameraDirection, hit.Normal));
            ColorRgb reflectionColor = baseColor * reflection;

            if (IsTotalInternalReflection(refractionCoeff))
            {
                final += tracer.TraceRay(hit.World, reflectedRay, hit.Depth);
            }
            else
            {
                Ray transmittedRay = ComputeTransmissionDirection(hit.HitPoint, toCameraDirection,
                    hit.Normal, eta, Math.Sqrt(refractionCoeff), cosIncidentAngle);
                ColorRgb transmissionColor = ComputeTransmissionColor(
                    eta, hit.Normal, transmittedRay.Direction);

                final += reflectionColor * tracer.TraceRay(hit.World, reflectedRay, hit.Depth);
                final += transmissionColor * tracer.TraceRay(hit.World, transmittedRay, hit.Depth);
            }

            return final;
        }

        Ray ComputeTransmissionDirection(Vector3 hitPoint, Vector3 toCameraDirection, Vector3 normal,
            double eta, double cosTransmittedAngle, double cosIncidentAngle)
        {
            if (cosIncidentAngle < 0)
            {
                normal = -normal;
                cosIncidentAngle = -cosIncidentAngle;
            }

            Vector3 direction = -toCameraDirection / eta
                - normal * (cosTransmittedAngle - cosIncidentAngle / eta);
            return new Ray(hitPoint, direction);
        }

        ColorRgb ComputeTransmissionColor(double eta, Vector3 normal, Vector3 transmissionDirection)
        {
            return ((ColorRgb.White * transmission) / (eta * eta));
        }

        double FindRefractionCoeff(double eta, double cosIncidentAngle)
        {
            return 1 - (1 - cosIncidentAngle * cosIncidentAngle) / (eta * eta);
        }

        bool IsTotalInternalReflection(double refractionCoeff)
        {
            return refractionCoeff < 0;
        }
    }
}