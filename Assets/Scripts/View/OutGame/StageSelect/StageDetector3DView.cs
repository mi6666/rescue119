using Interface.ViewInterface.OutGame.StageSelect;
using Module.SceneReference.Runtime;
using R3;
using UnityEngine;

namespace View.OutGame.StageSelect
{
    public class StageDetector3DView : MonoBehaviour, ISelectStageEventView
    {
        private readonly Subject<SceneGroup> _selectSubject = new();
        private readonly Subject<Unit> _unselectSubject = new();
        public Observable<SceneGroup> SelectStageObservable => _selectSubject;
        public Observable<Unit> UnSelectObservable => _unselectSubject;

        private int _currentSelect;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<StageView>(out var stageView))
            {
                _currentSelect = other.GetInstanceID();
                _selectSubject.OnNext(stageView.StageName);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_currentSelect == other.GetInstanceID())
            {
                _unselectSubject.OnNext(Unit.Default);
            }
        }
    }
}
