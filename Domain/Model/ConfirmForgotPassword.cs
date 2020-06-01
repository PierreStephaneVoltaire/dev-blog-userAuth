using System;

namespace Domain
{
    public class ConfirmForgotPassword
    {
        public ConfirmForgotPassword(string password, string confirmationCode, string userName)
        {
            Password = password ?? throw new ArgumentNullException(nameof(password));
            ConfirmationCode = confirmationCode ?? throw new ArgumentNullException(nameof(confirmationCode));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }

        public string Password { get; set; }
        public string ConfirmationCode { get; set; }
        public string UserName { get; set; }
    }
}