using Interface.ViewInterface.InGame.Stage;
using R3;
using Structure.InGame.Stage;
using UnityEngine;

namespace View.InGame.Stage
{
    public class EventCompositeView: MonoBehaviour, IStairEventView
    {
        private Subject<IEventContext> GimmickSubject { get; } = new ();
        public Observable<IEventContext> GimmickEventObservable => GimmickSubject;
        
        public void Invoke(IEventContext context)
        {
            GimmickSubject.OnNext(context);
        }
    }
}