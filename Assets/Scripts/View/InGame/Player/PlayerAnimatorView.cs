using Interface.ViewInterface.InGame;
using UnityEngine;

namespace View.InGame.Player
{
    public class PlayerAnimatorView : MonoBehaviour, IPlayerAnimatorView
    {
        [SerializeField] private Animator anim;

        public void SetFloat(string key, float value)
        {
            if (anim == null) return;
                
            anim?.SetFloat(key, value);
        }
    }
}