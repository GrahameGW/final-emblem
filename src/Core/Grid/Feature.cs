using Godot;

namespace FinalEmblem.Core
{
    [GlobalClass] 
    public partial class Feature : Resource
    {
        [Export] public UnitAction Interaction { get; private set; }
        [Export] public bool IsObstacle { get; private set; }
        [Export] public FeatureInteraction InteractMode { get; private set; }
    }
    
    public enum FeatureInteraction
    {
        None,
        SameTile,
        Adjacent,
        SameOrAdjacent
    }

    public static class FeatureInteractionExtensions
    {
        public static bool CanInteractAdjacent(this FeatureInteraction interaction)
        {
            return interaction == FeatureInteraction.Adjacent || interaction == FeatureInteraction.SameOrAdjacent;
        }

        public static bool CanInteractOnTile(this FeatureInteraction interaction)
        {
            return interaction == FeatureInteraction.SameTile || interaction == FeatureInteraction.SameOrAdjacent;
        }
    }
}
