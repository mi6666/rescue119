using System;
using Interface.ViewInterface.InGame.Stage;
using Structure.InGame;
using Structure.InGame.Stage;
using UnityEngine;
using UnityEngine.Tilemaps;
using ZLinq;

namespace View.InGame.Stage
{
    public class TileMapView : MonoBehaviour, IMapReaderView
    {
        [SerializeField] private Tilemap[] tilemap;

        public StageMap[] GetMap()
        {
            return tilemap.AsValueEnumerable().Select(x => GetFloorMap(x)).ToArray();
        }

        public Vector2 AlignToMapPosition(int floor, Vector2 position)
        {
            return AlignToMapPosition(tilemap[floor], position);
        }

        public Vector2 IndexToMapPosition(int floor, Vector2Int index)
        {
            return IndexToMapPosition(tilemap[floor], index);
        }

        public Vector2Int PositionToMapIndex(int floor, Vector2 worldPosition)
        {
            return PositionToMapIndex(tilemap[floor], worldPosition);
        }

        private static StageMap GetFloorMap(Tilemap floorMap)
        {
            var bounds = floorMap.cellBounds;
            var stageTileTips = new TipBase[bounds.size.y, bounds.size.x];

            for (var y = 0; y < bounds.size.y; y++)
            {
                for (var x = 0; x < bounds.size.x; x++)
                {
                    var cellPosition = new Vector3Int(x + bounds.x, y + bounds.y, 0);

                    if (floorMap.GetTile(cellPosition) is not StageTileTipData tile)
                    {
                        stageTileTips[y, x] = new NoneTip();
                        continue;
                    }

                    var instanceId = 0;
                    var isBurning = tile.IsBurning;
                    var objectHealth = tile.ObjectHealth;
                    var go = floorMap.GetInstantiatedObject(cellPosition);
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

            return new StageMap(stageTileTips);
        }

        /// <summary>
        /// タイルマップのセルに座標を揃える
        /// </summary>
        private static Vector2 AlignToMapPosition(Tilemap floorMap, Vector2 position)
        {
            var anchor = floorMap.tileAnchor;
            var cellPosition = floorMap.WorldToCell(position);
            return new Vector2(cellPosition.x + anchor.x, cellPosition.y + anchor.y);
        }

        /// <summary>
        /// `StageMap`のインデックスから、ワールド座標を算出する
        /// </summary>
        private static Vector2 IndexToMapPosition(Tilemap floorMap, Vector2Int index)
        {
            var bounds = floorMap.cellBounds;
            var cellPosition = new Vector3Int(index.x + bounds.x, index.y + bounds.y, 0);
            return floorMap.GetCellCenterWorld(cellPosition);
        }

        /// <summary>
        /// ワールド座標から`StageMap`へのインデックスに変換する
        /// </summary>
        private static Vector2Int PositionToMapIndex(Tilemap floorMap, Vector2 worldPosition)
        {
            var cellPosition = floorMap.WorldToCell(worldPosition);
            var bounds = floorMap.cellBounds;
            return new Vector2Int(cellPosition.x - bounds.x, cellPosition.y - bounds.y);
        }
    }
}