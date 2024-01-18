
using Godot;
using System;
using System.Collections.Generic;

namespace FinalEmblem.Core
{
    public partial class ActionList : ItemList
    {
        private List<UnitAction> currentActions;
        private bool isPlayersTurn;
        private Unit unit;
        private PlayerController player;

        public event Action<UnitAction> OnActionSelected;

        public void Initialize(PlayerController player)
        {
            this.player = player;
            ItemSelected += ItemClickedHandler;
            player.OnUnitSelected += UnitSelectedHandler;
            Hide();
        }

        public override void _ExitTree()
        {
            ItemSelected -= ItemClickedHandler;
            player.OnUnitSelected -= UnitSelectedHandler;
        }

        public void UnitSelectedHandler(Unit selected)
        {
            unit = selected;
            if (selected == null || selected.HasActed || selected.Faction != Faction.Player)
            {
                Hide();
            }
            else 
            {
                GenerateActionList(selected);
                ToggleVisibilityByCount();
            }
        }

        private void GenerateActionList(Unit unit)
        {
            var actions = unit.GetAvailableActions();
            currentActions = new List<UnitAction>();
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
            player.BuildTacticDesigner(currentActions[(int)index], unit);
            Hide();
        }
    }
}

