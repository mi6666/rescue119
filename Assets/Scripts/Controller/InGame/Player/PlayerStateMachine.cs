using System.Collections.Generic;
using Module.StateMachine;
using R3;
using Structure.InGame;

namespace Controller.InGame.Player
{
    public class PlayerStateMachine : AbstractStateMachine<PlayerStateType>
    {
        public PlayerStateMachine
        (
            IStateType<PlayerStateType> stateType,
            IReadOnlyList<IStateBehaviour<PlayerStateType>> behaviours,
            CompositeDisposable compositeDisposable
        ) : base(stateType, behaviours, compositeDisposable)
        {
        }
    }

    public abstract class PlayerStateBehaviourBase : AbstractStateBehaviour<PlayerStateType>
    {
        protected PlayerStateBehaviourBase
        (
            PlayerStateType state,
            IMutStateType<PlayerStateType> innerState
        ) : base(state, innerState)
        {
        }
    }

    public class PlayerStateEntity : AbstractStateType<PlayerStateType>
    {
        public PlayerStateEntity() : base(PlayerStateType.Normal)
        {
        }
    }
}