using ElektaTest.Contracts.Responses;
using System;
using System.Threading.Tasks;

namespace ElektaTest.Services
{
    public interface IEquipmentAvailabilityService
    {
        Task<EquipmentAvailabilityResponse> CheckAvailability(DateTime appointmentTime);

    }
}
