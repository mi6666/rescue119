using Interface.LogicInterface.InGame;
using Interface.ViewInterface.Global;
using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Module.StateMachine;
using Structure.InGame;
using UnityEngine;

namespace Controller.InGame.Player
{
    public class IdleStateBehaviour : PlayerStateBehaviourBase
    {
        public IdleStateBehaviour
        (
            IPlayerView playerView,
            IInput_MoveVectorView moveVectorView,
            ILocomotionLogic locomotionLogic,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Idle, innerState)
        {
            PlayerView = playerView;
            MoveVectorView = moveVectorView;
            LocomotionLogic = locomotionLogic;
        }

        public override void StateUpdate(float deltaTime)
        {
            Locomotion(deltaTime);
        }

        /// <summary>
        /// 移動処理
        /// </summary>
        private void Locomotion(float deltaTime)
        {
            // 入力受取
            var moveInput = MoveVectorView.Pool();
            DebugLogger.Log("move input", moveInput.ToString());
            var currentVelocity = PlayerView.CurrentVelocity;
            var frontHit = PlayerView.CastFront();

            var calcArg = new LocomotionArgument(
                moveInput,
                currentVelocity,
                frontHit,
                deltaTime
            );

            var calculatedVelocity = LocomotionLogic.CalcVelocity(calcArg);
            DebugLogger.Log("calculated velocity", calculatedVelocity.ToString());

            PlayerView.ApplyVelocity(calculatedVelocity);
        }

        private IPlayerView PlayerView { get; }
        private IInput_MoveVectorView MoveVectorView { get; }
        private ILocomotionLogic LocomotionLogic { get; }
    }
}