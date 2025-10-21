using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Structure.InGame;
using UnityEngine;

namespace View.InGame.Stage
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TileTipView: MonoBehaviour, ITileView
    {
        [SerializeField, AutoAssign] private SpriteRenderer selfRenderer;
        [SerializeField] private TipSprite tipSprite;
        
        public void ChangeTile(StageTileType stageTileType)
        {
            var next = tipSprite.Get(stageTileType);

            selfRenderer.sprite = next;
        }
    }
}