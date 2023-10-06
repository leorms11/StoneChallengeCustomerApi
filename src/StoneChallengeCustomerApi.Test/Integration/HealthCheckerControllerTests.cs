using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using StoneChallengeCustomerApi.Test.Integration.Config;
using StoneChallengeCustomerApi.Test.Integration.Fixtures;

namespace StoneChallengeCustomerApi.Test.Integration;

[Collection(nameof(IntegrationTestFixtureCollection))]
public class HealthCheckerControllerTests
{
    private readonly IntegrationTestFixture _fixture;

    public HealthCheckerControllerTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ItShouldBeAbleToGetHealthChecker()
    {
        var response = await _fixture.Client.GetAsync("/api/health-checker");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    }
}
