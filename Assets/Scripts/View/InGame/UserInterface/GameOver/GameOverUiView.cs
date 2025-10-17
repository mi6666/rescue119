using Cysharp.Threading.Tasks;
using Interface.ViewInterface.InGame.UserInterface;
using UnityEngine;

namespace View.InGame.UserInterface.GameOver
{
    public class GameOverUiView: MonoBehaviour, IGameOverUiView
    {
        public UniTask Show()
        {
            return UniTask.CompletedTask;
        }

        public UniTask Hide()
        {
            return UniTask.CompletedTask;
        }
    }
}