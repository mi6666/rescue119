using Module.Option.Runtime;
using UnityEngine;

namespace Structure.Global
{
    public static class Constants
    {
        public const float Threshold = 0.01f;

        public static Option<Vector2> Approx8Dir(Vector2 v)
        {
            if (Vector2.SqrMagnitude(v) <= Threshold) return Option<Vector2>.None();

            Vector2 bestDir = Vector2.zero;
            float maxDot = -Mathf.Infinity;
            Vector2 n = v.normalized;

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