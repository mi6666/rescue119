using R3;

namespace Interface.ViewInterface.InGame.UserInterface
{
    public interface IExitPauseEventView
    {
        public Observable<Unit> ExitPauseEvent { get; }
    }
}