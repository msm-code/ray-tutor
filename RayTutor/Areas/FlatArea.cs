namespace RayTutor
{
    class FlatArea : IArea
    {
        Vector3 origin;
        ISamplableShape shape;
        OrthonormalBasis inverse;
        Sampler distributor;

        public FlatArea(ISamplableShape shape, Vector3 origin, OrthonormalBasis inverse, Sampler distributor)
        {
            this.shape = shape;
            this.inverse = inverse;
            this.distributor = distributor;
            this.origin = origin;
        }

        public Vector3 SamplePoint()
        {
            Vector2 sample = distributor.Single();
            Vector2 shapePoint = shape.SamplePoint(sample);

            return origin + inverse * new Vector3(shapePoint.X, 0, shapePoint.Y);
        }
    }
}
