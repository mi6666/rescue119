using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Structure.InGame.Stage;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace Controller.InGame.Common
{
    /// <summary>
    /// Pawn関連のユーティリティ
    /// </summary>
    public class PawnConnection
    {
        public PawnConnection
        (
            IMapCoordinateView mapCoordinateView,
            IStageFloorModel stageFloorModel,
            IFloorPawnView floorPawnView,
            IPawnPoolView pawnPoolView,
            IStagePawnModel stagePawnModel
        )
        {
            MapCoordinateView = mapCoordinateView;
            StageFloorModel = stageFloorModel;
            FloorPawnView = floorPawnView;
            PawnPoolView = pawnPoolView;
            StagePawnModel = stagePawnModel;
        }

        /// <summary>
        /// Pawnを別フロアへと動かす
        /// </summary>
        public void MovePawn(IPawnView pawnView, int putFloor)
        {
            var mapIndex = MapCoordinateView.PositionToMapIndex(pawnView.Floor, pawnView.Position);
            var collider = new GridCollider(
                pawnView.InstanceId, pawnView.Type, pawnView.Floor,
                mapIndex, pawnView.Size
            );
            var pawn = TakePawn(collider);
            PutPawn(pawn, mapIndex, putFloor);
        }

        /// <summary>
        /// Pawnを別フロアへと動かす
        /// </summary>
        public void MovePawn(GridCollider collider, int putFloor)
        {
            var pawn = TakePawn(collider);
            PutPawn(pawn, collider.Position, putFloor);
        }

        /// <summary>
        /// マップのインデックスからPawnを作成する
        /// </summary>
        public void SpawnPawn(Vector2Int mapIndex, PawnType type)
        {
            var floor = StageFloorModel.CurrentFloor;
            var spawnPosition = MapCoordinateView.IndexToMapPosition(floor, mapIndex);
            var pawnView = PawnPoolView.Spawn(spawnPosition, type);
            var pawnCollider = new GridCollider(
                pawnView.InstanceId,
                pawnView.Type,
                floor,
                mapIndex,
                pawnView.Size);
            StagePawnModel.StorePawn(pawnCollider);
            FloorPawnView.GivePawn(pawnView, floor);
        }

        /// <summary>
        /// 座標からPawnを作成する
        /// </summary>
        public void SpawnPawn(Vector2 position, PawnType type)
        {
            var floor = StageFloorModel.CurrentFloor;
            var mapIndex = MapCoordinateView.PositionToMapIndex(floor, position);
            var pawnView = PawnPoolView.Spawn(position, type);
            var pawnCollider = new GridCollider(
                pawnView.InstanceId,
                pawnView.Type,
                floor,
                mapIndex,
                pawnView.Size);
            StagePawnModel.StorePawn(pawnCollider);
            FloorPawnView.GivePawn(pawnView, floor);
        }

        /// <summary>
        /// ポーンを置く
        /// </summary>
        public void PutPawn(IPawnView pawnView, Vector2 position, int floor)
        {
            var index = MapCoordinateView.PositionToMapIndex(floor, position);
            var collider = new GridCollider(
                pawnView.InstanceId, pawnView.Type,
                floor, index, pawnView.Size
            );
            StagePawnModel.StorePawn(collider);
            FloorPawnView.GivePawn(pawnView, floor);
        }

        /// <summary>
        /// フロアのポーンを取得する
        /// </summary>
        public IPawnView TakePawn(in GridCollider collider)
        {
            var floorPawnView = FloorPawnView.TakePawn(collider.PawnId, collider.Floor);
            StagePawnModel.RemovePawn(collider.PawnId);

            return floorPawnView;
        }

        /// <summary>
        /// ポーンをプールへ返す
        /// </summary>
        public void SendPawnPool(in GridCollider collider)
        {
            var floorPawnView = FloorPawnView.TakePawn(collider.PawnId, collider.Floor);
            PawnPoolView.Despawn(floorPawnView);
            StagePawnModel.RemovePawn(collider.PawnId);
        }

        private IMapCoordinateView MapCoordinateView { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IFloorPawnView FloorPawnView { get; }
        private IPawnPoolView PawnPoolView { get; }
        private IStagePawnModel StagePawnModel { get; }
    }
}