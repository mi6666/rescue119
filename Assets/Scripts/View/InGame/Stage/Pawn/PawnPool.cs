using System.Collections.Generic;
using System.Linq;
using Interface.ViewInterface.InGame;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    public class PawnPool : IPawnPool
    {
        public PawnPool(BasePawnView pawnView, int initialPool)
        {
            PawnView = pawnView;
            Pool = new List<BasePawnView>(initialPool);
            InitializePawn();
        }

        /// <summary>
        /// プールの初期化
        /// </summary>
        private void InitializePawn()
        {
            for (var i = 0; i < Pool.Capacity; i++)
            {
                var pawn = Object.Instantiate(PawnView);
                pawn.SetPool(this);
                pawn.gameObject.SetActive(false);
                Pool.Add(pawn);
            }
        }

        /// <summary>
        /// プールに存在する`Pawn`を取り出す
        /// </summary>
        public BasePawnView Spawn(int floor)
        {
            var pawn = Pool.FirstOrDefault(p => !p.gameObject.activeSelf);

            if (pawn == null)
            {
                pawn = Object.Instantiate(PawnView);
                Pool.Add(pawn);
            }

            pawn.gameObject.SetActive(true);
            if (pawn is IFactorablePawnView factorable)
            {
                factorable.SetFloor(floor);
            }

            return pawn;
        }

        public void ReturnToPool(BasePawnView self)
        {
            self.gameObject.SetActive(false);
        }

        private BasePawnView PawnView { get; }
        private List<BasePawnView> Pool { get; }
    }

    public interface IPawnPool
    {
        public void ReturnToPool(BasePawnView self);
    }
}