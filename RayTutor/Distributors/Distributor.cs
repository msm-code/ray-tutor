using System.Collections.Generic;
using System;

namespace RayTutor
{
    class Distributor<T>
    {
        Random r;
        List<T[]> sets;
        int sampleNdx;
        int setNdx;

        protected Distributor() { }

        protected void CreateSamples(ISampler sampler, Func<Vector2[], T[]> mapSamples, int sampleCt, int setCt)
        {
            sets = new List<T[]>(setCt);
            r = new Random(0);
            SampleCount = sampleCt;

            for (int i = 0; i < setCt; i++)
            {
                sets.Add(mapSamples(sampler.Sample(sampleCt)));
            }
        }

        public T Single()
        {
            T sample = sets[setNdx][sampleNdx];

            sampleNdx++;
            if (sampleNdx >= sets[setNdx].Length)
            { sampleNdx = 0; setNdx = r.Next(sets.Count); }

            return sample;
        }

        public int SampleCount { get; private set; }
        public int SetCount { get { return sets.Count; } }
    }
}
