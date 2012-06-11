using System.Collections.Generic;
namespace RayTutor
{
    class SquareDistributor : Distributor<Vector2>
    {
        public SquareDistributor(ISampler sampler, int setCt)
        {
            for (int i = 0; i < setCt; i++)
            { base.Add(sampler.Sample()); }
        }
    }
}
