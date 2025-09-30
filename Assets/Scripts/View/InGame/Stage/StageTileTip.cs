using Structure.InGame;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace View.InGame.Stage
{
    [CreateAssetMenu(menuName = "Tiles/StageTile")]
    public class StageTileTip: Tile
    {
        public StageTileType TileType { get; }
        
        [SerializeField] private StageTileType stageTileType;
    }
}