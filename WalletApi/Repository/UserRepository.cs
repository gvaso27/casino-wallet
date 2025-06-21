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

    public async Task<bool> UpdateUserAsync(UserEntity user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<UserEntity> CreateUserAsync(string username)
    {
        var userEntity = new UserEntity
        {
            Username = username,
            Balance = 0.0
        };

        _context.Users.Add(userEntity);
        await _context.SaveChangesAsync();

        return userEntity;
    }

    public async Task<UserEntity?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<List<UserEntity>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }


}
