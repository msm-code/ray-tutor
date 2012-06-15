namespace RayTutor.Geomerty
{
    class Face : GeometricObject
    {
        Vector3 vertexA;
        Vector3 vertexB;
        Vector3 vertexC;
        Vector3 edgeAB;
        Vector3 edgeAC;
        Vector3 normal;

        public Face(Vector3 vertexA, Vector3 vertexB, Vector3 vertexC, IMaterial material)
        {
            base.Material = material;

            this.vertexA = vertexA;
            this.vertexB = vertexB;
            this.vertexC = vertexC;
            this.edgeAB = vertexB - vertexA;
            this.edgeAC = vertexC - vertexA;
            this.normal = Vector3.Cross(edgeAB, edgeAC).Normalized;
        }

        public override bool HitTest(Ray ray, ref double distance, ref Vector3 normal)
        {
            Vector3 pVec = Vector3.Cross(ray.Direction, edgeAC);
            double determinant = pVec.Dot(edgeAB);

            if (determinant < Ray.Epsilon)
            { return false; }
            double invDet = 1 / determinant;

            Vector3 tVec = ray.Origin - vertexA;
            Vector3 qVec = Vector3.Cross(tVec, edgeAB);

            double u = tVec.Dot(pVec) * invDet;
            double v = ray.Direction.Dot(qVec) * invDet;

            if (u < 0 || v < 0 || u + v > 1) { return false; }

            distance = edgeAC.Dot(qVec) * invDet;
            normal = this.normal;

            return distance > Ray.Epsilon;
        }
    }
}
