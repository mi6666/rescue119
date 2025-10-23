using Interface.ViewInterface.InGame.UserInterface;
using Module.EditorExtension.Runtime;
using R3;
using TMPro;
using UnityEngine;

namespace View.InGame.UserInterface.FloorMove
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FloorMoveTextView : MonoBehaviour, IFloorMoveTextView
    {
        [SerializeField, AutoAssign] private TextMeshProUGUI floorText;

        public void SetFloorMove(int floor)
        {
            floorText.SetText(floor.ToString());
        }
    }
}