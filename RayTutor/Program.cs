using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RayTutor.Meshes;
using RayTutor.Geomerty;

namespace RayTutor
{
    class Program
    {
        static void Main(string[] args)
        {
            const int SampleCt = 1;
            const int MaxDepth = 4;
            //const int ImageSize = 100;

            World world = new World(Color.PowderBlue);

            SquareDistributor antiAlias = new SquareDistributor(new Regular(), SampleCt, 1);

            IMaterial transparentMat = new Transparent(Color.White, 0.5, 2500, 0.1, 1.5, 0.75);
            IMaterial redMat = new PerfectReflective(Color.LightCoral, 0.1, 300, 0.9);
            IMaterial greenMat = new PerfectReflective(Color.LightGreen, 0.1, 300, 0.9);
            IMaterial blueMat = new PerfectReflective(Color.LightBlue, 0.1, 300, 0.9);
            IMaterial planeMat = new PerfectReflective(Color.White, 0.5, 300, 0.5);

            world.Add(new Sphere(new Vector3(40, 10, 20), 17, greenMat));
            world.Add(new Sphere(new Vector3(-3, 1.7, 4), 1, blueMat));
            world.Add(new Sphere(new Vector3(4, -3, 4), 1, redMat));

            ObjMeshLoader loader = new ObjMeshLoader();
            var meshData = loader.Load("mesh.obj");
            foreach (var face in meshData.Faces)
            {
                world.Add(new Face(face.A, face.B, face.C, transparentMat));
            }

            world.Add(new Plane(new Vector3(0, 0, 3), new Vector3(0, 0, 1), planeMat));

            world.AddLight(new Light(new Point(-10, 3, 10), ColorRgb.White));

            ICamera camera = new Pinhole(new Vector3(-14, -3, 3.5),
                new Vector3(0, 0, 9),
                new Vector3(0, 0, -1),
                new Vector2(1280/1024f, 1),
                2);

            Renderer renderer = new Renderer(MaxDepth);

            Bitmap image = renderer.Raytrace(world, camera, new Size(1280/2, 1024/2), antiAlias);

            // Zapisanie obrazka w jakimś miłym miejscu na dysku.
            image.Save("D:\\raytraced.png");

            #region junks

            /*
             * 
            /*world.Add(FlatShape.Triangle(new Vector3(3, -2, 2),
                new Vector3(-3, -2, 2),
                new Vector3(2, 3, 3), redMat));
            world.Add(FlatShape.Triangle(new Vector3(-3, -2, 2),
                new Vector3(-2, 3, 3),
                new Vector3(2, 3, 3), redMat));

            world.Add(FlatShape.Triangle(new Vector3(-2, 3, 3), 
                new Vector3(-3, -2, 2),
                new Vector3(-8, -2, 5), greenMat));

            world.Add(FlatShape.Triangle(new Vector3(8, -2, 5), 
                new Vector3(3, -2, 2),
                new Vector3(2, 3, 3), blueMat));*/


            /*ISampler test = new NRooks(64, 0);
            using (Bitmap b = new Bitmap(300, 300))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    var r = test.Sample();
                    foreach (var sample in r)
                    {
                        g.FillEllipse(Brushes.Black, (int)(sample.X * 300) - 5,
                            (int)(sample.Y * 300) - 5,
                            10, 10);
                    }
                }

                b.Save("D:\\sampler.png");
            }*/

            /*
            ISampler test = new Jittered(64, 0);
            DiskDistributor distributor = new DiskDistributor(test, 1);
            using (Bitmap b = new Bitmap(300, 300))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    var r = distributor.NextSet();
                    foreach (var sample in r)
                    {
                        g.FillEllipse(Brushes.Black, (int)((sample.X + 1) * 150) - 5,
                            (int)((sample.Y + 1) * 150) - 5,
                            10, 10);
                    }

                    g.DrawEllipse(Pens.Violet, 0, 0, 300, 300);
                }

                b.Save("D:\\sampler.png");
            }
             * 
             * */

            /*ISampler test = new Jittered(256, 0);
            HemisphereDistributor distributor = new HemisphereDistributor(test, 1, 10);
            using (Bitmap b = new Bitmap(300, 300))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    var r = distributor.NextSet();
                    foreach (var sample in r)
                    {
                        g.FillEllipse(Brushes.Black, (int)((sample.X + 1) * 150) - 5,
                            (int)((sample.Y + 1) * 150) - 5,
                            5, 5);
                    }

                    g.DrawEllipse(Pens.Violet, 0, 0, 299, 299);
                }

                b.Save("D:\\sampler.png");
            }*/
            #endregion
        }
    }
}
