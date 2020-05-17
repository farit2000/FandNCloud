using System;
using System.Collections.Generic;

namespace FandNCloud.Services.BasketActivities.Domain.Models
{
    public class BasketFile
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Path { get; protected set; }
        public string Extension { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime LastChangeDate { get; protected set; }
        
        protected BasketFile()
        {
        }
        
        public BasketFile(string name, string path, string extension)
        {
            Id = Guid.NewGuid();
            Name = name;
            Path = path;
            Extension = extension;
            CreatedDate = DateTime.UtcNow;
            LastChangeDate = DateTime.UtcNow;
        }
    }
}