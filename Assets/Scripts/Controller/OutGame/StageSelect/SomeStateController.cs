using Cysharp.Threading.Tasks;
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
            ISomeStateUiView someStateUiView,
            ISelectStageEventView selectStageEventView,
            IDifficultyLevelView difficultyLevelView,
            IGameStartEventView gameStartEventView,
            ISelectedStageModel selectedStageModel,
            IStageInfoModel stageInfoModel,
            IScenePresenter scenePresenter,
            CompositeDisposable compositeDisposable,
            IMutStateType<StageSelectState> innerState
        ) : base(StageSelectState.Some, innerState)
        {
            SomeStateUiView = someStateUiView;
            SelectStageEventView = selectStageEventView;
            DifficultyLevelView = difficultyLevelView;
            GameStartEventView = gameStartEventView;
            SelectedStageModel = selectedStageModel;
            StageInfoModel = stageInfoModel;
            ScenePresenter = scenePresenter;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            DifficultyLevelView.SelectObservable
                .Subscribe(this, (level, controller) => controller.DifficultyLevelSelected(level))
                .AddTo(CompositeDisposable);
            SelectStageEventView.UnSelectObservable
                .Subscribe(this, (_, controller) => controller.ExitSomeState())
                .AddTo(CompositeDisposable);
            GameStartEventView.StartObservable
                .Subscribe(this, (_, controller) => controller.StartGame())
                .AddTo(CompositeDisposable);
        }

        private void DifficultyLevelSelected(DifficultyLevel level)
        {
            StageInfoModel.SetDifficultyLevel(level);
        }

        private void ExitSomeState()
        {
            InnerState.ChangeState(StageSelectState.None);
        }

        private void StartGame()
        {
            var selectedStage = SelectedStageModel.GetSelectedStage();

            ScenePresenter.LoadScene(selectedStage).Forget();
        }

        public override void OnEnter()
        {
            SomeStateUiView.Show().Forget();
        }

        public override void OnExit()
        {
            SomeStateUiView.Hide().Forget();
        }


        private CompositeDisposable CompositeDisposable { get; }
        private ISomeStateUiView SomeStateUiView { get; }
        private ISelectStageEventView SelectStageEventView { get; }
        private IDifficultyLevelView DifficultyLevelView { get; }
        private IGameStartEventView GameStartEventView { get; }
        private ISelectedStageModel SelectedStageModel { get; }
        private IStageInfoModel StageInfoModel { get; }
        private IScenePresenter ScenePresenter { get; }
    }
}