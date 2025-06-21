using WalletApi.Service.Models;

namespace WalletApi.Service;

public interface IUserService
{
    
    Task<User?> GetById(int id);

}
