using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Module.StateMachine;
using Structure.InGame;
using UnityEngine;

namespace Controller.InGame.Player
{
    public class HoldingStateController : PlayerStateBehaviourBase
    {
        public HoldingStateController
        (
            IPlayerView playerView,
            IFloorPawnView floorPawnView,
            ICurrentLookModel currentLookModel,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Holding, innerState)
        {
            PlayerView = playerView;
            FloorPawnView = floorPawnView;
            CurrentLookModel = currentLookModel;
        }

        public override void OnEnter()
        {
            var floor = StageFloorModel.CurrentFloor;
            var detectPosition = PlayerView.Position + CurrentLookModel.LookTo;
            var detectIndex = MapCoordinateView.PositionToMapIndex(StageFloorModel.CurrentFloor, detectPosition);
            var holdPawn = GridCastLogic.CastGridFirst(floor, detectIndex, Vector2Int.one, CastTargetType.Pawn).Unwrap();
            var holdPawnView = FloorPawnView.GetPawn(holdPawn.PawnId, floor);
            // holdPawnView.SetOwner(PlayerView.PlayerTransform);
            

        }

        private IPlayerView PlayerView { get; }
        private IMapCoordinateView MapCoordinateView { get; }
        private IFloorPawnView FloorPawnView { get; }
        private IStageFloorModel StageFloorModel { get; }
        private ICurrentLookModel CurrentLookModel { get; }
        private IStagePawnModel StagePawnModel { get; }
        private IGridCastLogic GridCastLogic { get; }
    }
}