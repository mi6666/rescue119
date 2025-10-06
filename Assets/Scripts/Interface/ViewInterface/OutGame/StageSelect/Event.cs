using Module.SceneReference.Runtime;
using R3;
using Structure.OutGame;

namespace Interface.ViewInterface.OutGame.StageSelect
{
    public interface ISelectStageEventView
    {
        public Observable<SceneGroup> ClickStageEventObservable { get; }
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