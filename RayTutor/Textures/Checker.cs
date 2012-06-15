using System;

namespace RayTutor
{
    class Checker : ITexture
    {
        public ColorRgb Get(HitInfo hitInfo)
        {
            Vector3 position = hitInfo.HitPoint;

            var sum = (int)(position.X - 100 + Ray.Epsilon) // lame
                + (int)(position.Y - 100 + Ray.Epsilon)
                + (int)(position.Z - 100 + Ray.Epsilon);

            if (sum % 2 != 0)
            { return ColorRgb.Black; }
            else { return ColorRgb.White; }
        }
    }
}
