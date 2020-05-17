using System;

namespace FandNCloud.Common.Requests
{
    public class SasFileReadRequest : IRequest
    {
        public Guid UserId { get; set; }
        public string ContainerName { get; set; }
        public string ContainerBlobName { get; set; }
        
        public SasFileReadRequest(Guid userId, string containerName, string blobName)
        {
            UserId = userId;
            ContainerName = containerName;
            ContainerBlobName = blobName;
        }
    }
}