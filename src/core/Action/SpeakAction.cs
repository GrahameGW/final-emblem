using Godot;

namespace FinalEmblem.Core
{
    public class SpeakAction : IUnitAction
    {
        public Unit Player { get; private set; }
        public Resource Timeline { get; private set; }

        private readonly Tile other;

        public SpeakAction(Unit player, Resource timeline, Tile speakTo) 
        { 
            other = speakTo;
            Player = player;
            Timeline = timeline;
        }

        public void Execute()
        {
            other.Feature.LogInteraction(Player);
            Player.HasActed = true;
            Player.Facing = Player.Tile.DirectionToNeighbor(other);
        }
    }
}

