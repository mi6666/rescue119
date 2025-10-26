using Module.EditorExtension.Runtime;
using Structure.InGame;
using Structure.InGame.Stage;
using UnityEngine;

namespace View.InGame.Stage
{
    /// <summary>
    /// 階段のView
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class StairView : MonoBehaviour
    {
        [SerializeField] private TagSelector targetTag;
        [SerializeField] private StairType stairType;

        private EventCompositeView _eventCompositeView;
        private StairContext StairContext { get; } = new ();

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
    }
}