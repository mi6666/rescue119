using Module.EnumArray.Runtime;
using Structure.Global;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    [CreateAssetMenu(fileName = nameof(PawnPrefabMasterView), menuName = MenuName)]
    public class PawnPrefabMasterView : ScriptableObject
    {
        private const string MenuName = Constants.MasterDataDiv + nameof(PawnPrefabMasterView);

        [SerializeField, EnumArray(typeof(PawnType))]
        private EnumArray<BasePawnView> pawnViews;

        public BasePawnView GetPawns(PawnType type)
        {
            return pawnViews.Get((int)type);
        }
    }
}