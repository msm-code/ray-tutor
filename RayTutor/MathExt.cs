namespace RayTutor
{
    static class MathExt
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        public static double ClampTo(this double value, double min, double max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}
