using System;

namespace Domain
{
    public class UserCredentialImp:IUserCredential
    {
        public UserCredentialImp(string email, string userName, string password)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Username = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public string Email { get; }
        public string Username { get; }
        public string Password { get; }
    }
}