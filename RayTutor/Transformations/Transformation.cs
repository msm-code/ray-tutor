using RayTutor.Geomerty;
namespace RayTutor.Transformations
{
    class Transformation
    {
        Matrix invTransform;

        public Transformation()
        {
            invTransform = Matrix.Identity;
        }

        public TransformedObject Transform(GeometricObject obj)
        {
            return new TransformedObject(invTransform, obj);
        }

        public Transformation Scale(double x, double y, double z)
        {
            invTransform *= Matrix.Scale(1 / x, 1 / y, 1 / z);
            return this;
        }

        public Transformation Translate(double x, double y, double z)
        {
            invTransform *= Matrix.Translate(-x, -y, -z);
            return this;
        }

        public Transformation RotateX(double angle)
        {
            invTransform *= Matrix.RotateX(-angle);
            return this;
        }

        public Transformation RotateY(double angle)
        {
            invTransform *= Matrix.RotateY(-angle);
            return this;
        }

        public Transformation RotateZ(double angle)
        {
            invTransform *= Matrix.RotateZ(-angle);
            return this;
        }

        public Matrix Matrix { get { return invTransform; } }
    }
}
