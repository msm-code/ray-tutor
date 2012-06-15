using RayTutor.Meshes;
using System.Collections.Generic;
namespace RayTutor.Geomerty
{
    class Mesh : GeometricObject
    {
        List<Face> faces;

        public Mesh(List<FaceData> faceData, double scale, IMaterial material)
        {
            base.Material = material;
            this.faces = new List<Face>();

            foreach (var data in faceData)
            {
                this.faces.Add(new Face(data.A * scale, data.B * scale, data.C * scale, material));
            }
        }

        public override bool HitTest(Ray ray, ref double distance, ref Vector3 normal)
        {
            foreach (var face in faces)
            {
                if (face.HitTest(ray, ref distance, ref normal)) { return true; }
            }

            return false;
        }
    }
}
