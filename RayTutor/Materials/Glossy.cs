using System;

namespace RayTutor
{
    /*class Glossy : IMaterial
    {
        ColorRgb materialColor;
        SampleSet<Vector3> distributor;
        double specularCoeff;
        double exponent;
        Phong direct;

        public Glossy(ColorRgb color, double diffuse, SampleSet<Vector3> distributor, double specular, double exponent)
        {
            this.direct = new Phong(color, diffuse, specular, exponent);
            this.materialColor = color;
            this.distributor = distributor;
            this.specularCoeff = specular;
            this.exponent = exponent;
        }

        public ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            ColorRgb color = direct.Shade(tracer, hit);

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
    }*/
}
