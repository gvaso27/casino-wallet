using Microsoft.EntityFrameworkCore;
using WalletApi.Repository.Entites;

namespace WalletApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users => Set<UserEntity>();
}

