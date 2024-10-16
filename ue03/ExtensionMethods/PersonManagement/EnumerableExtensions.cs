using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace PersonManagement {
    // static ist wichtig bei der Klasse und der Methode, sonst geht das mit der Extension-Method nicht
    public static class EnumerableExtensions {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action) {
            foreach (var item in items) {
                action.Invoke(item);
            }
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> items, Func<T, bool> predicate) {
            foreach (var item in items) {
                if (predicate.Invoke(item)){
                    yield return item;
                }
            }
        }

        public static IEnumerable<R> Map<T, R>(this IEnumerable<T> items, Func<T, R> transform) {
            foreach (var item in items){
                yield return transform(item);
            }
        }

        public static T MaxByCustom<T>(this IEnumerable<T> items, Comparison<T> comparer) {

            using IEnumerator<T> e = items.GetEnumerator();
            if (!e.MoveNext())
                throw new ArgumentException("Maximum for empty collection not defined");

            var max = e.Current;
            while (e.MoveNext()){
                if (comparer.Invoke(e.Current, max) > 0){
                    max = e.Current;
                }
            }

            return max;
        }
    }
}
