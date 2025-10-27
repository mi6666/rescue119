using Cysharp.Threading.Tasks;
using Interface.ViewInterface.InGame.UserInterface;
using Module.FadeContainer.Runtime;
using UnityEngine;

namespace View.InGame.UserInterface.FloorMove
{
    public class FloorMoveUiFadeView : MonoBehaviour, IFloorMoveUiFadeView
    {
        [SerializeField] private FadeContainer fadeContainer;
        [SerializeField] private FloorMoveTextView floorMoveTextView;
        
        public FloorMoveTextView FloorMoveTextView => floorMoveTextView;

        public async UniTask Show()
        {
            FloorMoveTextView.gameObject.SetActive((true));
            await fadeContainer.FadeIn();
        }

        public async UniTask Hide()
        {
            await fadeContainer.FadeOut();
            FloorMoveTextView.gameObject.SetActive(false);
        }
    }
}