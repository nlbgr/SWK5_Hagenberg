using System.Collections;
using System.Diagnostics;

// gegeben sind die Methoden aus FilterWrapper.cs




var timer = new Stopwatch();

timer.Start();

// ########### Verwende die Extension Method und gib alle Primzahlen von 1 bis 20 aus
var prims_1 = await Enumerable.Range(0, 20).FilterAsync(n => FilterWrapper.IsPrime(n));

foreach (var p in prims_1)
{
    Console.Out.WriteLine(p);
}

timer.Stop();
var timeTaken = timer.Elapsed;
Console.Out.WriteLine("1 took: " + timeTaken.ToString(@"g"));
timer.Reset();


timer.Start();
// ########### Ermittle alle Primzahlen von 1 bis 20 indem sie Parallel die Intervalle 1-10 und 11-20 errechnen und dann mergen (IEnumerable.Concat)

var leftTask = Enumerable.Range(0, 10).FilterAsync(n => FilterWrapper.IsPrime(n));
var rightTask = Enumerable.Range(11, 10).FilterAsync(n => FilterWrapper.IsPrime(n));

var left = await leftTask;
var right = await rightTask;

foreach (var p in left.Concat(right))
{
    Console.Out.WriteLine(p);
}

timer.Stop();
timeTaken = timer.Elapsed;
Console.Out.WriteLine("2 took: " + timeTaken.ToString(@"g"));
timer.Reset();