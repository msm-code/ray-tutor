namespace RayTutor
{
    class Pinhole : ICamera
    {
        OrthonormalBasis onb;
        Vector3 origin;
        Vector2 zoom;
        double distance;

        public Pinhole(Vector3 origin,
            Vector3 lookAt,
            Vector3 up,
            Vector2 zoom,
            double distance)
        {
            this.onb = new OrthonormalBasis(origin - lookAt, up);
            this.origin = origin;
            this.zoom = zoom;
            this.distance = distance;
        }

        public Ray GetRayTo(Vector2 relativeLoc)
        {
            Vector2 viewPlaneLocation = new Vector2(relativeLoc.X * zoom.X, relativeLoc.Y * zoom.Y);
            return new Ray(origin, RayDirection(viewPlaneLocation));
        }

        Vector3 RayDirection(Vector2 v)
        {
            return onb * new Vector3(v.X, v.Y, -distance);
        }
    }
}
