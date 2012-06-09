using System.Collections.Generic;
using System;

namespace RayTutor
{
    abstract class Distributor<T>
    {
        Random r;
        List<T[]> sets;

        public Distributor()
        { 
            sets = new List<T[]>();
            r = new Random(0);
        }

        protected void Add(T[] set)
        { sets.Add(set); }

        public T[] Next()
        {
            return sets[(int)(r.Next() % sets.Count)];
        }

        public T Single()
        {
            T[] set = Next();
            return set[r.Next() % set.Length];
        }
    }
}
