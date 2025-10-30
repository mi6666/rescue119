using Interface.ViewInterface.InGame.Stage;
using Structure.InGame.Stage;
using UnityEngine;

namespace Presenter.InGame.Stage
{
    public class EmptyMapPresenter: IMapCoordinateView
    {
        public FloorMap[] GetMap()
        {
            return null;
        }

        public Vector2 AlignToMapPosition(int floor, Vector2 position)
        {
            return position;
        }

        public Vector2 IndexToMapPosition(int floor, Vector2Int index)
        {
            return index;
        }

        public Vector2Int PositionToMapIndex(int floor, Vector2 position)
        {
            return Vector2Int.RoundToInt(position);
        }
    }
}