using Interface.ViewInterface.InGame.UserInterface;
using Module.EditorExtension.Runtime;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace View.InGame.UserInterface.GameClear
{
    [RequireComponent(typeof(Button))]
    public class ClearButtonView: MonoBehaviour, IGameClearEventView
    {
        [SerializeField, AutoAssign] private Button clearButton;
        public Observable<Unit> GameClearObservable => _clearSubject;
        private readonly Subject<Unit> _clearSubject = new();
        private void Awake()
        {
            clearButton.onClick.AddListener(Invoke);
        }

        private void Invoke()
        {
            _clearSubject.OnNext(Unit.Default);
        }

    }
}