using System;

namespace AsyncProgramming;

public class Downloader(HttpClient httpClient) {
    public void DownloadAndSaveSync(string url, string filePath) {
        byte[] bytes = httpClient.GetByteArray(url);
        Console.WriteLine($"{nameof(DownloadAndSaveSync)}: Downloaded '{url}'");

        File.WriteAllBytes(filePath, bytes);
        Console.WriteLine($"{nameof(DownloadAndSaveSync)}: Saved '{filePath}'");
    }

    public Task DownloadAndSaveTask(string url, string filePath) {
        return httpClient
                    .GetByteArrayAsync(url)
                    .ContinueWith(t => {
                        Console.WriteLine($"{nameof(DownloadAndSaveTask)}: Downloaded '{url}'");
                        return t.Result;
                    })
                    .ContinueWith(t => File.WriteAllBytes(filePath, t.Result) )
                    .ContinueWith(t => Console.WriteLine($"{nameof(DownloadAndSaveTask)}: Saved '{filePath}'"));
    }

    public async Task DownloadAndSaveAsync(string url, string filePath) {
        byte[] bytes = await httpClient.GetByteArrayAsync(url);
        Console.WriteLine($"{nameof(DownloadAndSaveAsync)}: Downloaded '{url}'");
        
        await File.WriteAllBytesAsync(filePath, bytes);
        Console.WriteLine($"{nameof(DownloadAndSaveAsync)}: Saved '{filePath}'");
    }

    public async Task DownloadAndSaveMultipleAsync(string url1, string filePath1, string url2, string filePath2) {
        Task t1 = DownloadAndSaveAsync(url1, filePath1);
        Console.WriteLine($"{nameof(DownloadAndSaveMultipleAsync)}: Downloaded '{url1}'");

        Task t2 = DownloadAndSaveAsync(url2, filePath1);
        Console.WriteLine($"{nameof(DownloadAndSaveMultipleAsync)}: Downloaded '{url2}'");

        // Variant 1
        // await t1;
        // await t2;

        // Variant 2
        await Task.WhenAll(t1, t2);

        Console.WriteLine($"{nameof(DownloadAndSaveMultipleAsync)}: Download of all files completed");
    }
}

