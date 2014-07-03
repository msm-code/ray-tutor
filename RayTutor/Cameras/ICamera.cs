namespace RayTutor
{
    interface ICamera
    {
        Ray GetRayTo(Vector2 relativeLocation);
    }
}
