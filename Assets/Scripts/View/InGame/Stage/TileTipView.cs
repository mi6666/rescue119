using Interface.ViewInterface.InGame.Stage;
using Module.EditorExtension.Runtime;
using Structure.InGame;
using UnityEngine;

namespace View.InGame.Stage
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TileTipView : MonoBehaviour, ITileView
    {
        [SerializeField, AutoAssign] private SpriteRenderer selfRenderer;
        [SerializeField] private TipSprite tipSprite;
        [SerializeField] private StateSprite stateSprite;

        public void ChangeTile(StageTileType stageTileType)
        {
            var next = tipSprite.Get(stageTileType);

            selfRenderer.sprite = next;
        }

        public void ChangeTileState(TileStateType tileStateType)
        {
            var state = stateSprite.Get(tileStateType);

            selfRenderer.sprite = state;
        }
    }
}