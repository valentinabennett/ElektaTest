using System;

namespace ElektaTest.IntegrationEvents
{
    public class AppointmentDto
    {
        public Guid PatientId { get; set; }

        public DateTime AppointmentTime { get; set; }
        public DateTime NewAppointmentTime { get; set; }
    }
}
