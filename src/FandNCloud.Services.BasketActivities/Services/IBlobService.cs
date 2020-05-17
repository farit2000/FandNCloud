using System.Threading.Tasks;

namespace FandNCloud.Services.BasketActivities.Services
{
    public interface IBlobService
    {
        Task<string> GetBlobSasUriReadAsync(string containerName, string blobName);
        Task<string> GetBlobSasUriDeleteAsync(string containerName, string blobName);
        Task<string> GetBlobSasUriAddAsync(string containerName, string blobName);

        Task AddNewBlobContainerAsync(string containerName);
    }
}