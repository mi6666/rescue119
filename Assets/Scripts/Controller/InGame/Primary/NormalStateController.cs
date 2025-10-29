using Cysharp.Threading.Tasks;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Interface.ViewInterface.InGame.UserInterface;
using Module.StateMachine;
using R3;
using Structure.InGame;
using VContainer;
using VContainer.Unity;

namespace Controller.InGame.Primary
{
    public class NormalStateController : PrimaryStateBehaviour, IStartable
    {
        [Inject]
        public NormalStateController
        (
            INormalUiView normalUiView,
            IHpUiView hpUiView,
            ITimerView timerView,
            IPauseEventView pauseEventView,
            IStairEventView stairEventView,
            IHpModel hpModel,
            ITimeModel timeModel,
            IFloorMoveContextModel floorMoveContextModel,
            CompositeDisposable compositeDisposable,
            IMutStateType<PrimaryStateType> innerState
        ) : base(PrimaryStateType.Normal, innerState)
        {
            NormalUiView = normalUiView;
            HpUiView = hpUiView;
            TimerView = timerView;
            PauseEventView = pauseEventView;
            StairEventView = stairEventView;
            HpModel = hpModel;
            TimeModel = timeModel;
            FloorMoveContextModel = floorMoveContextModel;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            PauseEventView.PauseEventObservable
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (_, controller) => controller.OnPause())
                .AddTo(CompositeDisposable);
            StairEventView.StairsEventObservable
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (type, controller) =>
                {
                    controller.FloorMoveContextModel.SetContext(type);
                    controller.InnerState.ChangeState(PrimaryStateType.FloorMove);
                })
                .AddTo(CompositeDisposable);
        }

        public override void StateUpdate(float deltaTime)
        {
            TimeModel.CountUpTime(deltaTime);

            var currentHp = HpModel.CurrentHp;
            var maxHp = HpModel.MaxHp;
            var remainTime = TimeModel.TimeLength - TimeModel.CurrentTime;

            HpUiView.SetHp(currentHp, maxHp);
            TimerView.SetTime(remainTime);
        }

        private void OnPause()
        {
            InnerState.ChangeState(PrimaryStateType.Pause);
        }

        public override void OnEnter()
        {
            NormalUiView.Show().Forget();
        }

        public override void OnExit()
        {
            NormalUiView.Hide().Forget();
        }

        private CompositeDisposable CompositeDisposable { get; }
        private INormalUiView NormalUiView { get; }
        private IHpUiView HpUiView { get; }
        private ITimerView TimerView { get; }
        private IPauseEventView PauseEventView { get; }
        private IStairEventView StairEventView { get; }
        private IHpModel HpModel { get; }
        private ITimeModel TimeModel { get; }
        private IFloorMoveContextModel FloorMoveContextModel { get; }
    }
}