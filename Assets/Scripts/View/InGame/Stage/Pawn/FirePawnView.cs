using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Structure.InGame;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    public class FirePawnView : MonoBehaviour, IPawnView
    {
        [SerializeField, AutoAssign] private Transform selfTransform;
        [SerializeField] private int floor;

        public int InstanceId => gameObject.GetInstanceID();
        public PawnType Type => PawnType.Fire;
        public Vector2 Position => selfTransform.position;
        public Vector2Int Size => Vector2Int.one;
        public int Floor => floor;
    }
}