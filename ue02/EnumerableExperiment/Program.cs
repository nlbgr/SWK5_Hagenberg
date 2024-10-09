using System;
using System.Collections.Generic;

namespace EnumerableExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            MyEnumerable enumerable = new MyEnumerable("a", "b", "c");

            foreach (string item in enumerable)
            {
                Console.WriteLine(item);
            }
        }
    }

    public class MyEnumerable
    {
        public MyEnumerable(params string[] values)
        {
            Items = new List<string>(values);
        }

        public IList<string> Items { get; set; }

        public MyEnumerator GetEnumerator()
        {
            return new MyEnumerator(this);
        }
    }

    public class MyEnumerator
    {
        private readonly MyEnumerable enumerable;
        private int index = -1;

        public MyEnumerator(MyEnumerable enumerable)
        {
            this.enumerable = enumerable;
        }

        private IList<string> Items
        {
            get { return enumerable.Items; }
        }

        public string Current
        {
            get { return index >= 0 ? Items[index] : null; }
        }

        public bool MoveNext()
        {
            if (index < Items.Count - 1)
            {
                index++;
                return true;
            }
            return false;
        }
    }
}
