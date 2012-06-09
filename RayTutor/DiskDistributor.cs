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
                // skalowanie do zakresu [-1; 1]
                IEnumerable<Vector2> inSamples = sampler.Sample();

                Vector2[] outSamples = MapSamples(inSamples).ToArray();

                base.Add(outSamples);
            }
        }

        IEnumerable<Vector2> MapSamples(IEnumerable<Vector2> square)
        {
            return square
                .Select((x) =>
                {
                    double r;
                    double phi;

                    if (x.X > -x.Y)
                        if (x.X > x.Y) { r = x.X; phi = x.Y / x.X; }
                        else { r = x.Y; phi = 2 - x.X / x.Y; }
                    else
                        if (x.X < x.Y) { r = -x.X; phi = 4 + x.Y / x.X; }
                        else { r = -x.Y; phi = 6 - x.X / x.Y; }

                    phi *= Math.PI / 4;

                    return new Vector2(
                        r * Math.Cos(phi),
                        r * Math.Sin(phi));
                }).ToArray();
        }
    }
}
