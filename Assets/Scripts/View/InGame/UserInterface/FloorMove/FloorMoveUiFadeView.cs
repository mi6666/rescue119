using Cysharp.Threading.Tasks;
using Interface.ViewInterface.InGame.UserInterface;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace View.InGame.UserInterface.FloorMove
{
    public class FloorMoveUiFadeView : MonoBehaviour, IFloorMoveUiView
    {
        [SerializeField] private float fadeDuration = 0.25f;
        [SerializeField] private GameObject selfObject;
        [SerializeField] private Image panel;
        [SerializeField] private FloorMoveTextView floorMoveTextView;
        
        public FloorMoveTextView FloorMoveTextView => floorMoveTextView;

        public async UniTask Show()
        {
            selfObject.SetActive(true);
            await LMotion.Create(0f, 1f, fadeDuration)
                .BindToColorA(panel)
                .ToUniTask();
        }

        public async UniTask Hide()
        {
            await LMotion.Create(1f, 0f, fadeDuration)
                .BindToColorA(panel)
                .ToUniTask();
            selfObject.SetActive(false);
        }
    }
}