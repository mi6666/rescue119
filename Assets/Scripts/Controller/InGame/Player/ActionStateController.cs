using System;
using Cysharp.Threading.Tasks;
using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Interface.PresenterInterface.InGame;
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
            IWaterView waterView,
            IDetectPositionView detectPositionView,
            IActionLengthModel actionLengthModel,
            IStageFloorModel stageFloorModel,
            IStageTileMapPresenter stageTileMapPresenter,
            IGridCastLogic gridCastLogic,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Action, innerState)
        {
            WaterView = waterView;
            DetectPositionView = detectPositionView;
            ActionLengthModel = actionLengthModel;
            StageFloorModel = stageFloorModel;
            StageTileMapPresenter = stageTileMapPresenter;
            GridCastLogic = gridCastLogic;
        }

        public override void OnEnter()
        {
            var detectPosition = DetectPositionView.DetectPosition;
            var currentFloor = StageFloorModel.CurrentFloor;
            var detectGridPosition = StageTileMapPresenter.ToMapIndex(currentFloor, detectPosition);
            var castResult =
                GridCastLogic.CastGridFirst(currentFloor, detectGridPosition, Vector2Int.one, CastTargetType.Pawn);


            if (!castResult.TryGetValue(out var value))
            {
                SpawnWater().Forget();
            }

            Debug.Log($"get {value.ToString()}");
            InnerState.ChangeState(PlayerStateType.Holding);
        }

        public override void OnExit()
        {
        }

        private async UniTask SpawnWater()
        {
            WaterView.SpawnWater();
            var duration = ActionLengthModel.SplashWater;

            await UniTask.Delay(TimeSpan.FromSeconds(duration));

            WaterView.DespawnWater();
            InnerState.ChangeState(PlayerStateType.Normal);
        }

        private IWaterView WaterView { get; }
        private IDetectPositionView DetectPositionView { get; }
        private IActionLengthModel ActionLengthModel { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IStageTileMapPresenter StageTileMapPresenter { get; }
        private IGridCastLogic GridCastLogic { get; }
    }
}