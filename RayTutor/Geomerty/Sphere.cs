using System.Drawing;
using System;
namespace RayTutor
{
    class Sphere : IGeometricObject
    {
        IMaterial material;
        Vector3 center;
        double radius;

        public Sphere(Vector3 center, double radius, IMaterial material)
        {
            this.center = center;
            this.radius = radius;
            this.material = material;

            Vector3 size = new Vector3(radius, radius, radius);
            this.BoundingBox = new Aabb(center - size, center + size);
        }

        public double Intersection(Ray ray, ref IntersectionInfo info)
        {
            double t;
            Vector3 distance = ray.Origin - center;

            double a = ray.Direction.LengthSq;
            double b = (distance * 2).Dot(ray.Direction);
            double c = distance.LengthSq - radius * radius;
            double disc = b * b - 4 * a * c;

            if (disc < 0) { return Ray.Huge; }

            double discSq = Math.Sqrt(disc);
            double denom = 2 * a;

            t = (-b - discSq) / denom;
            if (t < Ray.Epsilon)
            { t = (-b + discSq) / denom; }

            info.Material = this.material;
            info.Normal = (ray.Follow(t) - this.center).Normalized;

            return t;
        }

        public Aabb BoundingBox { get; private set; }
    }
}
