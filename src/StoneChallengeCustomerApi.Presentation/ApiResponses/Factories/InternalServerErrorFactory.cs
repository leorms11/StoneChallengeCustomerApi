namespace StoneChallengeCustomerApi.Presentation.ApiResponses.Factories;

public class InternalServerErrorFactory : ApiResponseFactory
{
    public override BaseErrorResponse CreateResponse(string message)
        => new InternalServerErrorResponse(message);

}