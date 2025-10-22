using Module.EnumArray.Runtime;
using Structure.InGame;
using UnityEngine;

namespace View.InGame.Stage
{
    [CreateAssetMenu(fileName = "StateSprite", menuName = "Tiles/StateSprite", order = 0)]
    public class StateSprite : ScriptableObject
    {
        [SerializeField, EnumArray(typeof(TileStateType))]
        private EnumArray<Sprite> stateSprites;
        
        public Sprite Get(TileStateType type)
        {
            return stateSprites.Get((int)type);
        }
    }
}