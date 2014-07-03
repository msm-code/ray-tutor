using System.Drawing;

namespace RayTutor
{
    class HitInfo
    {
        /// <summary>Trafiony obiekt lub null jeśli promień w nic nie trafił</summary>
        public IMaterial Material { get; set; }

        /// <summary>Referencja do świata który renderujemy</summary>
        public World World { get; set; }

        /// <summary>Normalna do punktu trafienia</summary>
        public Vector3 Normal { get; set; }

        /// <summary>Punkt trafienia (w koordynatach świata)</summary>
        public Vector3 HitPoint { get; set; }

        /// <summary>Promień który trafił obiekt</summary>
        public Ray Ray { get; set; }

        /// <summary>Zwiększana przy śledzeniu odbitego lub załamanego promienia</summary>
        public int Depth { get; set; }
    }
}
