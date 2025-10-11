using Interface.ViewInterface.OutGame.StageSelect;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace View.OutGame.StageSelect
{
    [RequireComponent(typeof(Button))]
    public class StartGameButtonView : MonoBehaviour, IGameStartEventView
    {
        private readonly Subject<Unit> _startSubject = new();
        public Observable<Unit> StartObservable => _startSubject;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(StartEvent);
        }

        private void StartEvent()
        {
            _startSubject.OnNext(Unit.Default);
        }
    }
}