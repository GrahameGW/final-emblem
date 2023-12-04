using FinalEmblem.Core;
using Godot;

namespace FinalEmblem.Godot2D
{
    public partial class OtherTurnPCS : PlayerControlState
    {
        public OtherTurnPCS(PlayerController player) : base(player) { }

        public override void EnterState()
        {
            player.SelectedTile = null;
            player.IsActing = false;
            Level.OnTurnStarted += FactionTurnStartHandler;
            GD.Print("Entered OtherTurn");
        }

        public override void ExitState()
        {
            GD.Print("Exited OtherTurn");
            Level.OnTurnStarted -= FactionTurnStartHandler;
            Free();
        }

        public override void HandleInput(InputEvent input)
        {
        }

        private void FactionTurnStartHandler(Faction faction)
        {
            if (faction == Faction.Player)
            {
                player.ChangeState(new PlayerTurnIdlePCS(player));
            }
        }
    }


}

