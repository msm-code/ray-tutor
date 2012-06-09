using System;

namespace RayTutor
{
    class Regular : ISampler
    {
        int sampleRow;

        public Regular(int sampleCt)
        {
            this.sampleRow = (int)Math.Sqrt(sampleCt);
        }

        public Vector2[] Sample()
        {
            Vector2[] result = new Vector2[sampleRow * sampleRow];

            for (int x = 0; x < sampleRow; x++)
                for (int y = 0; y < sampleRow; y++)
                {
                    double fracX = (x + 0.5) / sampleRow;
                    double fracY = (y + 0.5) / sampleRow;

                    result[x * sampleRow + y] = new Vector2(fracX, fracY);
                }

            return result;
        }
    }
}
