using System;
using Cysharp.Threading.Tasks;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame.UserInterface;
using Module.StateMachine;
using Structure.InGame;
using VContainer;

namespace Controller.InGame.Primary
{
    public class FloorMoveStateController : PrimaryStateBehaviour
    {
        [Inject]
        public FloorMoveStateController
        (
            IFloorMoveUiView floorMoveUiView,
            IFloorMoveTextView floorMoveTextView,
            IStageFloorModel stageFloorModel,
            IFloorMoveContextModel floorMoveContextModel,
            IFloorMoveTime floorMoveTime,
            IMutStateType<PrimaryStateType> innerState,
            IFloorMoveTime o
        ) : base(PrimaryStateType.FloorMove, innerState)
        {
            FloorMoveUiView = floorMoveUiView;
            FloorMoveTextView = floorMoveTextView;
            StageFloorModel = stageFloorModel;
            FloorMoveContextModel = floorMoveContextModel;
            FloorMoveTime = floorMoveTime;
        }

        public override void OnEnter()
        {
            var currentFloor = StageFloorModel.CurrentFloor;

            FloorMoveTextView.SetFloorMove(currentFloor);

            Wait().Forget();
        }

        private async UniTask Wait()
        {
            await FloorMoveUiView.Show();
            
            // todo なんか

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
        private IFloorMoveContextModel FloorMoveContextModel { get; }
        private IFloorMoveTime FloorMoveTime { get; }
    }
}