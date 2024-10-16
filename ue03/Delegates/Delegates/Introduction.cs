using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates {
    delegate void Procedure();
    delegate void ProdecureWithIntParameter(int value);
    delegate void ProcedureWithStringParameter(string value);
    delegate void GenericProcedure<T>(T value);
    delegate int IntFunction();
    delegate int StringToIntFunction(string value);

    internal class Introduction {
        public static void Test() {
            // local function
            static void SayHello() => Console.WriteLine("Hello World!");
            static void PrintInteger(int value) => Console.WriteLine(value);
            static void PrintString(string value) => Console.WriteLine(value);

            Procedure p1 = SayHello;
            p1();
            p1.Invoke();

            ProdecureWithIntParameter p2 = PrintInteger;
            p2(5);

            ProcedureWithStringParameter p3 = PrintString;
            p3.Invoke("test");

            p3 = Console.WriteLine;
            p3.Invoke("test");

            GenericProcedure<int> p4 = PrintInteger;
            p4.Invoke(0);

            GenericProcedure<string> p5 = PrintString;
            p5.Invoke("test");

            IntFunction f1 = new Random().Next;
            Console.WriteLine(f1.Invoke());

            StringToIntFunction f2 = int.Parse;
            Console.WriteLine(f2.Invoke("15"));
        }
    }
}
