namespace WalletApi.Controller.DTOs;

public class TransactionHistoryDTO
{
    public double Balance { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
