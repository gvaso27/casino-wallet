using WalletApi.Data;
using WalletApi.Repository.Entites;
using Microsoft.EntityFrameworkCore;

namespace WalletApi.Repository;
public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserEntity?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}