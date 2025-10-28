using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Interface.PresenterInterface.InGame;
using Interface.ViewInterface.Global;
using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Module.StateMachine;
using R3;
using Structure.Global;
using Structure.InGame;
using VContainer.Unity;

namespace Controller.InGame.Player
{
    public class NormalStateController : PlayerStateBehaviourBase, IStartable
    {
        public NormalStateController
        (
            IPlayerView playerView,
            IDetectPositionView detectPositionView,
            IInput_MoveVectorView moveVectorView,
            IInput_ActionEventView actionEventView,
            IPlayerAnimatorView playerAnimatorView,
            ICurrentLookModel currentLookModel,
            ILocomotionModel locomotionModel,
            IPlayerAnimationParameterKeyModel playerAnimationParameterKeyModel,
            IStageTileMapPresenter stageTileMapPresenter,
            ILocomotionLogic locomotionLogic,
            CompositeDisposable compositeDisposable,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Normal, innerState)
        {
            PlayerView = playerView;
            DetectPositionView = detectPositionView;
            MoveVectorView = moveVectorView;
            ActionEventView = actionEventView;
            PlayerAnimatorView = playerAnimatorView;
            CurrentLookModel = currentLookModel;
            LocomotionModel = locomotionModel;
            PlayerAnimationParameterKeyModel = playerAnimationParameterKeyModel;
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
        
        

        private void UpdatePawnDetectorPosition()
        {
            var detectorPosition = PlayerView.Position + CurrentLookModel.LookTo;
            var refinedPosition = StageTileMapPresenter.AlignToMapPosition(0, detectorPosition);
            DetectPositionView.SetPosition(refinedPosition);
        }

        private IPlayerView PlayerView { get; }
        private IDetectPositionView DetectPositionView { get; }
        private IInput_MoveVectorView MoveVectorView { get; }
        private IInput_ActionEventView ActionEventView { get; }
        private ICurrentLookModel CurrentLookModel { get; }
        private ILocomotionModel LocomotionModel { get; }
        private IStageTileMapPresenter StageTileMapPresenter { get; }
        private ILocomotionLogic LocomotionLogic { get; }
        private CompositeDisposable CompositeDisposable { get; }
        private IPlayerAnimatorView PlayerAnimatorView { get; }
        private IPlayerAnimationParameterKeyModel PlayerAnimationParameterKeyModel { get; }
    }
}