namespace FinalEmblem.Core
{
    public class AttackActionResult : IActionResult
    {
        public Unit Actor { get; private set; }
        public int Damage { get; private set; }

        public AttackActionResult(AttackActionOld action, int damage)
        {
            Actor = action.Actor;
            Damage = damage;
        }
    }
}

