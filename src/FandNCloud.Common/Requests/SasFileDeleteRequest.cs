namespace FandNCloud.Common.Requests
{
    public class SasFileDeleteRequest : IRequest
    {
        public string ContainerName { get; set; }
        public string ContainerBlobName { get; set; }
        
        public SasFileDeleteRequest(string containerName, string blobName)
        {
            ContainerName = containerName;
            ContainerBlobName = blobName;
        }
    }
}