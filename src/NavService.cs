using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public static class NavService
    {
        private static Grid grid;

        public static void SetGridInstance(Grid instance)
        {
            grid = instance;
        }

        public static List<Tile> FindTilesInRange(int distance, Tile start)
        {
            return null;
        }
    }
}
