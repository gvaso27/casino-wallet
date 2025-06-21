using WalletApi.Service.Models;
using WalletApi.Repository;
using WalletApi.Repository.Entites;
using Microsoft.Extensions.Logging;

namespace WalletApi.Service;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;
    private readonly TransactionHistoryRepository _historyRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(
        UserRepository userRepository,
        TransactionHistoryRepository historyRepository,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _historyRepository = historyRepository;
        _logger = logger;
    }

    public async Task<User> CreateUser(string username)
    {
        var userEntity = await _userRepository.CreateUserAsync(username);

        return new User
        {
            Id = userEntity.Id,
            Username = userEntity.Username,
            Balance = userEntity.Balance,
            History = new List<TransactionHistory>()
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

        var historyEntities = await _historyRepository.GetByUserIdAsync(id);

        return new User
        {
            Id = userEntity.Id,
            Username = userEntity.Username,
            Balance = userEntity.Balance,
            History = historyEntities.Select(h => new TransactionHistory
            {
                Id = h.Id,
                Balance = h.Balance,
                Type = (TransactionType)h.Type,
                Timestamp = h.Timestamp
            }).ToList()
        };
    }

    public async Task<double> GetBalanceById(int id)
    {
        var userEntity = await _userRepository.GetUserByIdAsync(id);
        if (userEntity == null)
        {
            _logger.LogWarning("GetById: User with ID {UserId} not found", id);
            return -1;
        }

        return userEntity.Balance;
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

        await _historyRepository.AddAsync(new TransactionHistoryEntity
        {
            UserId = id,
            Balance = amount,
            Type = TransactionTypeEntity.ADDITION,
            Timestamp = DateTime.UtcNow
        });

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

        await _historyRepository.AddAsync(new TransactionHistoryEntity
        {
            UserId = id,
            Balance = amount,
            Type = TransactionTypeEntity.WITHDRAWAL,
            Timestamp = DateTime.UtcNow
        });

        _logger.LogInformation("Withdrew {Amount} from User ID {UserId}", amount, id);
        return true;
    }

    public async Task<List<User>> GetAllUsers()
    {
        var userEntities = await _userRepository.GetAllUsersAsync();

        var users = new List<User>();
        foreach (var userEntity in userEntities)
        {
            var historyEntities = await _historyRepository.GetByUserIdAsync(userEntity.Id);

            users.Add(new User
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                Balance = userEntity.Balance,
                History = historyEntities.Select(h => new TransactionHistory
                {
                    Id = h.Id,
                    Balance = h.Balance,
                    Type = (TransactionType)h.Type,
                    Timestamp = h.Timestamp
                }).ToList()
            });
        }

        return users;
    }
}
