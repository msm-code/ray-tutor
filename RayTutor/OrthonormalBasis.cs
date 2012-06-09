namespace RayTutor
{
    class OrthonormalBasis
    {
        Vector3 u;
        Vector3 v;
        Vector3 w;

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
    }
}
