using System;

namespace ElektaTest.Contracts.Requests
{
    public class CancelAppointmentRequest 
    {
        public Guid PatientId { get; set; }

        public DateTime AppointmentTime { get; set; }
    }
}
