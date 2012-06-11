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
                var samples = sampler.Sample()
                    .Select((x) => MapSample(x, exponent))
                    .ToArray();

                base.Add(samples);
            }
        }

        public static Vector3 MapSample(Vector2 sample, double exp)
        {
            double cosPhi = Math.Cos(2 * Math.PI * sample.X);
            double sinPhi = Math.Sin(2 * Math.PI * sample.X);
            double cosTheta = Math.Pow(1 - sample.Y, 1 / (exp + 1));
            double sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

            double u = sinTheta * cosPhi;
            double v = sinTheta * sinPhi;
            double w = cosTheta;

            return new Vector3(u, v, w);
        }
    }
}
