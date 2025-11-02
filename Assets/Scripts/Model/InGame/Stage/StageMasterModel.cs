using Interface.ModelInterface.InGame;
using Module.EnumArray.Runtime;
using Structure.Global;
using Structure.OutGame;
using UnityEngine;

namespace Model.InGame.Stage
{
    [CreateAssetMenu(fileName = nameof(StageMasterModel), menuName = MenuName)]
    public class StageMasterModel : ScriptableObject, IStageMasterModel
    {
        private const string MenuName = Constants.MasterDataDiv + nameof(StageMasterModel);

        [SerializeField] private float timeLength;
        [SerializeField] private int maxFloorNum;
        [SerializeField] private float pawnTickInterval = 0.25f;
        [SerializeField, EnumArray(typeof(DifficultyLevel))]
        private EnumArray<float> tickIntervalRatio; 

        public float TimeLength => timeLength;
        public int MaxFloorNum => maxFloorNum;
        public float PawnTickInterval => pawnTickInterval;
        public float TickInterval(DifficultyLevel difficultyLevel)
        {
            Debug.Log(difficultyLevel);
            return tickIntervalRatio.Get((int)difficultyLevel) * pawnTickInterval;
        }
    }
}