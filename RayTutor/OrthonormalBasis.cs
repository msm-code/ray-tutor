namespace RayTutor
{
    class OrthonormalBasis
    {
        Vector3 u;
        Vector3 v;
        Vector3 w;

        private OrthonormalBasis() { }

        public OrthonormalBasis(Vector3 front, Vector3 up)
        {
            w = front;
            w = w.Normalized;
            u = Vector3.Cross(up, w);
            u = u.Normalized;
            v = Vector3.Cross(w, u);
        }

        public static Vector3 operator *(OrthonormalBasis onb, Vector3 v)
        {
            return (onb.u * v.X + onb.v * v.Y + onb.w * v.Z);
        }

        public OrthonormalBasis Invert()
        {
            double[,] m = new double[3, 3];
            m[0, 0] = u.X;
            m[0, 1] = u.Y;
            m[0, 2] = u.Z;

            m[1, 0] = v.X;
            m[1, 1] = v.Y;
            m[1, 2] = v.Z;

            m[2, 0] = w.X;
            m[2, 1] = w.Y;
            m[2, 2] = w.Z;

            Matrix3 mat = new Matrix3(m);
            mat = mat.Inversed();

            OrthonormalBasis r = new OrthonormalBasis();

            r.u.X = mat[0, 0];
            r.u.Y = mat[0, 1];
            r.u.Z = mat[0, 2];

            r.v.X = mat[1, 0];
            r.v.Y = mat[1, 1];
            r.v.Z = mat[1, 2];

            r.w.X = mat[2, 0];
            r.w.Y = mat[2, 1];
            r.w.Z = mat[2, 2];

            return r;
        }
    }
}
