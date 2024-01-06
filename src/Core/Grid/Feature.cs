using Godot;

namespace FinalEmblem.Core
{
    [GlobalClass] 
    public partial class Feature : Resource
    {
        [Export] public UnitAction Interaction { get; private set; }
        [Export] public bool IsObstacle { get; private set; }
        [Export] public FeatureInteraction InteractMode { get; private set; }
        [Export] public Resource Data { get; private set; }
        [Export] private bool disableAfterInteraction;
        
        public void LogInteraction(Unit _)
        {
            if (disableAfterInteraction)
            {
                InteractMode = FeatureInteraction.None;
            }
        }
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
