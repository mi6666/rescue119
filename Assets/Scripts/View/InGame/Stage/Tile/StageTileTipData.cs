using Structure.InGame;
using UnityEngine;

namespace View.InGame.Stage.Tile
{
    [CreateAssetMenu(menuName = "Tiles/StageTile")]
    public class StageTileTipData: UnityEngine.Tilemaps.Tile
    {
        public StageTileType TileType => stageTileType;
        public bool IsBurning => isBurning;
        public int ObjectHealth => objectHealth;

        [SerializeField] private StageTileType stageTileType;
        [SerializeField] private bool isBurning;
        [SerializeField] private int objectHealth;
    }
}