namespace WalletApi.Controller.DTOs;

public class UserDTO
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public double Balance { get; set; } = 0.0;
}