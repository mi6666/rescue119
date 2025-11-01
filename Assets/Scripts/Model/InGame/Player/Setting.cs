using System;
using Interface.ModelInterface.InGame;
using UnityEngine;

namespace Model.InGame.Player
{
    [Serializable]
    public class LocomotionSetting : ILocomotionSetting
    {
        [SerializeField] private float maxSpeed;
        [SerializeField] private float accelerationDuration;
        [SerializeField, Range(0f, 1f)] private float fireDeceleration;
        [SerializeField] private float directionChangeSpeed;
        [SerializeField] private AnimationCurve speedCurve;
        [SerializeField] private float wallFriction;
        [SerializeField] private float reverseAngleThreshold;

        public float MaxSpeed => maxSpeed;
        public float AccelerationDuration => accelerationDuration;
        public float FireDeceleration => fireDeceleration;
        public float DirectionChangeSpeed => directionChangeSpeed;
        public float WallFriction => wallFriction;
        public float ReverseAngleThreshold => reverseAngleThreshold;

        public float GetSpeedCurve(float ratio)
        {
            return speedCurve.Evaluate(Mathf.Clamp01(ratio));
        }
    }

    [Serializable]
    public class HpSetting : IHpSetting
    {
        [SerializeField] private int maxHp;

        public int MaxHp => maxHp;
    }

    [Serializable]
    public class ActionSetting : IActionSetting
    {
        [SerializeField] private float actionLength;
        [SerializeField] private int waterLength;
        [SerializeField] private float holdMotionLength;

        public float SplashWater => actionLength;
        public int WaterLengthMax => waterLength;
        public float HoldMotionLength => holdMotionLength;
    }
}