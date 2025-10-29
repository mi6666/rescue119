using System;
using Module.EditorExtension.Runtime;
using Structure.InGame;
using Structure.InGame.Stage;
using UnityEngine;
using UnityEngine.Tilemaps;
using View.InGame.Stage.Tile;

namespace View.InGame.Stage.Floor
{
    [RequireComponent(typeof(Tilemap))]
    public class FloorMapView : MonoBehaviour
    {
        [SerializeField, AutoAssign] private Tilemap tilemap;

        public FloorMap GetFloorMap()
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
                        StageTileType.Floor => new FloorTip(new InnerBurn(isBurning), new InnerObject(instanceId),
                            new InnerHealth(objectHealth)),
                        StageTileType.Wall => new WallTip(new InnerBurn(isBurning), new InnerObject(instanceId),
                            new InnerHealth(objectHealth)),
                        StageTileType.Rubble => new RubbleTip(new InnerBurn(isBurning), new InnerObject(instanceId),
                            new InnerHealth(objectHealth)),
                        StageTileType.Hole => new HoleTip(new InnerObject(instanceId), new InnerHealth(objectHealth)),
                        _ => throw new NotImplementedException(),
                    };
                }
            }

            return new FloorMap(stageTileTips);
        }

        /// <summary>
        /// タイルマップのセルに座標を揃える
        /// </summary>
        public Vector2 AlignToMapPosition(Vector2 position)
        {
            var anchor = tilemap.tileAnchor;
            var cellPosition = tilemap.WorldToCell(position);
            return new Vector2(cellPosition.x + anchor.x, cellPosition.y + anchor.y);
        }

        /// <summary>
        /// `StageMap`のインデックスから、ワールド座標を算出する
        /// </summary>
        public Vector2 IndexToMapPosition(Vector2Int index)
        {
            var bounds = tilemap.cellBounds;
            var cellPosition = new Vector3Int(index.x + bounds.x, index.y + bounds.y, 0);
            return tilemap.GetCellCenterWorld(cellPosition);
        }

        /// <summary>
        /// ワールド座標から`StageMap`へのインデックスに変換する
        /// </summary>
        public Vector2Int PositionToMapIndex(Vector2 worldPosition)
        {
            var cellPosition = tilemap.WorldToCell(worldPosition);
            var bounds = tilemap.cellBounds;
            return new Vector2Int(cellPosition.x - bounds.x, cellPosition.y - bounds.y);
        }
    }
}