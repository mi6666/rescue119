using Interface.ViewInterface.OutGame.StageSelect;
using Module.SceneReference.Runtime;
using R3;
using UnityEngine;

namespace View.OutGame.StageSelect
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class StageDetectorView : MonoBehaviour, ISelectStageEventView
    {
        private readonly Subject<SceneGroup> _selectSubject = new();
        private readonly Subject<Unit> _unselectSubject = new();
        public Observable<SceneGroup> SelectStageObservable => _selectSubject;
        public Observable<Unit> UnSelectObservable => _unselectSubject;

        private int _currentSelect;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<StageView>(out var stageView))
            {
                _currentSelect = other.GetInstanceID();
                _selectSubject.OnNext(stageView.StageName);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_currentSelect == other.GetInstanceID())
            {
                _unselectSubject.OnNext(Unit.Default);
            }
        }
    }
}