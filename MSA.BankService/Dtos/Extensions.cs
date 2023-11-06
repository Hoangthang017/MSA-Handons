using MSA.BankService.Entities;

namespace MSA.BankService.Dtos
{
    public static class Extensions
    {
        public static PaymentDto AsDto(this Payment payment)
        {
            return new PaymentDto(
                payment.Id,
                payment.OrderId,
                payment.Status,
                payment.Amount
            );
        }
    }
}