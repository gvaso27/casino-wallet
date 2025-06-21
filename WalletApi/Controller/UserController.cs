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
            Balance = user.Balance,
            History = user.History.Select(h => new TransactionHistoryDTO
            {
                Balance = h.Balance,
                Type = h.Type.ToString(),
                Timestamp = h.Timestamp
            }).ToList()
        };

        return Ok(userDto);
    }

    [HttpGet("balance/{id}")]
    public async Task<IActionResult> GetBalanceById(int id)
    {
        if (id <= 0)
            return BadRequest("User ID must be greater than 0");

        double balance = await _userService.GetBalanceById(id);

        if (balance == -1)
            return NotFound($"User with ID {id} not found");


        return Ok(balance);
    }

    [HttpPut("add-funds")]
    public async Task<IActionResult> AddFunds([FromBody] AddFundsDTO addFundsDTO)
    {
        if (addFundsDTO.Amount <= 0.0)
            return BadRequest("amount should be greater than zero");

        var success = await _userService.AddFunds(addFundsDTO.Id, addFundsDTO.Amount);

        if (!success)
            return NotFound($"User with ID {addFundsDTO.Id} not found");

        return Ok("Funds added successfully");
    }

    [HttpPut("withdraw-funds")]
    public async Task<IActionResult> WithdrawFunds([FromBody] WithdrawFundsDTO withdrawFundsDTO)
    {
        if (withdrawFundsDTO.Amount <= 0.0)
            return BadRequest("amount should be greater than zero");

        var success = await _userService.WithdrawFunds(withdrawFundsDTO.Id, withdrawFundsDTO.Amount);

        if (!success)
            return BadRequest($"Failed to withdraw funds. Either user with ID {withdrawFundsDTO.Id} not found or insufficient balance.");

        return Ok("Funds was withdrawn successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();

        var userDtos = users.Select(user => new UserDTO
        {
            Id = user.Id,
            Username = user.Username,
            Balance = user.Balance,
            
        }).ToList();

        return Ok(userDtos);
    }

}
