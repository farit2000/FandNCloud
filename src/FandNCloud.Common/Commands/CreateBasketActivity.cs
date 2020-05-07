using System;
using Microsoft.AspNetCore.Http;

namespace FandNCloud.Common.Commands
{
    public class CreateBasketActivity : IAuthenticatedCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public string ItemPath { get; set; }
        public string ItemExtension { get; set; }
        
        public DateTime ItemCreated { get; set; }
    }
}