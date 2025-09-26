using System.Collections.Generic;
using Module.StateMachine;
using R3;
using Structure.OutGame;

namespace Controller.OutGame.StageSelect
{
    public class StageSelectStateMachine : AbstractStateMachine<StageSelectState>
    {
        public StageSelectStateMachine(IStateType<StageSelectState> stateType,
            IReadOnlyList<IStateBehaviour<StageSelectState>> behaviours,
            CompositeDisposable compositeDisposable) : base(stateType, behaviours, compositeDisposable)
        {
        }
    }
    
    public abstract class StageSelectBehaviourBase: AbstractStateBehaviour<StageSelectState>
    {
        protected StageSelectBehaviourBase(StageSelectState state, IMutStateType<StageSelectState> innerState) : base(state, innerState)
        {
        }
    }
}