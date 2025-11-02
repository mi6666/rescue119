using Interface.ViewInterface.InGame.UserInterface;
using Module.EditorExtension.Runtime;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace View.InGame.UserInterface.Normal
{
    [RequireComponent(typeof(Button))]
    public class PauseButtonView: MonoBehaviour, IPauseEventView
    {
        [SerializeField, AutoAssign] private Button pauseButton;

        public Observable<Unit> PauseEventObservable => _pauseSubject;

        private readonly Subject<Unit> _pauseSubject = new();

        private void Awake()
        {
            pauseButton.onClick.AddListener(Invoke);
        }

        private void Invoke()
        {
            _pauseSubject.OnNext(Unit.Default);
        }
    }
}