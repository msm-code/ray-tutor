namespace RayTutor
{
class ThinLens : ICamera
{
    OrthonormalBasis onb;
    Vector3 origin;
    Vector2 scale;
    double distance;
    Sampler distributor;
    double lensRadius;
    double focal;

    public ThinLens(Vector3 origin,
        Vector3 lookAt,
        Vector3 up,
        Vector2 scale,
        double distance, 
        Sampler distributor,
        double lensRadius,
        double focal)
    {
        this.onb = new OrthonormalBasis(origin - lookAt, up);
        this.origin = origin;
        this.scale = scale;
        this.distance = distance;
        this.distributor = distributor;
        this.lensRadius = lensRadius;
        this.focal = focal;
    }

    public Ray GetRayTo(Vector2 relativePosition)
    {
        Vector2 pixelPosition = new Vector2(relativePosition.X * scale.X, relativePosition.Y * scale.Y);
        Vector2 lensPoint = distributor.Single() * lensRadius;
        Vector3 rayOrigin = origin + onb * new Vector3(lensPoint.X, lensPoint.Y, 0);

        return new Ray(rayOrigin, RayDirection(pixelPosition, lensPoint));
    }

    Vector3 RayDirection(Vector2 pixelPosition, Vector2 lensPoint)
    {

        Vector2 p = pixelPosition * focal / distance;
        return onb * new Vector3(p.X - lensPoint.X, p.Y - lensPoint.Y, -focal);
    }
}
}
