using System.Linq;

namespace FinalEmblem.Core
{
    public partial class KillAllOthersVictory : IVictoryCondition
    {
        private readonly Faction faction;

        public KillAllOthersVictory(Faction faction) { this.faction = faction; }

        public bool TestCondition(Level level) 
        {
            return level.Units.All(u => u.Faction == faction);
        }
    }
}

