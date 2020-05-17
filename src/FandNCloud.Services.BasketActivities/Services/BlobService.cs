using System;
using System.Globalization;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using UserDelegationKey = Azure.Storage.Blobs.Models.UserDelegationKey;

namespace FandNCloud.Services.BasketActivities.Services
{
    public class BlobService : IBlobService
    {
        private readonly CloudBlobClient _blobClient;
        
        public BlobService(CloudBlobClient blobClient)
        {
            _blobClient = blobClient;
        }
        
        public async Task<string> GetBlobSasUriReadAsync(string containerName, string blobName)
        {
            return GetSasForBlob(_blobClient.GetContainerReference(containerName).GetBlockBlobReference(blobName),
                SharedAccessBlobPermissions.Read, 15);
        }

        public async Task<string> GetBlobSasUriDeleteAsync(string containerName, string blobName)
        {
            return GetSasForBlob(_blobClient.GetContainerReference(containerName).GetBlockBlobReference(blobName),
                SharedAccessBlobPermissions.Delete, 15);
        }

        public async Task<string> GetBlobSasUriAddAsync(string containerName, string blobName)
        {
            return GetSasForBlob(_blobClient.GetContainerReference(containerName).GetBlockBlobReference(blobName),
                SharedAccessBlobPermissions.Write, 15);
        }

        public async Task AddNewBlobContainerAsync(string containerName)
        {
            var container = _blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
        }

        private static string GetSasForBlob(CloudBlockBlob blob, SharedAccessBlobPermissions permissions,
            int sasMinutesValid)
        {
            var sasToken = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = permissions,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(sasMinutesValid)
            });
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", blob.Uri, sasToken);
        }

        private static string GetSasForBlobContainer(CloudBlobContainer blobContainer,
            SharedAccessBlobPermissions permissions, int sasMinutesValid)
        {
            var sasToken = blobContainer.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = permissions,
                SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(sasMinutesValid)
            });
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", blobContainer.Uri, sasToken);
        }
    }
}