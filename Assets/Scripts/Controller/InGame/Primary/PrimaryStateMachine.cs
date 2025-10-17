using System.Collections.Generic;
using Module.StateMachine;
using R3;
using Structure.InGame;

namespace Controller.InGame.Primary
{
    public class PrimaryStateMachine : AbstractStateMachine<PrimaryStateType>
    {
        public PrimaryStateMachine
        (
            IStateType<PrimaryStateType> stateType,
            IReadOnlyList<IStateBehaviour<PrimaryStateType>> behaviours,
            CompositeDisposable compositeDisposable
        ) : base(stateType, behaviours, compositeDisposable)
        {
        }
    }

    public class PrimaryStateBehaviour : AbstractStateBehaviour<PrimaryStateType>
    {
        public PrimaryStateBehaviour
        (
            PrimaryStateType state,
            IMutStateType<PrimaryStateType> innerState
        ) : base(state, innerState)
        {
        }
    }

    public class PrimaryStateEntity : AbstractStateType<PrimaryStateType>
    {
        public PrimaryStateEntity() : base(PrimaryStateType.Normal)
        {
        }
    }
}