using System.Buffers;
using Interface.LogicInterface.InGame;
using Interface.ViewInterface.Global;
using Interface.ViewInterface.InGame;
using JetBrains.Annotations;
using Module.EditorExtension.Runtime;
using Module.StateMachine;
using R3;
using Structure.InGame;
using VContainer.Unity;

namespace Controller.InGame.Player
{
    public class NormalStateController : PlayerStateBehaviourBase, IStartable
    {
        public NormalStateController
        (
            IPlayerView playerView,
            IInput_MoveVectorView moveVectorView,
            IInput_ActionEventView actionEventView,
            ILocomotionLogic locomotionLogic,
            CompositeDisposable compositeDisposable,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Normal, innerState)
        {
            PlayerView = playerView;
            MoveVectorView = moveVectorView;
            LocomotionLogic = locomotionLogic;
            ActionEventView = actionEventView;
            CompositeDisposable = compositeDisposable;
        }
        public void Start()
        {
            ActionEventView.ActionObservable
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (s, controller) => controller.OnAction())
                .AddTo(CompositeDisposable);           
        }

        private void OnAction()
        {
            InnerState.ChangeState(PlayerStateType.Action);
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
        private IInput_ActionEventView ActionEventView { get; }
        private CompositeDisposable CompositeDisposable { get; }
    }
}