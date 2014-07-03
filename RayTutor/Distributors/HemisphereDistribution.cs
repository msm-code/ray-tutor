using System;
using System.Linq;

namespace RayTutor
{
    /*class HemisphereDistributor : ISampleDistributor<Vector3>
    {
        double exponent;

        public HemisphereDistributor(double exponent)
        {
            this.exponent = exponent;
        }

        public Vector3 MapSample(Vector2 sample)
        {
            double cosPhi = Math.Cos(2 * Math.PI * sample.X);
            double sinPhi = Math.Sin(2 * Math.PI * sample.X);
            double cosTheta = Math.Pow(1 - sample.Y, 1 / (exponent + 1));
            double sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

            double u = sinTheta * cosPhi;
            double v = sinTheta * sinPhi;
            double w = cosTheta;

            return new Vector3(u, v, w);
        }
    }*/
}
