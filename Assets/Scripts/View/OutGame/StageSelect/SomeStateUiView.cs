using Cysharp.Threading.Tasks;
using Interface.ViewInterface.OutGame.StageSelect;
using UnityEngine;

namespace View.OutGame.StageSelect
{
    public class SomeStateUiView : MonoBehaviour, ISomeStateUiView
    {
        [SerializeField] private DifficultyLevelView difficultyLevelView;
        [SerializeField] private StartGameButtonView startGameButtonView;

        public DifficultyLevelView DifficultyLevelView => difficultyLevelView;
        public StartGameButtonView StartGameButtonView => startGameButtonView;

        public UniTask Show()
        {
            difficultyLevelView.gameObject.SetActive(true);
            startGameButtonView.gameObject.SetActive(true);

            return UniTask.CompletedTask;
        }

        public UniTask Hide()
        {
            difficultyLevelView.gameObject.SetActive(false);
            startGameButtonView.gameObject.SetActive(false);

            return UniTask.CompletedTask;
        }
    }
}