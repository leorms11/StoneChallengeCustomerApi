using StoneChallengeCustomerApi.Domain.Interfaces;
using StoneChallengeCustomerApi.Infra.CrossCutting.Utils.Notifications;

namespace StoneChallengeCustomerApi.Domain.Models;

public abstract class BaseModel : Notifiable, ITimeStamped
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
}