using Controller.InGame.Common;
using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.Global;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Module.StateMachine;
using R3;
using Structure.InGame;
using Structure.InGame.Stage.Pawn;
using UnityEngine;
using VContainer.Unity;

namespace Controller.InGame.Player
{
    public class NormalStateController : PlayerStateBehaviourBase, IStartable
    {
        public NormalStateController
        (
            IPlayerView playerView,
            IInput_ActionEventView actionEventView,
            IMapCoordinateView mapCoordinateView,
            IHpModel hpModel,
            IStageFloorModel stageFloorModel,
            IPlayerLockModel playerLockModel,
            IGridCastLogic gridCastLogic,
            LocomotionConnection locomotionConnection,
            CompositeDisposable compositeDisposable,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Normal, innerState)
        {
            PlayerView = playerView;
            MapCoordinateView = mapCoordinateView;
            ActionEventView = actionEventView;
            HpModel = hpModel;
            StageFloorModel = stageFloorModel;
            PlayerLockModel = playerLockModel;
            GridCastLogic = gridCastLogic;
            LocomotionConnection = locomotionConnection;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            ActionEventView.ActionObservable
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (_, controller) => controller.OnAction())
                .AddTo(CompositeDisposable);
        }

        private void OnAction()
        {
            InnerState.ChangeState(PlayerStateType.Action);
        }

        public override void StateUpdate(float deltaTime)
        {
            if (PlayerLockModel.IsLocked()) return;
            
            LocomotionConnection.Update(deltaTime);
        }

        private void HpDecrease()
        {
        }

        private void FireDamage()
        {
            var currentFloor = StageFloorModel.CurrentFloor;
            var currentPosition = PlayerView.Position;
            var mapIndex =
                MapCoordinateView.PositionToMapIndex(currentFloor, currentPosition);
            var castResult =
                GridCastLogic.CastGrid(currentFloor, mapIndex, Vector2Int.one, CastTargetType.Pawn);
            foreach (var variable in castResult)
            {
                if (variable.PawnType == PawnType.Fire)
                {
                    // todo 
                    // Hpが減るメソッドでHpを減らす
                }
            }
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IPlayerView PlayerView { get; }
        private IInput_ActionEventView ActionEventView { get; }
        private IMapCoordinateView MapCoordinateView { get; }
        private IHpModel HpModel { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IPlayerLockModel PlayerLockModel { get; }
        private IGridCastLogic GridCastLogic { get; }
        private LocomotionConnection LocomotionConnection { get; }
    }
}