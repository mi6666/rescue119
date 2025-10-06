using System.Runtime.InteropServices.ComTypes;
using Interface.ViewInterface.InGame.UserInterface;
using Module.StateMachine;
using Structure.InGame;
using R3;

namespace Controller.InGame.UserInterface
{
    /// todo
    /// リタイア
    /// リスタート
    public class GameOverStateController : UiStateBehaviour
    {
        public GameOverStateController
        (
            IGameOverEventView gameOverEventView,
            CompositeDisposable compositeDisposable,
            IMutStateType<UserInterfaceStateType> innerState
        ) : base(UserInterfaceStateType.GameOver, innerState)
        {
            GameOverEventView = gameOverEventView;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            GameOverEventView.GameOverEvent
                .Subscribe(this, (_, controller) => controller.GameOver())
                .AddTo(CompositeDisposable);
        }

        public void GameOver()
        {
            InnerState.ChangeState(UserInterfaceStateType.GameOver);
        }
        
        private CompositeDisposable CompositeDisposable { get; }
        private IGameOverEventView GameOverEventView { get; }
    }
}