using System.Drawing;

namespace RayTutor
{
    interface IMaterial
    {
        ColorRgb Shade(Raytracer tracer, HitInfo hit);
    }
}
