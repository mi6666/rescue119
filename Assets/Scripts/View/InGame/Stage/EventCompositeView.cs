using Interface.ViewInterface.InGame.Stage;
using Interface.ViewInterface.InGame.UserInterface;
using R3;
using Structure.InGame.Stage;
using UnityEngine;

namespace View.InGame.Stage
{
    public class EventCompositeView: MonoBehaviour, IStairEventView, IGameClearEventView, IGameOverEventView
    {
        private Subject<IEventContext> GimmickSubject { get; } = new ();
        private Subject<Unit> ClearSubject { get; } = new ();
        public Observable<IEventContext> GimmickEventObservable => GimmickSubject;
        public Observable<Unit> GameClearObservable => ClearSubject;
        public Observable<Unit> GameOverEvent => Observable.Empty<Unit>();
        
        public void Invoke(IEventContext context)
        {
            GimmickSubject.OnNext(context);
        }

        public void InvokeClear()
        {
            Debug.Log("clear");
            ClearSubject.OnNext(Unit.Default);
        }

        private void OnDestroy()
        {
            GimmickSubject.Dispose();
            ClearSubject.Dispose();
        }
    }
}