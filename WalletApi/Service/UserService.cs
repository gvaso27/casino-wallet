using WalletApi.Service.Models;
using WalletApi.Repository;

namespace WalletApi.Service;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(UserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<User> CreateUser(string username)
    {
        var userEntity = await _userRepository.CreateUserAsync(username);

        return new User
        {
            Id = userEntity.Id,
            Username = userEntity.Username,
            Balance = userEntity.Balance
        };
    }

    public async Task<User?> GetById(int id)
    {
        var userEntity = await _userRepository.GetUserByIdAsync(id);

        if (userEntity == null)
        {
            _logger.LogWarning("GetById: User with ID {UserId} not found", id);
            return null;
        }

        return new User
        {
            Id = userEntity.Id,
            Username = userEntity.Username,
            Balance = userEntity.Balance
        };
    }

    public async Task<bool> AddFunds(int id, double amount)
    {
        var userEntity = await _userRepository.GetUserByIdAsync(id);
        if (userEntity == null)
        {
            _logger.LogWarning("AddFunds failed: User with ID {UserId} not found", id);
            return false;
        }

        userEntity.Balance += amount;
        await _userRepository.UpdateUserAsync(userEntity);

        _logger.LogInformation("Added {Amount} funds to User ID {UserId}", amount, id);
        return true;
    }

    public async Task<bool> WithdrawFunds(int id, double amount)
    {
        var userEntity = await _userRepository.GetUserByIdAsync(id);
        if (userEntity == null)
        {
            _logger.LogWarning("WithdrawFunds failed: User with ID {UserId} not found", id);
            return false;
        }

        if (userEntity.Balance < amount)
        {
            _logger.LogWarning("WithdrawFunds failed: Insufficient funds for User ID {UserId}", id);
            return false;
        }

        userEntity.Balance -= amount;
        await _userRepository.UpdateUserAsync(userEntity);

        _logger.LogInformation("Withdrew {Amount} from User ID {UserId}", amount, id);
        return true;
    }

    public async Task<List<User>> GetAllUsers()
    {
        var userEntities = await _userRepository.GetAllUsersAsync();

        return userEntities.Select(userEntity => new User
        {
            Id = userEntity.Id,
            Username = userEntity.Username,
            Balance = userEntity.Balance
        }).ToList();
    }
}
