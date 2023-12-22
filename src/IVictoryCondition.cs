namespace FinalEmblem.Core
{
    public interface IVictoryCondition
    {
        Faction? TestCondition(Game level);
    }
}

