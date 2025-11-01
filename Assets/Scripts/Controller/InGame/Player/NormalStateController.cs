using System;
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
            IStageMasterModel stageMasterModel,
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
            StageMasterModel = stageMasterModel;
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
            Observable
                .Interval(TimeSpan.FromSeconds(StageMasterModel.PawnTickInterval))
                .ObserveOnMainThread() // これがないと乱数がきちんと動かない
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (_, controller) => controller.FireDamage())
                .AddTo(CompositeDisposable);
        }

        private void OnAction()
        {
            InnerState.ChangeState(PlayerStateType.Action);
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

        private void FireDamage()
        {
            var currentFloor = StageFloorModel.CurrentFloor;
            var currentPosition = PlayerView.Position;
            var mapIndex =
                MapCoordinateView.PositionToMapIndex(currentFloor, currentPosition);
            var castResult =
                GridCastLogic.CastGrid(currentFloor, mapIndex, Vector2Int.one, CastTargetType.Pawn);
            foreach (var collider in castResult)
            {
                if (collider.PawnType == PawnType.Fire)
                {
                    HpModel.DecHp(1); // FIXME
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
        private IStageMasterModel StageMasterModel { get; }
        private IGridCastLogic GridCastLogic { get; }
        private LocomotionConnection LocomotionConnection { get; }
    }
}