using System;

namespace FandNCloud.Common.Responds.HelperModels
{
    public class FileInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public DateTime ItemCreated { get; set; }

        public FileInfo(Guid id, string name, string extension, string path, DateTime itemCreated)
        {
            Id = id;
            Name = name;
            Extension = extension;
            Path = path;
            ItemCreated = itemCreated;
        }
    }
}