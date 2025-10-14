using Interface.ModelInterface.InGame;
using Interface.PresenterInterface.Global;
using Interface.ViewInterface.InGame.UserInterface;
using Module.StateMachine;
using Structure.InGame;
using R3;
using VContainer.Unity;

namespace Controller.InGame.UserInterface
{
    /// todo
    /// リタイア
    /// リスタート
    public class GameOverStateController : UiStateBehaviour,IStartable
    {
        public GameOverStateController
        (
            IGameOverEventView gameOverEventView,
            IScenePresenter scenePresenter,
            IExitGameSceneModel exitGameSceneModel,
            IExitStageEventView exitStageEventView,
            CompositeDisposable compositeDisposable,
            IMutStateType<UserInterfaceStateType> innerState
        ) : base(UserInterfaceStateType.GameOver, innerState)
        {
            GameOverEventView = gameOverEventView;
            ScenePresenter = scenePresenter;
            ExitGameSceneModel = exitGameSceneModel;
            ExitStageEventView = exitStageEventView;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            GameOverEventView.GameOverEvent
                .Subscribe(this, (_, controller) => controller.GameOver())
                .AddTo(CompositeDisposable);
            ExitStageEventView.ExitStageObservable
                .Subscribe(this, (_, controller) => controller.GameOverBack())
                .AddTo(CompositeDisposable);
        }

        private void GameOver()
        {
            InnerState.ChangeState(UserInterfaceStateType.GameOver);
        }

        private void GameOverBack()
        {
            ScenePresenter.LoadScene(ExitGameSceneModel.StageSelect);
        }
        
        private CompositeDisposable CompositeDisposable { get; }
        private IGameOverEventView GameOverEventView { get; }
        private IScenePresenter ScenePresenter { get; }
        private IExitGameSceneModel ExitGameSceneModel { get; }
        private IExitStageEventView ExitStageEventView { get; }
    }
}