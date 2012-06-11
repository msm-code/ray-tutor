using System.Linq;
using System;
using System.Collections.Generic;

namespace RayTutor
{
    class DiskDistributor : Distributor<Vector2>
    {
        public DiskDistributor(ISampler sampler, int setCt)
        {
            for (int i = 0; i < setCt; i++)
            {
                Vector2[] outSamples = sampler.Sample()
                    .Select(x => MapSample(x))
                    .ToArray();

                base.Add(outSamples);
            }
        }

        public static Vector2 MapSample(Vector2 sample)
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

            phi *= Math.PI / 4;

            return new Vector2(
                r * Math.Cos(phi),
                r * Math.Sin(phi));
        }
    }
}
