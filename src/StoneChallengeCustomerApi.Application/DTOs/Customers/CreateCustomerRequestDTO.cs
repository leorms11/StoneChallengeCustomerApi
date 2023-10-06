using System.Text.Json.Serialization;
using Newtonsoft.Json;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;

namespace StoneChallengeCustomerApi.Application.DTOs.Customers;

public class CreateCustomerRequestDTO : Notifiable
{
    [JsonPropertyName("name"), JsonProperty("name")]
    public string Name { get; init; }

    [JsonPropertyName("cpf"), JsonProperty("cpf")]
    public string Cpf { get; init; }

    [JsonPropertyName("state"), JsonProperty("state")]
    public string State { get; init; }
}