using MSA.Common.Contracts.Domain;

namespace MSA.OrderService.Domain
{
    public class Payment : IEntity
    {
        public Guid Id { get; set; }
        public Guid PaymentId { get; set; }
    }
}