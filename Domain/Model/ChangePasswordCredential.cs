using System;

namespace Domain
{
    public interface IChangePasswordCredential
    {
        string AccessToken { get; }
        string PreviousPassword { get; }
        string NewPassword { get; }
    }
}