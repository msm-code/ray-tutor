namespace RayTutor
{
    class Pinhole : ICamera
    {
        OrthonormalBasis onb;
        Vector3 origin;
        double distance;

        public Pinhole(Vector3 origin, Vector3 lookAt,
            Vector3 up, double distance)
        {
            this.onb = new OrthonormalBasis(origin - lookAt, up);
            this.origin = origin;
            this.distance = distance;
        }

        public Ray GetRayTo(Vector2 relativeLocation)
        {
            return new Ray(origin, RayDirection(relativeLocation));
        }

        Vector3 RayDirection(Vector2 v)
        {
            return onb * new Vector3(v.X, v.Y, -distance);
        }
    }
}
