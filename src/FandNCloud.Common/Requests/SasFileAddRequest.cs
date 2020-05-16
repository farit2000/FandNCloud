namespace FandNCloud.Common.Requests
{
    public class SasFileAddRequest : IRequest
    {
        public string ContainerName { get; set; }
        public string ContainerBlobName { get; set; }
        
        public SasFileAddRequest(string containerName, string blobName)
        {
            ContainerName = containerName;
            ContainerBlobName = blobName;
        }
    }
}