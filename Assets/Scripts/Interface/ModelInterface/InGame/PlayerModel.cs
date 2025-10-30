using UnityEngine;

namespace Interface.ModelInterface.InGame
{
    /// <summary>
    /// 移動処理における一時的な値を持つ
    /// </summary>
    public interface ILocomotionModel
    {
        /// <summary>
        /// 最大速度
        /// </summary>
        public float MaxSpeed { get; }

        /// <summary>
        /// 加速にかかる時間
        /// </summary>
        public float AccelerationDuration { get; }

        /// <summary>
        /// 移動方向の変化速度
        /// </summary>
        public float DirectionChangeSpeed { get; }

        /// <summary>
        /// 加速曲線
        /// </summary>
        /// <param name="ratio">0.0~1.0</param>
        public float GetSpeedCurve(float ratio);

        /// <summary>
        /// 壁に接触している際の減速割合
        /// </summary>
        public float WallFriction { get; }

        /// <summary>
        /// 入力が反転しているとみなす角度の閾値
        /// </summary>
        public float ReverseAngleThreshold { get; }

        /// <summary>
        /// 加速・減速中にカウントアップ・カウントダウンされる時間量
        /// </summary>
        public float AccelerationTime { get; }

        public void DecreaseTime(float deltaTime);
        public void IncreaseTime(float deltaTime);
    }

    /// <summary>
    /// 体力のモデル
    /// </summary>
    public interface IHpModel
    {
        public int CurrentHp { get; }
        public int MaxHp { get; }
    }

    /// <summary>
    /// プレイヤーの挙動の長さを持つ
    /// </summary>
    public interface IActionLengthModel
    {
        public float SplashWater { get; }
        public int WaterLength { get; }
    }

    /// <summary>
    /// プレイヤーが向いている方向を持つ
    /// </summary>
    public interface ICurrentLookModel
    {
        public Vector2 LookTo { get; }
        public void SetLook(Vector2 lookTo);
    }

    public interface IPlayerAnimationParameterKeyModel
    {
        public string Key { get; }
        
    }
}