using System.Drawing;

namespace RayTutor
{
    abstract class GeometricObject
    {
        public IMaterial Material { get; set; }

        public abstract bool HitTest(Ray ray, ref double distance, ref Vector3 normal);
    }
}
