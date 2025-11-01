using System;
using Cysharp.Threading.Tasks;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame.UserInterface;
using Module.StateMachine;
using Structure.InGame;
using VContainer;

namespace Controller.InGame.Primary
{
    public class FloorTransitionStateController : PrimaryStateBehaviour
    {
        [Inject]
        public FloorTransitionStateController
        (
            IFloorMoveUiView floorMoveUiView,
            IFloorMoveTextView floorMoveTextView,
            IStageFloorModel stageFloorModel,
            IFloorMoveContextModel floorMoveContextModel,
            IFloorMoveTime floorMoveTime,
            IMutStateType<PrimaryStateType> innerState
        ) : base(PrimaryStateType.FloorTransition, innerState)
        {
            FloorMoveUiView = floorMoveUiView;
            FloorMoveTextView = floorMoveTextView;
            StageFloorModel = stageFloorModel;
            FloorMoveContextModel = floorMoveContextModel;
            FloorMoveTime = floorMoveTime;
        }

        public override void OnEnter()
        {
            Wait().Forget();
        }

        private async UniTask Wait()
        {
            var currentFloor = StageFloorModel.CurrentFloor;
            StageFloorModel.SetFloor(currentFloor);
            await FloorMoveUiView.Show();
            var context = FloorMoveContextModel.StairType;
            FloorMoveTextView.SetFloorMove(currentFloor, context);

            if (context == StairType.Up)
            {
                currentFloor++;
            }
            else
            {
                currentFloor--;
            }

            StageFloorModel.SetFloor(currentFloor);

            await UniTask.Delay(TimeSpan.FromSeconds(FloorMoveTime.FloorTime));

            await FloorMoveUiView.Hide();
            InnerState.ChangeState(PrimaryStateType.Normal);
        }

        private IFloorMoveUiView FloorMoveUiView { get; }
        private IFloorMoveTextView FloorMoveTextView { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IFloorMoveContextModel FloorMoveContextModel { get; }
        private IFloorMoveTime FloorMoveTime { get; }
    }
}