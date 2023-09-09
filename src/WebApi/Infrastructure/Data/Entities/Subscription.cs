using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Infrastructure.Data.Entities;

[Table("Subscriptions")]
public class Subscription
{
    [Key]
    public int Id { get; set; }   
    public Guid SubscriptionId { get; set; }
    public bool IsActive { get; set; }
    public DateTime UpdatedAt { get; set; }
}
