using FootballExtractor;
using FootballShared;
using FootballShared.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

class Program
{
    static void Main(string[] args)
    {
        IServiceCollection services = new ServiceCollection();

        services.AddSingleton<Job>();
        services.AddSingleton<PostgresContext>();
        services.AddSingleton<FootballApi>();
        services.AddSingleton<ApiResponseConverter>();
        services.AddSingleton<UnitOfWork>();
        services.AddLogging(logging =>
             logging.AddSimpleConsole(options =>
             {
                 options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                 options.IncludeScopes = true;
             })
         );

        var serviceProvider= services.BuildServiceProvider();
        Job job = serviceProvider.GetRequiredService<Job>();
        job.Process(2024, 2021);
        job.Process(2024, 2002);
        job.Process(2024, 2014);
        job.Process(2024, 2015);
        Console.WriteLine("asf");
    }
}
