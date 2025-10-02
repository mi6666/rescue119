using Structure.InGame;
using UnityEngine;

namespace Interface.LogicInterface.InGame
{
    public interface ILocomotionLogic
    {
        public Vector2 CalcVelocity(LocomotionArgument argument);
    }
}