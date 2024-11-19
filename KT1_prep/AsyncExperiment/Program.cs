// asnyc und await sind Schlüsselwörter, die es ermöglichen asynchrone Methoden zu schreiben, die auf Tasks basieren
// Ein Task ist ein Objekt, das eine Operation darstellt, die asynchron ausgeführt wird.
// Dadurch kann man Blockierende Operationen verwenden und während auf den Blockierenden Teil gewartet wird Code im Rufer ausführen
// bis man das Ergebnis der async-Methode benötigt

static async Task<string> AsyncMethod()
{
    return await Task.FromResult("Hello World"); // mit Task.FromResult kann ein Task erstellt werden, der sofort fertig ist
}

static async Task AwaitHelloWorld()
{
    var helloWorldTask = AsyncMethod(); // Hier wird ein Task entgegen genommen, womit man weiter den Code ausführen kann wärend der Task blockiert
    Console.Out.WriteLine("This will be executed before we wait for the task to complete");
    Console.Out.WriteLine(await helloWorldTask); // Mit await wird tatsächlich auf das Ergebnis des Tasks gewartet
}

await AwaitHelloWorld();