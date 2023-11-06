using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSA.BankService.Dtos;
using MSA.BankService.Entities;
using MSA.Common.Contracts.Domain;
using MSA.Common.Contracts.Domain.Events.Payment;

namespace MSA.ProductService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("v1/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IRepository<Payment> _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public PaymentController(
            IRepository<Payment> repository,
            IPublishEndpoint publishEndpoint)
        {
            this._repository = repository;
            this._publishEndpoint = publishEndpoint;
        }

        //Get v1/payment/123
        [HttpGet("{id}")]
        public async Task<ActionResult<Guid>> GetByIdAsync(Guid id)
        {
            if (id == null) return BadRequest();

            var payment = (await _repository.GetAsync(id));
            if (payment == null) return Ok(Guid.Empty);

            return Ok(payment.Id);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> PostAsync(
            CreatePaymentDto createPaymentDto)
        {
            var payment = new Payment
            {
                Id = new Guid(),
                OrderId = createPaymentDto.OrderId,
                Amount = createPaymentDto.Amount,
                Status = PaymentStatus.Waiting
            };
            await _repository.CreateAsync(payment);

            await _publishEndpoint.Publish(new PaymentCreated(){
               PaymentId = payment.Id
            });

            return CreatedAtAction(nameof(PostAsync), payment.AsDto());
        }

        [HttpGet("{id}/processed")]
        public async Task<ActionResult<Guid>> ProcessedAsync(Guid id)
        {
            if (id == null) return BadRequest();

            var payment = (await _repository.GetAsync(id));
            if (payment == null) return Ok(Guid.Empty);

            Random rand = new Random();
            if(rand.NextDouble() >= 0.5)
            {
                payment.Status = PaymentStatus.Success;
            }
            else
            {
                payment.Status = PaymentStatus.Fail;
            }

            await _repository.UpdateAsync(payment);

            return payment.Status == PaymentStatus.Success ? Ok(payment.Id) : BadRequest();
        }
    }
}