using Godot;

namespace TiercelFoundry.GDUtils
{
    public static class VectorExtensions
    {
        public static Vector2 Vector2XY(this Vector3 vector)
        {
            return new Vector2 (vector.X, vector.Y);
        }
    }
}

