using Cysharp.Threading.Tasks;
using Interface.ModelInterface.InGame;
using Interface.PresenterInterface.Global;
using Interface.ViewInterface.InGame.UserInterface;
using Module.StateMachine;
using Structure.InGame;
using VContainer.Unity;

namespace Controller.InGame.Primary
{
    public class FloorMoveStateController : PrimaryStateBehaviour, IStartable
    {
        public FloorMoveStateController
        (
            IFloorMoveUiView floorMoveUiView,
            IFloorMoveTextView floorMoveTextView,
            IStageFloorModel stageFloorModel,
            IMutStateType<PrimaryStateType> innerState
        ) : base(PrimaryStateType.FloorMove, innerState)
        {
            FloorMoveUiView = floorMoveUiView;
            FloorMoveTextView = floorMoveTextView;
            StageFloorModel = stageFloorModel;
        }

        public void Start()
        {
            
        }
        
        public void OnFloorMove()
        {
            InnerState.ChangeState(PrimaryStateType.FloorMove);
        }

        public override void OnEnter()
        {
            var currentFloor = StageFloorModel.CurrentFloor;
            
            FloorMoveTextView.SetFloorMove(currentFloor);
            FloorMoveUiView.Show().Forget();
        }
        
        public override void OnExit()
        {
            FloorMoveUiView.Hide().Forget();
        }

        public void OffFloorMove()
        {
            InnerState.ChangeState(PrimaryStateType.Normal);
        }

        private IFloorMoveUiView FloorMoveUiView { get; }
        private IFloorMoveTextView FloorMoveTextView { get; }
        private IStageFloorModel StageFloorModel { get; }
    }
}