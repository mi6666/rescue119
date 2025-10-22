using System;
using Interface.ModelInterface.InGame;
using UnityEngine;

namespace Model.InGame.Player
{
    [Serializable]
    public class ActionModel : IActionLengthModel
    {
        [SerializeField] private float actionLength;
        
        public float SplashWater => actionLength;
    }
}