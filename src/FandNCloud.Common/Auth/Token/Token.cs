namespace FandNCloud.Common.Auth
{
    public abstract class Token
    {
        public string Value { get; set; }
        public long Expires { get; set; }
    }
}