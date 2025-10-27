using Cysharp.Threading.Tasks;
using R3;

namespace Interface.ViewInterface.InGame.UserInterface
{
    public interface IPauseUiView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    public interface IPauseUiFadeView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    /// <summary>
    /// ポーズ状態から通常状態へ戻るイベントを提供する
    /// </summary>
    public interface IExitPauseEventView
    {
        public Observable<Unit> ExitPauseObservable { get; }
    }

    /// <summary>
    /// ステージを終了するイベントを提供する
    /// </summary>
    public interface IExitStageEventView
    {
        public Observable<Unit> ExitStageObservable { get; }
    }
}