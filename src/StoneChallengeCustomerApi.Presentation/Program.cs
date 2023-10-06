using StoneChallengeCustomerApi.Presentation.Extensions;

namespace StoneChallengeCustomerApi.Presentation;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders().AddConsole();
        builder.UseStartup<Startup>(false)
            .Run();
    }
}