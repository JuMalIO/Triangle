using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Triangle.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSerilog(this ServiceCollection serviceCollection, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            serviceCollection.AddLogging(x => x.AddSerilog(dispose: true));
        }
    }
}
