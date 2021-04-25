using System;

namespace ElektaTest.Contracts.Responses
{
    public class AppointmentResponse
    {
        public Guid PatientId { get; set; }

        public DateTime AppointmentTime { get; set; }
    }
}
