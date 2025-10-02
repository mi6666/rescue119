using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Structure.InGame;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace View.InGame.Stage
{
    [RequireComponent(typeof(Tilemap))]
    public class TileMapView: MonoBehaviour, IStageTileMapView
    {
        [AutoAssign, SerializeField] private Tilemap tilemap;

        public StageMap GetMap()
        {
            var bounds = tilemap.cellBounds;
            var stageTileTips = new Structure.InGame.StageTileTip[bounds.size.y, bounds.size.x];

            for (var y = 0; y < bounds.size.y; y++)
            {
                for (var x = 0; x < bounds.size.x; x++)
                {
                    var cellPosition = new Vector3Int(x + bounds.x, y + bounds.y, 0);

                    var tile = tilemap.GetTile(cellPosition) as StageTileTip;

                    if (tile == null)
                    {
                        stageTileTips[y, x] = new NoneTileTip();
                        continue;
                    }

                    var instanceId = 0;
                    var go = tilemap.GetInstantiatedObject(cellPosition);
                    if (go != null)
                    {
                        instanceId = go.GetInstanceID();
                    }

                    // NOTE: The 'TileType' property on the 'StageTileTip' class appears to be unimplemented.
                    // This implementation assumes it correctly returns the intended StageTileType.
                    // Default values are used for IsBurning and ObjectHealth as their source is not specified.
                    stageTileTips[y, x] = tile.TileType switch
                    {
                        StageTileType.Floor => new FloorTileTip(instanceId, false, 100),
                        StageTileType.Wall => new WallTileTip(instanceId, false, 100),
                        StageTileType.Rubble => new RubbleTileTip(instanceId, false, 100),
                        _ => new NoneTileTip()
                    };
                }
            }

            return new StageMap(stageTileTips);
        }
    }
}