using UnityEngine;
using UnityEngine.Pool;

namespace View.InGame.Stage
{
    public class RubbleView : MonoBehaviour
    {
        private IObjectPool<RubbleView> _pool;

        public void SetPool(IObjectPool<RubbleView> pool)
        {
            _pool = pool;
        }

        private void Release()
        {
            if (_pool != null && gameObject.activeInHierarchy)
            {
                _pool.Release(this);
            }
        }

        private void OnBecameInvisible()
        {
            Release();
        }
    }
}
