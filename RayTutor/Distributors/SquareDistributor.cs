namespace RayTutor
{
    class SquareDistributor : Distributor<Vector2>
    {
        public SquareDistributor(ISampler sampler, int sampleCt, int setCt)
        {
            base.CreateSamples(sampler, (x) => x, sampleCt, setCt);
        }
    }
}
