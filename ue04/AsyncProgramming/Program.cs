using System.Diagnostics;
using AsyncProgramming;

const string URL1 = "https://github.com/progit/progit2/releases/download/2.1.363/progit.pdf"; // Pro Git
const string URL2 = "https://github.com/jnguyen095/clean-code/raw/master/Clean.Code.A.Handbook.of.Agile.Software.Craftsmanship.pdf"; // Clean Code

using var httpClient = new HttpClient();
var downloader = new Downloader(httpClient);

Console.WriteLine("====================== DownloadAndSaveSync ======================");

downloader.DownloadAndSaveSync(URL1, "book1.pdf");

Console.WriteLine("Main: DownloadAndSaveSync completed work.");
Console.WriteLine();




Console.WriteLine("====================== DownloadAndSaveTask ======================");

Task task1 = downloader.DownloadAndSaveTask(URL1, "book2.pdf"); // returns immediately without completing the task. Task is executed in background

Console.WriteLine("Main: DownloadAndSaveTask gave control back to caller");

task1.Wait();

Console.WriteLine("Main: DownloadAndSaveTask) completed work.");
Console.WriteLine();




Console.WriteLine("====================== DownloadAndSaveAsync ======================");

Task task2 = downloader.DownloadAndSaveAsync(URL1, "book3.pdf");

Console.WriteLine("Main: DownloadAndSaveAsync gave control back to caller");

await task2;

Console.WriteLine("Main: DownloadAndSaveAsync) completed work.");
Console.WriteLine();




Console.WriteLine("======================= DownloadAndSaveMultipleAsync =======================");

Task task3 = downloader.DownloadAndSaveMultipleAsync(URL1, "book4_1.pdf", URL2, "book4_2.pdf");

Console.WriteLine("Main: DownloadAndSaveMultipleAsync gave control back to caller");

await task3;

Console.WriteLine("Main: DownloadAndSaveMultipleAsync) completed work.");
Console.WriteLine();
