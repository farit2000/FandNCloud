using System;

namespace FandNCloud.Common.Events
{
    public class FolderCreated : IAuthenticatedEvent
    {
        public Guid UserId { get; }
    }
}