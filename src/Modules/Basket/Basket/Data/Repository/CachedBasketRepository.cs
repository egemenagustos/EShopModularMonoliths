using System.Text.Json;
using System.Text.Json.Serialization;
using Basket.Data.JsonConverters;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Data.Repository;

public class CachedBasketRepository
    (IBasketRepository basketRepository, IDistributedCache distributedCache)
    : IBasketRepository
{

    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new ShoppingCartConverter(), new ShoppingCartItemConverter() }
    };

    public async Task<ShoppingCart> CreateBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await basketRepository.CreateBasketAsync(basket, cancellationToken);

        await distributedCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket, _options), cancellationToken);

        return basket;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasketAsync(userName, cancellationToken);

        await distributedCache.RemoveAsync(userName, cancellationToken);

        return true;
    }

    public async Task<ShoppingCart> GetBasketAsync(string userName, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        if (!asNoTracking)
            return await basketRepository.GetBasketAsync(userName, false, cancellationToken);

        var cachedBasket = await distributedCache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket, _options)!;
        }


        var basket = await basketRepository.GetBasketAsync(userName, asNoTracking, cancellationToken);

        await distributedCache.SetStringAsync(userName, JsonSerializer.Serialize(basket, _options), cancellationToken);
        return basket;
    }

    public async Task<int> SaveChangesAsync(string? userName = null, CancellationToken cancellationToken = default)
    {
        var result = await basketRepository.SaveChangesAsync(userName, cancellationToken);

        if (userName is not null)
            await distributedCache.RemoveAsync(userName, cancellationToken);

        return result;
    }
}