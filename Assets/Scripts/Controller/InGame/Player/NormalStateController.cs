using Interface.LogicInterface.InGame;
using Interface.PresenterInterface.InGame;
using Interface.ViewInterface.Global;
using Interface.ViewInterface.InGame;
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
            IPawnDetectView pawnDetectView,
            IInput_MoveVectorView moveVectorView,
            IInput_ActionEventView actionEventView,
            IStageTileMapPresenter stageTileMapPresenter,
            ILocomotionLogic locomotionLogic,
            CompositeDisposable compositeDisposable,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Normal, innerState)
        {
            PlayerView = playerView;
            PawnDetectView = pawnDetectView;
            MoveVectorView = moveVectorView;
            ActionEventView = actionEventView;
            StageTileMapPresenter = stageTileMapPresenter;
            LocomotionLogic = locomotionLogic;
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
            Locomotion(deltaTime);
            UpdatePawnDetectorPosition();
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

        private void UpdatePawnDetectorPosition()
        {
            var detectorPosition = PlayerView.Position;
            var refinedPosition = StageTileMapPresenter.ToMapPosition(0, detectorPosition);
            PawnDetectView.SetPosition(refinedPosition);
        }

        private IPlayerView PlayerView { get; }
        private IPawnDetectView PawnDetectView { get; }
        private IInput_MoveVectorView MoveVectorView { get; }
        private IInput_ActionEventView ActionEventView { get; }
        private IStageTileMapPresenter StageTileMapPresenter { get; }
        private ILocomotionLogic LocomotionLogic { get; }
        private CompositeDisposable CompositeDisposable { get; }
    }
}