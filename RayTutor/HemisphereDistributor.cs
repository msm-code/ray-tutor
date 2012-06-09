using System;
using System.Linq;

namespace RayTutor
{
    class HemisphereDistributor : Distributor<Vector3>
    {
        public HemisphereDistributor(ISampler sampler, int setCt, double exponent)
        {
            for (int i = 0; i < setCt; i++)
            {
                base.Add(MapSamples(sampler.Sample(), exponent));
            }
        }

        Vector3[] MapSamples(Vector2[] samples, double exp)
        {
            return samples.Select((x) =>
            {
                double cosPhi = Math.Cos(2 * Math.PI * x.X);
                double sinPhi = Math.Sin(2 * Math.PI * x.X);
                double cosTheta = Math.Pow(1 - x.Y, 1 / (exp + 1));
                double sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

                double u = sinTheta * cosPhi;
                double v = sinTheta * sinPhi;
                double w = cosTheta;

                return new Vector3(u, v, w);
            }).ToArray();
        }
    }
}
