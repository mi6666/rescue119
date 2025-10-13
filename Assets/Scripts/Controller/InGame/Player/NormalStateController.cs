using Interface.LogicInterface.InGame;
using Interface.ViewInterface.Global;
using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Module.StateMachine;
using Structure.InGame;

namespace Controller.InGame.Player
{
    public class NormalStateController : PlayerStateBehaviourBase
    {
        public NormalStateController
        (
            IPlayerView playerView,
            IInput_MoveVectorView moveVectorView,
            ILocomotionLogic locomotionLogic,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Normal, innerState)
        {
            PlayerView = playerView;
            MoveVectorView = moveVectorView;
            LocomotionLogic = locomotionLogic;
        }

        public override void StateUpdate(float deltaTime)
        {
            Locomotion(deltaTime);
        }

        private void Locomotion(float deltaTime)
        {
            var moveInput = MoveVectorView.Pool();
            var currentVelocity = PlayerView.CurrentVelocity;
            var frontHit = PlayerView.RayCast(moveInput);

            var calcArg = new LocomotionArgument(
                moveInput,
                currentVelocity,
                frontHit,
                deltaTime
            );

            var calculatedVelocity = LocomotionLogic.CalcVelocity(calcArg);
            DebugLogger.Log("move input", moveInput.ToString());
            DebugLogger.Log("calculated velocity", calculatedVelocity.ToString());

            PlayerView.ApplyVelocity(calculatedVelocity);
        }

        private IPlayerView PlayerView { get; }
        private IInput_MoveVectorView MoveVectorView { get; }
        private ILocomotionLogic LocomotionLogic { get; }
    }
}