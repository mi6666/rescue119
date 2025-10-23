using Interface.ViewInterface.InGame;
using R3;
using Structure.InGame.Stage;

namespace View.InGame.Stage
{
    public class GimmickEventView : IGimmickEventView
    {
        public GimmickEventView
        (
            CompositeDisposable compositeDisposable
        )
        {
            EventSubject = new();
            EventSubject.AddTo(compositeDisposable);
        }

        public Observable<EventContext> GimmickEventObservable => EventSubject;

        public void Invoke(EventContext context)
        {
            EventSubject.OnNext(context);
        }

        private Subject<EventContext> EventSubject { get; }
    }
}