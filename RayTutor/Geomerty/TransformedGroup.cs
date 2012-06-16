using System.Collections.Generic;
using RayTutor.Transformations;

namespace RayTutor.Geomerty
{
    class TransformedGroup : GeometricObject
    {
        List<GeometricObject> objects;
        Matrix invTransform;

        public TransformedGroup(Matrix invTransform)
        {
            this.objects = new List<GeometricObject>();
            this.invTransform = invTransform;
        }

        public void Add(GeometricObject untransformedObj)
        {
            objects.Add(untransformedObj);
        }

        public override bool HitTest(Ray ray, ref double outDistance, ref Vector3 outNormal)
        {
            Ray invRay = new Ray(
                invTransform.TransformPoint(ray.Origin),
                invTransform.TransformVector(ray.Direction));

            Vector3 normal = default(Vector3);
            outDistance = Ray.Huge;
            double lastDistance = 0; // zmienna pomocnicza, ostatnia odległość

            foreach (var obj in objects)
            {
                if (obj.HitTest(invRay, ref lastDistance, ref normal) &&
                    lastDistance < outDistance) // jeśli najbliższe trafienie
                {
                    outDistance = lastDistance; // nowa najmniejsza odległość
                    outNormal = normal;
                    this.Material = obj.Material;
                }
            }

            if (outDistance >= Ray.Huge) { return false; }

            outNormal = invTransform.TransformNormal(outNormal).Normalized;
            return true;
        }
    }
}
