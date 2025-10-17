using Cysharp.Threading.Tasks;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame.UserInterface;
using Module.StateMachine;
using R3;
using Structure.InGame;
using VContainer.Unity;

namespace Controller.InGame.Primary
{
    public class NormalStateController : PrimaryStateBehaviour, IStartable
    {
        public NormalStateController
        (
            INormalUiView normalUiView,
            IHpUiView hpUiView,
            ITimerView timerView,
            IPauseEventView pauseEventView,
            IHpModel hpModel,
            ITimeModel timeModel,
            IStageSettingModel stageSettingModel,
            CompositeDisposable compositeDisposable,
            IMutStateType<PrimaryStateType> innerState
        ) : base(PrimaryStateType.Normal, innerState)
        {
            NormalUiView = normalUiView;
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
            TimeModel.CountUpTime(deltaTime);

            var currentHp = HpModel.CurrentHp;
            var maxHp = HpModel.MaxHp;
            var remainTime = StageSettingModel.TimeLength - TimeModel.CurrentTime;

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
        private IHpModel HpModel { get; }
        private ITimeModel TimeModel { get; }
        private IStageSettingModel StageSettingModel { get; }
    }
}