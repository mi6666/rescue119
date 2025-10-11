using Cysharp.Threading.Tasks;
using Interface.ViewInterface.InGame.UserInterface;
using UnityEngine;

namespace View.InGame.UserInterface.Normal
{
    public class NormalUiView : MonoBehaviour, INormalUiView
    {
        [SerializeField] private TimerView timerView;
        [SerializeField] private HpUiView hpUiView;
        [SerializeField] private PauseButtonView pauseButtonView;

        public TimerView TimerView => timerView;
        public HpUiView HpUiView => hpUiView;
        public PauseButtonView PauseButtonView => pauseButtonView;

        public UniTask Show()
        {
            timerView.gameObject.SetActive(true);
            hpUiView.gameObject.SetActive(true);
            pauseButtonView.gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask Hide()
        {
            timerView.gameObject.SetActive(false);
            hpUiView.gameObject.SetActive(false);
            pauseButtonView.gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}