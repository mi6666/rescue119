using System.Collections.Generic;
using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using UnityEngine;
using ZLinq;

namespace View.InGame.Stage.Floor
{
    public class FloorPawnView : MonoBehaviour
    {
        [SerializeField, AutoAssign] private Transform selfTransform;
        private List<IPawnView> _pawnViews;

        public Transform SelfTransform => selfTransform;

        private void Awake()
        {
            _pawnViews = GetComponentsInChildren<IPawnView>().AsValueEnumerable().ToList();
        }

        public IPawnView[] GetAll()
        {
            return _pawnViews.AsValueEnumerable().ToArray();
        }

        public IPawnView GetPawn(int id)
        {
            IPawnView pawnView = null;
            foreach (var view in _pawnViews.AsValueEnumerable())
            {
                if (view.InstanceId == id)
                {
                    pawnView = view;
                }
            }

            return pawnView;
        }

        public IPawnView TakePawn(int id)
        {
            var result = 0;
            IPawnView pawnView = null;
            foreach (var (index, view) in _pawnViews.AsValueEnumerable().Index())
            {
                if (view.InstanceId == id)
                {
                    result = index;
                    pawnView = view;
                }
            }

            _pawnViews.RemoveAt(result);

            return pawnView;
        }

        public void GivePawn(IPawnView pawnView)
        {
            _pawnViews.Add(pawnView);
        }
    }
}