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
            IFloorPawnView floorPawnView,
            IStageFloorModel stageFloorModel,
            ICurrentLookModel currentLookModel,
            IStagePawnModel stagePawnModel,
            IPlayerLockModel playerLockModel,
            IGridCastLogic gridCastLogic,
            LocomotionConnection locomotionConnection,
            CompositeDisposable compositeDisposable,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Holding, innerState)
        {
            PlayerView = playerView;
            HoldingPawnView = holdingPawnView;
            ActionEventView = actionEventView;
            MapCoordinateView = mapCoordinateView;
            FloorPawnView = floorPawnView;
            StageFloorModel = stageFloorModel;
            CurrentLookModel = currentLookModel;
            StagePawnModel = stagePawnModel;
            PlayerLockModel = playerLockModel;
            GridCastLogic = gridCastLogic;
            LocomotionConnection = locomotionConnection;
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

            // 前方にある`Pawn`があればキャンセルする
            var floor = StageFloorModel.CurrentFloor;
            var detectPosition = PlayerView.Position + CurrentLookModel.LookTo;
            var detectIndex = MapCoordinateView.PositionToMapIndex(StageFloorModel.CurrentFloor, detectPosition);
            var frontPawn = GridCastLogic
                .CastGridFirst(floor, detectIndex, Vector2Int.one, CastTargetType.Pawn);

            if (frontPawn.IsSome) return;

            // 保持している`Pawn`を置く
            var holdingPawn = HoldingPawnView.HoldingPawn;
            var putPosition = MapCoordinateView.AlignToMapPosition(floor, detectPosition);
            StagePawnModel.StorePawn(new GridCollider(holdingPawn.InstanceId, holdingPawn.Type, floor, detectIndex,
                holdingPawn.Size));
            FloorPawnView.GivePawn(holdingPawn, floor);
            await HoldingPawnView.PutPawn(putPosition);

            InnerState.ChangeState(PlayerStateType.Normal);
        }

        public override void StateUpdate(float deltaTime)
        {
            if (PlayerLockModel.IsLocked()) return;

            LocomotionConnection.Update(deltaTime);
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IPlayerView PlayerView { get; }
        private IHoldingPawnView HoldingPawnView { get; }
        private IInput_ActionEventView ActionEventView { get; }
        private IMapCoordinateView MapCoordinateView { get; }
        private IFloorPawnView FloorPawnView { get; }
        private IStageFloorModel StageFloorModel { get; }
        private ICurrentLookModel CurrentLookModel { get; }
        private IStagePawnModel StagePawnModel { get; }
        private IPlayerLockModel PlayerLockModel { get; }
        private IGridCastLogic GridCastLogic { get; }
        private LocomotionConnection LocomotionConnection { get; }
    }
}