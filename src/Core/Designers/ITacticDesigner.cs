using System;
using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public interface ITacticDesigner
    {
        Action<List<IUnitAction>> OnActionBuilt { get; set; }
        virtual void SetTileUnderMouse(Tile tile) { }
        virtual void SetSelectedTile(Tile tile) { }
    }
}

