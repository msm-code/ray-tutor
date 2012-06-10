using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RayTutor
{
    class Program
    {
        static void Main(string[] args)
        {
            int sampleCt = 1;

            // Stworzenie świata (kolor tła = łagodny niebieski)
            World world = new World(Color.PowderBlue);

            SquareDistributor antiAlias = new SquareDistributor(new Regular(sampleCt), 1);
            SquareDistributor lightDist = new SquareDistributor(new Jittered(sampleCt, 1), 97);

            // Materiały
            IMaterial redMat = new Phong(Color.LightCoral, 0.8, 10);
            //new PerfectReflective(Color.LightCoral, 0.3, 30, 0.7);
            IMaterial greenMat = new Phong(Color.LightGreen, 0.8, 10);
            //new PerfectReflective(Color.LightGreen, 0.3, 30, 0.7);
            IMaterial blueMat = new PerfectReflective(Color.LightBlue, 0.3, 30, 0.7);
            IMaterial grayMat = new PerfectReflective(Color.Gray, 0.3, 30, 0.7);

            // Trójkąt
            /*FlatObject triangle = FlatObject.Triangle(
                new Vector3(5, 0.5, -1),
                new Vector3(0, 3.5, -1),
                new Vector3(-5, 0.5, -1),
                grayMat);*/

            FlatObject triangle = FlatObject.Triangle(new Vector3(2, -2, 1),
                new Vector3(4, 2, 0),
                new Vector3(6, -2, -1),
                greenMat);

            // Trzy różnokolorowe kule
            //world.Add(new Sphere(new Vector3(-4, 0, 0), 2, redMat));
            //world.Add(new Sphere(new Vector3(4, 0, 0), 2, greenMat));
            //world.Add(new Sphere(new Vector3(0, 0, 3), 2, blueMat));
            world.Add(new Plane(new Vector3(0, -2, 0), new Vector3(0, 1, 0), grayMat));

            world.Add(FlatObject.Disk(new Vector3(-4, 0, 0), new Vector3(1, 0.1, -1).Normalized, 2, redMat));
            //world.Add(FlatObject.Disk(new Vector3(4, 0, 0), new Vector3(0, 0.1, -1).Normalized, 2, greenMat));
            world.Add(triangle);
            //world.Add(FlatObject.Disk(new Vector3(0, 0, 3), new Vector3(0, 0.1, -1).Normalized, 2, blueMat));
            world.Add(FlatObject.Rectangle(new Vector3(0, 0, 3), new Vector2(1.5, 2), 0, new Vector3(0, 0.1, -1), blueMat));

            //world.AddLight(new Light(triangle.CreateArea(lightDist), Color.White));
            world.AddLight(new Light(new Point(0, 5, -5), Color.White));

            ICamera camera = new Pinhole(new Vector3(0, 1, -8),
                new Vector3(0, 0, 0),
                new Vector3(0, -1, 0),
                1);

            Raytracer tracer = new Raytracer();

            Bitmap image = tracer.Raytrace(world, camera, new Size(300, 300), antiAlias);

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

             /* 
            ISampler test = new Jittered(64, 0);
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
