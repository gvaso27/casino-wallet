using Microsoft.AspNetCore.Mvc;
using WalletApi.Service;
using WalletApi.Controller.DTOs;

namespace WalletApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("User ID must be greater than 0");

        var user = await _userService.GetById(id);

        if (user == null)
            return NotFound($"User with ID {id} not found");

        var userDto = new UserDTO
        {
            Id = user.Id,
            Username = user.Username,
            Balance = user.Balance
        };

        return Ok(userDto);
    }
}
