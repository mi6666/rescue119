using R3;

namespace Interface.ViewInterface.InGame.UserInterface
{
    public interface IGameClearEventView
    {
        public Observable<Unit> GameClearEvent { get; }
    }
}