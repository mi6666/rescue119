using R3;
using Structure.InGame;

namespace Interface.ViewInterface.InGame
{
    /// <summary>
    /// ゲームの状態変化イベントを発行する。
    /// </summary>
    public interface IPrimaryStateEventView
    {
        public Observable<PrimaryStateType> StateObservable { get; }
        public void Invoke(PrimaryStateType next);
    }
}