using System.Collections.Generic;
using System.Linq;

namespace RayTutor.Meshes
{
    class ObjLoaderContext
    {
        public ObjLoaderContext()
        {
            Verticles = new List<Vector3>();
            Faces = new List<FaceData>();
        }

        public List<Vector3> Verticles { get; private set; }
        public List<FaceData> Faces { get; private set; }
    }
}
