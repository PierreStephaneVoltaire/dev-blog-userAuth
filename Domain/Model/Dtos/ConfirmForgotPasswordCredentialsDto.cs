using System;

namespace Domain
{
    public class ConfirmForgotPasswordCredentialsDto
    {
   

        public string Password { get; set; }
        public string ConfirmationCode { get; set; }
        public string UserName { get; set; }
    }
}