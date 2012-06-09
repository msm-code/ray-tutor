using System;
namespace RayTutor
{
    class PureRandom : ISampler
    {
        int sampleCt;
        Random r;

        public PureRandom(int sampleCt, int seed)
        {
            this.sampleCt = sampleCt;
            this.r = new Random(seed);
        }

        public Vector2[] Sample()
        {
            Vector2[] samples = new Vector2[sampleCt];

            for (int i = 0; i < sampleCt; i++)
            {
                samples[i] = new Vector2(r.NextDouble(), r.NextDouble());
            }

            return samples;
        }
    }
}
