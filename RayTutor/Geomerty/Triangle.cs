namespace RayTutor.Geomerty
{
    class Triangle : IGeometricObject
    {
        IMaterial material;
        Vector3 vertexA;
        Vector3 edgeAB;
        Vector3 edgeAC;
        Vector3 normalA;
        Vector3 normalAB;
        Vector3 normalAC;

        public Triangle(Vector3 vertexA, Vector3 vertexB, Vector3 vertexC,
            IMaterial material)
        {
            this.material = material;

            this.vertexA = vertexA;
            this.edgeAB = vertexB - vertexA;
            this.edgeAC = vertexC - vertexA;

            this.normalA = Vector3.Cross(edgeAB, edgeAC).Normalized;
            this.normalAB = new Vector3();
            this.normalAC = new Vector3();

            this.BoundingBox = Aabb.Point(vertexA).Wrap(vertexB).Wrap(vertexC);
        }

        public Triangle(Vector3 vertexA, Vector3 vertexB, Vector3 vertexC,
            Vector3 normalA, Vector3 normalB, Vector3 normalC,
            IMaterial material)
        {
            this.material = material;

            this.vertexA = vertexA;
            this.edgeAB = vertexB - vertexA;
            this.edgeAC = vertexC - vertexA;

            this.normalA = normalA;
            this.normalAB = normalB - normalA;
            this.normalAC = normalC - normalA;

            this.BoundingBox = Aabb.Point(vertexA).Wrap(vertexB).Wrap(vertexC);
        }

        public double Intersection(Ray ray, ref IntersectionInfo info)
        {
            Vector3 pVec = Vector3.Cross(ray.Direction, edgeAC);
            double determinant = pVec.Dot(edgeAB);

            if (determinant < Ray.Epsilon)
            { return Ray.Huge; }
            double invDet = 1 / determinant;

            Vector3 aVec = ray.Origin - vertexA;
            Vector3 bVec = Vector3.Cross(aVec, edgeAB);

            double u = aVec.Dot(pVec) * invDet;
            double v = ray.Direction.Dot(bVec) * invDet;

            if (u < 0 || v < 0 || u + v > 1) { return Ray.Huge; }

            info.Material = this.material;
            info.Normal = this.normalA + normalAB * u + normalAC * v;
            return edgeAC.Dot(bVec) * invDet;
        }

        public Aabb BoundingBox { get; private set; }
    }
}
