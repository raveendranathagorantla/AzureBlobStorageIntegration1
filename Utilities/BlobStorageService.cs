using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureBlobStorageIntegration.Utilities
{
    public class BlobStorageService
    {
        private readonly string blobUrl = "https://nmrkpidev.blob.core.windows.net/dev-test/dev-test.json";
        private readonly string sasToken = "?sp=r&st=2024-10-28T10:35:48Z&se=2025-10-28T18:35:48Z&spr=https&sv=2022-11-02&sr=b&sig=bdeoPWtefikVgUGFCUs4ihsl22ZhQGu4%2B4cAfoMwd4k%3D";

        public async Task<string?> GetJsonDataFromBlobAsync()
        {
            try
            {
                // Create a BlobClient from the URL and SAS token
                Uri blobUri = new Uri($"{blobUrl}{sasToken}");
                BlobClient blobClient = new BlobClient(blobUri);

                // Download the blob data
                BlobDownloadInfo blobDownload = await blobClient.DownloadAsync();

                using (StreamReader reader = new StreamReader(blobDownload.Content))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                // Handle errors, such as invalid SAS token or blob not found
                Console.WriteLine($"Error fetching blob: {ex.Message}");
                return null;
            }
        }
    }
}
