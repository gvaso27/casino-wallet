using WalletApi.Service.Models;

namespace WalletApi.Service;

public interface IUserService
{

    Task<User> CreateUser(string username);

    Task<User?> GetById(int id);

    Task<bool> AddFunds(int id, double amount);

    Task<bool> WithdrawFunds(int id, double amount);

    Task<List<User>> GetAllUsers();

}
