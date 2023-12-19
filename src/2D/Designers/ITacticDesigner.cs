using FinalEmblem.Core;
using System;

namespace FinalEmblem.src.Query.Designers
{
    public interface ITacticDesigner
    {
        Action<IAction> OnActionBuilt { get; set; }
        virtual void SetTileUnderMouse(Tile tile) { }
        virtual void SetSelectedTile(Tile tile) { }
    }
}

