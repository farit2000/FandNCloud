using System;
using System.Collections.Generic;

namespace FandNCloud.Services.BasketActivities.Domain.Models
{
    public class BasketBlock
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public string UserEmail { get; protected set; }
        public string ContainerName { get; protected set; }
        public List<BasketFolder> Folders { get; protected set; }
        public List<BasketFile> Files { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime LastChangeDate { get; protected set; }
        
        protected BasketBlock()
        {
            
        }
        
        public BasketBlock(Guid userId, string userEmail, string containerName)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ContainerName = containerName;
            UserEmail = userEmail;
            CreatedDate = DateTime.Now;
            LastChangeDate = DateTime.Now;
        }
    
        public BasketBlock(Guid userId, string userEmail, string containerName, List<BasketFile> files,
            List<BasketFolder> folders)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ContainerName = containerName;
            UserEmail = userEmail;
            Files = files;
            Folders = folders;
            CreatedDate = DateTime.UtcNow;
            LastChangeDate = DateTime.UtcNow;
        }
    }
    
}