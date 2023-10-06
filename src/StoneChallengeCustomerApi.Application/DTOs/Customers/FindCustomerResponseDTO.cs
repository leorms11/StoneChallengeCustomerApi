using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace StoneChallengeCustomerApi.Application.DTOs.Customers;

public class FindCustomerResponseDTO
{
    [JsonPropertyName("name"), JsonProperty("name")]
    public string Name { get; init; }
    
    [JsonPropertyName("cpf"), JsonProperty("cpf")]
    public string Cpf { get; init; }
    
    [JsonPropertyName("state"), JsonProperty("state")]
    public string State { get; init; }
    
    [JsonPropertyName("createdAt"), JsonProperty("createdAt")]
    public string CreatedAt { get; init; }
    
    [JsonPropertyName("lastModifiedAt"), JsonProperty("lastModifiedAt")]
    public string LastModifiedAt { get; init; }
}