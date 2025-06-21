using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletApi.Repository.Entites;

[Table("history")]
public class TransactionHistoryEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; } = 0;

    [ForeignKey(nameof(UserId))]
    public UserEntity User { get; set; } = null!;

    [Required]
    public double Balance { get; set; } = 0.0;

    [Required]
    public TransactionTypeEntity Type { get; set; } = TransactionTypeEntity.ADDITION;

    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
