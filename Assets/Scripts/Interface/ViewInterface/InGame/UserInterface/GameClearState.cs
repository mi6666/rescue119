using Cysharp.Threading.Tasks;
using R3;

namespace Interface.ViewInterface.InGame.UserInterface
{
    public interface IGameClearUiView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    public interface IGameClearEventView
    {
        public Observable<Unit> GameClearObservable { get; }
    }
}