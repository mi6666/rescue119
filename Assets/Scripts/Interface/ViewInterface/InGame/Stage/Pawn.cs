using R3;
using Structure.InGame;
using Structure.InGame.Stage;

namespace Interface.ViewInterface.InGame.Stage
{
    public interface IPawnEventView
    {
        public Observable<IEventContext> GimmickEventObservable { get; }
        public void Invoke(IEventContext context);
    }

    public interface IStairEventView : IPawnEventView
    {
        public Observable<StairType> StairsEventObservable => GimmickEventObservable
            .Where(x => x is StairContext)
            .Select(x => (x as StairContext)!.EventContext.StairType);
    }
}