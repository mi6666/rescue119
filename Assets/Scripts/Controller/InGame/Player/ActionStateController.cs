using System;
using Cysharp.Threading.Tasks;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame;
using Module.StateMachine;
using Structure.InGame;

namespace Controller.InGame.Player
{
    public class ActionStateController : PlayerStateBehaviourBase
    {
        public ActionStateController
        (
            IActionLengthModel actionLengthModel,
            IWaterView waterView,
            IPawnDetectView pawnDetectView,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Action, innerState)
        {
            ActionLengthModel = actionLengthModel;
            WaterView = waterView;
            PawnDetectView = pawnDetectView;
        }

        public override void OnEnter()
        {
            WaterView.SpawnWater();
            ExitAction().Forget();
        }

        public override void OnExit()
        {
            WaterView.DespawnWater();
        }

        private async UniTask ExitAction()
        {
            var duration = ActionLengthModel.SplashWater;
            
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            
            InnerState.ChangeState(PlayerStateType.Normal);
        }

        private IActionLengthModel ActionLengthModel { get; }
        private IWaterView WaterView { get; }
        private IPawnDetectView PawnDetectView { get; }
    }
}