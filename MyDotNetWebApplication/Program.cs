using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Exporter;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    // Prevent loading settings from other config sources
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
});

// Configure Kestrel explicitly
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(6001); // Listen for HTTP
    serverOptions.ListenAnyIP(6002, configure => configure.UseHttps()); // Listen for HTTPS
});

// Clear any URLs that might be set in other config files
builder.WebHost.UseUrls(); // This clears any URLs set in other config files

builder.Services.AddControllers();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MyApplication"))
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri("https://api.honeycomb.io:443");
            options.Headers = $"x-honeycomb-team={Environment.GetEnvironmentVariable("HONEYCOMB_API_KEY")}";
            options.Protocol = OtlpExportProtocol.HttpProtobuf;
        })
    );

var app = builder.Build();

// Add these lines for development environment
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Add middleware in the correct order
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(() => 
{
    Console.WriteLine("Application started. Listening on:");
    Console.WriteLine("  http://localhost:6001");
    Console.WriteLine("  https://localhost:6002");
});

app.Run();
