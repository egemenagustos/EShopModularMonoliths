var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config)
        => config.ReadFrom.Configuration(context.Configuration));

var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;
var orderAssembly = typeof(OrderingModule).Assembly;

builder.Services
        .AddCarterWithAssemblies(catalogAssembly, basketAssembly, orderAssembly);

builder.Services
        .AddMediatRWithAssemblies(catalogAssembly, basketAssembly, orderAssembly);

builder.Services.AddStackExchangeRedisCache(options =>
{
        options.Configuration = builder.Configuration["Redis"];
});

builder.Services.
        AddMassTransitWithAssemblies(builder.Configuration, catalogAssembly, basketAssembly, orderAssembly);

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services
        .AddCatalogModule(builder.Configuration)
        .AddBasketModule(builder.Configuration)
        .AddOrderingModule(builder.Configuration);

builder.Services
       .AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => { });
app.UseAuthentication();
app.UseAuthorization();
app
.UseCatalogModule()
.UseBasketModule()
.UseOrderingModule();

app.Run();