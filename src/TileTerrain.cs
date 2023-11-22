namespace FinalEmblem.Core
{
    public struct TileTerrain
    {
        public TerrainIndex Index;
        public int Evade;
        public int Defense;
    }

    public enum TerrainIndex
    {
        Grass,
        Water,
        Stone,
        Road,
        Dirt,
        Forest,
        Jungle,
        Mountain,
        River,
        Lake,
        Coast,
        Beach,
        Sea,
        Cliff,
        Floor
    }
}
