using StoneChallengeCustomerApi.Presentation.MiddleWares;

namespace StoneChallengeCustomerApi.Presentation.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder builder)
        => builder.UseMiddleware<GlobalErrorHandlingMiddleware>();
}