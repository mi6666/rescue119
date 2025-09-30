using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace View.InGame.Stage
{
    public class TileMapView: MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;

        private void Awake()
        {
            var bounds = tilemap.cellBounds;

            foreach (var position in bounds.allPositionsWithin)
            {
                
            }
        }
    }
}