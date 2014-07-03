using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RayTutor.Meshes;
using RayTutor.Geomerty;
using RayTutor.Transformations;
using RayTutor.Areas;

namespace RayTutor
{
    class Program
    {
        static void Main(string[] args)
        {
            const int SampleCt = 25;
            const int MaxDepth = 5;
            const int ImageSize = 256;

            World world = new World(new ColorRgb(0.0, 0, 0));

            Sampler antiAlias = new Sampler(
                new Regular(),
                new SquareDistribution(),
                SampleCt,
                1);

            Func<Color, IMaterial> makeMat = c => new Phong(c, 0.9, 1, 500);
            IMaterial redMat = new Reflective(Color.LightCoral, 0.4, 1, 1000, 0.6);
            IMaterial greenMat = new Reflective(Color.LightGreen, 0.4, 1, 1000, 0.6);
            IMaterial blueMat = new Reflective(Color.LightBlue, 0.4, 1, 1000, 0.6);

            IMaterial banii = new Phong(Color.LightBlue, 0.9, 1, 100);

            // Trzy różnokolorowe kule
            List<IGeometricObject> objs = new List<IGeometricObject>();

            /*var loader = new ObjMeshLoader();
            var mesh = loader.Load("bunny69k.obj");
            mesh.ComputeNormals();
            foreach (var face in mesh.Faces)
            {
                objs.Add(new Triangle(
                    mesh.Verticles[face.A], mesh.Verticles[face.B], mesh.Verticles[face.C],
                    mesh.Normals[face.A], mesh.Normals[face.B], mesh.Normals[face.C],
                    whiteMat));
            }*/

            /*var grid = new Grid(objs);
            var trans1 = new Transformation()
                .Translate(0, -5, 0)
                .RotateY(Math.PI * 3 / 2)
                .Translate(-5, 0, 10);

            world.Add(new Transformed(trans1, grid, redMat));

            var trans2 = new Transformation()
                .Translate(0, -5, 0)
                .RotateY(Math.PI * 3 / 2)
                .Translate(5, 0, 10);
            world.Add(new Transformed(trans2, grid, greenMat));

            var trans3 = new Transformation()
                .Translate(0, -5, 0)
                .RotateY(Math.PI - 0.1)
                .Translate(0, 0, 15);
            world.Add(new Transformed(trans3, grid, banii));*/

world.Add(new Sphere(new Vector3(-3.5, 0, 0), 2,
    new Reflective(Color.LightCoral, 0.7, 0.5, 1000, 0.3)));
world.Add(new Sphere(new Vector3(3.5, 0, 0), 2,
    new Reflective(Color.LightGreen, 0.7, 0.5, 1000, 0.3)));
world.Add(new Sphere(new Vector3(0, 0, 3.5), 2,
    new Reflective(Color.LightBlue, 0.7, 0.5, 1000, 0.3)));
world.Add(new Sphere(new Vector3(0, 0, -3.5), 2,
    new Transparent(Color.LightBlue, 0.1, 0, 0, 0.3, 1.05, 0.9)));

world.Add(new Plane(new Vector3(0, -2, 0), new Vector3(0, 1, 0),
    new Reflective(Color.White, 0.4, 0, 1000, 0.6, true)));

world.AddLight(new Light(ColorRgb.White, new Vector3(-5, 5, -3)));

ICamera camera = new Pinhole(new Vector3(6, 2, -15),
    new Vector3(0, 0.3, 0),
    new Vector3(0, -1, 0),
    new Vector2(0.7, 0.7*1024/1280),
    2);

            var renderer = new StaticRenderer(MaxDepth);

            // Raytracing!
            Bitmap image = renderer.Raytrace(world, camera, new Size(1280/2, 1024/2), antiAlias);

            image.Save("D:\\raytraced05.png");
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


            /*            Sampler test = new Sampler(new PureRandom(0), new SquareDistribution(), 100, 1);
                        using (Bitmap bm = new Bitmap(300, 300))
                        {
                            using (Graphics g = Graphics.FromImage(bm))
                            {
                                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                                for(int i = 0; i < 100; i++)
                                {
                                    Vector2 sample = test.Single();

                                    double z = 2 * sample.X - 1;
                                    double t = 2 * Math.PI * sample.Y;
                                    double r = Math.Sqrt(1 - z * z);
                                    Vector3 v = new Vector3(
                                        r * Math.Cos(t),
                                        r * Math.Sin(t),
                                        z);


                                    g.FillEllipse(Brushes.Black,
                                        (int)(v.Z * 100) + 120 - 5,
                                        (int)(v.X * 100) + 120 - 5,
                                        10, 10);
                                }
                            }

                            bm.Save("D:\\sampler.png");
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
