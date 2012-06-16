using RayTutor.Transformations;
namespace RayTutor.Geomerty
{
    class TransformedObject : GeometricObject
    {
        GeometricObject instanced;
        Matrix invTransform;

        public TransformedObject(Matrix invTransform, GeometricObject instanced)
        {
            this.Material = instanced.Material;
            this.invTransform = invTransform;
            this.instanced = instanced;
        }

        public override bool HitTest(Ray ray, ref double distance, ref Vector3 normal)
        {
            Ray invRay = new Ray(
                invTransform.TransformPoint(ray.Origin),
                invTransform.TransformVector(ray.Direction));

            bool hit = instanced.HitTest(invRay, ref distance, ref normal);

            if (!hit) { return false; }

            normal = invTransform.TransformNormal(normal).Normalized;

            return true;
        }
    }
}
