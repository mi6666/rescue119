using Module.StateMachine;
using Structure.OutGame;

namespace Controller.OutGame.StageSelect
{
    public class SomeStateController : StageSelectBehaviourBase
    {
        public SomeStateController
        (
            IMutStateType<StageSelectState> innerState
        ) : base(StageSelectState.Some, innerState)
        {
        }
    }
}