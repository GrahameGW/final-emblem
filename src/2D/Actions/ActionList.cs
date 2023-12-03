using FinalEmblem.Core;
using Godot;
using System.Collections.Generic;

namespace FinalEmblem.Godot2D
{
    public partial class ActionList : ItemList
    {
        private PlayerController player;
        private List<UnitAction> currentActions;
        private Unit currentUnit;

        public void Initialize(PlayerController playerController)
        {
            player = playerController;
            player.OnSelectedTileChanged += SelectedTileChangedHandler;
            player.OnPlayerStateChanged += PlayerStateChangedHandler;
            ItemSelected += ItemClickedHandler;
            Hide();
        }

        public override void _ExitTree()
        {
            player.OnSelectedTileChanged -= SelectedTileChangedHandler;
            player.OnPlayerStateChanged -= PlayerStateChangedHandler;
            ItemSelected -= ItemClickedHandler;
        }

        private void SelectedTileChangedHandler(Tile tile)
        {
            Clear();
            bool unitIsValid = tile?.Unit != null && tile.Unit.Faction == Faction.Player;

            if (player.IsActing && unitIsValid)
            {
                GenerateActionList(tile.Unit);
                currentUnit = tile.Unit;
                // don't clear currently but should be okay?
            }

            ToggleVisibilityByCount();
        }

        private void GenerateActionList(Unit unit)
        {
            var actions = unit.GetAvailableActions();
            currentActions = new List<UnitAction>();

            if (actions == null) { return; }


            for (int i = 0; i < actions.Count; i++)
            {
                AddItem(actions[i].ToString());
                currentActions.Add(actions[i]);
            }
        }

        private void ToggleVisibilityByCount()
        {
            if (ItemCount == 0)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void ItemClickedHandler(long index)
        {
            player.StartActionPlanning(currentUnit, currentActions[(int)index]);
            Hide();
        }

        private void PlayerStateChangedHandler()
        {
            SelectedTileChangedHandler(player.SelectedTile);
        }
    }
}

