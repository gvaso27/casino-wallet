namespace WalletApi.Service.Models;

public class TransactionHistory
{
    public int Id { get; set; }
    public double Balance { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Timestamp { get; set; }
}
