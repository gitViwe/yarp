using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace API;

public static class ServiceCollectionExtension
{
    internal static IServiceCollection RegisterOpenTelemetry(
        this IServiceCollection services,
        IConfiguration configuration,
        ILoggingBuilder loggingBuilder,
        IHostEnvironment environment)
    {
        var resource = ResourceBuilder.CreateDefault().AddService(configuration["APIConfiguration:ApplicationName"]!);

        services.AddOpenTelemetry().WithTracing(builder =>
        {
            builder.AddSource("YARP-API")
                   .SetResourceBuilder(resource)
                   .AddHttpClientInstrumentation(options =>
                   {
                       options.RecordException = true;
                       options.EnrichWithException = (activity, exception) => activity?.RecordException(exception);
                   })
                   .AddAspNetCoreInstrumentation(options =>
                   {
                       options.RecordException = true;
                       options.EnrichWithException = (activity, exception) => activity.RecordException(exception);

                   });

            if (environment.IsProduction())
            {
                builder.AddOtlpExporter(option =>
                {
                    option.Endpoint = new Uri(configuration["OpenTelemetry:Honeycomb:Endpoint"]!);
                    option.Headers = configuration["OpenTelemetry:Honeycomb:Headers"]!;
                });
            }
            else
            {
                builder.AddOtlpExporter(option =>
                {
                    option.Endpoint = new Uri(configuration["OpenTelemetry:Jaeger:Endpoint"]!);
                });
            }
        });

        loggingBuilder.AddOpenTelemetry(options =>
        {
            options.SetResourceBuilder(resource);
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;
        });

        return services;
    }
}
