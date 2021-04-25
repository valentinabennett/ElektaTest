using System;

namespace ElektaTest.Contracts.Requests
{
    public class UpdateAppointmentRequest
    {
        public Guid PatientId { get; set; }

        public DateTime AppointmentTime { get; set; }
        public DateTime NewAppointmentTime { get; set; }
    }
}
