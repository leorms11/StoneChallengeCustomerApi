using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using StoneChallengeCustomerApi.Presentation;

[assembly: InternalsVisibleTo("StoneChallenge.BillingApi.Test.Fixtures")]
namespace StoneChallengeCustomerApi.Test.Integration.Config;

internal class CustomerAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.UseContentRoot(Environment.CurrentDirectory);
        builder.UseEnvironment("Testing");
    }
}