using System.ComponentModel;

namespace StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications.Enums;

public enum EErrorType
{
    [Description("Dados inválidos!")]
    InvalidData = 1000,
    [Description("CPF Já cadastrado!")]
    CpfAlreadyExists = 1001,
    [Description("CPF inválido!")]
    InvalidCpf = 1002,
    [Description("Cliente não encontrado.")]
    CustomerNotFound = 1003
}