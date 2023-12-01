using Godot;
using System.Collections.Generic;

namespace TiercelFoundry.GDUtils
{
    public static class NodeExtensions
    {
        public static List<T> FindNodesOfType<T>(this Node node, List<T> result) where T : Node
        {
            result ??= new List<T>();
            if (node is T)
            {
                result.Add(node as T);
            }
            var children = node.GetChildren();
            for (int i = 0; i < children.Count; i++)
            {
                result = children[i].FindNodesOfType<T>(result);
            }
            return result;
        }
    }

    public static class VectorExtensions
    {
        public static Vector2 Vector2XY(this Vector3 vector)
        {
            return new Vector2 (vector.X, vector.Y);
        }
    }
}

