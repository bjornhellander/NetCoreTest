using System.Collections.Generic;

namespace NetCoreTest.UI
{
    internal static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> obj, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                obj.Add(value);
            }
        }
    }
}
