namespace RayTutor
{
    class ThinLens : ICamera
    {
        OrthonormalBasis onb;
        Vector3 origin;
        double distance;
        DiskDistributor distributor;
        double lensRadius;
        double focal;

        public ThinLens(Vector3 origin, Vector3 lookAt,
            Vector3 up, double distance, DiskDistributor distributor,
            double lensRadius, double focal)
        {
            this.onb = new OrthonormalBasis(origin - lookAt, up);
            this.origin = origin;
            this.distance = distance;
            this.distributor = distributor;
            this.lensRadius = lensRadius;
            this.focal = focal;
        }

        public Ray GetRayTo(Vector2 relativeLocation)
        {
            Vector2 focalPoint = distributor.Single() * lensRadius;
            Vector3 focalOrigin = origin + onb * new Vector3(focalPoint.X, focalPoint.Y, 0);

            return new Ray(focalOrigin, RayDirection(relativeLocation, focalPoint));
        }

        Vector3 RayDirection(Vector2 pixelP, Vector2 lensP)
        {
            Vector2 v = pixelP * focal / distance;
            Vector3 direction = onb * new Vector3(v.X - lensP.X, v.Y - lensP.Y, -focal);

            return direction;
        }
    }
}
