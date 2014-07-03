using System;
namespace RayTutor
{
    class Light
    {
        Vector3 center;
        float radius;
        Sampler sampler;
        ColorRgb color;

        // Konstruktor udający światło punktowe
        public Light(ColorRgb color, Vector3 position)
            : this(color, position, null, 0) { }

        // pełny konstruktor
        public Light(ColorRgb color, Vector3 center, Sampler sampler, float radius)
        {
            this.center = center;
            this.radius = radius;
            this.sampler = sampler;
            this.color = color;
        }

        public Vector3 Sample()
        {
            if (radius == 0) { return center; } // symulowanie światła punktowego

            var sample = sampler.Single();
            return center + RemapSampleToUnitSphere(sample) * radius;
        }

        Vector3 RemapSampleToUnitSphere(Vector2 sample)
        {
            double z = 2 * sample.X - 1;
            double t = 2 * Math.PI * sample.Y;
            double r = Math.Sqrt(1 - z * z);
            return new Vector3(
                r * Math.Cos(t),
                r * Math.Sin(t),
                z);
        }

        public ColorRgb Color { get { return color; } }
    }
}
