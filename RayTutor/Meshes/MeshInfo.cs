using System.Collections.Generic;
using System.Linq;

namespace RayTutor.Meshes
{
    struct Face
    {
        int a, b, c;

        public Face(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public int A { get { return a; } }
        public int B { get { return b; } }
        public int C { get { return c; } }
    }

    class MeshInfo
    {
        public MeshInfo()
        {
            Verticles = new List<Vector3>();
            Faces = new List<Face>();
            Normals = new List<Vector3>();
        }

        public void AddVertex(double x, double y, double z)
        {
            Verticles.Add(new Vector3(x, y, z));
        }

        public void AddFace(int a, int b, int c)
        {
            Faces.Add(new Face(AbsoluteNdx(a), AbsoluteNdx(b), AbsoluteNdx(c)));
        }

        int AbsoluteNdx(int n)
        {
            return n < 0 ? Verticles.Count - n : n - 1;
        }

        public void ComputeNormals()
        {
            var normalsForVerticles = new List<List<Vector3>>(Verticles.Count);
            Normals = new List<Vector3>(Verticles.Count);
            for (int i = 0; i < Verticles.Count; i++)
            {
                normalsForVerticles.Add(new List<Vector3>());
                Normals.Add(new Vector3());
            }

            foreach (var face in Faces)
            {
                Vector3 normal = GetNormal(face);
                normalsForVerticles[face.A].Add(normal);
                normalsForVerticles[face.B].Add(normal);
                normalsForVerticles[face.C].Add(normal);
            }

            for(int i = 0; i < normalsForVerticles.Count; i++)
            {
                Normals[i] = normalsForVerticles[i].Aggregate((x, y)=>x+y).Normalized;
            }
        }

        Vector3 GetNormal(Face face)
        {
            Vector3 edgeBA = Verticles[face.B] - Verticles[face.A];
            Vector3 edgeCA = Verticles[face.C] - Verticles[face.A];
            return Vector3.Cross(edgeBA, edgeCA).Normalized;
        }

        public List<Vector3> Verticles { get; private set; }
        public List<Vector3> Normals { get; private set; }
        public List<Face> Faces { get; private set; }
    }
}
