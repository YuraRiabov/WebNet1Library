﻿using AutoMapper;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.BLL.Services.Abstract;
using WebNetLibrary.Common.Contracts.User;
using WebNetLibrary.DAL.Context;
using WebNetLibrary.DAL.Entities;

namespace WebNetLibrary.BLL.Services;

public class UserService : BaseService, IUserService
{
    private readonly IAuthorizationService _authorizationService;

    public UserService(LibraryContext context, IMapper mapper, IAuthorizationService authorizationService) : base(context, mapper)
    {
        _authorizationService = authorizationService;
    }

    public async Task<long> CreateUser(CreateUserDto dto)
    {
        var authorizeUserResponse = await _authorizationService.CreateUser(dto);
        if (!authorizeUserResponse)
        {
            throw new ArgumentException();
        }

        var user = new User
        {
            Name = dto.Username
        };

        var createdUser = Context.Users.Add(user).Entity;
        await Context.SaveChangesAsync();

        return createdUser.Id;
    }
}