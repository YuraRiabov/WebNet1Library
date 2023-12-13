﻿namespace WebNetLibrary.Common.Contracts.User;

public class CreateUserDto
{
    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string Username { get; init; } = string.Empty;
}