using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletApi.Repository.Entites;

[Table("users")]
public class UserEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    public double Balance { get; set; } = 0.0;

    public List<TransactionHistoryEntity> TransactionHistory { get; set; } = new();
}
