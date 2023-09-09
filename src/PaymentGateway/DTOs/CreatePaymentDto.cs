using PaymentGateway.Infrastructure.Data.Entities;

namespace PaymentGateway.DTOs;

public record CreatePaymentDto(Guid SubscriptiontId);

public static class CreatePaymentDtoExtensions
{
    public static Payment ToEntity(this CreatePaymentDto createPaymentDto)
    {
        return new Payment(Guid.NewGuid(), createPaymentDto.SubscriptiontId);
    }
}
