using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Module.StateMachine;
using R3;
using Structure.InGame;
using VContainer.Unity;

namespace Controller.InGame.Stage
{
    public class FloorTransitionStateController : StageStateBehaviour, IStartable
    {
        public FloorTransitionStateController
        (
            IPrimaryStateEventView primaryStateEventView,
            IFloorView floorView,
            IStageFloorModel stageFloorModel,
            CompositeDisposable compositeDisposable,
            IMutStateType<StageStateType> innerState
        ) : base(StageStateType.FloorTransition, innerState)
        {
            PrimaryStateEventView = primaryStateEventView;
            FloorView = floorView;
            StageFloorModel = stageFloorModel;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            PrimaryStateEventView.StateObservable
                .Subscribe(this, (type, controller) => controller.StateControl(type))
                .AddTo(CompositeDisposable);
            StageFloorModel.FloorObservable
                .Pairwise()
                .Subscribe(this, (i, controller) => controller.FloorControl(i.Previous, i.Current))
                .AddTo(CompositeDisposable);
        }

        private void FloorControl(int prev, int current)
        {
            FloorView.MoveFloor(prev, current);
        }

        private void StateControl(PrimaryStateType nextState)
        {
            if (nextState == PrimaryStateType.FloorTransition)
            {
                InnerState.ChangeState(StageStateType.FloorTransition);
            }
            else if (IsInState())
            {
                InnerState.ChangeState(StageStateType.Normal);
            }
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IPrimaryStateEventView PrimaryStateEventView { get; }
        private IFloorView FloorView { get; }
        private IStageFloorModel StageFloorModel { get; }
    }
}