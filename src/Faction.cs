using System;
using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public class Faction
    {
        public static Action<Faction> OnTurnComplete;
        public readonly List<Unit> Units = new();

        public void StartTurn()
        {

        }
    }
}
