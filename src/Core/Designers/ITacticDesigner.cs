using System;

namespace FinalEmblem.Core
{
    public interface ITacticDesigner
    {
        Action<IAction> OnActionBuilt { get; set; }
        virtual void SetTileUnderMouse(Tile tile) { }
        virtual void SetSelectedTile(Tile tile) { }
    }
}

