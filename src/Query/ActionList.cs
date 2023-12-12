using FinalEmblem.Core;
using Godot;
using System;
using System.Collections.Generic;

namespace FinalEmblem.QueryModel
{
    public partial class ActionList : ItemList
    {
        private List<UnitTactic> currentActions;
        private bool isPlayersTurn;
        private Unit unit;
        private TacticsController tactics;

        public event Action<UnitTactic> OnActionSelected;

        public void Initialize(TacticsController tactics)
        {
            this.tactics = tactics;
            ItemSelected += ItemClickedHandler;
            UpdatePanelForTile(null);
        }

        public override void _ExitTree()
        {
            ItemSelected -= ItemClickedHandler;
        }

        public void UpdatePanelForTile(Tile tile)
        {
            if (tile?.Unit?.Faction != Faction.Player || !isPlayersTurn)
            {
                unit = null;
                Hide();
            }
            else 
            {
                unit = tile.Unit;
                GenerateActionList(unit);
                ToggleVisibilityByCount();
            }
        }

        private void GenerateActionList(Unit unit)
        {
            var actions = unit.GetAvailableActions();
            currentActions = new List<UnitTactic>();
            Clear();

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

        public void TogglePlayersTurn(Faction faction)
        {
            isPlayersTurn = faction == Faction.Player;
            if (!isPlayersTurn)
            {
                Hide();
            }
        }

        private void ItemClickedHandler(long index)
        {
            tactics.BuildTacticDesigner(currentActions[(int)index], unit);
            Hide();
        }
    }
}

