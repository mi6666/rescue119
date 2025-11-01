using Cysharp.Threading.Tasks;
using Interface.ModelInterface.InGame;
using Interface.PresenterInterface.Global;
using Interface.ViewInterface.InGame.UserInterface;
using JetBrains.Annotations;
using Module.StateMachine;
using R3;
using Structure.InGame;
using VContainer;
using VContainer.Unity;

namespace Controller.InGame.Primary
{
    public class GameOverStateController : PrimaryStateBehaviour, IStartable
    {
        [UsedImplicitly]
        public GameOverStateController
        (
            IGameOverEventView gameOverEventView,
            IGameOverUiView gameOverUiView,
            [Key(PrimaryStateType.GameOver)] IExitStageEventView exitStageEventView,
            IHpModel hpModel,
            IExitGameSceneModel exitGameSceneModel,
            IScenePresenter scenePresenter,
            CompositeDisposable compositeDisposable,
            IMutStateType<PrimaryStateType> innerState
        ) : base(PrimaryStateType.GameOver, innerState)
        {
            GameOverEventView = gameOverEventView;
            GameOverUiView = gameOverUiView;
            ExitStageEventView = exitStageEventView;
            HpModel = hpModel;
            ScenePresenter = scenePresenter;
            ExitGameSceneModel = exitGameSceneModel;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            HpModel.IsDeadObservable
                .Where(x => x)
                .Subscribe(this, (_, controller) => controller.GameOver())
                .AddTo(CompositeDisposable);
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

        public override void OnEnter()
        {
            GameOverUiView.Show().Forget();
        }

        public override void OnExit()
        {
            GameOverUiView.Hide().Forget();
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IGameOverEventView GameOverEventView { get; }
        private IGameOverUiView GameOverUiView { get; }
        private IHpModel HpModel { get; }
        private IExitGameSceneModel ExitGameSceneModel { get; }
        private IExitStageEventView ExitStageEventView { get; }
        private IScenePresenter ScenePresenter { get; }
    }
}