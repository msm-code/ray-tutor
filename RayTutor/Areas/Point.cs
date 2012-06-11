namespace RayTutor
{
    class Point : IArea
    {
        Vector3 position;

        public Point(double x, double y, double z)
            : this(new Vector3(x, y, z)) { }

        public Point(Vector3 point)
        {
            this.position = point;
        }

        public Vector3 SamplePoint()
        {
            return position;
        }
    }
}
