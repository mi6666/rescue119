using System;
using Cysharp.Threading.Tasks;
using Interface.ModelInterface.InGame;
using Module.StateMachine;
using Structure.InGame;

namespace Controller.InGame.Player
{
    public class ActionStateController : PlayerStateBehaviourBase
    {
        public ActionStateController
        (
            IActionLengthModel actionLengthModel,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Action, innerState)
        {
            ActionLengthModel = actionLengthModel;
        }

        public override void OnEnter()
        {
            ExitAction().Forget();
        }

        private async UniTask ExitAction()
        {
            var duration = ActionLengthModel.ActionLength;

            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            
            InnerState.ChangeState(PlayerStateType.Normal);
        }

        private IActionLengthModel ActionLengthModel { get; }
    }
}