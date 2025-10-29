using Module.EditorExtension.Runtime;
using Structure.InGame;
using Structure.InGame.Stage;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    /// <summary>
    /// 階段のView
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class StairView : BasePawnView
    {
        [SerializeField] private int floor;
        [SerializeField] private Vector2Int gridColliderSize;
        [SerializeField] private TagSelector targetTag;
        [SerializeField] private StairType stairType;

        private EventCompositeView _eventCompositeView;
        private StairContext StairContext { get; } = new();

        private void Awake()
        {
            _eventCompositeView = FindAnyObjectByType<EventCompositeView>();

            Debug.Assert(_eventCompositeView is not null);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(targetTag))
            {
                StairContext.EventContext = new StairContext.Context(stairType);
                _eventCompositeView.Invoke(StairContext);
            }
        }

        public override PawnType Type => PawnType.Static;
        public override Vector2Int Size => gridColliderSize;
        public override int Floor => floor;
    }
}