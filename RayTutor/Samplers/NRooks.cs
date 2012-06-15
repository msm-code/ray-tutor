using System;

namespace RayTutor
{
    class NRooks : ISampler
    {
        Random r;

        public NRooks(int seed)
        {
            this.r = new Random(seed);
        }

        public Vector2[] Sample(int sampleCt)
        {
            Vector2[] samples = new Vector2[sampleCt];

            for (int i = 0; i < sampleCt; i++)
            {
                samples[i] = new Vector2(
                    (i + r.NextDouble()) / sampleCt,
                    (i + r.NextDouble()) / sampleCt);
            }

            ShuffleX(samples, sampleCt);

            return samples;
        }

        void ShuffleX(Vector2[] samples, int sampleCt)
        {
            for (int i = 0; i < sampleCt - 1; i++)
            {
                int target = r.Next() % sampleCt;
                double temp = samples[i].X;
                samples[i].X = samples[target].X;
                samples[target].X = temp;
            }
        }
    }
}
