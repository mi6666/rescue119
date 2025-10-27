using Interface.ViewInterface.InGame;
using Structure.InGame;
using UnityEngine;
using UnityEngine.Pool;

namespace View.InGame.Stage.Pawn
{
    public class RubbleView : BasePawnView, IPawnPoolable<RubbleView>
    {
        private IObjectPool<RubbleView> _pool;
        private int _floor;

        public void SetPool(IObjectPool<RubbleView> pool)
        {
            _pool = pool;
        }

        public void Spawn(int floor)
        {
            _floor = floor;
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

        public override PawnType Type => PawnType.Rubble;
        public override Vector2Int Size => Vector2Int.one;
        public override int Floor => _floor;
    }
}
