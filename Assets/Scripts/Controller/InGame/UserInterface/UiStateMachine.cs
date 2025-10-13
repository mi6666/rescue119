using System.Collections.Generic;
using Module.StateMachine;
using R3;
using Structure.InGame;

namespace Controller.InGame.UserInterface
{
    public class UiStateMachine : AbstractStateMachine<UserInterfaceStateType>
    {
        public UiStateMachine
        (
            IStateType<UserInterfaceStateType> stateType,
            IReadOnlyList<IStateBehaviour<UserInterfaceStateType>> behaviours,
            CompositeDisposable compositeDisposable
        ) : base(stateType, behaviours, compositeDisposable)
        {
        }
    }

    public class UiStateBehaviour : AbstractStateBehaviour<UserInterfaceStateType>
    {
        public UiStateBehaviour
        (
            UserInterfaceStateType state,
            IMutStateType<UserInterfaceStateType> innerState
        ) : base(state, innerState)
        {
        }
    }

    public class UiStateEntity : AbstractStateType<UserInterfaceStateType>
    {
        public UiStateEntity() : base(UserInterfaceStateType.Normal)
        {
        }
    }
}