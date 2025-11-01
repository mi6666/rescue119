using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.Global;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Module.EditorExtension.Runtime;
using Structure.Global;
using Structure.InGame;

namespace Controller.InGame.Common
{
    /// <summary>
    /// 移動処理のまとまり
    /// </summary>
    public class LocomotionConnection
    {
        public LocomotionConnection
        (
            IPlayerView playerView,
            IDetectPositionView detectPositionView,
            IInput_MoveVectorView moveVectorView,
            IMapCoordinateView mapCoordinateView,
            IPlayerAnimatorView playerAnimatorView,
            ICurrentLookModel currentLookModel,
            ILocomotionSetting locomotionSetting,
            IAnimationKeyModel animationKeyModel,
            ILocomotionLogic locomotionLogic
        )
        {
            PlayerView = playerView;
            DetectPositionView = detectPositionView;
            MoveVectorView = moveVectorView;
            MapCoordinateView = mapCoordinateView;
            PlayerAnimatorView = playerAnimatorView;
            CurrentLookModel = currentLookModel;
            LocomotionSetting = locomotionSetting;
            AnimationKeyModel = animationKeyModel;
            LocomotionLogic = locomotionLogic;
        }

        public void Update(float deltaTime, float ratio)
        {
            Locomotion(deltaTime, ratio);
            UpdatePawnDetectorPosition();
        }

        private void Locomotion(float deltaTime, float ratio)
        {
            var moveInput = MoveVectorView.Pool();
            var currentVelocity = PlayerView.CurrentVelocity;
            var frontHit = PlayerView.RayCast(moveInput);

            var lookAt = Utility.Approx8Dir(moveInput);
            if (lookAt.TryGetValue(out var value))
            {
                CurrentLookModel.SetLook(value);
            }

            var calcArg = new LocomotionArgument(
                moveInput,
                currentVelocity,
                frontHit,
                deltaTime
            );

            var calculatedVelocity = LocomotionLogic.CalcVelocity(calcArg) * ratio;
            DebugLogger.Log("move input", moveInput.ToString());
            DebugLogger.Log("calculated velocity", calculatedVelocity.ToString());

            PlayerView.ApplyVelocity(calculatedVelocity * deltaTime);

            var walkValue = calculatedVelocity.magnitude / LocomotionSetting.MaxSpeed;

            PlayerAnimatorView.SetFloat(AnimationKeyModel.Key, walkValue);
        }

        private void UpdatePawnDetectorPosition()
        {
            var detectorPosition = PlayerView.Position + CurrentLookModel.LookTo;
            var refinedPosition = MapCoordinateView.AlignToMapPosition(0, detectorPosition);
            DetectPositionView.SetPosition(refinedPosition);
        }

        private IPlayerView PlayerView { get; }
        private IDetectPositionView DetectPositionView { get; }
        private IInput_MoveVectorView MoveVectorView { get; }
        private IMapCoordinateView MapCoordinateView { get; }
        private IPlayerAnimatorView PlayerAnimatorView { get; }
        private ICurrentLookModel CurrentLookModel { get; }
        private ILocomotionSetting LocomotionSetting { get; }
        private IAnimationKeyModel AnimationKeyModel { get; }
        private ILocomotionLogic LocomotionLogic { get; }
    }
}