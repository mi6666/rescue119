using Cysharp.Threading.Tasks;
using Interface.ViewInterface.InGame.UserInterface;
using UnityEngine;

namespace View.InGame.UserInterface.GameClear
{
    public class GameClearUiView : MonoBehaviour, IGameClearUiView
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