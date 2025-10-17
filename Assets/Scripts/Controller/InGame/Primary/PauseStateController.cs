using Cysharp.Threading.Tasks;
using Interface.ModelInterface.InGame;
using Interface.PresenterInterface.Global;
using Interface.ViewInterface.InGame.UserInterface;
using Module.StateMachine;
using R3;
using Structure.InGame;
using VContainer.Unity;

namespace Controller.InGame.Primary
{
    /// todo
    /// リタイア
    /// ポーズ終了
    public class PauseStateController : PrimaryStateBehaviour, IStartable
    {
        public PauseStateController
        (
            IPauseUiView pauseUiView,
            IExitStageEventView exitStageEventView,
            IExitPauseEventView exitPauseEventView,
            IScenePresenter scenePresenter,
            IExitGameSceneModel exitGameSceneModel,
            CompositeDisposable compositeDisposable,
            IMutStateType<PrimaryStateType> innerState
        ) : base(PrimaryStateType.Pause, innerState)
        {
            PauseUiView = pauseUiView;
            ExitStageEventView = exitStageEventView;
            ExitPauseEventView = exitPauseEventView;
            CompositeDisposable = compositeDisposable;
            ScenePresenter = scenePresenter;
            ExitGameSceneModel = exitGameSceneModel;
        }

        public void Start()
        {
            ExitPauseEventView.ExitPauseObservable
                .Subscribe(this, (_, controller) => controller.OffPause())
                .AddTo(CompositeDisposable);
            ExitStageEventView.ExitStageObservable
                .Subscribe(this, (_, controller) => controller.Retire())
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

        private void OffPause()
        {
            InnerState.ChangeState(PrimaryStateType.Normal);
        }

        private void Retire()
        {
            ScenePresenter.LoadScene(ExitGameSceneModel.StageSelect).Forget();
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IPauseUiView PauseUiView { get; }
        private IExitPauseEventView ExitPauseEventView { get; }
        private IScenePresenter ScenePresenter { get; }
        private IExitGameSceneModel ExitGameSceneModel { get; }
        private IExitStageEventView ExitStageEventView { get; }
    }
}