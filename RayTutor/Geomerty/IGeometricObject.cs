using System.Drawing;

namespace RayTutor
{
    interface IGeometricObject
    {
        double Intersection(Ray ray, ref IntersectionInfo info);
        Aabb BoundingBox { get; }
    }

    struct IntersectionInfo
    {
        public Vector3 Normal { get; set; }
        public IMaterial Material { get; set; }
    }
}
