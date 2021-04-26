using ElektaTest.IntegrationEvents;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ElektaTest.Services
{
    public interface IEquipmentAvailabilityNotification
    {
        JsonSerializerSettings SerializerSettings { get; set; }

        Task PublishMessageAsync(EventMessage eventMessage);

    }
}
