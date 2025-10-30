using System;
using Interface.ModelInterface.InGame;
using UnityEngine;

namespace Model.InGame.Player
{
    [Serializable]
    public class ActionModel : IActionLengthModel
    {
        [SerializeField] private float actionLength;
        [SerializeField] private int waterLength;
        [SerializeField] private float holdMotionLength;
        
        public float SplashWater => actionLength;
        public int WaterLength => waterLength;
        public float HoldMotionLength => holdMotionLength;
    }
}