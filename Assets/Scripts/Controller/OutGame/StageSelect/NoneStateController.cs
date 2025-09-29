using Interface.ModelInterface.OutGame.StageSelect;
using Interface.ViewInterface.OutGame.StageSelect;
using Module.StateMachine;
using R3;
using Structure.OutGame;
using VContainer.Unity;

namespace Controller.OutGame.StageSelect
{
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
            // todo ステージ選択イベントを購読
        }

        private void OnSelect(string selectedStage)
        {
            // todo 選択されたステージを保持し、選択済み状態へ
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IClickStageEventView ClickStageEventView { get; }
        private ISelectedStageModel SelectedStageModel { get; }
    }
}