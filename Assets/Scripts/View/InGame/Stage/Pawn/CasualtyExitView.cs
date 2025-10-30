using Module.EditorExtension.Runtime;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CasualtyExitView : BasePawnView
    {
        [SerializeField] private Vector2Int gridColliderSize;
        [SerializeField] private TagSelector casualty;

        private EventCompositeView _eventCompositeView;
        private void Awake()
        {
            _eventCompositeView = FindAnyObjectByType<EventCompositeView>();

            Debug.Assert(_eventCompositeView is not null);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(casualty))
            {
                Debug.Log("clear");
                _eventCompositeView.InvokeClear();
            }
        }

        public override PawnType Type => PawnType.Exit;
        public override Vector2Int Size => gridColliderSize;
    }
}