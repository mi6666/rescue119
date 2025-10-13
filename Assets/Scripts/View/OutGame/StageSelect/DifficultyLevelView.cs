using Interface.ViewInterface.OutGame.StageSelect;
using R3;
using Structure.OutGame;
using UnityEngine;
using UnityEngine.UI;

namespace View.OutGame.StageSelect
{
    public class DifficultyLevelView : MonoBehaviour, IDifficultyLevelView
    {
        [SerializeField] private Button easy;
        [SerializeField] private Button normal;
        [SerializeField] private Button hard;
        private Subject<DifficultyLevel> SelectSubject { get; } = new Subject<DifficultyLevel>();
        public Observable<DifficultyLevel> SelectObservable => SelectSubject;

        private void Awake()
        {
            easy.onClick.AddListener(() => Event(DifficultyLevel.Easy));
            normal.onClick.AddListener(() => Event(DifficultyLevel.Normal));
            hard.onClick.AddListener(() => Event(DifficultyLevel.Hard));
        }

        private void Event(DifficultyLevel level)
        {
            SelectSubject.OnNext(level);
        }

        private void OnDestroy()
        {
            SelectSubject.Dispose();
        }
    }
}