namespace RayTutor
{
    class TmpArea : IArea
    {
        DiskDistributor distributor;
        Vector3 center;
        double area;

        public TmpArea(DiskDistributor distributor, Vector3 center, double area)
        {
            this.distributor = distributor;
            this.center = center;
            this.area = area;
        }

        public Vector3 SamplePoint()
        {
            Vector2 sample = distributor.Single();
            return center + new Vector3((sample.X - 0.5) * area, 0, (sample.Y - 0.5) * area);
        }
    }
}
