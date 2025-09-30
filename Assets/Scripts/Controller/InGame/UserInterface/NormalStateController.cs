using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame.UserInterface;
using Module.StateMachine;
using R3;
using Structure.InGame;
using VContainer.Unity;

namespace Controller.InGame.UserInterface
{
    public class NormalStateController : UiStateBehaviour, IStartable
    {
        public NormalStateController
        (
            IHpUiView hpUiView,
            ITimerView timerView,
            IPauseEventView pauseEventView,
            IHpModel hpModel,
            ITimeModel timeModel,
            IStageSettingModel stageSettingModel,
            CompositeDisposable compositeDisposable,
            IMutStateType<UserInterfaceStateType> innerState
        ) : base(UserInterfaceStateType.Normal, innerState)
        {
            HpUiView = hpUiView;
            TimerView = timerView;
            PauseEventView = pauseEventView;
            HpModel = hpModel;
            TimeModel = timeModel;
            StageSettingModel = stageSettingModel;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            PauseEventView.PauseEventObservable
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (_, controller) => controller.OnPause())
                .AddTo(CompositeDisposable);
        }

        public override void StateUpdate(float deltaTime)
        {
            TimeModel.CountUpTime(deltaTime); // よろしくないかも

            var currentHp = HpModel.CurrentHp;
            var maxHp = HpModel.MaxHp;
            var remainTime = StageSettingModel.TimeLength - TimeModel.CurrentTime;

            HpUiView.SetHp(currentHp, maxHp);
            TimerView.SetTime(remainTime);
        }

        private void OnPause()
        {
            // ポーズ
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IHpUiView HpUiView { get; }
        private ITimerView TimerView { get; }
        private IPauseEventView PauseEventView { get; }
        private IHpModel HpModel { get; }
        private ITimeModel TimeModel { get; }
        private IStageSettingModel StageSettingModel { get; }
    }
}