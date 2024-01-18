using Godot;


namespace FinalEmblem.Core
{
    public partial class UnitInfoPanel : PanelContainer
    {
        [Export] Label unitNameLabel;
        [Export] Label unitClassLabel;
        [Export] Label unitFactionLabel;
        [Export] Label HpLabel;
        [Export] ProgressBar HpBar;

        private Unit current;

        public void TogglePanel(Unit unit)
        {
            if (current != null) 
            {
                current.OnUnitHpChanged -= UnitHpHealthChanged;
                current.OnUnitDied -= UnitDiedHandler;
            }
            current = unit;

            if (unit == null)
            {
                Hide();
            }
            else
            {
                Show();
                unitNameLabel.Text = unit.Name;
                unitClassLabel.Text = unit.Faction == Faction.Player ? "Barbarian" : "Voidsinger";
                unitFactionLabel.Text = unit.Faction.ToString();
                HpLabel.Text = $"HP: {unit.HP} / {unit.MaxHP}";
                HpBar.Value = 100 * unit.HP / unit.MaxHP;

                current.OnUnitHpChanged += UnitHpHealthChanged;
                current.OnUnitDied += UnitDiedHandler;
            }
        }
        
        private void UnitHpHealthChanged(int newHp)
        {
            HpBar.Value = 100 * newHp / current.MaxHP;
            HpLabel.Text = $"HP: {newHp} / {current.MaxHP}";
        }

        private void UnitDiedHandler()
        {
            TogglePanel(null);
        }
    }
}

