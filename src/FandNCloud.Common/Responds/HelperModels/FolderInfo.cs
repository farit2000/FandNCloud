using System;

namespace FandNCloud.Common.Responds.HelperModels
{
    public class FolderInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime ItemCreated { get; set; }

        public FolderInfo(Guid id, string name, string path, DateTime itemCreated)
        {
            Id = id;
            Name = name;
            Path = path;
            ItemCreated = itemCreated;
        }
    }
}