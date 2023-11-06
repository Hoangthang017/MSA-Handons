using System.ComponentModel.DataAnnotations;
using MSA.BankService.Entities;

namespace MSA.BankService.Dtos
{
    public record PaymentDto
    (
        Guid Id,
        Guid OrderId,
        PaymentStatus Status,
        decimal Amount
    );

    public record CreatePaymentDto
    (
        [Required] Guid OrderId,
        [Range(0,1000)] decimal Amount
    );
}