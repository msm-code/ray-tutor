using System.Drawing;
using System.Collections.Generic;
namespace RayTutor
{
    class Raytracer
    {
        const int MaxDepth = 5; // Maksymalna dopuszczalna głębokość rekurencji

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

            for (int y = 0; y < imageSize.Height; y++)
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

            return bmp;
        }

        public ColorRgb TraceRay(World world, Ray ray)
        { return this.TraceRay(world, ray, 0); }

        public ColorRgb TraceRay(World world, Ray ray, int currentDepth)
        {
            if (currentDepth > MaxDepth) { return ColorRgb.Black; }

            HitInfo info = world.TraceRay(ray);
            info.Depth = currentDepth + 1;

            if (info.HitObject == null) { return world.BackgroundColor; }

            ColorRgb finalColor = ColorRgb.Black;
            IMaterial material = info.HitObject.Material;

            foreach (var light in world.Lights)
            {
                if (world.AnyObstacleBetween(info.HitPoint, light.Position)) { continue; }

                finalColor += material.Radiance(this, light, info);
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
