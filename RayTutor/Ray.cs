namespace RayTutor
{
    struct Ray
    {
        public const double Epsilon = 0.0001;
        public const double Huge = double.MaxValue;

        public Ray(Vector3 origin, Vector3 direction)
            : this()
        {
            this.Origin = origin;
            this.Direction = direction.Normalized;
        }

        public Vector3 Origin { get; set; }
        public Vector3 Direction { get; set; }

        public Vector3 Follow(double t)
        {
            return Origin + Direction * t;
        }
    }
}
