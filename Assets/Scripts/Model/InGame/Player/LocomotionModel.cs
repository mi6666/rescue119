using Interface.ModelInterface.InGame;
using UnityEngine;

namespace Model.InGame.Player
{
    public class LocomotionModel : ILocomotionModel
    {
        public LocomotionModel(ILocomotionSetting locomotionSetting)
        {
            LocomotionSetting = locomotionSetting;
        }
        
        private ILocomotionSetting LocomotionSetting { get; }
        
        public float AccelerationTime { get; private set; }

        public void DecreaseTime(float deltaTime)
        {
            AccelerationTime = Mathf.Clamp(AccelerationTime - deltaTime, 0, LocomotionSetting.AccelerationDuration);
        }

        public void IncreaseTime(float deltaTime)
        {
            AccelerationTime = Mathf.Clamp(AccelerationTime + deltaTime, 0, LocomotionSetting.AccelerationDuration);
        }
    }
}