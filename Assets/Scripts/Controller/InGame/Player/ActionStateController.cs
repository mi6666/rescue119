using System;
using System.Threading;
using Controller.InGame.Common;
using Cysharp.Threading.Tasks;
using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Module.StateMachine;
using R3;
using Structure.InGame;
using Structure.InGame.Stage;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace Controller.InGame.Player
{
    public class ActionStateController : PlayerStateBehaviourBase
    {
        public ActionStateController
        (
            IPlayerView playerView,
            IWaterView waterView,
            IHoldingPawnView holdingPawnView,
            IDetectPositionView detectPositionView,
            IMapCoordinateView mapCoordinateView,
            IActionSetting actionSetting,
            IStageFloorModel stageFloorModel,
            ICurrentLookModel currentLookModel,
            IStageTileMapModel stageTileMapModel,
            IGridCastLogic gridCastLogic,
            IPlayerLockModel playerLockModel,
            PawnConnection pawnConnection,
            CompositeDisposable compositeDisposable,
            CancellationTokenSource　cancellationTokenSource,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Action, innerState)
        {
            PlayerView = playerView;
            WaterView = waterView;
            HoldingPawnView = holdingPawnView;
            DetectPositionView = detectPositionView;
            MapCoordinateView = mapCoordinateView;
            ActionSetting = actionSetting;
            StageFloorModel = stageFloorModel;
            CurrentLookModel = currentLookModel;
            StageTileMapModel = stageTileMapModel;
            GridCastLogic = gridCastLogic;
            PlayerLockModel = playerLockModel;
            PawnConnection = pawnConnection;
            CancellationTokenSource = cancellationTokenSource;
            
            compositeDisposable.Add(cancellationTokenSource);
        }

        public override void OnEnter()
        {
            var currentFloor = StageFloorModel.CurrentFloor;
            var detectGridPosition = FrontMapPosition();
            var castResult =
                GridCastLogic.CastGridFirst(currentFloor, detectGridPosition, Vector2Int.one, CastTargetType.Pawn);

            if (castResult.TryGetValue(out var value))
            {
                if (value.PawnType.IsHoldable())
                {
                    Hold().Forget();
                    return;
                }
            }

            SpawnWater().Forget();
        }

        private async UniTask SpawnWater()
        {
            var waterLength = CheckWall();
            var frontPosition = DetectPositionView.DetectPosition;
            var lookAt = Vector2Int.FloorToInt(CurrentLookModel.LookTo);
            WaterView.SpawnWater(frontPosition, lookAt, waterLength);
            var duration = ActionSetting.SplashWater;

            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: CancellationTokenSource.Token);
            }
            finally
            {
                WaterView.DespawnWater();
                InnerState.ChangeState(PlayerStateType.Normal);
            }
        }

        private const string HoldingAnimationLock = "Holding Animation Lock";

        private async UniTask Hold()
        {
            // 他の動作を受け付けない
            using var operation = PlayerLockModel.GetOperation(HoldingAnimationLock);

            // 前方にある`Pawn`を取得する
            var floor = StageFloorModel.CurrentFloor;
            var detectPosition = PlayerView.Position + CurrentLookModel.LookTo;
            var detectIndex = MapCoordinateView.PositionToMapIndex(StageFloorModel.CurrentFloor, detectPosition);
            var holdPawn = GridCastLogic
                .CastGridFirst(floor, detectIndex, Vector2Int.one, CastTargetType.Pawn)
                .Unwrap();
            
            var holdPawnView = PawnConnection.TakePawn(holdPawn);

            // `Pawn`を保持する
            await HoldingPawnView.HoldPawn(holdPawnView);
            InnerState.ChangeState(PlayerStateType.Holding);
        }

        private int CheckWall()
        {
            int frontCount;
            var frontPosition = FrontMapPosition();
            var lookAt = Vector2Int.FloorToInt(CurrentLookModel.LookTo);
            var currentFloor = StageFloorModel.CurrentFloor;

            for (frontCount = 0; frontCount < ActionSetting.WaterLengthMax; frontCount++)
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

                    if (collider.PawnType == PawnType.Fire)
                    {
                        PawnConnection.SendPawnPool(collider);
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

        private IPlayerView PlayerView { get; }
        private IWaterView WaterView { get; }
        private IHoldingPawnView HoldingPawnView { get; }
        private IDetectPositionView DetectPositionView { get; }
        private IMapCoordinateView MapCoordinateView { get; }
        private IActionSetting ActionSetting { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IStageTileMapModel StageTileMapModel { get; }
        private ICurrentLookModel CurrentLookModel { get; }
        private IGridCastLogic GridCastLogic { get; }
        private IPlayerLockModel PlayerLockModel { get; }
        private PawnConnection PawnConnection { get; }
        private CancellationTokenSource CancellationTokenSource { get; }
    }
}