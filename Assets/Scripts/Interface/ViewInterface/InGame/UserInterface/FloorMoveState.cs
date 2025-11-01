using Cysharp.Threading.Tasks;
using Structure.InGame;

namespace Interface.ViewInterface.InGame.UserInterface
{
    public interface IFloorMoveUiView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    public interface IFloorMoveTextView
    {
        public void SetFloorMove(int floor, StairType stairType);
    }
}