using Godot;

namespace FinalEmblem.Godot2D
{
    public partial class UnitSprite : Sprite2D
    {
        [Export] Material defaultMaterial;
        [Export] Material unitActedMaterial;

        public void ToggleMaterial(bool hasActed)
        {
            Material = hasActed ? unitActedMaterial : defaultMaterial;
        }
    }
}

