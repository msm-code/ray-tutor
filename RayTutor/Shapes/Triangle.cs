namespace RayTutor
{
    /*
    class Triangle : IShape, ISamplableShape
    {
        // algorithm: http://www.blackpawn.com/texts/pointinpoly/default.html

        Vector2 a;
        Vector2 b;
        Vector2 c;

        Vector2 vB;
        Vector2 vC;

        double dotCC;
        double dotBC;
        double dotBB;

        public Triangle(Vector2 a, Vector2 b, Vector2 c)
        {
            this.a = a;
            this.b = b;
            this.c = c;

            this.vB = b - a;
            this.vC = c - a;

            this.dotBB = vB.Dot(vB);
            this.dotCC = vC.Dot(vC);
            this.dotBC = vB.Dot(vC);
        }

        public bool Contains(Vector2 p)
        {
            Vector2 vP = p - a;
            double dotBP = vB.Dot(vP);
            double dotCP = vC.Dot(vP);

            // barycentric
            double denominator = (dotCC * dotBB - dotBC * dotBC);
            double u = (dotBB * dotCP - dotBC * dotBP) / denominator;
            double v = (dotCC * dotBP  - dotBC * dotCP) / denominator;

            // Check if point is in triangle
            return (u >= 0) && (v >= 0) && (u + v < 1);
        }

        public Vector2 SamplePoint(Vector2 sample)
        {
            if (sample.Y > sample.X)
            {
                double tmp = sample.Y;
                sample.Y = sample.X;
                sample.X = tmp;
            }

            return a + vB * sample.X + vC * sample.Y;
        }
    }*/
}
