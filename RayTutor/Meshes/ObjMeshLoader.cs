using System.IO;
using System.Collections.Generic;
using System;
namespace RayTutor.Meshes
{
    class ObjMeshLoader
    {
        delegate void LoaderAction(ObjLoaderContext context, string[] args);

        Dictionary<string, LoaderAction> actions;

        public ObjMeshLoader()
        {
            actions = new Dictionary<string, LoaderAction>();

            actions.Add("v", VertexAction);
            actions.Add("f", FaceAction);
        }

        public MeshInfo Load(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            { return Load(reader); }
        }

        public MeshInfo Load(StreamReader data)
        {
            ObjLoaderContext context = new ObjLoaderContext();

            foreach (var line in LogicalLines(data))
            {
                string lineData = line.Trim();
                if (lineData.Length == 0 || lineData.StartsWith("#")) { continue; }

                int actionLength = lineData.IndexOf(' ');
                string action = lineData.Substring(0, actionLength);
                string[] args = lineData.Substring(actionLength + 1)
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (!actions.ContainsKey(action)) { continue; }
                actions[action](context, args);
            }

            return new MeshInfo(context.Faces);
        }

        void VertexAction(ObjLoaderContext context, string[] args)
        {
            context.Verticles.Add(new Vector3(Read(args[0]), Read(args[1]), Read(args[2])));
        }

        void FaceAction(ObjLoaderContext ctx, string[] args)
        {
            for (int i = 1; i < args.Length - 1; i++)
            { AddFace(ctx, ReadVertex(args[0]), ReadVertex(args[i]), ReadVertex(args[i + 1])); }
        }

        void AddFace(ObjLoaderContext ctx, int ndxA, int ndxB, int ndxC)
        {
            Func<int, int> absoluteIndex = (x) => x < 0 ? ctx.Verticles.Count + x : x - 1;

            ctx.Faces.Add(new FaceData(
                ctx.Verticles[absoluteIndex(ndxA)],
                ctx.Verticles[absoluteIndex(ndxB)],
                ctx.Verticles[absoluteIndex(ndxC)]));
        }

        int ReadVertex(string text)
        {
            string vertex = text.Split('/')[0];
            return int.Parse(vertex);
        }

        double Read(string text) { return double.Parse(text, System.Globalization.CultureInfo.InvariantCulture); }

        IEnumerable<string> LogicalLines(StreamReader data)
        {
            string last = "";

            while (!data.EndOfStream)
            {
                string next = data.ReadLine();
                if (next.EndsWith("\\")) { last += next.TrimEnd('\\'); }
                else { yield return last + next; last = ""; }
            }

            yield return last;
        }
    }
}
