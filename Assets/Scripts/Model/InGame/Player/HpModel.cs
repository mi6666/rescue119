using System;
using Interface.ModelInterface.InGame;
using UnityEngine;

namespace Model.InGame.Player
{
    [Serializable]
    public class HpModel: IHpModel
    {
        public int CurrentHp => _currentHp;
        public int MaxHp => maxHp;

        [SerializeField] private int maxHp;
        private int _currentHp;
    }
}