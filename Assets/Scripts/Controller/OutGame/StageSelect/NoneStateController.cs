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
            CompositeDisposable disposables,
            IMutStateType<StageSelectState> innerState
        ) : base(StageSelectState.None, innerState)
        {
            _clickStageEventView = clickStageEventView;
            _selectedStageModel = selectedStageModel;
            _disposables = disposables;
        }

        public void Start()
        {
            _clickStageEventView.ClickStageEventObservable
                .Where(this, (s, controller) => controller.IsInState())
                .Subscribe(this, (s, controller) => controller.OnSelect(s))
                .AddTo(_disposables);
        }

        private void OnSelect(string selectedStage)
        {
             _selectedStageModel.SetSelectedStage(selectedStage);
             InnerState.ChangeState(StageSelectState.Some);
        }
        private CompositeDisposable _disposables; 
        private IClickStageEventView _clickStageEventView;
        private ISelectedStageModel _selectedStageModel;
    }
}