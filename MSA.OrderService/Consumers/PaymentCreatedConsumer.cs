using MassTransit;
using MSA.Common.Contracts.Domain;
using MSA.Common.Contracts.Domain.Events.Payment;
using MSA.Common.PostgresMassTransit.PostgresDB;
using MSA.OrderService.Domain;
using MSA.OrderService.Infrastructure.Data;

namespace MSA.OrderService.Consumers;

public class PaymentCreatedConsumer : IConsumer<PaymentCreated>
{
    private readonly IRepository<Payment> paymentRepository;
    private readonly PostgresUnitOfWork<MainDbContext> uoW;

    public PaymentCreatedConsumer(
        IRepository<Payment> productRepository,
        PostgresUnitOfWork<MainDbContext> uoW)
    {
        this.paymentRepository = productRepository;
        this.uoW = uoW;
    }

    public async Task Consume(ConsumeContext<PaymentCreated> context)
    {
        var message = context.Message;
        Payment payment = new Payment {
            Id = new Guid(),
            PaymentId = message.PaymentId
        };
        await paymentRepository.CreateAsync(payment);
        await uoW.SaveChangeAsync();
    }
}