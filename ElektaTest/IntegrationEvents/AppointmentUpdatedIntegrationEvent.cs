using ElektaTest.Contracts.Requests;

namespace ElektaTest.IntegrationEvents
{
    public class AppointmentUpdatedIntegrationEvent : IIntegrationEvent
    {
        public AppointmentUpdatedIntegrationEvent(UpdateAppointmentRequest appointment)
        {
            Appointment = new AppointmentDto { PatientId = appointment.PatientId, AppointmentTime = appointment.AppointmentTime, NewAppointmentTime = appointment.NewAppointmentTime };
        }

        public AppointmentDto Appointment { get; }
    }
}
