using Interface.ViewInterface.InGame;
using R3;
using Structure.InGame;

namespace View.InGame
{
    public class PrimaryStateEventView : IPrimaryStateEventView
    {
        private Subject<PrimaryStateType> EventSubject { get; } = new();
        public Observable<PrimaryStateType> StateObservable => EventSubject;

        public void Invoke(PrimaryStateType next)
        {
            EventSubject.OnNext(next);
        }
    }
}