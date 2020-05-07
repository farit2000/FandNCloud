using System;

namespace FandNCloud.Common.Events
{
    public class BasketActivityCreateRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        protected BasketActivityCreateRejected()
        {
            
        }

        public BasketActivityCreateRejected(Guid id, string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}