using Interface.ModelInterface.InGame;
using Interface.PresenterInterface.Global;
using Interface.ViewInterface.InGame.UserInterface;
using Module.StateMachine;
using R3;
using Structure.InGame;
using VContainer.Unity;

namespace Controller.InGame.UserInterface
{
    /// todo
    /// スコア表示
    /// クリアタイム表示
    /// ステージセレクトへ
    /// リスタート
    public class GameClearStateController : UiStateBehaviour,IStartable
    {
        public GameClearStateController
        (
            IGameClearEventView gameClearEventView,
            IScenePresenter scenePresenter,
            IExitGameSceneModel exitGameSceneModel,
            IExitStageEventView exitStageEventView,
            CompositeDisposable compositeDisposable,
            IMutStateType<UserInterfaceStateType> innerState
        ) : base(UserInterfaceStateType.GameClear, innerState)
        {
            GameClearEventView = gameClearEventView;
            ScenePresenter = scenePresenter;
            ExitGameSceneModel = exitGameSceneModel;
            ExitStageEventView = exitStageEventView;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            GameClearEventView.GameClearObservable
                .Subscribe(this, (_, controller) => controller.GameClear())
                .AddTo(CompositeDisposable);
            ExitStageEventView.ExitStageObservable
                .Subscribe(this, (_, controller) => controller.GameClearNext())
                .AddTo(CompositeDisposable);
        }

        private void GameClear()
        {
            InnerState.ChangeState(UserInterfaceStateType.GameClear);
        }

        private void GameClearNext()
        {
            ScenePresenter.LoadScene(ExitGameSceneModel.StageSelect);
        }
        
        private IGameClearEventView GameClearEventView { get; }
        private CompositeDisposable CompositeDisposable { get; }
        private IScenePresenter ScenePresenter { get; }
        private IExitGameSceneModel ExitGameSceneModel { get; }
        private IExitStageEventView ExitStageEventView { get; }
    }
}