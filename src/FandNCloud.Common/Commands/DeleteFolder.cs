using System;

namespace FandNCloud.Common.Commands
{
    public class DeleteFolder : IAuthenticatedCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}