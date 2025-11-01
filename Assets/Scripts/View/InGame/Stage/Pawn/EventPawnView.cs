using Module.EditorExtension.Runtime;
using Structure.InGame.Stage;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EventPawnView : BasePawnView
    {
        [SerializeField] private TagSelector tagSelector;
        [SerializeField, SceneVector(CoordinateSpace.World)] private Vector2 spawnPosition;

        private EventCompositeView _eventCompositeView;
        private SpawnRubbleContext SpawnRubbleContext { get; } = new();

        private void Awake()
        {
            _eventCompositeView = FindAnyObjectByType<EventCompositeView>();

            Debug.Assert(_eventCompositeView is not null);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(tagSelector))
            {
                SpawnRubbleContext.EventContext = new SpawnRubbleContext.Context(spawnPosition);
                _eventCompositeView.Invoke(SpawnRubbleContext);
            }
        }

        public override PawnType Type => PawnType.Static;
        public override Vector2Int Size => Vector2Int.one;
    }
}