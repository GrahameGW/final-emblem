using System.Collections.Generic;

namespace TiercelFoundry.GDUtils
{
    public static class IListExtensions
    {
        public static T NextOrFirst<T>(this IList<T> list, T current)
        {
            if (list[^1].Equals(current))
            {
                return list[0];
            }
            else
            {
                return list[list.IndexOf(current) + 1];
            }
        }

        public static int NextOrFirstIndex<T>(this IList<T> list, int current)
        {
            return list.Count - 1 == current ? 0 : current + 1;
        }
    }
}

