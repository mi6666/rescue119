using System;
using Interface.ModelInterface.InGame;
using UnityEngine;

namespace Model.InGame.Stage
{
    [Serializable]
    public class StageSettingModel : IStageSettingModel
    {
        [SerializeField] private float timeLength;
        [SerializeField] private int maxFloorNum;

        public float TimeLength => timeLength;
        public int MaxFloorNum => maxFloorNum;
    }
}