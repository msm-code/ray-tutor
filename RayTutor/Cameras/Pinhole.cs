namespace RayTutor
{
    class Pinhole : ICamera
    {
        OrthonormalBasis onb;
        Vector3 origin;
        Vector2 scale;
        double distance;

        public Pinhole(Vector3 origin,
            Vector3 lookAt,
            Vector3 up,
            Vector2 scale, // !
            double distance)
        {
            this.onb = new OrthonormalBasis(origin - lookAt, up);
            this.origin = origin;
            this.scale = scale; // !
            this.distance = distance;
        }

        public Ray GetRayTo(Vector2 relLoc)
        {
            Vector2 vpLoc = new Vector2(relLoc.X * scale.X, relLoc.Y * scale.Y);
            return new Ray(origin, RayDirection(vpLoc));
        }

        Vector3 RayDirection(Vector2 v)
        {
            return onb * new Vector3(v.X, v.Y, -distance);
        }
    }
}
