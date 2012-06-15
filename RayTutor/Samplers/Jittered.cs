using System;

namespace RayTutor
{
    class Jittered : ISampler
    {
        Random r;

        public Jittered(int seed)
        {
            this.r = new Random(seed);
        }

        public Vector2[] Sample(int sampleCt)
        {
            int sampleRow = (int)Math.Sqrt(sampleCt);
            Vector2[] result = new Vector2[sampleRow * sampleRow];

            for (int x = 0; x < sampleRow; x++)
                for (int y = 0; y < sampleRow; y++)
                {
                    double fracX = (x + r.NextDouble()) / sampleRow;
                    double fracY = (y + r.NextDouble()) / sampleRow;

                    result[x * sampleRow + y] = new Vector2(fracX, fracY);
                }

            return result;
        }
    }
}
