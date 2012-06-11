using System.Drawing;

namespace RayTutor
{
    interface IMaterial
    {
        ColorRgb Radiance(Raytracer tracer, LightInfo light, HitInfo hit);
    }
}
