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
            this.basis = new OrthonormalBasis(normal, new Vector3(0, -1, 0));

            base.Material = mat;
        }

        public FlatObject(IShape shape, Vector3 point, Vector3 normal, IMaterial mat)
        {
            this.point = point;
            this.shape = shape;
            this.normal = normal;
            this.basis = new OrthonormalBasis(normal, new Vector3(0, 1, 0));

            this.Material = mat;
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

        public FlatArea CreateArea(SquareDistributor distributor)
        {
            ISamplableShape samplable = shape as ISamplableShape;

            if (samplable == null)
            { throw new System.InvalidOperationException("Shape is not samplable"); }

            return new FlatArea(samplable, basis.Invert(), distributor);
        }

        public static FlatObject Triangle(Vector3 a, Vector3 b, Vector3 c, IMaterial material)
        {
            Vector3 normal = Vector3.Cross((b - a), (c - a)).Normalized;

            FlatObject result = new FlatObject(a, normal, material);

            result.shape = new Triangle(result.ToPlaneCoordinates(a),
                result.ToPlaneCoordinates(b),
                result.ToPlaneCoordinates(c));

            return result;
        }

        public static FlatObject Disk(Vector3 point, Vector3 normal, double radius, IMaterial material)
        {
            FlatObject result = new FlatObject(point, normal, material);

            result.shape = new Disk(result.ToPlaneCoordinates(point), radius);

            return result;
        }

        public static FlatObject Rectangle(Vector3 center, Vector2 halfSize, double rotation, Vector3 normal, IMaterial material)
        {
            FlatObject result = new FlatObject(center, normal, material);

            result.shape = new Rectangle(
                result.ToPlaneCoordinates(center),
                halfSize, rotation);

            return result;
        }

        Vector2 ToPlaneCoordinates(Vector3 point)
        {
            Vector3 projected = basis * point;
            return new Vector2(projected.X, projected.Y);
        }
    }
}
