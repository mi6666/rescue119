using Module.Option.Runtime;
using UnityEngine;

namespace Structure.Global
{
    public readonly struct CastHit
    {
        public Vector2 Normal { get; }

        public CastHit(Vector2 normal)
        {
            Normal = normal;
        }
    }

    public static class Utility
    {
        public static Option<Vector2> Approx8Dir(Vector2 v)
        {
            if (Vector2.SqrMagnitude(v) <= Constants.Threshold) return Option<Vector2>.None();

            var bestDir = Vector2.zero;
            var maxDot = -Mathf.Infinity;
            var n = v.normalized;

            foreach (var d in Dirs)
            {
                float dot = Vector2.Dot(n, d.normalized);
                if (dot > maxDot)
                {
                    maxDot = dot;
                    bestDir = d;
                }
            }

            return Option<Vector2>.Some(bestDir);
        }

        private static readonly Vector2[] Dirs = new Vector2[]
        {
            new Vector2(1, 0), // right
            new Vector2(1, 1), // up-right
            new Vector2(0, 1), // up
            new Vector2(-1, 1), // up-left
            new Vector2(-1, 0), // left
            new Vector2(-1, -1), // down-left
            new Vector2(0, -1), // down
            new Vector2(1, -1), // down-right
        };
    }
}