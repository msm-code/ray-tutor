using System.Drawing;
using System.Collections.Generic;

namespace RayTutor
{
    class World
    {
        List<GeometricObject> objects;
        List<Light> lights;

        public World(ColorRgb background)
        {
            this.BackgroundColor = background;
            this.objects = new List<GeometricObject>();
            this.lights = new List<Light>();
        }

        public void Add(GeometricObject obj)
        {
            objects.Add(obj);
        }

        public void AddLight(Light light)
        {
            lights.Add(light);
        }

        public HitInfo TraceRay(Ray ray)
        {
            HitInfo result = new HitInfo();
            Vector3 normal = default(Vector3);
            double minimalDistance = Ray.Huge; // najbliższe trafienie
            double lastDistance = 0; // zmienna pomocnicza, ostatnia odległość

            foreach (var obj in objects)
            {
                if (obj.HitTest(ray, ref lastDistance, ref normal) &&
                    lastDistance < minimalDistance) // jeśli najbliższe trafienie
                {
                    minimalDistance = lastDistance; // nowa najmniejsza odległość
                    result.HitObject = obj; // nowy trafiony obiekt
                    result.Normal = normal;
                }
            }

            if (result.HitObject != null) // jeśli trafiliśmy cokolwiek
            {
                result.HitPoint = ray.Follow(minimalDistance);
                result.Ray = ray;
                result.World = this;
            }

            return result;
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
            double currDistance = Ray.Huge;
            double distAB = (rayAB.Origin - pointB).Length;
            Vector3 ignoredNormal = default(Vector3);
            foreach (var obj in objects)
            {
                // jeśli jakiś obiekt jest na drodze promienia oraz trafienie
                // nastąpiło bliżej niż odległość punktu do światła,
                // obiekt jest w cieniu
                if (obj.HitTest(rayAB, ref currDistance, ref ignoredNormal) && currDistance < distAB)
                { return true; }
            }

            // obiekt nie jest w cieniu
            return false;
        }

        public ColorRgb BackgroundColor { get; private set; }
        public List<GeometricObject> Objects { get { return objects; } }
        public List<Light> Lights { get { return lights; } }
    }
}
