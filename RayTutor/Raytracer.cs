using System.Drawing;
using System.Collections.Generic;
namespace RayTutor
{
    class Raytracer
    {
        const int MaxDepth = 200; // Maksymalna dopuszczalna głębokość rekurencji

        public Bitmap Raytrace(World world, ICamera camera, Size imageSize)
        {
            SquareDistributor singleSample = new SquareDistributor(new Regular(1), 1);
            return this.Raytrace(world, camera, imageSize, singleSample);
        }

        public Bitmap Raytrace(World world,
            ICamera camera,
            Size imageSize,
            SquareDistributor antiAlias)
        {
            Bitmap bmp = new Bitmap(imageSize.Width, imageSize.Height);

            int start = System.Environment.TickCount;

            for (int y = 0; y < imageSize.Height; y++)
            {
                for (int x = 0; x < imageSize.Width; x++)
                {
                    ColorRgb totalColor = ColorRgb.Black;
                    Vector2[] samples = antiAlias.Next();
                    foreach (var sample in samples)
                    {
                        // przeskalowanie x i y do zakresu [-1; 1]
                        Vector2 pictureCoordinates = new Vector2(
                            ((x + sample.X) / (double)imageSize.Width) * 2 - 1,
                            ((y + sample.Y) / (double)imageSize.Height) * 2 - 1);

                        // wysłanie promienia i sprawdzenie w co właściwie trafił
                        Ray ray = camera.GetRayTo(pictureCoordinates);

                        totalColor += TraceRay(world, ray) / (double)samples.Length;
                    }

                    bmp.SetPixel(x, y, StripColor(totalColor));
                }

                System.Console.WriteLine("{0}%, {1} Rays Tracked", (100 * y) / (float)imageSize.Height, ct);
            }

            System.Console.WriteLine("{0} ms per ray", (double)(System.Environment.TickCount - start) / ct);

            return bmp;
        }

        int ct;
        public ColorRgb TraceRay(World world, Ray ray)
        { return this.TraceRay(world, ray, 0); }

        public ColorRgb TraceRay(World world, Ray ray, int currentDepth)
        {
            ct++;
            if (currentDepth > MaxDepth) { return ColorRgb.Black; }

            HitInfo hit = world.TraceRay(ray);
            hit.Depth = currentDepth + 1;

            if (hit.HitObject == null) { return world.BackgroundColor; }

            ColorRgb finalColor = ColorRgb.Black;
            IMaterial material = hit.HitObject.Material;

            foreach (var light in world.Lights)
            {
                Ray rayFromLight = light.SampleRay(hit.HitPoint);
                if (world.AnyObstacleBefore(hit.HitPoint, rayFromLight)) { continue; }

                LightInfo lightInfo = new LightInfo(rayFromLight.Origin, light.Color);

                finalColor += material.Radiance(this, lightInfo, hit);
            }

            return finalColor;
        }

        Color StripColor(ColorRgb colorInfo)
        {
            colorInfo.R = colorInfo.R < 0 ? 0 : colorInfo.R > 1 ? 1 : colorInfo.R;
            colorInfo.G = colorInfo.G < 0 ? 0 : colorInfo.G > 1 ? 1 : colorInfo.G;
            colorInfo.B = colorInfo.B < 0 ? 0 : colorInfo.B > 1 ? 1 : colorInfo.B;

            return Color.FromArgb((int)(colorInfo.R * 255),
                (int)(colorInfo.G * 255),
                (int)(colorInfo.B * 255));
        }
    }
}
