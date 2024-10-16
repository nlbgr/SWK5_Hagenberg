using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement {
    // static ist wichtig bei der Klasse und der Methode, sonst geht das mit der Extension-Method nicht
    public static class CollectionExtensions {
        public static void AddAll<T>(this ICollection<T> target, IEnumerable<T> source) {
            foreach (var item in source){
                target.Add(item);
            }
        }
    }
}
