using Cysharp.Threading.Tasks;
using Interface.ViewInterface.InGame;
using LitMotion;
using LitMotion.Extensions;
using Module.EditorExtension.Runtime;
using UnityEngine;
using View.InGame.Stage.Pawn;

namespace View.InGame.Player
{
    public class HoldingPawnView : MonoBehaviour, IHoldingPawnView
    {
        [SerializeField, AutoAssign] private Transform selfTransform;
        [SerializeField] private float holdLength;

        public UniTask HoldPawn(IPawnView pawnView)
        {
            pawnView.PawnTransform.parent = selfTransform;
            HoldingPawn = pawnView;
            return LMotion.Create(new Vector3(pawnView.Position.x, pawnView.Position.y), selfTransform.position,
                    holdLength)
                .BindToPosition(pawnView.PawnTransform)
                .ToUniTask();
        }

        public UniTask PutPawn(Vector2 position)
        {
            var task = LMotion.Create(HoldingPawn.Position, position, holdLength)
                .BindToPositionXY(HoldingPawn.PawnTransform)
                .ToUniTask();
            HoldingPawn = null;
            return task;
        }

        public async UniTask PutAndFall(Vector2 position)
        {
            await LMotion.Create(HoldingPawn.Position, position, holdLength)
                .BindToPositionXY(HoldingPawn.PawnTransform)
                .ToUniTask();
            await LMotion.Create(Vector2.one, Vector2.zero, holdLength)
                .BindToLocalScaleXY(HoldingPawn.PawnTransform)
                .ToUniTask();
            Debug.Log("fall", HoldingPawn as BasePawnView);
            HoldingPawn = null;
        }

        public IPawnView HoldingPawn { get; private set; }
    }
}