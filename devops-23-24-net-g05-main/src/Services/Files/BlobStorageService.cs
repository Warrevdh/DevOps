using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Domain.Files;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Services.Files
{
    public class BlobStorageService : IStorageService
    {
        private readonly string connectionString;

        public Uri BasePath => new Uri("https://g05devops.blob.core.windows.net/g05devopsblob");

        public BlobStorageService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Storage");
        }

        public Uri GenerateImageUploadSas(Image image)
        {
            string containerName = "g05devopsblob";
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(image.Filename);

            var blobSasBuilder = new BlobSasBuilder
            {
                ExpiresOn = DateTime.UtcNow.AddMinutes(5),
                BlobContainerName = containerName,
                BlobName = image.Filename,
            };

            blobSasBuilder.SetPermissions(BlobSasPermissions.Create | BlobSasPermissions.Write);
            var sas = blobClient.GenerateSasUri(blobSasBuilder);
            return sas;
        }
    }
}

