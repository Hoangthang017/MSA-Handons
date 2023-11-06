using MSA.Common.Contracts.Domain;

namespace MSA.BankService.Entities
{
    public class Payment : IEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public PaymentStatus Status { get; set; }
        public decimal Amount { get; set; }
    }
}