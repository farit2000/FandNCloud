using System;
using System.Collections.Generic;

namespace FandNCloud.Services.BasketActivities.Domain.Models
{
    public class BasketBlock
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public string UserEmail { get; protected set; }
        public List<BasketItem> Items { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime LastChangeDate { get; protected set; }
        
        protected BasketBlock()
        {
            
        }
        
        public BasketBlock(Guid id, Guid userId, string userEmail)
        {
            Id = id;
            UserId = userId;
            UserEmail = userEmail;
            CreatedDate = DateTime.Now;
            LastChangeDate = DateTime.Now;
        }
    
        public BasketBlock(Guid id, Guid userId, string userEmail, List<BasketItem> items)
        {
            Id = id;
            UserId = userId;
            UserEmail = userEmail;
            Items = items;
            CreatedDate = DateTime.Now;
            LastChangeDate = DateTime.Now;
        }
    }
    
}