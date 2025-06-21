namespace WalletApi.Controller.DTOs;

public class UserDTO
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public int Balance { get; set; } = 0;
}