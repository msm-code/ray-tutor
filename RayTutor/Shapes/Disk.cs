namespace RayTutor
{
    class Disk : IShape, ISamplableShape
    {
        Vector2 origin;
        double radius;

        public Disk(Vector2 origin, double radius)
        {
            this.origin = origin;
            this.radius = radius;
        }

        public bool Contains(Vector2 point)
        {
            Vector2 dist = point - origin;
            return dist.Length < radius;
        }

        public Vector2 SampleShapePoint(Vector2 sample)
        {
            Vector2 diskSample = DiskDistributor.MapSample(sample);

            return origin + diskSample * radius;
        }
    }
}
