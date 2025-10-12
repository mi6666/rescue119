using Interface.ViewInterface.InGame.UserInterface;
using Module.EditorExtension.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace View.InGame.UserInterface.Normal
{
    [RequireComponent(typeof(Image))]
    public class HpUiView : MonoBehaviour, IHpUiView
    {
        [SerializeField, AutoAssign] private Image hpGage;

        public void SetHp(int currentHp, int maxHp)
        {
            hpGage.fillAmount = (float)currentHp / maxHp;
        }
    }
}