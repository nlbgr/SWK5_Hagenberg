using AsyncProgramming;

const string URL1 = "https://github.com/progit/progit2/releases/download/2.1.363/progit.pdf"; // Pro Git
const string URL2 = "https://github.com/jnguyen095/clean-code/raw/master/Clean.Code.A.Handbook.of.Agile.Software.Craftsmanship.pdf"; // Clean Code

using var httpClient = new HttpClient();
var downloader = new Downloader(httpClient);

//Console.WriteLine("====================== DownloadAndSaveSync ======================");
//// TODO
//Console.WriteLine("Main: DownloadAndSaveSync completed work.");
//Console.WriteLine();

//Console.WriteLine("====================== DownloadAndSaveTask ======================");
//// TODO
//Console.WriteLine("Main: DownloadAndSaveTask gave control back to caller");
//// TODO
//Console.WriteLine("Main: DownloadAndSaveTask) completed work.");
//Console.WriteLine();

//Console.WriteLine("====================== DownloadAndSaveAsync ======================");
//// TODO
//Console.WriteLine("Main: DownloadAndSaveAsync gave control back to caller");
//// TODO
//Console.WriteLine("Main: DownloadAndSaveAsync) completed work.");
//Console.WriteLine();

//Console.WriteLine("======================= DownloadAndSaveMultipleAsync =======================");
//// TODO
//Console.WriteLine("Main: DownloadAndSaveMultipleAsync gave control back to caller");
//// TODO
//Console.WriteLine("Main: DownloadAndSaveMultipleAsync) completed work.");
//Console.WriteLine();
