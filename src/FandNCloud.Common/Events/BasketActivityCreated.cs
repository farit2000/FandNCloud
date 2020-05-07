using System;
using Microsoft.AspNetCore.Http;

namespace FandNCloud.Common.Events
{
    public class BasketActivityCreated : IAuthenticatedEvent
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public string ItemName { get; }
        public string ItemType { get; }
        public string ItemPath { get; }
        public string ItemExtension { get; }
        public DateTime ItemCreated { get; }

        protected BasketActivityCreated()
        {
            
        }

        public BasketActivityCreated(Guid id, Guid userId, string itemName, string itemType, string itemPath,
            string itemExtension, DateTime itemCreated)
        {
            Id = id;
            UserId = userId;
            ItemName = itemName;
            ItemType = itemType;
            ItemPath = itemPath;
            ItemExtension = itemExtension;
            ItemCreated = itemCreated;
        }
    }
}