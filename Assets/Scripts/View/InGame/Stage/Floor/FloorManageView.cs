using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Structure.InGame.Stage;
using UnityEngine;
using ZLinq;

namespace View.InGame.Stage.Floor
{
    /// <summary>
    /// 複数フロアを横断的に管理する
    /// </summary>
    public class FloorManageView : MonoBehaviour, IMapReaderView, IFloorView, IFloorPawnView
    {
        [SerializeField] private FloorMapView[] floorMapViews;
        [SerializeField] private FloorPawnView[] floorPawnViews;

        private void Awake()
        {
            foreach (var (index, floorPawnView) in floorPawnViews.AsValueEnumerable().Index())
            {
                foreach (var pawnView in floorPawnView.GetAll().AsValueEnumerable())
                {
                    pawnView.InitFloor(index);
                }
            }
        }

        #region IFloorView

        public void Activate(int floor)
        {
            floorMapViews[floor].gameObject.SetActive(true);
        }

        public void Deactivate(int floor)
        {
            floorMapViews[floor].gameObject.SetActive(false);
        }

        #endregion

        #region IFloorPawnView

        public IPawnView[] GetAllPawn()
        {
            var iter = floorPawnViews.AsValueEnumerable()
                .SelectMany(x => x.GetAll())
                .ToArray();

            return iter;
        }

        public IPawnView[] GetFloorPawn(int floor)
        {
            return floorPawnViews[floor].GetAll();
        }

        public IPawnView MovePawn(int id, int floorPrevious, int floorNext)
        {
            var view = TakePawn(id, floorPrevious);
            GivePawn(view, floorNext);

            return view;
        }

        public IPawnView GetPawn(int id, int floor)
        {
            return floorPawnViews[floor].GetPawn(id);
        }

        public IPawnView TakePawn(int id, int floor)
        {
            var targetFloor = floorPawnViews[floor];
            var view = targetFloor.TakePawn(id);

            view.SetFloor(null, floor);

            return view;
        }

        public void GivePawn(IPawnView pawnView, int floor)
        {
            var targetFloor = floorPawnViews[floor];
            targetFloor.GivePawn(pawnView);

            pawnView.SetFloor(targetFloor.SelfTransform, floor);
        }

        #endregion

        #region IMapReaderView

        public Vector2 AlignToMapPosition(int floor, Vector2 position)
        {
            return floorMapViews[floor].AlignToMapPosition(position);
        }

        public Vector2 IndexToMapPosition(int floor, Vector2Int index)
        {
            return floorMapViews[floor].IndexToMapPosition(index);
        }

        public Vector2Int PositionToMapIndex(int floor, Vector2 worldPosition)
        {
            return floorMapViews[floor].PositionToMapIndex(worldPosition);
        }

        public FloorMap[] GetMap()
        {
            return floorMapViews.AsValueEnumerable().Select(x => x.GetFloorMap()).ToArray();
        }

        #endregion
    }
}