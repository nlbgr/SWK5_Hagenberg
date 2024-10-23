namespace AsyncProgramming {
    internal static class HttpClientExtensions {
        public static byte[] GetByteArray(this HttpClient httpClient, string url) {
            return httpClient.GetByteArrayAsync(url).GetAwaiter().GetResult();
        }
    }
}
