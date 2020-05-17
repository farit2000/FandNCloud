namespace FandNCloud.Common.Responds
{
    public class SasFileReadRespond : IRespond
    {
        public string ContainerName { get; set; }
        public string BlobName { get; set; }
        public string Url { get; set; }

        public SasFileReadRespond(string containerName, string blobName, string url)
        {
            ContainerName = containerName;
            BlobName = blobName;
            Url = url;
        }
    }
}