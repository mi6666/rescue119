using Module.StateMachine;
using Structure.InGame;

namespace Controller.InGame.Player
{
    public class ActionStateController : PlayerStateBehaviourBase
    {
        public ActionStateController
        (
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Action, innerState)
        {
        }
    }
}