using System;
using Interface.ViewInterface.InGame;
using Structure.InGame;
using UnityEngine;
using View.InGame.Stage.Pawn;

namespace View.InGame.Stage
{
    public class RubbleFactoryView : MonoBehaviour, IRubbleFactoryView
    {
        [SerializeField] private PawnPrefabMasterView pawnPrefabMasterView;

        private PawnPool[] _pawnPools;

        private void Start()
        {
            var values = Enum.GetValues(typeof(PawnType));
            var len = values.Length;
            _pawnPools = new PawnPool[len];
            for (int i = 0; i < len; i++)
            {
                var pawn = pawnPrefabMasterView.GetPawns((PawnType)i);
                _pawnPools[i] = new PawnPool(pawn, 32);
            }
        }

        public IPawnView Spawn(int floor, Vector2 position, PawnType type)
        {
            var rubble = _pawnPools[(int)type].Spawn(floor);
            rubble.transform.position = position;
            return rubble;
        }
    }
}