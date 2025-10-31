using Interface.ViewInterface.InGame;
using UnityEngine;

namespace View.InGame.Player
{
    public class WaterView: MonoBehaviour, IWaterView
    {
        public void SpawnWater(Vector2 position, Vector2 lookAt, int length)
        {
        }

        public void DespawnWater()
        {
        }
    }
}