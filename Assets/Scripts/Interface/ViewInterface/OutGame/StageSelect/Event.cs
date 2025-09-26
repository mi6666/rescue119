using R3;

namespace Interface.ViewInterface.OutGame.StageSelect
{
    public interface IClickStageEventView
    {
        public Observable<string> ClickStageEventObservable { get; }
    }
}