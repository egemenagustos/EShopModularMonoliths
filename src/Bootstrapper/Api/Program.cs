var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config)
        => config.ReadFrom.Configuration(context.Configuration));

builder.Services
        .AddCarterWithAssemblies(typeof(CatalogModule).Assembly);

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

app
.UseCatalogModule()
.UseBasketModule()
.UseOrderingModule();

app.Run();