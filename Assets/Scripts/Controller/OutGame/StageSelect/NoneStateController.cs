using Interface.ModelInterface.OutGame.StageSelect;
using Interface.ViewInterface.OutGame.StageSelect;
using Module.SceneReference.Runtime;
using Module.StateMachine;
using R3;
using Structure.OutGame;
using VContainer.Unity;

namespace Controller.OutGame.StageSelect
{
    /// todo
    /// ステージ選択
    public class NoneStateController : StageSelectBehaviourBase, IStartable
    {
        public NoneStateController
        (
            ISelectStageEventView selectStageEventView,
            ISelectedStageModel selectedStageModel,
            CompositeDisposable compositeDisposable,
            IMutStateType<StageSelectState> innerState
        ) : base(StageSelectState.None, innerState)
        {
            SelectStageEventView = selectStageEventView;
            SelectedStageModel = selectedStageModel;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            SelectStageEventView.ClickStageEventObservable
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (s, controller) => controller.OnSelect(s))
                .AddTo(CompositeDisposable);
        }

        private void OnSelect(SceneGroup selectedStage)
        {
            SelectedStageModel.SetSelectedStage(selectedStage);
            
            InnerState.ChangeState(StageSelectState.Some);
        }

        private CompositeDisposable CompositeDisposable { get; }
        private ISelectStageEventView SelectStageEventView { get; }
        private ISelectedStageModel SelectedStageModel { get; }
    }
}