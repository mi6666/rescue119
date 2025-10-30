using Interface.ViewInterface.InGame;
using Module.StateMachine;
using R3;
using Structure.InGame;
using VContainer.Unity;

namespace Controller.InGame.Player
{
    public class StopStateController : PlayerStateBehaviourBase, IStartable
    {
        public StopStateController
        (
            IPrimaryStateEventView primaryStateEventView,
            CompositeDisposable compositeDisposable,
            IMutStateType<PlayerStateType> innerState
        ) : base(PlayerStateType.Stopping, innerState)
        {
            PrimaryStateEventView = primaryStateEventView;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            PrimaryStateEventView.StateObservable
                .Subscribe(this, (type, controller) => controller.OnStateChange(type))
                .AddTo(CompositeDisposable);
        }

        private void OnStateChange(PrimaryStateType type)
        {
            if (type == PrimaryStateType.Normal)
            {
                InnerState.ChangeState(_prevState);
            }
            else
            {
                _prevState = InnerState.CurrentState;
                InnerState.ChangeState(PlayerStateType.Stopping);
            }
        }

        private PlayerStateType _prevState;
        private CompositeDisposable CompositeDisposable { get; }
        private IPrimaryStateEventView PrimaryStateEventView { get; }
    }
}