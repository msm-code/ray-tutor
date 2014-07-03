using System;
namespace RayTutor.Areas
{
    class SphereArea : IArea
    {
        Vector3 position;
        double r;
        Random rnd;

        public SphereArea(double x, double y, double z, double r)
            : this(new Vector3(x, y, z), r) { }

        public SphereArea(Vector3 point, double r)
        {
            this.position = point;
            this.r = r;
            this.rnd = new Random();
        }

        public Vector3 SamplePoint()
        {
            return position + new Vector3(rnd.NextDouble() * 2 - 1, rnd.NextDouble() * 2 - 1, rnd.NextDouble() * 2 - 1);
        }
    }
}
