namespace MSA.Common.Contracts.Domain.Events.Payment;

public record PaymentCreated
{
    public Guid PaymentId { get; init; }
};