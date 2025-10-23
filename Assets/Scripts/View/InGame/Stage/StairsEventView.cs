using Interface.ViewInterface.InGame;
using R3;
using Structure.InGame;
using UnityEngine;

namespace View.InGame.Stage
{
    public class StairsEventView : MonoBehaviour, IStairsEventView
    {
        private Subject<StairType> StairSubject { get; } = new();
        public Observable<StairType> StairsEventObservable => StairSubject;

        public void Invoke(StairType type)
        {
            StairSubject.OnNext(type);
        }

        private void OnDestroy()
        {
            StairSubject.Dispose();
        }
    }
}