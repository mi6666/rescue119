using System.Collections.Generic;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    public class PawnPool : IPawnPool
    {
        private readonly List<BasePawnView> _available = new(); // 使用可能なPawn

        public PawnPool(Transform poolParent, BasePawnView pawnView, int initialPool)
        {
            PoolParent = poolParent;
            PawnView = pawnView;
            InitializePawn(initialPool);
        }

        /// <summary>
        /// プールの初期化
        /// </summary>
        private void InitializePawn(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var pawn = Object.Instantiate(PawnView, parent: PoolParent);
                pawn.gameObject.SetActive(false);
                _available.Add(pawn);
            }
        }

        /// <summary>
        /// プールに存在する`Pawn`を取り出す
        /// </summary>
        public BasePawnView Spawn()
        {
            BasePawnView pawn;

            if (_available.Count > 0)
            {
                pawn = _available[^1];
                _available.RemoveAt(_available.Count - 1);
            }
            else
            {
                pawn = Object.Instantiate(PawnView, parent: PoolParent);
            }

            pawn.gameObject.SetActive(true);
            return pawn;
        }

        /// <summary>
        /// `Pawn`をプールに戻す
        /// </summary>
        public void ReturnToPool(BasePawnView self)
        {
                _available.Add(self);
                self.gameObject.SetActive(false);
                self.transform.SetParent(PoolParent);
        }

        private Transform PoolParent { get; }
        public BasePawnView PawnView { get; }
    }

    public interface IPawnPool
    {
        public void ReturnToPool(BasePawnView self);
    }
}