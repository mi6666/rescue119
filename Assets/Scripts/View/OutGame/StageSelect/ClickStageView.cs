using Interface.ViewInterface.OutGame.StageSelect;
using Module.SceneReference.Runtime;
using R3;
using UnityEngine;

namespace View.OutGame.StageSelect
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ClickStageView : MonoBehaviour, ISelectStageEventView
    {
        private readonly Subject<SceneGroup> _selectSubject = new ();
        public Observable<SceneGroup> ClickStageEventObservable => _selectSubject;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<StageView>(out var stageView))
            {
                _selectSubject.OnNext(stageView.StageName);
            }
        }
    }
}