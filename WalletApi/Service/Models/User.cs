namespace WalletApi.Service.Models;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public double Balance { get; set; } = 0;
}
