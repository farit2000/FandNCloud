using System;
using FandNCloud.Common.Commands;

namespace FandNCloud.Common.Requests
{
    public class BrowseFolderRequest : IRequest
    {
        // public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public BrowseFolderRequest(Guid userId, string name, string path)
        {
            // Id = command.Id;
            UserId = userId;
            Name = name;
            Path = path;
        }
    }
}