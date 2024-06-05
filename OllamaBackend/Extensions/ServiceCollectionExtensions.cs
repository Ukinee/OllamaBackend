namespace OllamaBackend.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScoped<TInterface, TImplementation>(this IServiceCollection services)
        {
            ServiceDescriptor descriptor = new ServiceDescriptor
            (
                typeof(TInterface),
                typeof(TImplementation),
                ServiceLifetime.Scoped
            );

            services.Add(descriptor);

            return services;
        }
    }
}
