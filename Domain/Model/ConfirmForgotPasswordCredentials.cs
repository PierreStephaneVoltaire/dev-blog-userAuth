using System;

namespace Domain
{
    public interface IConfirmForgotPasswordCredentials
    {
        string Password { get; set; }
        string ConfirmationCode { get; set; }
        string UserName { get; set; }
    }

}