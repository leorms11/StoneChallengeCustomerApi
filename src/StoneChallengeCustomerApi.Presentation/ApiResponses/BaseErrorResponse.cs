using System.Text.Json.Serialization;
using Newtonsoft.Json;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;

namespace StoneChallengeCustomerApi.Presentation.ApiResponses;

public abstract class BaseErrorResponse
{
    public BaseErrorResponse(string reason, int statusCode)
    {
        Reason = reason;
        StatusCode = statusCode;
    }

    [JsonPropertyName("reason"), JsonProperty("reason")]
    public string Reason { get; init; }

    [JsonPropertyName("errors"), JsonProperty("errors")]
    public ResultError Errors { get; init; }

    [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
    public int StatusCode { get; init; }
}