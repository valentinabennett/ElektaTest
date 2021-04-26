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
        private readonly IEquipmentAvailabilityService _equipmentAvailabilityService;

        public EventPublisher(IEquipmentAvailabilityService equipmentAvailabilityService)
        {
            _equipmentAvailabilityService = equipmentAvailabilityService;
        }

        public async Task PublishAsync(IIntegrationEvent integrationEvent)
        {
            var message = new EventMessage(integrationEvent);
            await _equipmentAvailabilityService.PublishMessageAsync(message);
        }
    }
}