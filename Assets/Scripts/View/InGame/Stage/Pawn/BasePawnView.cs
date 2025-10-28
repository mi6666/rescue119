using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Structure.InGame;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    /// <summary>
    /// `Pawn`の基底クラス
    /// </summary>
    public abstract class BasePawnView : MonoBehaviour, IPawnView
    {
        [SerializeField] protected GameObject selfObject;
        [SerializeField, AutoAssign] protected Transform selfTransform;

        public int InstanceId => selfObject.GetInstanceID();
        public abstract PawnType Type { get; }
        public Vector2 Position => selfTransform.position;
        public abstract Vector2Int Size { get; }
        public abstract int Floor { get; }

        protected IPawnPool PawnPool { get; private set; }

        public void SetPool(IPawnPool pawnPool)
        {
            PawnPool = pawnPool;
        }
    }
}