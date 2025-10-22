using Interface.ViewInterface.InGame.UserInterface;
using Module.EditorExtension.Runtime;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace View.InGame.UserInterface.GameOver
{
    [RequireComponent(typeof(Button))]
    public class GameOverButtonView : MonoBehaviour, IGameOverEventView
    {
        [SerializeField, AutoAssign] private Button clearButton;

        public Observable<Unit> GameOverEvent => _gameOverSubject;
        private readonly Subject<Unit> _gameOverSubject = new();

        private void Awake()
        {
            clearButton.onClick.AddListener(Invoke);
        }

        private void Invoke()
        {
            _gameOverSubject.OnNext(Unit.Default);
        }
    }
}