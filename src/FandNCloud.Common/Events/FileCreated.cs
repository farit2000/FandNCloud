using System;

namespace FandNCloud.Common.Events
{
    public class FileCreated : IAuthenticatedEvent
    {
        public Guid UserId { get; }
    }
}