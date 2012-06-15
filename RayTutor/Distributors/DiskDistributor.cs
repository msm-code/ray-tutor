using System;
using System.Linq;

namespace RayTutor
{
    class DiskDistributor : Distributor<Vector2>
    {
        public DiskDistributor(ISampler sampler, int sampleCt, int setCt)
        {
            base.CreateSamples(sampler, (x) => MapSamples(x), sampleCt, setCt);
        }

        public Vector2[] MapSamples(Vector2[] samples)
        {
            return samples.Select((sample) =>
            {
                // Skalowanie do zakresu [-1, 1]
                sample.X = sample.X * 2 - 1;
                sample.Y = sample.Y * 2 - 1;

                double r;
                double phi;

                if (sample.X > -sample.Y)
                    if (sample.X > sample.Y) { r = sample.X; phi = sample.Y / sample.X; }
                    else { r = sample.Y; phi = 2 - sample.X / sample.Y; }
                else
                    if (sample.X < sample.Y) { r = -sample.X; phi = 4 + sample.Y / sample.X; }
                    else { r = -sample.Y; phi = 6 - sample.X / sample.Y; }

                if (sample.X == 0 && sample.Y == 0) { phi = 0; }

                phi *= Math.PI / 4;

                return new Vector2(
                    r * Math.Cos(phi),
                    r * Math.Sin(phi));
            }).ToArray();
        }
    }
}
