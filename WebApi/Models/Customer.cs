namespace WebApi.Models;

public class Customer
{
    public string Id { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }    
}
