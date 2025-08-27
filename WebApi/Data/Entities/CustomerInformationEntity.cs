using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Data.Entities;

public class CustomerInformationEntity
{
    [Key]
    [ForeignKey(nameof(Customer))]
    public string CustomerId { get; set; } = null!;
    public CustomerEntity Customer { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null!;
}
