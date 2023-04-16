namespace DDD.Domain.Orders;

public enum OrderStatus
{
    None = 0,
    PendingPayment = 1,
    ReadyForShipping = 2,
    InTransit = 3
}
