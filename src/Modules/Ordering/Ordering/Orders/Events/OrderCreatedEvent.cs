namespace Ordering.Orders.Events;

public record OrderCreatedEvent(Order order)
    : IDomainEvent;
