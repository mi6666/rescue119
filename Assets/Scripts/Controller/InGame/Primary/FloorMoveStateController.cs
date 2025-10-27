using System;
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
            IFloorMoveTime floorMoveTime,
            IMutStateType<PrimaryStateType> innerState, IFloorMoveTime o) : base(PrimaryStateType.FloorMove, innerState)
        {
            FloorMoveUiView = floorMoveUiView;
            FloorMoveTextView = floorMoveTextView;
            StageFloorModel = stageFloorModel;
            FloorMoveTime = floorMoveTime;
        }

        public void Start()
        {
            
        }

        public override void OnEnter()
        {
            var currentFloor = StageFloorModel.CurrentFloor;
            
            FloorMoveTextView.SetFloorMove(currentFloor);
            FloorMoveUiView.Show().Forget();
            
            Wait().Forget();
        }

        private async UniTask Wait()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(FloorMoveTime.FloorTime));
            
            InnerState.ChangeState(PrimaryStateType.Normal);
        }
        
        public override void OnExit()
        {
            FloorMoveUiView.Hide().Forget();
        }

        private IFloorMoveUiView FloorMoveUiView { get; }
        private IFloorMoveTextView FloorMoveTextView { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IFloorMoveTime FloorMoveTime { get; }
    }
}