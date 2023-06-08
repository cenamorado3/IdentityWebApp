using WebApplication1.Services;
using WebApplication1.Interfaces;


namespace WebApplication1.ServiceRegistration;

public static class RubiconServiceCollectionExtension
{
    public static IServiceCollection AddRubiconService(this IServiceCollection collection, IConfiguration config)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (config == null) throw new ArgumentNullException(nameof(config));
        collection.Configure<RubiconServiceOptions>(config);
        return collection.AddTransient<IRubiconService, RubiconService>();
    }
}

public class RubiconServiceOptions
{
    public string? Con { get; set; }
}