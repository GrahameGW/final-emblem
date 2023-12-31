using Godot;

namespace FinalEmblem.Core
{
    [GlobalClass] 
    public partial class Feature : Resource
    {
        [Export] public UnitAction Interaction { get; private set; }
    }

}
