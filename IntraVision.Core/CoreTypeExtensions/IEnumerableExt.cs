using System;
using System.Collections.Generic;
using System.Linq;
using IntraVision.CoreTypeExtensions;

namespace IntraVision.Core
{
    public static class IEnumerableExt
    {
        public static IEnumerable<T> Traverse<T>
               (this IEnumerable<T> source, Func<T, IEnumerable<T>> fnRecurse)
        {
            foreach (T item in source)
            {
                yield return item;

                var seqRecurse = fnRecurse(item);

                if (seqRecurse != null)
                {
                    foreach (T itemRecurse in Traverse(seqRecurse, fnRecurse))
                    {
                        yield return itemRecurse;
                    }
                }
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            source = source.ToArray();
            foreach (var item in source) {
                action(item);
            }

            return source;
        }

        public static GenericListDataReader<T> GetDataReader<T>(this IEnumerable<T> list)
        {
            return new GenericListDataReader<T>(list);
        }
    }
}
