using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;
using static DDBlazorApp1.Startup;

namespace DDBlazorApp1.Data
{
    public class AzStorage
    {
        public string storageConnectionString;

        public AzStorage(string connectionstring)
        {
            storageConnectionString = connectionstring;
        }

        public async void UploadExcelAsync(MemoryStream uploadstream)
        {
            CloudStorageAccount storageAccount;
            CloudStorageAccount.TryParse(storageConnectionString, out storageAccount);

            // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            // Create a container
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("diodocs");

            // Get a reference to the blob address, then upload the file to the blob.
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference("Result.xlsx");

            await cloudBlockBlob.UploadFromStreamAsync(uploadstream);
        }

        public async void UploadPdfAsync(MemoryStream uploadstream)
        {
            CloudStorageAccount storageAccount;
            CloudStorageAccount.TryParse(storageConnectionString, out storageAccount);

            // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            // Create a container
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("diodocs");

            // Get a reference to the blob address, then upload the file to the blob.
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference("Result.pdf");

            await cloudBlockBlob.UploadFromStreamAsync(uploadstream);
        }
    }
}
