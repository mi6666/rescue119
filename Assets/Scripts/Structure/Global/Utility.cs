using System;
using System.Text;

namespace Structure.Global
{
    public static class Utility
    {
        public static string ToString<T>(this ReadOnlySpan<T> span)
        {
            var builder = new StringBuilder(typeof(T).Name);

            builder.Append("{");
            foreach (var value in span)
            {
                builder.Append(value);
                builder.Append(",");
            }
            builder.Append("}");

            return builder.ToString();
        }
    }
}