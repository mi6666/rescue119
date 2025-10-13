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
        public Observable<SceneGroup> SelectStageObservable { get; }
        public Observable<Unit> UnSelectObservable { get; }
    }

    
    /// <summary>
    /// ステージの難易度が選択された際のイベントを提供する
    /// </summary>
    public interface IDifficultyLevelView
    {
        public Observable<DifficultyLevel> SelectObservable { get; }
    }

    /// <summary>
    /// ゲーム開始が選択された際のイベントを提供する
    /// </summary>
    public interface IGameStartEventView
    {
        public Observable<Unit> StartObservable { get; }
    }
}