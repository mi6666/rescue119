using System;
using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Structure.InGame;
using Structure.InGame.Stage;
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
            var stageTileTips = new TipBase[bounds.size.y, bounds.size.x];

            for (var y = 0; y < bounds.size.y; y++)
            {
                for (var x = 0; x < bounds.size.x; x++)
                {
                    var cellPosition = new Vector3Int(x + bounds.x, y + bounds.y, 0);

                    if (tilemap.GetTile(cellPosition) is not StageTileTipData tile)
                    {
                        stageTileTips[y, x] = new NoneTip();
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
                        StageTileType.None => new NoneTip(),
                        StageTileType.Floor => new FloorTip(new InnerBurn(), new InnerObject(instanceId), new InnerHealth(objectHealth)),
                        StageTileType.Wall => new WallTip(new InnerBurn(), new InnerObject(instanceId), new InnerHealth(objectHealth)),
                        StageTileType.Rubble => new RubbleTip(new InnerBurn(), new InnerObject(instanceId), new InnerHealth(objectHealth)),
                        StageTileType.Hole => new HoleTip(new InnerObject(instanceId),new InnerHealth(objectHealth)),
                        _ => throw new NotImplementedException(),
                    };
                }
            }

            return new StageMap(stageTileTips);
        }

        public Vector2 ConvertToMapPosition(Vector2 position)
        {
            var cellPosition = tilemap.WorldToCell(position);
            return new Vector2(cellPosition.x - tilemap.cellBounds.x, cellPosition.y - tilemap.cellBounds.y);
        }
    }
}