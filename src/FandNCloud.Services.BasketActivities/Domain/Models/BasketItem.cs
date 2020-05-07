using System;

namespace FandNCloud.Services.BasketActivities.Domain.Models
{
    public class BasketItem
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public string Path { get; protected set; }
        public string Extension { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime LastChangeDate { get; protected set; }
        
        protected BasketItem()
        {
            
        }
        
        public BasketItem(string name, string type, string path, string extension)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            Path = path;
            Extension = extension;
            CreatedDate = DateTime.Now;
            LastChangeDate = DateTime.Now;
        }
    }
}