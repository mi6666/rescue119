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
    /// リスタート
    public class GameOverStateController : PrimaryStateBehaviour, IStartable
    {
        public GameOverStateController
        (
            IGameOverEventView gameOverEventView,
            IScenePresenter scenePresenter,
            IExitGameSceneModel exitGameSceneModel,
            IExitStageEventView exitStageEventView,
            CompositeDisposable compositeDisposable,
            IMutStateType<PrimaryStateType> innerState
        ) : base(PrimaryStateType.GameOver, innerState)
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
            InnerState.ChangeState(PrimaryStateType.GameOver);
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