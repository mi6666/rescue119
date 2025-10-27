using Cysharp.Threading.Tasks;
using R3;

namespace Interface.ViewInterface.InGame.UserInterface
{
    public interface INormalUiView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    public interface ITimerView
    {
        public void SetTime(float time);
    }

    public interface IHpUiView
    {
        public void SetHp(int currentHp, int maxHp);
    }

    public interface IPauseEventView
    {
        public Observable<Unit> PauseEventObservable { get; }
    }
}