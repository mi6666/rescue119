using Cysharp.Threading.Tasks;
using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.Global;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Module.EditorExtension.Runtime;
using Module.StateMachine;
using Structure.Global;
using Structure.InGame;
using UnityEngine;

namespace Controller.InGame.Player
{
    public class HoldingStateController : PlayerStateBehaviourBase
    {
        public HoldingStateController
        (
            IPlayerView playerView,
            IInput_MoveVectorView inputMoveVectorView,
            IMapCoordinateView mapCoordinateView,
            IFloorPawnView floorPawnView,
            IPlayerAnimatorView playerAnimatorView,
            IPlayerAnimationParameterKeyModel playerAnimationParameterKeyModel,
            IStageFloorModel stageFloorModel,
            ICurrentLookModel currentLookModel,
            IStagePawnModel stagePawnModel,
            IActionLengthModel actionLengthModel,
            IPlayerLockModel playerLockModel,
            ILocomotionModel locomotionModel,
            ILocomotionLogic locomotionLogic,
            IGridCastLogic gridCastLogic,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Holding, innerState)
        {
            PlayerView = playerView;
            InputMoveVectorView = inputMoveVectorView;
            MapCoordinateView = mapCoordinateView;
            FloorPawnView = floorPawnView;
            PlayerAnimatorView = playerAnimatorView;
            PlayerAnimationParameterKeyModel = playerAnimationParameterKeyModel;
            StageFloorModel = stageFloorModel;
            CurrentLookModel = currentLookModel;
            StagePawnModel = stagePawnModel;
            ActionLengthModel = actionLengthModel;
            PlayerLockModel = playerLockModel;
            LocomotionModel = locomotionModel;
            LocomotionLogic = locomotionLogic;
            GridCastLogic = gridCastLogic;
        }

        public override void OnEnter()
        {
            Hold().Forget();
        }

        private async UniTask Hold()
        {
            using var operation = PlayerLockModel.GetOperation();
            
            var floor = StageFloorModel.CurrentFloor;
            var detectPosition = PlayerView.Position + CurrentLookModel.LookTo;
            var detectIndex = MapCoordinateView.PositionToMapIndex(StageFloorModel.CurrentFloor, detectPosition);
            var holdPawn = GridCastLogic.CastGridFirst(floor, detectIndex, Vector2Int.one, CastTargetType.Pawn).Unwrap();
            var holdPawnView = FloorPawnView.GetPawn(holdPawn.PawnId, floor);
            await holdPawnView.SetOwner(PlayerView.PlayerTransform, ActionLengthModel.HoldMotionLength);
        }

        public override void StateUpdate(float deltaTime)
        {
            if (PlayerLockModel.IsLocked()) return;
            
            Locomotion(deltaTime);
        }

        private void Locomotion(float deltaTime)
        {
            var moveInput = InputMoveVectorView.Pool();
            var currentVelocity = PlayerView.CurrentVelocity;
            var frontHit = PlayerView.RayCast(moveInput);

            var lookAt = Utility.Approx8Dir(moveInput);
            if (lookAt.TryGetValue(out var value))
            {
                CurrentLookModel.SetLook(value);
                DebugLogger.Log("look at", value.ToString());
            }

            var calcArg = new LocomotionArgument(
                moveInput,
                currentVelocity,
                frontHit,
                deltaTime
            );

            var calculatedVelocity = LocomotionLogic.CalcVelocity(calcArg);
            DebugLogger.Log("move input", moveInput.ToString());
            DebugLogger.Log("calculated velocity", calculatedVelocity.ToString());

            PlayerView.ApplyVelocity(calculatedVelocity * deltaTime);

            var walkValue = calculatedVelocity.magnitude / LocomotionModel.MaxSpeed;

            PlayerAnimatorView.SetFloat(PlayerAnimationParameterKeyModel.Key, walkValue);
        }

        private IPlayerView PlayerView { get; }
        private IInput_MoveVectorView InputMoveVectorView { get; }
        private IMapCoordinateView MapCoordinateView { get; }
        private IFloorPawnView FloorPawnView { get; }
        private IPlayerAnimatorView PlayerAnimatorView { get; }
        private IPlayerAnimationParameterKeyModel PlayerAnimationParameterKeyModel { get; }
        private IStageFloorModel StageFloorModel { get; }
        private ICurrentLookModel CurrentLookModel { get; }
        private IStagePawnModel StagePawnModel { get; }
        private IActionLengthModel ActionLengthModel { get; }
        private IPlayerLockModel PlayerLockModel { get; }
        private ILocomotionModel LocomotionModel { get; }
        private ILocomotionLogic LocomotionLogic { get; }
        private IGridCastLogic GridCastLogic { get; }
    }
}