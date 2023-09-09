namespace WebApi.Models;

public class PaymentNotification
{
    public string Id { get; set; }
    public string SubscriptionId { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DateOccurred { get; set; }
}
public enum PaymentStatus
{
    Pending,
    Confirmed,
    Declined
}