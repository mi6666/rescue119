using R3;

namespace Interface.ViewInterface.InGame.UserInterface
{
    public interface IGameOverEventView
    {
        public Observable<Unit> GameOverEvent { get; }
    }

}