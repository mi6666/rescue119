using Cysharp.Threading.Tasks;

namespace Interface.ViewInterface.OutGame.StageSelect
{
    public interface ISomeStateUiView
    {
        public UniTask Show();
        public UniTask Hide();
    }
}