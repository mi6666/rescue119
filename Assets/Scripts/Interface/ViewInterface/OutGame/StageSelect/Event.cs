using Module.SceneReference.Runtime;
using R3;
using Structure.OutGame;

namespace Interface.ViewInterface.OutGame.StageSelect
{
    /// <summary>
    /// ステージが選択された際のイベントを提供する
    /// </summary>
    public interface ISelectStageEventView
    {
        public Observable<SceneGroup> SelectStageEventObservable { get; }
    }

    
    /// <summary>
    /// ステージの難易度が選択された際のイベントを提供する
    /// </summary>
    public interface IDifficultyLevelView
    {
        public Observable<DifficultyLevel> SelectObservable { get; }
    }

    public interface IClickGameStart
    {
        public Observable<Unit> ClickGameStartObservable { get; }
    }
}