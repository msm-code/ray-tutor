using RayTutor.Geomerty;
namespace RayTutor.Transformations
{
    class Transformation
    {
        Matrix invTransform;
        Matrix transform;

        public Transformation()
        {
            invTransform = Matrix.Identity;
            transform = Matrix.Identity;
        }

        public Transformation Scale(double x, double y, double z)
        {
            transform *= Matrix.Scale(x, y, z);
            invTransform *= Matrix.Scale(1 / x, 1 / y, 1 / z);
            return this;
        }

        public Transformation Translate(double x, double y, double z)
        {
            transform *= Matrix.Translate(x, y, z);
            invTransform *= Matrix.Translate(-x, -y, -z);
            return this;
        }

        public Transformation RotateX(double angle)
        {
            transform *= Matrix.RotateX(angle);
            invTransform *= Matrix.RotateX(-angle);
            return this;
        }

        public Transformation RotateY(double angle)
        {
            transform *= Matrix.RotateY(angle);
            invTransform *= Matrix.RotateY(-angle);
            return this;
        }

        public Transformation RotateZ(double angle)
        {
            transform *= Matrix.RotateZ(angle);
            invTransform *= Matrix.RotateZ(-angle);
            return this;
        }

        public Matrix InvMatrix { get { return invTransform; } }
        public Matrix Matrix { get { return transform; } }
    }
}
