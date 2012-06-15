using System.Drawing;
using System.Collections.Generic;
namespace RayTutor
{
    class Raytracer
    {
        int maxDepth;

        public Raytracer(int maxDepth)
        {
            this.maxDepth = maxDepth;
        }

        public ColorRgb TraceRay(World world, Ray ray, int currentDepth)
        {
            TraceCount++;
            if (currentDepth >= maxDepth) { return ColorRgb.Black; }

            HitInfo hit = world.TraceRay(ray);
            hit.Depth = currentDepth + 1;

            if (hit.HitObject == null) { return world.BackgroundColor; }

            ColorRgb finalColor = ColorRgb.Black;
            IMaterial material = hit.HitObject.Material;

            foreach (var light in world.Lights)
            {
                Vector3 lightRayOrigin = light.Sample(hit.HitPoint);
                LightInfo lightInfo = new LightInfo(lightRayOrigin, light.Color);

                finalColor += material.Radiance(this, lightInfo, hit);
            }

            return finalColor;
        }

        public int TraceCount { get; set; }
    }
}
