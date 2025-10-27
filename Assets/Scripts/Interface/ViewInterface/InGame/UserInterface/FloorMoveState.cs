using Cysharp.Threading.Tasks;
using R3;

namespace Interface.ViewInterface.InGame.UserInterface
{
    public interface IFloorMoveUiView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    public interface IFloorMoveUiFadeView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    public interface IFloorMoveTextView
    {
        public void SetFloorMove(int floor);
    }
}