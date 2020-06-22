using System;

namespace Domain
{
    public interface ISigninCredential
    {
        string Username { get; }
        string Password { get; }
    }

}