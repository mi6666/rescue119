using Interface.ViewInterface.InGame;
using UnityEngine;
using UnityEngine.Pool;
using View.InGame.Stage.Pawn;

namespace View.InGame.Stage
{
    public class SpawnRubbleView : MonoBehaviour, ISpawnRubbleView
    {
        [SerializeField] private RubbleView rubbleViewPrefab;

        private IObjectPool<RubbleView> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<RubbleView>(
                CreateRubble,
                OnGetRubble,
                OnReleaseRubble,
                OnDestroyRubble,
                collectionCheck: true,
                defaultCapacity: 10,
                maxSize: 20);
        }

        private RubbleView CreateRubble()
        {
            var rubble = Instantiate(rubbleViewPrefab);
            rubble.SetPool(_pool);
            return rubble;
        }

        private static void OnGetRubble(RubbleView rubbleView)
        {
            rubbleView.gameObject.SetActive(true);
        }

        private static void OnReleaseRubble(RubbleView rubbleView)
        {
            rubbleView.gameObject.SetActive(false);
        }

        private static void OnDestroyRubble(RubbleView rubbleView)
        {
            if (rubbleView != null)
            {
                Destroy(rubbleView.gameObject);
            }
        }

        public void Spawn(Vector2 position)
        {
            var rubble = _pool.Get();
            rubble.transform.position = position;
        }
    }
}