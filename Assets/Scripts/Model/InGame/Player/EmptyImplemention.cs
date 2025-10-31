using System;
using Interface.ModelInterface.InGame;

namespace Model.InGame.Player
{
    public class EmptyHpModel : IHpModel
    {
        public int CurrentHp => 1;
    }

    public class EmptyLocomotionSetting : ILocomotionSetting
    {
        public float MaxSpeed => 10;
        public float AccelerationDuration => 0;
        public float DirectionChangeSpeed => 0;

        public float GetSpeedCurve(float ratio)
        {
            return 0;
        }

        public float WallFriction => 1;
        public float ReverseAngleThreshold => 10;
    }

    public class EmptyAnimationKeyModel : IAnimationKeyModel
    {
        public string Key => string.Empty;
    }

    public class EmptyActionSetting : IActionSetting
    {
        public float SplashWater => 0;
        public int WaterLengthMax => 0;
        public float HoldMotionLength => 0;
    }
}