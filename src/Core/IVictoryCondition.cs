namespace FinalEmblem.Core
{
    public interface IVictoryCondition
    {
        Faction? TestCondition(Level level);
    }
}

