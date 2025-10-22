using Interface.ViewInterface.InGame.UserInterface;
using Module.EditorExtension.Runtime;
using TMPro;
using UnityEngine;

namespace View.InGame.UserInterface.Normal
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TimerView : MonoBehaviour, ITimerView
    {
        [SerializeField, AutoAssign] private TextMeshProUGUI timerText;

        public void SetTime(float time)
        {
            timerText.SetText(time.ToString("F1"));
        }
    }
}