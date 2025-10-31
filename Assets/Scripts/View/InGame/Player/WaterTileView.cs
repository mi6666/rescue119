using System.Collections.Generic;
using Interface.ViewInterface.InGame;
using UnityEngine;
using UnityEngine.Pool;

namespace View.InGame.Player
{
    public class WaterTileView :  MonoBehaviour, IWaterView
    {
        [SerializeField] private GameObject waterPrefab;
        [SerializeField] private int initialPoolSize = 50;

        private ObjectPool<GameObject> _pool;
        private readonly List<GameObject> _activeWaters = new();

        private void Awake()
        {
            _pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(waterPrefab),
                actionOnGet: obj => obj.SetActive(true),
                actionOnRelease: obj => obj.SetActive(false),
                actionOnDestroy: Destroy,
                collectionCheck: false,
                defaultCapacity: initialPoolSize
            );
        }

        public void SpawnWater(Vector2 position, Vector2 lookAt, int length)
        {
            for (int i = 0; i < length; i++)
            {
                var waterObj = _pool.Get();
                waterObj.transform.position = position + lookAt * i;
                _activeWaters.Add(waterObj);
            }
        }

        public void DespawnWater()
        {
            foreach (var obj in _activeWaters)
            {
                _pool.Release(obj);
            }
            _activeWaters.Clear();
        }
    }
}