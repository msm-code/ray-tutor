using System.Collections.Generic;

namespace RayTutor.Meshes
{
    class FaceData
    {
        public FaceData(Vector3 a, Vector3 b, Vector3 c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        public Vector3 A { get; private set; }
        public Vector3 B { get; private set; }
        public Vector3 C { get; private set; }
    }

    class MeshInfo
    {
        public MeshInfo(List<FaceData> faces)
        {
            this.Faces = faces;
        }

        public List<FaceData> Faces { get; private set; }
    }
}
