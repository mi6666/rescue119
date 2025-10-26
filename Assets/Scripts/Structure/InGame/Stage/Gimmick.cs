using UnityEngine;

namespace Structure.InGame.Stage
{
    public interface IEventContext
    {
    }

    public class SpawnRubbleContext : IEventContext
    {
        public Context EventContext;

        public readonly struct Context
        {
            public Vector2 SpawnPosition { get; }

            public Context(Vector2 spawnPosition)
            {
                SpawnPosition = spawnPosition;
            }
        }
    }

    public class StairContext : IEventContext
    {
        public Context EventContext;

        public readonly struct Context
        {
            public StairType StairType { get; }

            public Context(StairType stairType)
            {
                StairType = stairType;
            }
        }
    }
}