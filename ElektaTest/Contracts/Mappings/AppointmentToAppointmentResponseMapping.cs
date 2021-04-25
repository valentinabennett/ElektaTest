using ElektaTest.Contracts.Responses;
using ElektaTest.Infrastructure;

namespace ElektaTest.Contracts.Mappings
{
    public static class AppointmentToAppointmentResponseMapping
    {
        public static AppointmentResponse Map(Appointment appointment)
        {
            return new AppointmentResponse
            {
                PatientId = appointment.PatientId,
                AppointmentTime = appointment.AppointmentTime
            };
        }
    }
}
