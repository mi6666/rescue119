using System;
using Interface.ViewInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace View.InGame.Stage.Floor
{
    public class EmptyFloorView: IFloorPawnView
    {
        public IPawnView[] GetAllPawn()
        {
            return Array.Empty<IPawnView>();
        }

        public IPawnView[] GetFloorPawn(int floor)
        {
            return Array.Empty<IPawnView>();
        }

        public IPawnView MovePawn(int id, int floorPrevious, int floorNext)
        {
            return null;
        }

        public IPawnView GetPawn(int id, int floor)
        {
            return null;
        }

        public IPawnView TakePawn(int id, int floor)
        {
            return null;
        }

        public void GivePawn(IPawnView pawnView, int floor)
        {
        }
    }
    
    public class EmptyPawnPoolView: IPawnPoolView
    {
        public IPawnView Spawn(Vector2 position, PawnType type)
        {
            return null;
        }

        public void Despawn(IPawnView pawnView)
        {
        }
    }
}
