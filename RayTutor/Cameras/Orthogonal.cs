using System;

namespace RayTutor
{
    class Orthogonal : ICamera
    {
        Vector3 eyePosition;
        double angle;
        Vector2 cameraSize;

        public Orthogonal(Vector3 eye, double angle, Vector2 size)
        {
            this.eyePosition = eye;
            this.angle = angle;
            this.cameraSize = size;
        }

        public Ray GetRayTo(Vector2 pictureLocation)
        {
            // Kierunek w którym skierowane są wszystkie promienie
            // wychodzące z kamery.
            // Otrzymany prostymi funkcjami trygonometrycznymi.
            Vector3 direction = new Vector3(
                Math.Sin(angle),
                0,
                Math.Cos(angle));

            // Kierunek promienia zawsze musi być znormalizowany.
            direction = direction.Normalized;

            // Jak bardzo początek promienia jest oddalony od 
            // położenia kamery (patrz: rysunek)
            Vector2 offsetFromCenter = new Vector2(
                pictureLocation.X * cameraSize.X,
                pictureLocation.Y * cameraSize.Y);

            // Obliczenie finalnego położenia kamery,
            // rówież proste funkcje trygonometryczne.
            Vector3 position = new Vector3(
                eyePosition.X + offsetFromCenter.X * Math.Cos(angle),
                eyePosition.Y + offsetFromCenter.Y,
                eyePosition.Z + offsetFromCenter.X * Math.Sin(angle));

            return new Ray(
                position,
                direction);
        }
    }
}
