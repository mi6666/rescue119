using System;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Structure.InGame.Stage.Pawn;
using UnityEngine;
using ZLinq;

namespace View.InGame.Stage.Pawn
{
    public class PawnPoolView : MonoBehaviour, IPawnPoolView
    {
        [SerializeField] private Transform poolTransform;
        [SerializeField] private PawnPrefabMasterView pawnPrefabMasterView;
        [SerializeField] private int initialPoolSize;

        private PawnPool[] _pawnPools;

        private void Awake()
        {
            _pawnPools = ((PawnType[])Enum.GetValues(typeof(PawnType)))
                .AsValueEnumerable()
                .Select(x => pawnPrefabMasterView.GetPawns(x))
                .Where(x => x == null)
                .Select(x => new PawnPool(poolTransform, x, initialPoolSize))
                .ToArray();
        }

        public IPawnView Move(int id, int floorPrevious, int floorNext)
        {
            throw new NotImplementedException();
        }

        public IPawnView Spawn(int floor, Vector2 position, PawnType type)
        {
            throw new NotImplementedException();
        }

        public void Despawn(int id, int floorPrevious)
        {
            throw new NotImplementedException();
        }
    }
}