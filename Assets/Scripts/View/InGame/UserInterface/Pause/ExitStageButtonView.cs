using Interface.ViewInterface.InGame.UserInterface;
using Module.EditorExtension.Runtime;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace View.InGame.UserInterface.Pause
{
    [RequireComponent(typeof(Button))]
    public class ExitStageButtonView : MonoBehaviour, IExitStageEventView
    {
        [SerializeField, AutoAssign] private Button exitStageButton;

        private readonly Subject<Unit> _exitStageSubject = new();

        public Observable<Unit> ExitStageObservable => _exitStageSubject;

        private void Awake()
        {
            exitStageButton.onClick.AddListener(() => _exitStageSubject.OnNext(Unit.Default));
        }
    }
}