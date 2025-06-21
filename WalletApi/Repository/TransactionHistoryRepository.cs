using Microsoft.EntityFrameworkCore;
using WalletApi.Data;
using WalletApi.Repository.Entites;

namespace WalletApi.Repository;

public class TransactionHistoryRepository
{
    private readonly AppDbContext _context;

    public TransactionHistoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TransactionHistoryEntity history)
    {
        _context.History.Add(history);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TransactionHistoryEntity>> GetByUserIdAsync(int userId)
    {
        return await _context.History
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.Timestamp)
            .ToListAsync();
    }
}
