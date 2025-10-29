using System;
using Cysharp.Threading.Tasks;
using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Module.StateMachine;
using Structure.InGame;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace Controller.InGame.Player
{
    public class ActionStateController : PlayerStateBehaviourBase
    {
        public ActionStateController
        (
            IWaterView waterView,
            IDetectPositionView detectPositionView,
            IScenePawnsView scenePawnsView,
            IMapCoordinateView mapCoordinateView,
            IActionLengthModel actionLengthModel,
            IStageFloorModel stageFloorModel,
            IGridCastLogic gridCastLogic,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Action, innerState)
        {
            WaterView = waterView;
            DetectPositionView = detectPositionView;
            ScenePawnsView = scenePawnsView;
            MapCoordinateView = mapCoordinateView;
            ActionLengthModel = actionLengthModel;
            StageFloorModel = stageFloorModel;
            GridCastLogic = gridCastLogic;
        }

        public override void OnEnter()
        {
            var detectPosition = DetectPositionView.DetectPosition;
            var currentFloor = StageFloorModel.CurrentFloor;
            var detectGridPosition = MapCoordinateView.PositionToMapIndex(currentFloor, detectPosition);
            var castResult =
                GridCastLogic.CastGridFirst(currentFloor, detectGridPosition, Vector2Int.one, CastTargetType.Pawn);

            if (castResult.TryGetValue(out var value))
            {
                var pawn = ScenePawnsView.FindPawn(value.PawnId);
                if (pawn is not null)
                {
                    if (pawn.Type == PawnType.Casualty)
                    {
                        InnerState.ChangeState(PlayerStateType.Holding);
                        return;
                    }
                }
            }

            SpawnWater().Forget();
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
        private IScenePawnsView ScenePawnsView { get; }
        private IMapCoordinateView MapCoordinateView { get; }
        private IActionLengthModel ActionLengthModel { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IGridCastLogic GridCastLogic { get; }
    }
}