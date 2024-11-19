// Delegates sind Vergleichbar mit Funktionszeigern in C++.
// Sie sind ein Typ, der auf eine Methode verweist und wie eine Methode aufgerufen werden kann (ruft die Methode auf, auf die er verweist).
// Die Methode die referenziert wird, muss die gleiche Signatur haben wie der Delegate.

void printHello()
{
    Console.WriteLine("Hello");
}

void printInt(int x)
{
    Console.Out.WriteLine(x);
}

int echoInt(int x)
{
    return x;
}




NoParameterProcedureDelegate noParameterProcedureDelegate = printHello;
OneIntParameterProcedureDelegate oneIntParameterProcedureDelegate = printInt;
OneGenericParameterProcedureDelegate<int> oneGenericParameterProcedureDelegate = printInt;
OneIntParameterFunctionDelegate oneIntParameterFunctionDelegate = echoInt;
OneGenericParameterFunctionDelegate<int> oneGenericParameterFunctionDelegate = echoInt;

noParameterProcedureDelegate();
oneIntParameterProcedureDelegate(42);
oneGenericParameterProcedureDelegate(69);
Console.Out.WriteLine(oneIntParameterFunctionDelegate(42));
Console.Out.WriteLine(oneGenericParameterFunctionDelegate(69));



delegate void NoParameterProcedureDelegate();
delegate void OneIntParameterProcedureDelegate(int x);
delegate void OneGenericParameterProcedureDelegate<T>(T x);
delegate int OneIntParameterFunctionDelegate(int x);
delegate T OneGenericParameterFunctionDelegate<T>(T x);