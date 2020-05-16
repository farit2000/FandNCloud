using System;

namespace FandNCloud.Services.BasketActivities.Domain.Models
{
    public class BasketFolder
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Path { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime LastChangeDate { get; protected set; }

        protected BasketFolder()
        {
        }

        public BasketFolder(string name, string path)
        {
            Id = Guid.NewGuid();
            Name = name;
            Path = path;
            CreatedDate = DateTime.UtcNow;
            LastChangeDate = DateTime.UtcNow;
        }
    }
}