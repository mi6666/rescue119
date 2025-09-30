using Interface.ModelInterface.OutGame.StageSelect;
using Interface.ViewInterface.OutGame.StageSelect;
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
            IClickStageEventView clickStageEventView,
            ISelectedStageModel selectedStageModel,
            CompositeDisposable compositeDisposable,
            IMutStateType<StageSelectState> innerState
        ) : base(StageSelectState.None, innerState)
        {
            ClickStageEventView = clickStageEventView;
            SelectedStageModel = selectedStageModel;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            ClickStageEventView.ClickStageEventObservable
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (s, controller) => controller.OnSelect(s))
                .AddTo(CompositeDisposable);
        }

        private void OnSelect(string selectedStage)
        {
            // todo 選択されたステージを保持し、選択済み状態へ
            
            InnerState.ChangeState(StageSelectState.Some);
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IClickStageEventView ClickStageEventView { get; }
        private ISelectedStageModel SelectedStageModel { get; }
    }
}