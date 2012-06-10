namespace RayTutor
{
    class FlatObject : GeometricObject
    {
        Vector3 point;
        Vector3 normal;
        OrthonormalBasis basis;
        IShape shape;

        private FlatObject(Vector3 anyPoint, Vector3 normal, IMaterial mat)
        {
            this.point = anyPoint;
            this.normal = normal;
            this.basis = new OrthonormalBasis(normal, new Vector3(0, 1, 0));

            base.Material = mat;
        }

        public override bool HitTest(Ray ray, ref double distance, ref Vector3 outNormal)
        {
            double t = (point - ray.Origin).Dot(normal) / ray.Direction.Dot(normal);

            if (t <= Ray.Epsilon) { return false; }

            Vector3 hitPoint = ray.Follow(t);

            Vector2 planeHit = ToPlaneCoordinates(hitPoint);
            if (!shape.Contains(planeHit)) { return false; }

            distance = t;
            outNormal = this.normal;
            return true;
        }

        public static FlatObject Triangle(Vector3 a, Vector3 b, Vector3 c, IMaterial mat)
        {
            Vector3 normal = Vector3.Cross((b - a), (c - a)).Normalized;

            FlatObject result = new FlatObject(a, normal, mat);

            result.shape = new TriangleShape(result.ToPlaneCoordinates(a),
                result.ToPlaneCoordinates(b),
                result.ToPlaneCoordinates(c));

            return result;
        }

        Vector2 ToPlaneCoordinates(Vector3 point)
        {
            Vector3 projected = basis * point;
            return new Vector2(projected.X, projected.Y);
        }
    }
}
