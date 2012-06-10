namespace RayTutor
{
    class FlatArea : IArea
    {
        ISamplableShape shape;
        OrthonormalBasis inverse;

        public FlatArea(ISamplableShape shape, OrthonormalBasis inverse)
        {
            this.shape = shape;
            this.inverse = inverse;
        }

        public Vector3 SamplePoint()
        {
            Vector2 shapePoint = shape.SampleShapePoint();
            return inverse * new Vector3(shapePoint.X, shapePoint.Y, 0);
        }
    }
}
