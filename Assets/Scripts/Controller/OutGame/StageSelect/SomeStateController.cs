using Interface.ModelInterface.OutGame.StageSelect;
using Interface.ViewInterface.OutGame.StageSelect;
using Module.StateMachine;
using R3;
using Structure.OutGame;
using VContainer.Unity;

namespace Controller.OutGame.StageSelect
{
    public class SomeStateController : StageSelectBehaviourBase, IStartable
    {
        public SomeStateController
        (
            IClickStageEventView clickStageEventView,
            ISelectedStageModel selectedStageModel,
            CompositeDisposable compositeDisposable,
            IMutStateType<StageSelectState> innerState
        ) : base(StageSelectState.Some, innerState)
        {
            ClickStageEventView = clickStageEventView;
            SelectedStageModel = selectedStageModel;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            // todo ステージ選択
        }
        
        private CompositeDisposable CompositeDisposable { get; }
        private IClickStageEventView ClickStageEventView { get; }
        private ISelectedStageModel SelectedStageModel { get; }
    }
}