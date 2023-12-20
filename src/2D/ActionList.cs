
using Godot;
using System;
using System.Collections.Generic;

namespace FinalEmblem.Core
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
            tactics.OnUnitSelected += UnitSelectedHandler;
            Hide();
        }

        public override void _ExitTree()
        {
            ItemSelected -= ItemClickedHandler;
            tactics.OnUnitSelected -= UnitSelectedHandler;
        }

        public void UnitSelectedHandler(Unit selected)
        {
            unit = selected;
            if (selected == null || selected.HasActed || !tactics.IsIdleState)
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

