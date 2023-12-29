using Godot;
using System.Formats.Asn1;
using System.Threading.Tasks;

namespace FinalEmblem.Core
{
    public partial class AttackAnimationDisplay : Control
    {
        [ExportGroup("References")]
        [Export] Label attackerName;
        [Export] Label targetName;
        [Export] ProgressBar attackerHealth;
        [Export] ProgressBar targetHealth;
        [ExportGroup("Timing")]
        [Export] float fullHealthDrainTime;
        [Export] float minHealthDrainTime;
        [Export] float closeDelay;

        private Unit attacker, defender;

        public void SetActors(Unit attacker, Unit defender)
        {
            this.attacker = attacker;
            this.defender = defender;
            attackerName.Text = attacker.Name;
            targetName.Text = defender.Name;

            attackerHealth.MaxValue = attacker.MaxHP;
            attackerHealth.Value = attacker.HP;

            targetHealth.MaxValue = defender.MaxHP;
            targetHealth.MinValue = 0f;
            targetHealth.Value = defender.HP;
            targetHealth.AllowLesser = true;
        }
        
        public async Task PlayAttack(int damage)
        {
            float resultHealth = Mathf.Clamp(defender.HP - damage, 0, defender.MaxHP);
            float duration = damage / defender.MaxHP;
            duration = Mathf.Clamp(duration * fullHealthDrainTime, minHealthDrainTime, fullHealthDrainTime);

            Tween tween = GetTree().CreateTween();
            tween.TweenProperty(targetHealth, "value", resultHealth, duration).SetTrans(Tween.TransitionType.Quad);
            tween.TweenInterval(closeDelay);
            await ToSignal(tween, MagicString.TWEEN_FINISHED);
        }
    }
}

