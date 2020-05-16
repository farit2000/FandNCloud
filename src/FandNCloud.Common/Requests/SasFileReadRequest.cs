namespace FandNCloud.Common.Requests
{
    public class SasFileReadRequest : IRequest
    {
        public string ContainerName { get; set; }
        public string ContainerBlobName { get; set; }
        
        public SasFileReadRequest(string containerName, string blobName)
        {
            ContainerName = containerName;
            ContainerBlobName = blobName;
        }
    }
}