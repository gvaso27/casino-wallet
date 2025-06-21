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

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDto)
    {
        if (string.IsNullOrWhiteSpace(createUserDto.Username))
            return BadRequest("Username is required");

        var user = await _userService.CreateUser(createUserDto.Username);
        
        var userDto = new UserDTO
        {
            Id = user.Id,
            Username = user.Username,
            Balance = user.Balance
        };

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, userDto);
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
