using Interface.ModelInterface.InGame;
using UnityEngine;

namespace Model.InGame.Player
{
    public class CurrentLookModel : ICurrentLookModel
    {
        private Vector2 _currentLook;
        public Vector2 LookTo => _currentLook;

        public void SetLook(Vector2 lookTo)
        {
            _currentLook = lookTo;
        }
    }
}