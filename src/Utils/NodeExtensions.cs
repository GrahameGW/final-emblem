using Godot;
using System.Collections.Generic;

namespace TiercelFoundry.GDUtils
{
    public static class NodeExtensions
    {
        public static List<T> FindNodesOfType<T>(this Node node, List<T> result = null) where T : Node
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

        public static T FindChildOfType<T>(this Node node) where T : Node
        {
            var children = node.GetChildren();
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] is T)
                {
                    return children[i] as T;
                }
            }

            return null;
        }
    }
}

