using System;
using System.Collections.Generic;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Module.EditorExtension.Runtime;
using Structure.InGame.Stage.Pawn;
using UnityEngine;
using ZLinq;

namespace View.InGame.Stage.Pawn
{
    public class PawnPoolView : MonoBehaviour, IPawnPoolView
    {
        [SerializeField, AutoAssign] private Transform poolTransform;
        [SerializeField] private PawnPrefabMasterView pawnPrefabMasterView;
        [SerializeField] private int initialPoolSize = 32;

        private Dictionary<PawnType, PawnPool> _pawnDictionary;

        private void Awake()
        {
            _pawnDictionary = ((PawnType[])Enum.GetValues(typeof(PawnType)))
                .AsValueEnumerable()
                .Select(x => pawnPrefabMasterView.GetPawns(x))
                .Where(x => x != null)
                .Select(x => new PawnPool(poolTransform, x, initialPoolSize))
                .ToDictionary(x => x.PawnView.Type);
        }

        public IPawnView Spawn(Vector2 position, PawnType type)
        {
            var view = _pawnDictionary[type].Spawn();
            view.SetPosition(position);
            return view;
        }

        public void Despawn(IPawnView pawnView)
        {
            _pawnDictionary[pawnView.Type].ReturnToPool(pawnView as BasePawnView);
        }
    }
}