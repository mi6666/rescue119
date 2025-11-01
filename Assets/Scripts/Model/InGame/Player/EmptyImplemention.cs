using Interface.ModelInterface.InGame;
using R3;

namespace Model.InGame.Player
{
    public class EmptyHpModel : IHpModel
    {
        public int CurrentHp => 1;
        public void DecHp(int value)
        {
        }

        public Observable<bool> IsDeadObservable => Observable.Return(true);
    }

    public class EmptyLocomotionSetting : ILocomotionSetting
    {
        public float MaxSpeed => 10;
        public float AccelerationDuration => 0;
        public float FireDeceleration => 0f;
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