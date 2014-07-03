using System.Drawing;
namespace RayTutor
{
    class Plane : IGeometricObject
    {
        IMaterial material;
        Vector3 point;
        Vector3 normal;

        public Plane(Vector3 point, Vector3 normal, IMaterial material)
        {
            this.point = point;
            this.normal = normal.Normalized;
            this.material = material;
        }

        public double Intersection(Ray ray, ref IntersectionInfo info)
        {
            info.Normal = this.normal;
            info.Material = this.material;
            return (point - ray.Origin).Dot(normal) / ray.Direction.Dot(normal);
        }

        public Aabb BoundingBox
        {get { throw new System.InvalidOperationException(); }}
    }
}
