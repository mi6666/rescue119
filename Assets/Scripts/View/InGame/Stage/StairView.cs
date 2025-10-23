using Structure.InGame;
using UnityEngine;

namespace View.InGame.Stage
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class StairView : MonoBehaviour
    {
        [SerializeField] private StairsEventView stairsEventView;
        [SerializeField] private StairType type;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                stairsEventView.Invoke(type);
            }
        }
    }
}