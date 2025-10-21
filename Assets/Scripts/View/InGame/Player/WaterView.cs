using Interface.ViewInterface.InGame;
using UnityEngine;

namespace View.InGame.Player
{
    public class WaterView :  MonoBehaviour, IWaterView
    {
        public void SpawnWater()
        {
            gameObject.SetActive(true);
        }

        public void DespawnWater()
        {
            gameObject.SetActive(false);
        }
    }
}