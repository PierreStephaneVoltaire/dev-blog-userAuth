﻿using System;

namespace Domain
{
    public interface IUserCredential
    {
        string Email { get; }
        string Username { get; }
        string Password { get; }
    }
}