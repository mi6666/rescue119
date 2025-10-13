using Cysharp.Threading.Tasks;
using Interface.ViewInterface.InGame.UserInterface;
using Module.StateMachine;
using R3;
using Structure.InGame;
using VContainer.Unity;

namespace Controller.InGame.UserInterface
{
    /// todo
    /// リタイア
    /// ポーズ終了
    public class PauseStateController : UiStateBehaviour, IStartable
    {
        public PauseStateController
        (
            IPauseUiView pauseUiView,
            IExitPauseEventView exitPauseEventView,
            CompositeDisposable compositeDisposable,
            IMutStateType<UserInterfaceStateType> innerState
        ) : base(UserInterfaceStateType.Pause, innerState)
        {
            PauseUiView = pauseUiView;
            ExitPauseEventView = exitPauseEventView;
            CompositeDisposable = compositeDisposable;
        }
        
        public void Start()
        {
            ExitPauseEventView.ExitPauseObservable
                .Subscribe(this, (_, controller) => controller.OffPause())
                .AddTo(CompositeDisposable);
        }

        public override void OnEnter()
        {
            PauseUiView.Show().Forget();
        }

        public override void OnExit()
        {
            PauseUiView.Hide().Forget();
        }

        public void OffPause()
        {
            InnerState.ChangeState(UserInterfaceStateType.Normal);
        }

        public void Retire()
        {
            // todo シーン読み込み
        }
        
        private CompositeDisposable CompositeDisposable { get; }
        private IPauseUiView PauseUiView { get; }
        private IExitPauseEventView ExitPauseEventView { get; }
    }
}