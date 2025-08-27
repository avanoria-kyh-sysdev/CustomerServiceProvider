namespace WebApi.Models;

public class CustomerRequest
{
    public string CustomerName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
}