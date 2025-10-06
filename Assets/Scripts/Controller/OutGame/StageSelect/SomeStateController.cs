using Interface.ModelInterface.OutGame.StageSelect;
using Interface.PresenterInterface.Global;
using Interface.ViewInterface.OutGame.StageSelect;
using Module.StateMachine;
using R3;
using Structure.OutGame;
using VContainer.Unity;

namespace Controller.OutGame.StageSelect
{
    /// todo
    /// ステージの詳細を表示
    /// 難易度選択
    /// 選択状態の解除
    public class SomeStateController : StageSelectBehaviourBase, IStartable
    {
        public SomeStateController
        (
            IClickStageEventView clickStageEventView,
            IClickDifficultyLevel clickDifficultyLevel,
            ISelectedStageModel selectedStageModel,
            IStageInfoModel stageInfoModel,
            IScenePresenter scenePresenter,
            CompositeDisposable compositeDisposable,
            IMutStateType<StageSelectState> innerState
        ) : base(StageSelectState.Some, innerState)
        {
            ClickStageEventView = clickStageEventView;
            ClickDifficultyLevel = clickDifficultyLevel;
            SelectedStageModel = selectedStageModel;
            StageInfoModel = stageInfoModel;
            ScenePresenter = scenePresenter;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            ClickDifficultyLevel.ClickStageEventObservable
                .Subscribe(this, (level, controller) => controller.DifficultyLevelSelected(level))
                .AddTo(CompositeDisposable);
        }

        private void DifficultyLevelSelected(DifficultyLevel level)
        {
            StageInfoModel.SetDifficultyLevel(level);
        }

        private void StartGame()
        {
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IClickStageEventView ClickStageEventView { get; }
        private IClickDifficultyLevel ClickDifficultyLevel { get; }
        private ISelectedStageModel SelectedStageModel { get; }
        private IStageInfoModel StageInfoModel { get; }
        private IScenePresenter ScenePresenter { get; }
    }
}