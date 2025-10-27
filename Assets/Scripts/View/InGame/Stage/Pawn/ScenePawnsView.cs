using System.Collections.Generic;
using System.Linq;
using Interface.ViewInterface.InGame;
using UnityEngine;
using View.InGame.Player;

namespace View.InGame.Stage.Pawn
{
    public class ScenePawnsView : MonoBehaviour, IScenePawnsView
    {
        private List<IPawnView> _pawns;

        private void Awake()
        {
            var foundPawns = FindObjectsByType<PawnView>(FindObjectsSortMode.None);
            _pawns = foundPawns.Select(x => x as IPawnView).ToList();
        }

        public IReadOnlyList<IPawnView> GetPawns()
        {
            return _pawns;
        }

        public IPawnView FindPawn(int id)
        {
            return _pawns.Find(x => x.InstanceId == id);
        }
    }
}