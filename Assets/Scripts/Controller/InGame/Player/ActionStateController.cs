using System;
using Cysharp.Threading.Tasks;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame;
using Module.StateMachine;
using Structure.InGame;
using UnityEngine;

namespace Controller.InGame.Player
{
    public class ActionStateController : PlayerStateBehaviourBase
    {
        public ActionStateController
        (
            IActionLengthModel actionLengthModel,
            IWaterView waterView,
            IPawnTypeView pawnTypeView,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Action, innerState)
        {
            ActionLengthModel = actionLengthModel;
            WaterView = waterView;
            PawnTypeView = pawnTypeView;
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
            var duration = ActionLengthModel.ActionLength;
            
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            
            InnerState.ChangeState(PlayerStateType.Normal);
        }

        private IActionLengthModel ActionLengthModel { get; }
        private IWaterView WaterView { get; }
        private IPawnTypeView PawnTypeView { get; }
    }
}