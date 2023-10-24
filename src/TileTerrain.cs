namespace FinalEmblem.Core
{
    public class TileTerrain
    {
        public TerrainType Terrain { get; private set; }
        public int Evade { get; private set; }
        public int Defense { get; private set; }
    }

    public enum TerrainType
    {
        Grass,
        Road,
        Dirt,
        Forest,
        Jungle,
        Mountain,
        Stone,
        River,
        Stream,
        Lake,
        Pond,
        Coast,
        Beach,
        Sea,
        Cliff,
        Floor
    }
}
