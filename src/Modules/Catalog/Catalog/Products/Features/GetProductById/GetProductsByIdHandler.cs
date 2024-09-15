namespace Catalog.Products.Features.GetProductById;

public record GetProductByIdQuery(Guid Id)
    : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(ProductDto Products);

public class GetProductsByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
                                      .AsNoTracking()
                                      .SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        var productDto = product.Adapt<ProductDto>();

        return new(productDto);
    }
}