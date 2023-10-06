using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace StoneChallengeCustomerApi.Application.DTOs.Customers;

public class ListCustomersResponseDTO
{
    [JsonPropertyName("name"), JsonProperty("name")]
    public string Name { get; init; }
    
    [JsonPropertyName("cpf"), JsonProperty("cpf")]
    public string Cpf { get; init; }
}