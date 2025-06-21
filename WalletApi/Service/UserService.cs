using WalletApi.Service.Models;
using WalletApi.Repository;

namespace WalletApi.Service;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
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
            return null;

        return new User
        {
            Id = userEntity.Id,
            Username = userEntity.Username,
            Balance = userEntity.Balance
        };
    }
}
