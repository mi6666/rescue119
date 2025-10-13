using Cysharp.Threading.Tasks;
using Interface.ViewInterface.InGame.UserInterface;
using UnityEngine;

namespace View.InGame.UserInterface.Pause
{
    public class PauseUiView : MonoBehaviour, IPauseUiView
    {
        [SerializeField] private ExitPauseButtonView exitPauseButtonView;
        [SerializeField] private ExitStageButtonView exitStageButtonView;

        public ExitPauseButtonView ExitPauseButtonView => exitPauseButtonView;
        public ExitStageButtonView ExitStageButtonView => exitStageButtonView;

        public UniTask Show()
        {
            exitPauseButtonView.gameObject.SetActive(true);
            exitStageButtonView.gameObject.SetActive(true);

            return UniTask.CompletedTask;
        }

        public UniTask Hide()
        {
            exitPauseButtonView.gameObject.SetActive(false);
            exitStageButtonView.gameObject.SetActive(false);

            return UniTask.CompletedTask;
        }
    }
}