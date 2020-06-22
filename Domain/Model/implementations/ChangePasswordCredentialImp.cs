using System;

namespace Domain
{
    public class ChangePasswordCredentialImp:IChangePasswordCredential
    {
        public ChangePasswordCredentialImp(string accessToken, string previousPassword, string newPassword)
        {
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            PreviousPassword = previousPassword ?? throw new ArgumentNullException(nameof(previousPassword));
            NewPassword = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
        }

        public string AccessToken { get; }
        public string PreviousPassword { get; }
        public string NewPassword { get; }
    }
}