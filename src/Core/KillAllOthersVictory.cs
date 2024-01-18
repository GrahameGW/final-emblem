using System.Linq;

namespace FinalEmblem.Core
{
    public partial class KillAllOthersVictory : IVictoryCondition
    {
        private readonly Faction faction;

        public KillAllOthersVictory(Faction faction) { this.faction = faction; }

        public Faction? TestCondition(Level level) 
        {
            if (level.Units.All(u => u.Faction == faction))
            {
                return faction;
            }
            else
            {
                return null;
            }
        }
    }
}

