using System;

namespace FandNCloud.Common.Commands
{
    public class DeleteFile : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}