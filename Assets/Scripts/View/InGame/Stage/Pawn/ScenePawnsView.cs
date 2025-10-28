using System.Collections.Generic;
using Interface.ViewInterface.InGame;
using UnityEngine;
using ZLinq;

namespace View.InGame.Stage.Pawn
{
    /// <summary>
    /// 現在シーン上でアクティブな`Pawn`を保持する
    /// </summary>
    public class ScenePawnsView : MonoBehaviour, IScenePawnsView
    {
        private List<IPawnView> _pawns;

        private void Awake()
        {
            var foundPawns = FindObjectsByType<BasePawnView>(FindObjectsSortMode.None);
            _pawns = foundPawns
                .AsValueEnumerable()
                .Select(x => x as IPawnView)
                .ToList();
        }

        public IReadOnlyList<IPawnView> GetPawns()
        {
            return _pawns;
        }

        public IPawnView FindPawn(int id)
        {
            return _pawns.AsValueEnumerable().FirstOrDefault(x => x.InstanceId == id);
        }

        public void AddPawn(IPawnView pawnView)
        {
            Debug.Assert(!_pawns.Contains(pawnView), pawnView.InstanceId.ToString() + pawnView.Type);

            _pawns.Add(pawnView);
        }

        public void RemovePawn(int id)
        {
            var pawn = FindPawn(id);
            if (pawn != null)
            {
                _pawns.Remove(pawn);
            }
        }
    }
}