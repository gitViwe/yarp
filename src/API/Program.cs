using API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterOpenTelemetry(builder.Configuration, builder.Logging, builder.Environment);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapReverseProxy(proxyPipeline =>
{
    proxyPipeline.UsePassiveHealthChecks();
});

//app.UseHttpsRedirection();

app.Run();
