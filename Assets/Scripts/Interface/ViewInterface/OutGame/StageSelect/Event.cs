using R3;
using Structure.OutGame;

namespace Interface.ViewInterface.OutGame.StageSelect
{
    public interface ISelectStageEventView
    {
        public Observable<string> ClickStageEventObservable { get; }
    }

    public interface IClickDifficultyLevel
    {
        public Observable<DifficultyLevel> ClickStageEventObservable { get; }
    }

    public interface IClickGameStart
    {
        public Observable<Unit> ClickGameStartObservable { get; }
    }
}