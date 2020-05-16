

namespace FandNCloud.Common.Responds
{
    public class SasFileAddRespond : IRespond
    {
        public string ContainerName { get; set; }
        public string BlobName { get; set; }
        public string Url { get; set; }

        public SasFileAddRespond(string containerName, string blobName, string url)
        {
            ContainerName = containerName;
            BlobName = blobName;
            Url = url;
        }
    }
}