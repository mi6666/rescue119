using System;
using Cysharp.Threading.Tasks;
using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Module.StateMachine;
using Structure.InGame;
using Structure.InGame.Stage;
using Structure.InGame.Stage.Pawn;
using UnityEngine;
using VContainer;

namespace Controller.InGame.Player
{
    public class ActionStateController : PlayerStateBehaviourBase
    {
        [Inject]
        public ActionStateController
        (
            IWaterView waterView,
            IDetectPositionView detectPositionView,
            IScenePawnsView scenePawnsView,
            IMapCoordinateView mapCoordinateView,
            IActionLengthModel actionLengthModel,
            IStageFloorModel stageFloorModel,
            ICurrentLookModel currentLookModel,
            IStageTileMapModel stageTileMapModel,
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
            CurrentLookModel = currentLookModel;
            StageTileMapModel = stageTileMapModel;
            GridCastLogic = gridCastLogic;
        }

        public override void OnEnter()
        {
            var currentFloor = StageFloorModel.CurrentFloor;
            var detectGridPosition = FrontMapPosition();
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
            var waterLength = CheckWall();
            var frontPosition = DetectPositionView.DetectPosition;
            var lookAt = Vector2Int.FloorToInt(CurrentLookModel.LookTo);
            WaterView.SpawnWater(frontPosition, lookAt, waterLength);
            var duration = ActionLengthModel.SplashWater;

            await UniTask.Delay(TimeSpan.FromSeconds(duration));

            WaterView.DespawnWater();
            InnerState.ChangeState(PlayerStateType.Normal);
        }

        private int CheckWall()
        {
            int frontCount;
            var frontPosition = FrontMapPosition();
            var lookAt = Vector2Int.FloorToInt(CurrentLookModel.LookTo);
            var currentFloor = StageFloorModel.CurrentFloor;

            for (frontCount = 0; frontCount < ActionLengthModel.WaterLength;  frontCount++)
            {
                var castPosition = frontPosition + lookAt * frontCount;
                
                // マップ上の壁をチェックする
                var mapCastResult = StageTileMapModel.GetTip(currentFloor, castPosition);
                if (mapCastResult.TryGetValue(out var tipBase))
                {
                    if (tipBase is ITipBlocking)
                    {
                        return frontCount;
                    }
                }
                
                // 道を阻む`Pawn`がないかチェックする
                var castResult =
                    GridCastLogic.CastGrid(
                        currentFloor, castPosition,
                        Vector2Int.one, CastTargetType.Pawn
                    );
                foreach (var collider in castResult)
                {
                    if (collider.PawnType.IsBlock())
                    {
                        return frontCount;
                    }
                }
            }

            return frontCount;
        }

        private Vector2Int FrontMapPosition()
        {
            var detectPosition = DetectPositionView.DetectPosition;
            var currentFloor = StageFloorModel.CurrentFloor;
            return MapCoordinateView.PositionToMapIndex(currentFloor, detectPosition);
        }

        private IWaterView WaterView { get; }
        private IDetectPositionView DetectPositionView { get; }
        private IScenePawnsView ScenePawnsView { get; }
        private IMapCoordinateView MapCoordinateView { get; }
        private IActionLengthModel ActionLengthModel { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IStageTileMapModel StageTileMapModel { get; }
        private ICurrentLookModel CurrentLookModel { get; }
        private IGridCastLogic GridCastLogic { get; }
    }
}