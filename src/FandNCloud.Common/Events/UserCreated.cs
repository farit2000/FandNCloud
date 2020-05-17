using System;

namespace FandNCloud.Common.Events
{
    public class UserCreated : IEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Role { get; }

        protected UserCreated()
        {
            
        }

        public UserCreated(Guid userId, string email, string firstName, string lastName, string role=null)
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }
    }
}