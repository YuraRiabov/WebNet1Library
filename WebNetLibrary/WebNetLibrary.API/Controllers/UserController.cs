using Microsoft.AspNetCore.Mvc;
using WebNetLibrary.BLL.Interfaces;
using WebNetLibrary.Common.Contracts.User;

namespace WebNetLibrary.API.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        return Ok(await _userService.CreateUser(dto));
    }
}