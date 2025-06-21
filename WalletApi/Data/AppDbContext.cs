using Microsoft.EntityFrameworkCore;
using WalletApi.Repository.Entites;

namespace WalletApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<TransactionHistoryEntity> History => Set<TransactionHistoryEntity>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionHistoryEntity>()
            .HasOne(th => th.User)
            .WithMany(u => u.TransactionHistory)
            .HasForeignKey(th => th.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
