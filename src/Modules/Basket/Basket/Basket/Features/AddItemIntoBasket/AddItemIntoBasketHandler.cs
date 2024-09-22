using Catalog.Contracts.Products.Features.GetProductById;

namespace Basket.Basket.Features.AddItemIntoBasket;

public record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDto ShoppingCartItem)
    : ICommand<AddItemIntoBasketResult>;

public record AddItemIntoBasketResult(Guid Id);

public class AddItemIntoBasketValidator : AbstractValidator<AddItemIntoBasketCommand>
{
    public AddItemIntoBasketValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
        RuleFor(x => x.ShoppingCartItem.ProductId).NotEmpty().WithMessage("ProductId is required.");
        RuleFor(x => x.ShoppingCartItem.Quantity).GreaterThan(0).WithMessage("Quantity must be than 0.");
    }
}

public class AddItemIntoBasketHandler
    (IBasketRepository basketRepository, ISender sender)
    : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
{
    public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await basketRepository.GetBasketAsync(command.UserName, false, cancellationToken);

        var result = await sender.Send(new GetProductByIdQuery(command.ShoppingCartItem.ProductId));

        shoppingCart.AddItem(
            command.ShoppingCartItem.ProductId,
            command.ShoppingCartItem.Quantity,
            command.ShoppingCartItem.Color,
            result.Products.Price,
            result.Products.Name
        );

        await basketRepository.SaveChangesAsync(command.UserName, cancellationToken);
        return new(shoppingCart.Id);
    }
}