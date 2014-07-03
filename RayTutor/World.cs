using System.Drawing;
using System.Collections.Generic;

namespace RayTutor
{
    class World
    {
        List<IGeometricObject> objects;
        List<Light> lights;

        public World(ColorRgb background)
        {
            this.BackgroundColor = background;
            this.objects = new List<IGeometricObject>();
            this.lights = new List<Light>();
        }

        public void Add(IGeometricObject obj)
        {
            objects.Add(obj);
        }

        public void AddLight(Light light)
        {
            lights.Add(light);
        }

        public HitInfo TraceRay(Ray ray)
        {
            double lastDistance = Ray.Huge;
            IGeometricObject hitObject = null;
            IntersectionInfo lastInfo = default(IntersectionInfo);
            IntersectionInfo closestInfo = default(IntersectionInfo);

            foreach (var obj in objects)
            {
                double distance = obj.Intersection(ray, ref lastInfo);
                if (distance > Ray.Epsilon && distance < lastDistance)
                { lastDistance = distance; closestInfo = lastInfo; hitObject = obj; }
            }

            if (hitObject != null) // jeśli trafiliśmy cokolwiek
            {
                HitInfo info = new HitInfo();
                info.Ray = ray;
                info.World = this;
                info.HitPoint = ray.Follow(lastDistance);
                info.Normal = closestInfo.Normal;
                info.Material = closestInfo.Material;
                return info;
            }

            return null;
        }

        public bool AnyObstacleBetween(Vector3 pointA, Vector3 pointB)
        {
            // odległość od cieniowanego punktu do światła
            Vector3 vectorAB = pointB - pointA;

            // promień (półprosta) z cieniowanego punktu w kierunku światła
            Ray rayAB = new Ray(pointA, vectorAB);

            return AnyObstacleBefore(pointB, rayAB);
        }

        public bool AnyObstacleBefore(Vector3 pointB, Ray rayAB)
        {
            double distAB = (rayAB.Origin - pointB).Length;
            IntersectionInfo ignoredInfo = default(IntersectionInfo);
            foreach (var obj in objects)
            {
                // jeśli jakiś obiekt jest na drodze promienia oraz trafienie
                // nastąpiło bliżej niż odległość punktu do światła,
                // obiekt jest w cieniu
                double intersection = obj.Intersection(rayAB, ref ignoredInfo);
                if (intersection > Ray.Epsilon && intersection < distAB)
                { return true; }
            }

            // obiekt nie jest w cieniu
            return false;
        }

        public ColorRgb BackgroundColor { get; private set; }
        public List<IGeometricObject> Objects { get { return objects; } }
        public List<Light> Lights { get { return lights; } }
    }
}
