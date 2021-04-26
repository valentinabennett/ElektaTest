using System;

namespace ElektaTest.IntegrationEvents
{
    public class AppointmentCancelledIntegrationEvent : IIntegrationEvent
    {
        public AppointmentCancelledIntegrationEvent(DateTime appointmentTime, Guid patientId)
        {
            PatientId = patientId;
            AppointmentTime = appointmentTime;
        }

        public Guid PatientId { get; }
        public DateTime AppointmentTime { get; }
    }
}
