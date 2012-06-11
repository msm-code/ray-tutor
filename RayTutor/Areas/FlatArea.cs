namespace RayTutor
{
    class FlatArea : IArea
    {
        Vector3 origin;
        ISamplableShape shape;
        OrthonormalBasis inverse;
        SquareDistributor distributor;

        public FlatArea(ISamplableShape shape, Vector3 origin, OrthonormalBasis inverse, SquareDistributor distributor)
        {
            this.shape = shape;
            this.inverse = inverse;
            this.distributor = distributor;
            this.origin = origin;
        }

        public Vector3 SamplePoint()
        {
            Vector2 sample = distributor.Single();
            Vector2 shapePoint = shape.SampleShapePoint(sample);

            return origin + inverse * new Vector3(shapePoint.X, 0, shapePoint.Y);
        }
    }
}
