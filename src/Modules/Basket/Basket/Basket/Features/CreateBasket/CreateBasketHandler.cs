namespace Basket.Basket.Features.CreateBasket;

public record CreateBasketCommand(ShoppingCartDto ShoppingCartDto)
    : ICommand<CreateBasketResult>;

public record CreateBasketResult(Guid Id);

public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCartDto.UserName).NotEmpty().WithMessage("UserName is required!");
    }
}

public class CreateBasketHandler(BasketDbContext dbContext)
    : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = CreateNewBasket(command.ShoppingCartDto);

        await dbContext.ShoppingCarts.AddAsync(shoppingCart);
        await dbContext.SaveChangesAsync();

        return new(shoppingCart.Id);
    }

    private ShoppingCart CreateNewBasket(ShoppingCartDto shoppingCartDto)
    {
        var newBasket = ShoppingCart.Create(
            Guid.NewGuid(),
            shoppingCartDto.UserName
        );

        shoppingCartDto.Items.ForEach(x =>
        {
            newBasket.AddItem(
                x.ProductId,
                x.Quantity,
                x.Color,
                x.Price,
                x.ProductName
            );
        });

        return newBasket;
    }
}