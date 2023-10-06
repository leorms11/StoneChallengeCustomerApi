using System.Text.Json.Serialization;
using Newtonsoft.Json;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Enums;

namespace StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;

public class ResultError
{
    public ResultError(EErrorType type, IEnumerable<ResultErrorField> fields)
    {
        Type = type;
        Fields = fields;
    }

    public ResultError(EErrorType type)
    {
        Type = type;
    }

    [JsonPropertyName("type"), JsonProperty("type")]
    public EErrorType Type { get; }

    [JsonPropertyName("fields"), JsonProperty("fields")]
    public IEnumerable<ResultErrorField> Fields { get; } = new List<ResultErrorField>();
}