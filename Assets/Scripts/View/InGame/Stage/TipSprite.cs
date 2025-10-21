using Module.EnumArray.Runtime;
using Structure.InGame;
using UnityEngine;

namespace View.InGame.Stage
{
    [CreateAssetMenu(fileName = "TipSprite", menuName = "Tiles/TipSprite", order = 0)]
    public class TipSprite : ScriptableObject
    {
        [SerializeField, EnumArray(typeof(StageTileType))]
        private EnumArray<Sprite> tipSprites;

        public Sprite Get(StageTileType type)
        {
            return tipSprites.Get((int)type);
        }
    }
}