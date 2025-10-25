using System;
using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Structure.InGame.Stage;
using UnityEngine;
using VContainer;

namespace Logic.InGame.Stage
{
    public class GridCastLogic: IGridCastLogic
    {
        [Inject]
        public GridCastLogic
        (
            IStagePawnModel stagePawnModel
        )
        {
            StagePawnModel = stagePawnModel;
        }
        public ReadOnlySpan<GridCollider> CastGrid(Vector2Int position, Vector2Int size)
        {
            // todo
            throw new NotImplementedException();
        }
        
        private IStagePawnModel StagePawnModel { get; }
    }
}