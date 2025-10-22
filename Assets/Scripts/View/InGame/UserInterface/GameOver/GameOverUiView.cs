using Cysharp.Threading.Tasks;
using Interface.ViewInterface.InGame.UserInterface;
using UnityEngine;
using View.InGame.UserInterface.Pause;

namespace View.InGame.UserInterface.GameOver
{
    public class GameOverUiView: MonoBehaviour, IGameOverUiView
    {
        [SerializeField] private ExitStageButtonView exitStageButtonView;

        public ExitStageButtonView ExitStageButtonView => exitStageButtonView;
        
        public UniTask Show()
        {
            exitStageButtonView.gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask Hide()
        {
            exitStageButtonView.gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}