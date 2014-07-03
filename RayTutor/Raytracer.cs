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
            if (hit == null) { return world.BackgroundColor; }

            hit.Depth = currentDepth + 1;

            ColorRgb finalColor = ColorRgb.Black;
            IMaterial material = hit.Material;

            finalColor += material.Shade(this, hit);

            return finalColor;
        }

        public int TraceCount { get; set; }
    }
}
