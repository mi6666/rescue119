using Controller.InGame.Common;
using Cysharp.Threading.Tasks;
using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.Global;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Module.StateMachine;
using R3;
using Structure.InGame;
using Structure.InGame.Stage;
using Structure.InGame.Stage.Pawn;
using UnityEngine;
using VContainer.Unity;

namespace Controller.InGame.Player
{
    public class HoldingStateController : PlayerStateBehaviourBase, IStartable
    {
        public HoldingStateController
        (
            IPlayerView playerView,
            IHoldingPawnView holdingPawnView,
            IInput_ActionEventView actionEventView,
            IMapCoordinateView mapCoordinateView,
            IStageFloorModel stageFloorModel,
            ICurrentLookModel currentLookModel,
            IPlayerLockModel playerLockModel,
            IStageTileMapModel stageTileMapModel,
            IGridCastLogic gridCastLogic,
            LocomotionConnection locomotionConnection,
            PawnConnection pawnConnection,
            CompositeDisposable compositeDisposable,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Holding, innerState)
        {
            PlayerView = playerView;
            HoldingPawnView = holdingPawnView;
            ActionEventView = actionEventView;
            MapCoordinateView = mapCoordinateView;
            StageFloorModel = stageFloorModel;
            CurrentLookModel = currentLookModel;
            PlayerLockModel = playerLockModel;
            StageTileMapModel = stageTileMapModel;
            GridCastLogic = gridCastLogic;
            LocomotionConnection = locomotionConnection;
            PawnConnection = pawnConnection;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            ActionEventView.ActionObservable
                .Where(this, (_, controller) => controller.IsInState())
                .Where(this, (_, controller) => !controller.PlayerLockModel.IsLocked())
                .Subscribe(this, (_, controller) => controller.Put().Forget())
                .AddTo(CompositeDisposable);
        }

        private const string HoldingAnimationLock = "Holding Animation Lock";

        private async UniTask Put()
        {
            // 他の動作を受け付けない
            using var operation = PlayerLockModel.GetOperation(HoldingAnimationLock);

            var holdingPawn = HoldingPawnView.HoldingPawn;
            var floor = StageFloorModel.CurrentFloor;
            var detectPosition =
                MapCoordinateView.AlignToMapPosition(floor, PlayerView.Position + CurrentLookModel.LookTo);
            var detectIndex = MapCoordinateView.PositionToMapIndex(StageFloorModel.CurrentFloor, detectPosition);

            // マップの前方を確認
            int putToFloor;
            for (putToFloor = floor; putToFloor >= 0; putToFloor--)
            {
                var frontTile = StageTileMapModel.GetTip(putToFloor, detectIndex);
                if (!frontTile.TryGetValue(out var tipBase)) break;
                if (tipBase is not HoleTip) break;
            }

            // 前方にある`Pawn`を取得
            var frontPawn = GridCastLogic.CastGridFirst(
                putToFloor, detectIndex, Vector2Int.one, CastTargetType.Pawn
            );

            if (frontPawn.TryGetValue(out var frontCollider))
            {
                // 岩は火を消す
                if (holdingPawn.Type == PawnType.Rubble & frontCollider.PawnType == PawnType.Fire)
                {
                    PawnConnection.SendPawnPool(frontCollider);
                }
                // それ以外は置けない
                else
                {
                    return;
                }
            }

            // 保持している`Pawn`を置く
            if (putToFloor == floor)
            {
                await HoldingPawnView.PutPawn(detectPosition);
            }
            else
            {
                await HoldingPawnView.PutAndFall(detectPosition);
            }

            PawnConnection.PutPawn(holdingPawn, detectPosition, putToFloor);
            InnerState.ChangeState(PlayerStateType.Normal);
        }

        public override void StateUpdate(float deltaTime)
        {
            if (PlayerLockModel.IsLocked()) return;

            var currentFloor = StageFloorModel.CurrentFloor;
            var currentPosition = PlayerView.Position;
            var mapIndex =
                MapCoordinateView.PositionToMapIndex(currentFloor, currentPosition);
            var castResult =
                GridCastLogic.CastGrid(currentFloor, mapIndex, Vector2Int.one, CastTargetType.Pawn);
            bool onFire = false;
            foreach (var collider in castResult)
            {
                if (collider.PawnType == PawnType.Fire)
                {
                    onFire = true;
                    break;
                }
            }

            LocomotionConnection.Update(deltaTime, onFire ? 0.5f : 1);
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IPlayerView PlayerView { get; }
        private IHoldingPawnView HoldingPawnView { get; }
        private IInput_ActionEventView ActionEventView { get; }
        private IMapCoordinateView MapCoordinateView { get; }
        private IStageFloorModel StageFloorModel { get; }
        private ICurrentLookModel CurrentLookModel { get; }
        private IPlayerLockModel PlayerLockModel { get; }
        private IStageTileMapModel StageTileMapModel { get; }
        private IGridCastLogic GridCastLogic { get; }
        private LocomotionConnection LocomotionConnection { get; }
        private PawnConnection PawnConnection { get; }
    }
}