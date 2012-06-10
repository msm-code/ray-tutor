using System.Collections.Generic;
using System;

namespace RayTutor
{
    abstract class Distributor<T>
    {
        Random r;
        List<T[]> sets;
        int sampleNdx;
        int setNdx;

        public Distributor()
        { 
            sets = new List<T[]>();
            r = new Random(0);
        }

        protected void Add(T[] set)
        { sets.Add(set); }

        public T[] Next()
        {
            setNdx = r.Next(sets.Count);
            return sets[setNdx];
        }

        public T Single()
        {
            T sample = sets[setNdx][sampleNdx];

            sampleNdx++;
            if (sampleNdx >= sets[setNdx].Length)
            { sampleNdx = 0; setNdx = r.Next(sets.Count); }

            return sample;
        }
    }
}
