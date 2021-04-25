using ElektaTest.Contracts.Requests;
using ElektaTest.Contracts.Responses;
using System;
using System.Threading.Tasks;

namespace ElektaTest.Services
{
    public interface IEquipmentAvailabilityService
    {
        Task<EquipmentAvailabilityResponse> CheckAvailability(DateTime appointmentTime);

        Task CancelAppointment(CancelAppointmentRequest request);

        Task UpdateAppointment(UpdateAppointmentRequest request);
    }
}
