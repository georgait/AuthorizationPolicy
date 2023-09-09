using PaymentGateway.Infrastructure.Data.Entities;

namespace PaymentGateway.DTOs;

public record PaymentDto(
    Guid Id,
    Guid SubscriptiontId,
    PaymentStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

public static class PaymentDtoExtensions
{
    public static PaymentDto ToDto(this Payment payment)
    {
        return new PaymentDto(
            payment.Id,
            payment.SubscriptiontId,
            payment.Status,
            payment.CreatedAt,
            payment.UpdatedAt);
    }
}
