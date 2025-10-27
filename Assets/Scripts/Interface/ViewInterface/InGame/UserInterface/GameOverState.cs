using Cysharp.Threading.Tasks;
using R3;

namespace Interface.ViewInterface.InGame.UserInterface
{
    public interface IGameOverUiView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    public interface IGameOverUiFadeView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    public interface IGameOverEventView
    {
        public Observable<Unit> GameOverEvent { get; }
    }
}