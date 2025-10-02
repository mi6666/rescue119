using System.Collections.Generic;
using Module.StateMachine;
using R3;
using Structure.InGame;

namespace Controller.InGame.Stage
{
    public class StageStateMachine : AbstractStateMachine<StageStateType>
    {
        public StageStateMachine
        (
            IStateType<StageStateType> stateType,
            IReadOnlyList<IStateBehaviour<StageStateType>> behaviours,
            CompositeDisposable compositeDisposable
        ) : base(stateType, behaviours, compositeDisposable)
        {
        }
    }

    public class StageStateBehaviour : AbstractStateBehaviour<StageStateType>
    {
        public StageStateBehaviour
        (
            StageStateType state,
            IMutStateType<StageStateType> innerState
        ) : base(state, innerState)
        {
        }
    }

    public class StageState : AbstractStateType<StageStateType>
    {
        public StageState() : base(StageStateType.Normal)
        {
        }
    }
}