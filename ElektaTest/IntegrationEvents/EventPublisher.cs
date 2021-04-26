using ElektaTest.Services;
using System.Threading.Tasks;

namespace ElektaTest.IntegrationEvents
{
    public interface IEventPublisher
    {
        Task PublishAsync(IIntegrationEvent integrationEvent);
    }

    public class EventPublisher : IEventPublisher
    {
        private readonly IEquipmentAvailabilityNotification _equipmentAvailabilityNotification;

        public EventPublisher(IEquipmentAvailabilityNotification equipmentAvailabilityNotification)
        {
            _equipmentAvailabilityNotification = equipmentAvailabilityNotification;
        }

        public async Task PublishAsync(IIntegrationEvent integrationEvent)
        {
            var message = new EventMessage(integrationEvent);
            await _equipmentAvailabilityNotification.PublishMessageAsync(message);
        }
    }
}