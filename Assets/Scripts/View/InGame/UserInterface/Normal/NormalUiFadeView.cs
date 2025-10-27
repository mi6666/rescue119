using Cysharp.Threading.Tasks;
using Interface.ViewInterface.InGame.UserInterface;
using Module.FadeContainer.Runtime;
using UnityEngine;

namespace View.InGame.UserInterface.Normal
{
    public class NormalUiFadeView : MonoBehaviour, INormalUiView
    {
        [SerializeField] private FadeContainer fadeContainer;
        [SerializeField] private TimerView timerView;
        [SerializeField] private HpUiView hpUiView;
        [SerializeField] private PauseButtonView pauseButtonView;
        
        public TimerView TimerView => timerView;
        public HpUiView HpUiView => hpUiView;
        public PauseButtonView PauseButtonView => pauseButtonView;
        
        public async UniTask Show()
        {
            timerView.gameObject.SetActive(true);
            hpUiView.gameObject.SetActive(true);
            pauseButtonView.gameObject.SetActive(true);
            await fadeContainer.FadeIn();
        }

        public async UniTask Hide()
        {
            await fadeContainer.FadeOut();
            timerView.gameObject.SetActive(false);
            hpUiView.gameObject.SetActive(false);
            pauseButtonView.gameObject.SetActive(false);
        }
    }
}