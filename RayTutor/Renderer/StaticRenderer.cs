using System.Drawing;
using System;

namespace RayTutor
{
    class StaticRenderer
    {
        Raytracer tracer;

        public StaticRenderer(int maxDepth)
        {
            tracer = new Raytracer(maxDepth);
        }

        public Bitmap Raytrace(World world, ICamera camera, Size imageSize)
        {
            Sampler singleSample = new Sampler(new Regular(), new SquareDistribution(), 1, 1);
            return this.Raytrace(world, camera, imageSize, singleSample);
        }

        public Bitmap Raytrace(World world,
            ICamera camera,
            Size imageSize,
            Sampler sampler)
        {
            Bitmap bmp = new Bitmap(imageSize.Width, imageSize.Height);

            int start = System.Environment.TickCount;

            for (int y = 0; y < imageSize.Height; y++)
            {
                for (int x = 0; x < imageSize.Width; x++)
                {
                ColorRgb totalColor = ColorRgb.Black;
                for (int i = 0; i < sampler.SampleCount; i++)
                {
                    Vector2 sample = sampler.Single();

                    Vector2 pictureCoordinates = new Vector2(
                        ((x + sample.X) / (double)imageSize.Width) * 2 - 1,
                        ((y + sample.Y) / (double)imageSize.Height) * 2 - 1);

                    Ray ray = camera.GetRayTo(pictureCoordinates);

                    totalColor += tracer.TraceRay(world, ray, 0) / (double)sampler.SampleCount;
                }

                bmp.SetPixel(x, y, StripColor(totalColor));
                }

                Console.WriteLine("Progress: {0}%", (100 * y) / (float)imageSize.Height, tracer.TraceCount);
            }

            int renderTime = Environment.TickCount - start;
            int imagePixels = imageSize.Width * imageSize.Height;
            int primaryRays = imagePixels * sampler.SampleCount;
            Console.WriteLine();
            Console.WriteLine("==========RAYTRACED==========");
            Console.WriteLine("Pixels Rendered        : {0}", imagePixels);
            Console.WriteLine("Primary Rays           : {0}", primaryRays);
            Console.WriteLine("Total Rays             : {0}", tracer.TraceCount);
            Console.WriteLine("Primary Rays per Pixel : {0}", sampler.SampleCount);
            Console.WriteLine("Rays per Primary Ray   : {0}", tracer.TraceCount / (double)primaryRays);
            Console.WriteLine("Rays per Pixel         : {0}", tracer.TraceCount / (double)imagePixels);
            Console.WriteLine("Render Time            : {0}", renderTime);
            Console.WriteLine("Ms per Pixel           : {0}", renderTime / (double)imagePixels);
            Console.WriteLine("Ms per Primary Ray     : {0}", renderTime / (double)primaryRays);
            Console.WriteLine("Ms per Ray             : {0}", renderTime / (double)tracer.TraceCount);

            return bmp;
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
