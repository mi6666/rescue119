using Interface.ViewInterface.InGame.UserInterface;
using Module.EditorExtension.Runtime;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace View.InGame.UserInterface.Pause
{
    [RequireComponent(typeof(Button))]
    public class ExitPauseButtonView : MonoBehaviour, IExitPauseEventView
    {
        [SerializeField, AutoAssign] private Button exitPauseButton;
        
        private readonly Subject<Unit> _exitPauseSubject = new();
        
        public Observable<Unit> ExitPauseObservable => _exitPauseSubject;

        private void Awake()
        {
            exitPauseButton.onClick.AddListener(() => _exitPauseSubject.OnNext(Unit.Default));
        }

        private void OnDestroy()
        {
            _exitPauseSubject.Dispose();
        }
    }
}