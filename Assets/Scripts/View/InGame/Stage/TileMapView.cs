using System;
using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Structure.InGame;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace View.InGame.Stage
{
    [RequireComponent(typeof(Tilemap))]
    public class TileMapView : MonoBehaviour, IStageTileMapView
    {
        [AutoAssign, SerializeField] private Tilemap tilemap;

        public StageMap GetMap()
        {
            var bounds = tilemap.cellBounds;
            var stageTileTips = new StageTileTip[bounds.size.y, bounds.size.x];

            for (var y = 0; y < bounds.size.y; y++)
            {
                for (var x = 0; x < bounds.size.x; x++)
                {
                    var cellPosition = new Vector3Int(x + bounds.x, y + bounds.y, 0);

                    if (tilemap.GetTile(cellPosition) is not StageTileTipData tile)
                    {
                        stageTileTips[y, x] = new NoneTileTip();
                        continue;
                    }

                    var instanceId = 0;
                    var isBurning = tile.IsBurning;
                    var objectHealth = tile.ObjectHealth;
                    var go = tilemap.GetInstantiatedObject(cellPosition);
                    if (go != null)
                    {
                        instanceId = go.GetInstanceID();
                    }

                    stageTileTips[y, x] = tile.TileType switch
                    {
                        StageTileType.None => new NoneTileTip(),
                        StageTileType.Floor => new FloorTileTip(instanceId, objectHealth),
                        StageTileType.Wall => new WallTileTip(instanceId, isBurning, objectHealth),
                        StageTileType.Rubble => new RubbleTileTip(instanceId, isBurning, objectHealth),
                        _ => throw new NotImplementedException(),
                    };
                }
            }

            return new StageMap(stageTileTips);
        }
    }
}