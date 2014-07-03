using System.IO;
using System.Collections.Generic;
using System;
namespace RayTutor.Meshes
{
    class ObjMeshLoader
    {
        delegate void LoaderAction(MeshInfo context, string[] args);

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
            MeshInfo context = new MeshInfo();

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

            return context;
        }

        void VertexAction(MeshInfo ctx, string[] args)
        {
            ctx.AddVertex(ReadD(args[0]), ReadD(args[1]), ReadD(args[2]));
        }

        void FaceAction(MeshInfo ctx, string[] args)
        {
            for (int i = 1; i < args.Length - 1; i++)
            { ctx.AddFace(ReadI(args[0]), ReadI(args[i]), ReadI(args[i + 1])); }
        }

        int ReadI(string text) { return int.Parse(text); }

        double ReadD(string text) { return double.Parse(text, System.Globalization.CultureInfo.InvariantCulture); }

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
