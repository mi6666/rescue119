using Structure.InGame;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace View.InGame.Stage
{
    [CreateAssetMenu(menuName = "Tiles/StageTile")]
    public class StageTileTipData: Tile
    {
        public StageTileType TileType => stageTileType;
        public bool IsBurning => isBurning;
        public int ObjectHealth => objectHealth;

        [SerializeField] private StageTileType stageTileType;
        [SerializeField] private bool isBurning;
        [SerializeField] private int objectHealth;
    }
}