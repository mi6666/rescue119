using Interface.ViewInterface.InGame;
using Module.Option.Runtime;
using Structure.InGame;
using UnityEngine;

namespace View.InGame.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PawnDetectView : MonoBehaviour, IPawnDetectView
    {
        private PawnType _detectedPawn;
        private int _detectedId;

        private const int NotFound = -1;

        public Option<PawnType> Detection()
        {
            if (_detectedId == NotFound)
            {
                return Option<PawnType>.None();
            }

            return Option<PawnType>.Some(_detectedPawn);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<IPawnView>(out var pawn)) return;

            _detectedPawn = pawn.Type;
            _detectedId = other.gameObject.GetInstanceID();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_detectedId != other.gameObject.GetInstanceID()) return;

            _detectedId = NotFound;
        }
    }
}