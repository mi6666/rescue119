using Cysharp.Threading.Tasks;
using Interface.ViewInterface.InGame.UserInterface;
using UnityEngine;

namespace View.InGame.UserInterface.FloorMove
{
    public class FloorMoveUiView : MonoBehaviour, IFloorMoveUiView
    {
        [SerializeField] private FloorMoveTextView floorMoveTextView;

        public FloorMoveTextView FloorMoveTextView => floorMoveTextView;

        public UniTask Show()
        {
            FloorMoveTextView.gameObject.SetActive((true));
            return UniTask.CompletedTask;
        }
        
        public UniTask Hide()
        {
            FloorMoveTextView.gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}