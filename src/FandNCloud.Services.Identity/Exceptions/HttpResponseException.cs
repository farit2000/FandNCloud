using System;

namespace FandNCloud.Services.Identity.Exceptions
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;

        public object Value { get; set; }

        public HttpResponseException(int status, object message)
        {
            Status = status;
            Value = message;
        }
    }
}