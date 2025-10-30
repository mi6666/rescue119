using Interface.ModelInterface.InGame;
using Structure.Global;
using UnityEngine;

namespace Model.InGame.Player
{   
    [CreateAssetMenu(fileName = "PlayerMasterData", menuName = MenuName, order = 0)]
    public class PlayerMasterData : ScriptableObject, IPlayerAnimationParameterKeyModel
    {
        [SerializeField] private string key;
        private const string MenuName = Constants.MasterDataDiv + nameof(PlayerMasterData);
        public string Key => key;
    }
}