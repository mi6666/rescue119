using Interface.ModelInterface.InGame;
using Structure.Global;
using UnityEngine;

namespace Model.InGame.Stage
{
    [CreateAssetMenu(fileName = nameof(StageMasterModel), menuName = MenuName)]
    public class StageMasterModel : ScriptableObject, IStageMasterModel
    {
        private const string MenuName = Constants.MasterModelDiv + nameof(StageMasterModel);

        [SerializeField] private float timeLength;
        [SerializeField] private int maxFloorNum;
        [SerializeField] private float pawnTickInterval = 0.25f;

        public float TimeLength => timeLength;
        public int MaxFloorNum => maxFloorNum;
        public float PawnTickInterval => pawnTickInterval;
    }
}