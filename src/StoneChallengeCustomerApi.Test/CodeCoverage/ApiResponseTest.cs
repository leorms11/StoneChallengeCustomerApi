using FluentAssertions;
using StoneChallengeCustomerApi.Domain.Enums;
using StoneChallengeCustomerApi.Presentation.ApiResponses;
using StoneChallengeCustomerApi.Presentation.ApiResponses.Factories;

namespace StoneChallengeCustomerApi.Test.CodeCoverage;

public class ApiResponseTest
{
    [Fact]
    public void ItShouldBeAbleToCreateaBadRequestResponse()
    {
        var sut = ApiResponseFactory.Create(EApiResponseType.BadRequest)
            .CreateResponse("BadRequest");

        Assert.IsType<BadRequestResponse>(sut);
        sut.StatusCode.Should().Be(400);
        sut.Reason.Should().Be("Erro ao executar a operação");
    }

    [Fact]
    public void ItShouldBeAbleToCreateaInternalServerErrorResponse()
    {
        var sut = ApiResponseFactory.Create(EApiResponseType.InternalServerError)
            .CreateResponse("BadRequest");

        Assert.IsType<InternalServerErrorResponse>(sut);
        sut.StatusCode.Should().Be(500);
        sut.Reason.Should().Be("Erro interno do servidor");
    }
}