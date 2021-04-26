using ElektaTest.Contracts.Responses;
using ElektaTest.IntegrationEvents;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ElektaTest.Services
{
    public interface IEquipmentAvailabilityService
    {
        Task<EquipmentAvailabilityResponse> CheckAvailability(DateTime appointmentTime);

        JsonSerializerSettings SerializerSettings { get; set; }

        Task PublishMessageAsync(EventMessage eventMessage);
    }
}
