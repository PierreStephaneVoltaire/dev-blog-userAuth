using System;

namespace Domain
{
    public class ChangePasswordCredentialDto
    {
      

        public string AccessToken { get; }
        public string PreviousPassword { get; }
        public string NewPassword { get; }
    }
}