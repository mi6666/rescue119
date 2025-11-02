using Cysharp.Threading.Tasks;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Interface.ViewInterface.InGame.UserInterface;
using Module.StateMachine;
using R3;
using Structure.InGame;
using UnityEngine;
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
            IPrimaryStateEventView primaryStateEventView,
            IHpModel hpModel,
            IHpSetting hpSetting,
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
            PrimaryStateEventView = primaryStateEventView;
            HpModel = hpModel;
            HpSetting = hpSetting;
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
                    Debug.Log("on floor move");
                    controller.FloorMoveContextModel.SetContext(type);
                    controller.InnerState.ChangeState(PrimaryStateType.FloorTransition);
                })
                .AddTo(CompositeDisposable);
            InnerState.StateEnterObservable
                .Subscribe(this, (type, controller) => controller.PrimaryStateEventView.Invoke(type))
                .AddTo(CompositeDisposable);
        }

        public override void StateUpdate(float deltaTime)
        {
            TimeModel.CountUpTime(deltaTime);

            var currentHp = HpModel.CurrentHp;
            var maxHp = HpSetting.MaxHp;
            var remainTime = TimeModel.TimeLength - TimeModel.CurrentTime;

            if (remainTime < 0)
            {
                TimerView.SetTime(0);
                InnerState.ChangeState(PrimaryStateType.GameOver);
            }

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
        private IPrimaryStateEventView PrimaryStateEventView { get; }
        private IHpModel HpModel { get; }
        private IHpSetting HpSetting { get; }
        private ITimeModel TimeModel { get; }
        private IFloorMoveContextModel FloorMoveContextModel { get; }
    }
}