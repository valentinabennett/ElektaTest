using System;

namespace ElektaTest.Contracts.Requests
{
    public class NewAppointmentRequest 
    {
        public Guid PatientId { get; set; }

        public DateTime AppointmentTime { get; set; }
    }
}
