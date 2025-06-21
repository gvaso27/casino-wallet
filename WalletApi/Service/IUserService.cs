using WalletApi.Service.Models;

namespace WalletApi.Service;

public interface IUserService
{

    Task<User> CreateUser(string username);

    Task<User?> GetById(int id);

    Task<bool> addFunds(int id, double amount);

}
