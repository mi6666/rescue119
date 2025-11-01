using Interface.ViewInterface.InGame.UserInterface;
using Structure.InGame;
using TMPro;
using UnityEngine;

namespace View.InGame.UserInterface.FloorMove
{
    public class FloorMoveTextView : MonoBehaviour, IFloorMoveTextView
    {
        [SerializeField] private TextMeshProUGUI upperText;
        [SerializeField] private TextMeshProUGUI lowerText;

        [SerializeField] private TextMeshProUGUI context;

        public void SetFloorMove(int floor, StairType stairType)
        {
            if (stairType == StairType.Down)
            {
                upperText.SetText(floor.ToString());
                lowerText.SetText((floor - 1).ToString());
                context.SetText("Down to");
            }
            else
            {
                upperText.SetText((floor + 1).ToString());
                lowerText.SetText(floor.ToString());
                context.SetText("Up to");
            }
        }
    }
}