using System.Collections.Generic;

namespace RayTutor
{
    struct Aabb
    {
        Vector3 min;
        Vector3 max;

        public Aabb(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public Aabb Wrap(Vector3 add)
        {
            return new Aabb(
                Vector3.Min(min, add),
                Vector3.Max(max, add));
        }

        Vector3 Vertex(int x, int y, int z)
        {
            return new Vector3(
                x == 0 ? min.X : max.X,
                y == 0 ? min.Y : max.Y,
                z == 0 ? min.Z : max.Z);
        }

        public IEnumerable<Vector3> Verticles
        {
            get
            {
                for (int i = 0; i < 8; i++)
                { yield return Vertex(i & 1, i & 2, i & 4); }
            }
        }

        public Vector3 Min { get { return min; } }

        public Vector3 Max { get { return max; } set { max = value; } }

        public double Intersection(Ray ray)
        {
            double tmin = (min.X - ray.Origin.X) / ray.Direction.X;
            double tmax = (max.X - ray.Origin.X) / ray.Direction.X;
            if (tmin > tmax) { MathExt.Swap(ref tmin, ref tmax); }

            double tymin = (min.Y - ray.Origin.Y) / ray.Direction.Y;
            double tymax = (max.Y - ray.Origin.Y) / ray.Direction.Y;
            if (tymin > tymax) { MathExt.Swap(ref tymin, ref tymax); }
            if ((tmin > tymax) || (tymin > tmax)) { return Ray.Huge; }
            if (tymin > tmin) { tmin = tymin; }
            if (tymax < tmax) { tmax = tymax; }

            double tzmin = (min.Z - ray.Origin.Z) / ray.Direction.Z;
            double tzmax = (max.Z - ray.Origin.Z) / ray.Direction.Z;
            if (tzmin > tzmax) { MathExt.Swap(ref tzmin, ref tzmax); }
            if ((tmin > tzmax) || (tzmin > tmax)) { return Ray.Huge; }
            if (tzmin > tmin) { tmin = tzmin; }
            if (tzmax < tmax) { tmax = tzmax; }

            if (tmin > tmax || tmax < Ray.Epsilon || tmin < Ray.Epsilon) { return Ray.Huge; }
            return tmin;
        }

        public bool Contains(Vector3 point)
        {
            return point.X >= min.X && point.X <= max.X &&
                   point.Y >= min.Y && point.Y <= max.Y &&
                   point.Z >= min.Z && point.Z <= max.Z;
        }

        public static Aabb Union(Aabb fst, Aabb snd)
        {
            return new Aabb(
                Vector3.Min(fst.min, snd.min),
                Vector3.Max(fst.max, snd.max));
        }

        public static Aabb Point(Vector3 point)
        {
            return new Aabb(point, point);
        }
    }
}
