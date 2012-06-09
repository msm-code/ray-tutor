using System.Drawing;

namespace RayTutor
{
    interface IMaterial
    {
        ColorRgb Radiance(Raytracer tracer, PointLight light, HitInfo hit);
    }
}
