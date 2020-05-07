namespace FandNCloud.Common.Events
{
    public class UserCreated : IEvent
    {
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Role { get; }

        protected UserCreated()
        {
            
        }

        public UserCreated(string email, string firstName, string lastName, string role)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }
    }
}