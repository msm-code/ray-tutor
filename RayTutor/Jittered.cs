using System;

namespace RayTutor
{
    class Jittered : ISampler
    {
        int sampleRow;
        Random r;

        public Jittered(int sampleCt, int seed)
        {
            this.sampleRow = (int)Math.Sqrt(sampleCt);
            this.r = new Random(seed);
        }

        public Vector2[] Sample()
        {
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
