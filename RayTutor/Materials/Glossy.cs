using System;
namespace RayTutor
{
    class Glossy : IMaterial
    {
        ColorRgb materialColor;
        HemisphereDistributor distributor;
        double specularCoeff;
        double exponent;
        Phong direct;

        public Glossy(ColorRgb color, double diffuse, HemisphereDistributor distributor, double speculer, double exponent)
        {
            this.direct = new Phong(color, diffuse, exponent);
            this.materialColor = color;
            this.distributor = distributor;
            this.specularCoeff = speculer;
            this.exponent = exponent;
        }

        public ColorRgb Radiance(Raytracer tracer, LightInfo light, HitInfo hit)
        {
            ColorRgb color = direct.Radiance(tracer, light, hit);

            Vector3 toCameraDirection = -hit.Ray.Direction;
            Vector3 mirrorReflectionVec = Vector3.Reflect(toCameraDirection, hit.Normal);

            OrthonormalBasis basis = new OrthonormalBasis(mirrorReflectionVec, Up);

            Vector3 sample = distributor.Single();

            Vector3 reflectionVec = basis * sample;
            if (reflectionVec.Dot(hit.Normal) < 0)
            { reflectionVec = Vector3.Reflect(reflectionVec, hit.Normal); }

            double phongLobe = Math.Pow(mirrorReflectionVec.Dot(reflectionVec), exponent);
            double pdf = phongLobe * hit.Normal.Dot(reflectionVec);

            ColorRgb fr = materialColor * specularCoeff * phongLobe;

            Ray reflectedRay = new Ray(hit.HitPoint, reflectionVec);

            ColorRgb reflected = fr * tracer.TraceRay(hit.World, reflectedRay, hit.Depth)
                * ((hit.Normal.Dot(reflectionVec)) / pdf);

            color += reflected;

            return color;
        }

        static readonly Vector3 Up = new Vector3(0, 1, 0);
    }
}
