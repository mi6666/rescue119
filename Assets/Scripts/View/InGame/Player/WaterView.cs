using Interface.ViewInterface.InGame;
using UnityEngine;

namespace View.InGame.Player
{
    public class WaterView : MonoBehaviour, IWaterView
    {
        [SerializeField] private ParticleSystem waterParticle;

        private Transform _waterTransform;

        private void Awake()
        {
            _waterTransform = waterParticle.transform;
        }

        public void SpawnWater(Vector2 position, Vector2 lookAt, int length)
        {
            var size = _waterTransform.localScale;

            _waterTransform.position = position;
            _waterTransform.rotation = Quaternion.LookRotation(lookAt);
            _waterTransform.transform.localScale = new Vector3(size.x, size.y, length);

            waterParticle.Play();
        }

        public void DespawnWater()
        {
            waterParticle.Stop();
        }
    }
}