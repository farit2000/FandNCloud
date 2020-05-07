using System;

namespace FandNCloud.Common.Commands
{
    public class CreateFile : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
    }
}