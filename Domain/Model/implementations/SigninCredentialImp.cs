using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class SigninCredentialImp:ISigninCredential
    {
        public SigninCredentialImp(string userName, string password, string email = null)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(email));
            Username = string.IsNullOrEmpty(userName) ? email : userName;
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }


        public string Username { get; }

        public string Password { get; }
    }
}