using System;
using System.Linq;

namespace RayTutor
{
    class HemisphereDistributor : Distributor<Vector3>
    {
        double exponent;

        public HemisphereDistributor(ISampler sampler, double exponent, int sampleCt, int setCt)
        {
            this.exponent = exponent;

            base.CreateSamples(sampler, (x) => MapSamples(x), sampleCt, setCt);
        }

        public Vector3[] MapSamples(Vector2[] samples)
        {
            return samples.Select((sample) =>
            {
                double cosPhi = Math.Cos(2 * Math.PI * sample.X);
                double sinPhi = Math.Sin(2 * Math.PI * sample.X);
                double cosTheta = Math.Pow(1 - sample.Y, 1 / (exponent + 1));
                double sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

                double u = sinTheta * cosPhi;
                double v = sinTheta * sinPhi;
                double w = cosTheta;

                return new Vector3(u, v, w);
            }).ToArray();
        }

    }
}
