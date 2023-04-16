using DDD.Application.Common.Exceptions;
using DDD.Application.Common.Interfaces;
using DDD.Domain.Common;
using DDD.Domain.Orders;
using System.Text.Json.Serialization;

namespace DDD.Application.Orders.Commands.AddPayment;

public record AddPaymentCommand(string Currency, decimal Amount) : IRequest
{
    [JsonIgnore]
    public Guid OrderId { get; set; }
}

public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand>
{
    private readonly IOrderRepository _repository;

    public AddPaymentCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(AddPaymentCommand request, CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);
        var order = await _repository.GetOrder(orderId) ?? throw new NotFoundException();

        var payment = new Money(request.Currency, request.Amount);
        order.AddPayment(payment);

        await _repository.SaveChangesAsync(cancellationToken);
    }
}