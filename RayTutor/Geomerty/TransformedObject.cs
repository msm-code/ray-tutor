using RayTutor.Transformations;
using System.Collections.Generic;
using System.Linq;

namespace RayTutor.Geomerty
{
    class Transformed : IGeometricObject
    {
        IGeometricObject instanced;
        Matrix transform;
        Matrix invTransform;

        public Transformed(Transformation transformation, IGeometricObject instanced)
        {
            this.invTransform = transformation.InvMatrix;
            this.transform = transformation.Matrix;
            this.instanced = instanced;

            Aabb nestedBox = instanced.BoundingBox;

            IEnumerable<Vector3> boundsVerticles = nestedBox.Verticles
                .Select((x) => transform.TransformPoint(x));
            BoundingBox = Aabb.Point(boundsVerticles.First());

            foreach (var vertex in boundsVerticles)
            { BoundingBox = BoundingBox.Wrap(vertex); }
        }

        public Transformed(Transformation transformation, IGeometricObject instanced, IMaterial matOverride)
            :this(transformation, instanced)
        {
            this.MaterialOverride = matOverride;
        }

        public double Intersection(Ray ray, ref IntersectionInfo info)
        {
            Ray invRay = new Ray(
                invTransform.TransformPoint(ray.Origin),
                invTransform.TransformVector(ray.Direction));

            double intersection = instanced.Intersection(invRay, ref info);
            if (Ray.IsValid(intersection)) { info.Normal = invTransform.TransformNormal(info.Normal).Normalized; }

            if (MaterialOverride != null) { info.Material = MaterialOverride; }

            return intersection;
        }

        public IMaterial MaterialOverride { get; private set; }
        public Aabb BoundingBox { get; private set; }
    }
}
