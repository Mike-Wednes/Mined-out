using Microsoft.Extensions.DependencyInjection;
using UI;

namespace Logic
{
    class Program
    {
        static void Main(string[] args)
        {
            Application app = ConfigureServices(new ServiceCollection())
                .BuildServiceProvider()
                .GetRequiredService<Application>();

            app.Run();
        }

        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IInterface, ConsoleInterface>();
            services.AddTransient<Application>();
            return services;
        }
    }
}
