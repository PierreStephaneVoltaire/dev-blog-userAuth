using System;

namespace Domain
{
    public interface IConfirmSignupCredential
    {
        string ConfirmationCode { get; set; }
        string Username { get; set; }
    }

}