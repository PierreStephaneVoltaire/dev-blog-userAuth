using System;

namespace Domain
{
    public class ConfirmSignupCredentialImp: IConfirmSignupCredential
    {
        public ConfirmSignupCredentialImp(string confirmationCode, string username)
        {
            ConfirmationCode = confirmationCode ?? throw new ArgumentNullException(nameof(confirmationCode));
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }

        public string ConfirmationCode { get; set; }
        public string Username { get; set; }
    }
}