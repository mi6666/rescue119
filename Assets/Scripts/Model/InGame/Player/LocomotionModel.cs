using System;
using Interface.ModelInterface.InGame;
using UnityEngine;

namespace Model.InGame.Player
{
    [Serializable]
    public class LocomotionModel : ILocomotionModel
    {
        [SerializeField] private float maxSpeed;
        [SerializeField] private float accelerationDuration;
        [SerializeField] private float directionChangeSpeed;
        [SerializeField] private AnimationCurve speedCurve;
        [SerializeField] private float wallFriction;
        [SerializeField] private float reverseAngleThreshold;

        public float MaxSpeed => maxSpeed;
        public float AccelerationDuration => accelerationDuration;
        public float DirectionChangeSpeed => directionChangeSpeed;
        public float WallFriction => wallFriction;
        public float ReverseAngleThreshold => reverseAngleThreshold;
        public float AccelerationTime { get; private set; }


        public float GetSpeedCurve(float ratio)
        {
            return speedCurve.Evaluate(Mathf.Clamp01(ratio));
        }

        public void DecreaseTime(float deltaTime)
        {
            AccelerationTime = Mathf.Clamp(AccelerationTime - deltaTime, 0, accelerationDuration);
        }

        public void IncreaseTime(float deltaTime)
        {
            AccelerationTime = Mathf.Clamp(AccelerationTime + deltaTime, 0, accelerationDuration);
        }
    }
}