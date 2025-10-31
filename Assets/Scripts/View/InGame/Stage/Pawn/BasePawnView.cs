using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Structure.InGame.Stage.Pawn;
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
        [SerializeField] private int floor;

        public int InstanceId => selfObject.GetInstanceID();
        public abstract PawnType Type { get; }
        public Transform PawnTransform => selfTransform;
        public Vector2 Position => selfTransform.position;
        public abstract Vector2Int Size { get; }

        public int Floor
        {
            get { return floor; }
            protected set { floor = value; }
        }

        public void SetPosition(Vector2 position)
        {
            selfTransform.position = position;
        }

        public void InitFloor(int changedFloor)
        {
            Floor = changedFloor;
        }

        public void SetFloor(Transform newParent, int changedFloor)
        {
            selfTransform.parent = newParent;
            Floor = changedFloor;
            selfTransform.localScale = Vector3.one;
        }

        public virtual void OnPut()
        {
        }

        public virtual void OnTake()
        {
        }
    }
}