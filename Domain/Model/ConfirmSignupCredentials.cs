using System;

namespace Domain
{
    public class ConfirmSignupCredentials
    {
        public ConfirmSignupCredentials(string confirmationCode, string username)
        {
            ConfirmationCode = confirmationCode ?? throw new ArgumentNullException(nameof(confirmationCode));
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }

        public string ConfirmationCode { get; set; }
        public string Username { get; set; }
    }
}