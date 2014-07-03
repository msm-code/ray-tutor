using System;
using System.Linq;

namespace RayTutor
{
    class DiskDistribution : ISampleDistributor
    {
        public Vector2 MapSample(Vector2 sample)
        {
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
        }
    }
}
