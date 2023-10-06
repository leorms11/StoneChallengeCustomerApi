namespace StoneChallengeCustomerApi.Presentation.ApiResponses.Factories;

public class BadRequestResponseFactory : ApiResponseFactory
{
    public override BaseErrorResponse CreateResponse(string message)
        => new BadRequestResponse(message);
}