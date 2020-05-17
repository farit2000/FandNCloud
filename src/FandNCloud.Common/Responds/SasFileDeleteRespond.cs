namespace FandNCloud.Common.Responds
{
    public class SasFileDeleteRespond : IRespond
    {
        public string ContainerName { get; set; }
        public string BlobName { get; set; }
        public string Url { get; set; }

        public SasFileDeleteRespond(string containerName, string blobName, string url)
        {
            ContainerName = containerName;
            BlobName = blobName;
            Url = url;
        }
    }
}