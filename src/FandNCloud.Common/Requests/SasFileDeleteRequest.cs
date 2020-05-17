using System;

namespace FandNCloud.Common.Requests
{
    public class SasFileDeleteRequest : IRequest
    {
        public Guid UserId { get; set; }
        public string ContainerName { get; set; }
        public string ContainerBlobName { get; set; }
        
        public SasFileDeleteRequest(Guid userId, string containerName, string blobName)
        {
            UserId = userId;
            ContainerName = containerName;
            ContainerBlobName = blobName;
        }
    }
}