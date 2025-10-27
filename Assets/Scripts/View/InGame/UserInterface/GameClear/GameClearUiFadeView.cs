using Cysharp.Threading.Tasks;
using Interface.ViewInterface.InGame.UserInterface;
using Module.FadeContainer.Runtime;
using UnityEngine;
using View.InGame.UserInterface.Pause;

namespace View.InGame.UserInterface.GameClear
{
    public class GameClearUiFadeView : MonoBehaviour, IGameClearUiFadeView
    {
        [SerializeField] private FadeContainer fadeContainer;
        [SerializeField] private ExitStageButtonView exitStageButtonView;
        
        public ExitStageButtonView ExitStageButtonView => exitStageButtonView;

        public async UniTask Show()
        {
            exitStageButtonView.gameObject.SetActive(true);
            await fadeContainer.FadeIn();
        }

        public async UniTask Hide()
        {
            await fadeContainer.FadeOut();
            exitStageButtonView.gameObject.SetActive(false);
        }
    }
}