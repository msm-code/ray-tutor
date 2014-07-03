using System.Collections.Generic;
using System.Linq;

namespace RayTutor.Geomerty
{
    class Composite : IGeometricObject
    {
        List<IGeometricObject> objects;
        Aabb? bounds;

        public Composite()
        {
            this.objects = new List<IGeometricObject>();
        }

        public void Add(IGeometricObject obj)
        {
            this.objects.Add(obj);
            this.bounds = null;
        }

        public double Intersection(Ray ray, ref IntersectionInfo info)
        {
            double minDist = Ray.Huge;
            IntersectionInfo infoRec = default(IntersectionInfo);

            foreach (var obj in objects)
            {
                double dist = obj.Intersection(ray, ref infoRec);
                if (dist < minDist && dist > Ray.Epsilon)
                { minDist = dist; info = infoRec; }
            }

            return minDist;
        }

        public List<IGeometricObject> Objects { get { return objects; } }

        public Aabb BoundingBox
        {
            get
            {
                if (bounds == null) { ComputeAabb(); }
                return bounds.Value;
            }
        }


        void ComputeAabb()
        {
            this.bounds = objects
                .Select((x) => x.BoundingBox)
                .Aggregate((x, y) => Aabb.Union(x, y));
        }
    }
}
