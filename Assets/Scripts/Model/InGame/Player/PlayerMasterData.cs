using Interface.ModelInterface.InGame;
using Structure.Global;
using UnityEngine;

namespace Model.InGame.Player
{   
    [CreateAssetMenu(fileName = "PlayerMasterData", menuName = MenuName, order = 0)]
    public class PlayerMasterData : ScriptableObject, IAnimationKeyModel, ILocomotionSetting, IActionSetting, IHpSetting
    {
        private const string MenuName = Constants.MasterDataDiv + nameof(PlayerMasterData);

        #region PublickProperty

        public string Key => key;
        
        public float MaxSpeed => locomotionSetting.MaxSpeed;
        public float AccelerationDuration => locomotionSetting.AccelerationDuration;
        public float DirectionChangeSpeed => locomotionSetting.DirectionChangeSpeed;
        public float GetSpeedCurve(float ratio)
        {
            return locomotionSetting.GetSpeedCurve(ratio);
        }
        public float WallFriction => locomotionSetting.WallFriction;
        public float ReverseAngleThreshold => locomotionSetting.ReverseAngleThreshold;
        
        public float SplashWater => actionSetting.SplashWater;
        public int WaterLengthMax => actionSetting.WaterLengthMax;
        public float HoldMotionLength => actionSetting.HoldMotionLength;

        public int MaxHp => hpSetting.MaxHp;

        #endregion
        
        [SerializeField] private string key;
        [SerializeField] private LocomotionSetting locomotionSetting;
        [SerializeField] private ActionSetting actionSetting;
        [SerializeField] private HpSetting hpSetting;
    }
}