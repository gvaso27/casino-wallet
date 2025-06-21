namespace WalletApi.Service.Models;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public int Balance { get; set; } = 0;
}
