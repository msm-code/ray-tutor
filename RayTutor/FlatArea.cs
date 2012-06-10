namespace RayTutor
{
    class FlatArea : IArea
    {
        ISamplableShape shape;
        OrthonormalBasis inverse;
        SquareDistributor distributor;

        public FlatArea(ISamplableShape shape, OrthonormalBasis inverse, SquareDistributor distributor)
        {
            this.shape = shape;
            this.inverse = inverse;
            this.distributor = distributor;
        }

        public Vector3 SamplePoint()
        {
            Vector2 sample = distributor.Single();
            Vector2 shapePoint = shape.SampleShapePoint(sample);

            return inverse * new Vector3(shapePoint.X, shapePoint.Y, 0);
        }
    }
}
