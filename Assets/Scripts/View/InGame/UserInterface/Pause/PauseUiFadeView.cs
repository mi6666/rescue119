using Cysharp.Threading.Tasks;
using Interface.ViewInterface.InGame.UserInterface;
using Module.FadeContainer.Runtime;
using UnityEngine;

namespace View.InGame.UserInterface.Pause
{
    public class PauseUiFadeView: MonoBehaviour, IPauseUiView
    {
        [SerializeField] private FadeContainer fadeContainer;
        [SerializeField] private ExitPauseButtonView exitPauseButtonView;
        [SerializeField] private ExitStageButtonView exitStageButtonView;
        
        public ExitPauseButtonView ExitPauseButtonView => exitPauseButtonView;
        public ExitStageButtonView ExitStageButtonView => exitStageButtonView;

        public async UniTask Show()
        {
            exitPauseButtonView.gameObject.SetActive(true);
            exitStageButtonView.gameObject.SetActive(true);
            await fadeContainer.FadeIn();
        }

        public async UniTask Hide()
        {
            await fadeContainer.FadeOut();
            exitPauseButtonView.gameObject.SetActive(false);
            exitStageButtonView.gameObject.SetActive(false);
        }
    }
}